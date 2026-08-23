using UnityEngine;

public class playerscript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float maxspeed;
    [SerializeField] float deceleration;
    [SerializeField] float acceleration;
    [SerializeField] Animator anim; 

    [Header("Configuración de Límites (Gizmo)")]
    [SerializeField] Vector2 centroLimites = Vector2.zero;
    [SerializeField] Vector2 tamanoLimites = new Vector2(10f, 10f);

    Vector2 move;
    Vector2 targetVelocity;
    Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // 1. Leer el input normal del jugador
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // 2. Filtrar el input para que no acepte más movimiento si toca los bordes
        move = FiltrarInputPorLimites(inputX, inputY);

        Debug.Log(move);

        //3. Animator 
        anim.SetFloat("Horizontal", move.x);
        anim.SetFloat("Vertical", move.y);
    }


    private Vector2 FiltrarInputPorLimites(float inputX, float inputY)
    {
        // Calcular el radio del personaje
        float radioX = col != null ? col.bounds.extents.x : 0.5f;
        float radioY = col != null ? col.bounds.extents.y : 0.5f;

        // Calcular extremos de la caja del Gizmo
        float minX = centroLimites.x - (tamanoLimites.x / 2f) + radioX;
        float maxX = centroLimites.x + (tamanoLimites.x / 2f) - radioX;
        float minY = centroLimites.y - (tamanoLimites.y / 2f) + radioY;
        float maxY = centroLimites.y + (tamanoLimites.y / 2f) - radioY;

        // Bloquear input X si está en el borde e intenta seguir avanzando hacia afuera
        if (transform.position.x <= minX && inputX < 0) inputX = 0;
        if (transform.position.x >= maxX && inputX > 0) inputX = 0;

        // Bloquear input Y si está en el borde e intenta seguir avanzando hacia afuera
        if (transform.position.y <= minY && inputY < 0) inputY = 0;
        if (transform.position.y >= maxY && inputY > 0) inputY = 0;

        // Retornar el vector normalizado final (solo con las direcciones permitidas)
        return new Vector2(inputX, inputY).normalized;
    }

    private void FixedUpdate()
    {
        targetVelocity = move * maxspeed;

        if (move != Vector2.zero)
        {
            rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centroLimites, new Vector3(tamanoLimites.x, tamanoLimites.y, 0f));
    }
}