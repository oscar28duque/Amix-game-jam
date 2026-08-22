using UnityEngine;

public class limite : MonoBehaviour
{

    private Rigidbody2D rb;

    [Header("Configuración de Límites")]
    public Vector2 centroLimites = Vector2.zero;
    public Vector2 tamanoLimites = new Vector2(10f, 10f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Calcular extremos de la caja
        float minX = centroLimites.x - (tamanoLimites.x / 2f);
        float maxX = centroLimites.x + (tamanoLimites.x / 2f);
        float minY = centroLimites.y - (tamanoLimites.y / 2f);
        float maxY = centroLimites.y + (tamanoLimites.y / 2f);

        // Forzar la posición del Rigidbody dentro del rango
        float posXClamped = Mathf.Clamp(rb.position.x, minX, maxX);
        float posYClamped = Mathf.Clamp(rb.position.y, minY, maxY);

        // Aplicar la posición segura usando MovePosition para no romper las físicas
        rb.MovePosition(new Vector2(posXClamped, posYClamped));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centroLimites, new Vector3(tamanoLimites.x, tamanoLimites.y, 0f));
    }
}


