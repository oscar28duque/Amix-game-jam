using UnityEngine;

public class limite : MonoBehaviour
{
    [Header("¿A quién queremos encerrar?")]
    public Transform jugador; // Aquí arrastraremos al personaje desde el Inspector

    [Header("Configuración de Límites")]
    public Vector2 centroLimites = Vector2.zero;
    public Vector2 tamanoLimites = new Vector2(10f, 10f);

    private Rigidbody2D jugadorRb;
    private Collider2D jugadorCol;

    void Start()
    {
        // Si asignaste al jugador, obtenemos sus componentes físicos
        if (jugador != null)
        {
            jugadorRb = jugador.GetComponent<Rigidbody2D>();
            jugadorCol = jugador.GetComponent<Collider2D>();
        }
    }

    void FixedUpdate()
    {
        // Si no hay jugador asignado o no tiene Rigidbody, no hacemos nada
        if (jugador == null || jugadorRb == null) return;

        // 1. Conseguir el radio del personaje (mitad de su ancho/alto)
        float radioX = jugadorCol != null ? jugadorCol.bounds.extents.x : 0.5f;
        float radioY = jugadorCol != null ? jugadorCol.bounds.extents.y : 0.5f;

        // 2. Calcular los extremos de la caja del Gizmo
        float minX = centroLimites.x - (tamanoLimites.x / 2f) + radioX;
        float maxX = centroLimites.x + (tamanoLimites.x / 2f) - radioX;
        float minY = centroLimites.y - (tamanoLimites.y / 2f) + radioY;
        float maxY = centroLimites.y + (tamanoLimites.y / 2f) - radioY;

        // 3. Leer y corregir la velocidad del jugador para anular su input en los bordes
        Vector2 velocidadActual = jugadorRb.linearVelocity;

        if (jugadorRb.position.x <= minX && velocidadActual.x < 0) velocidadActual.x = 0;
        if (jugadorRb.position.x >= maxX && velocidadActual.x > 0) velocidadActual.x = 0;

        if (jugadorRb.position.y <= minY && velocidadActual.y < 0) velocidadActual.y = 0;
        if (jugadorRb.position.y >= maxY && velocidadActual.y > 0) velocidadActual.y = 0;

        jugadorRb.linearVelocity = velocidadActual;

        // 4. Forzar al jugador a quedarse exactamente dentro de los rangos
        float posXClamped = Mathf.Clamp(jugadorRb.position.x, minX, maxX);
        float posYClamped = Mathf.Clamp(jugadorRb.position.y, minY, maxY);

        jugadorRb.position = new Vector2(posXClamped, posYClamped);
    }

    private void OnDrawGizmos()
    {
        // Dibujar el Gizmo en la posición de este objeto independiente
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centroLimites, new Vector3(tamanoLimites.x, tamanoLimites.y, 0f));
    }
}