using UnityEngine;
using UnityEngine.InputSystem;

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

    [Header("Configurar sonido de pasos")]

    [SerializeField] AudioClip stepsSound;

    [SerializeField] AudioSource audioSource;

    [SerializeField] float intervaloPasos = 0.35f;
    public GameObject prefabOnda;
    public Transform puntoGeneracion;
    private float tiempoProximoPaso = 0f;

    public int cantidadBalas = 36;
    public float anguloPaso = 5f;

    public float tiempoCooldown = 1f;
    private float tiempoProximoDisparo = 0f;

    Vector2 move;
    Vector2 targetVelocity;
    Collider2D col;

    //Guardamos los limites calculados para reutilizacion
    float minX, maxX, minY, maxY;

    private void Start()

    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        CalcularLimites();
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

        if (Keyboard.current.spaceKey.wasPressedThisFrame && Time.time >= tiempoProximoDisparo)
        {
            LanzarOnda();
            tiempoProximoDisparo = Time.time + tiempoCooldown;
        }
       
        if (move != Vector2.zero)
        {
            ManejarSonidoPasos();
        }
    }

    void ManejarSonidoPasos()
    {
        if (Time.time >= tiempoProximoPaso)
        {
            audioSource.PlayOneShot(stepsSound);
            tiempoProximoPaso = Time.time + intervaloPasos;
        }
    }
    void LanzarOnda()
    {
        Vector3 posicion = puntoGeneracion != null ? puntoGeneracion.position : transform.position;
        float anguloActual = 0f;
        for (int i = 0; i < cantidadBalas; i++)
        {
            Quaternion rotacion = Quaternion.Euler(0, 0, anguloActual);
            Instantiate(prefabOnda, posicion, rotacion);
            anguloActual += anguloPaso;
        }
    }

    private void CalcularLimites()
    {
        float radioX = col != null ? col.bounds.extents.x : 0.5f;
        float radioY = col != null ? col.bounds.extents.y : 0.5f;

        minX = centroLimites.x - (tamanoLimites.x / 2f) + radioX;
        maxX = centroLimites.x + (tamanoLimites.x / 2f) - radioX;
        minY = centroLimites.y - (tamanoLimites.y / 2f) + radioY;
        maxY = centroLimites.y + (tamanoLimites.y / 2f) - radioY;
    }

    private Vector2 FiltrarInputPorLimites(float inputX, float inputY)
    {
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

        Vector2 vel = rb.linearVelocity;

        if (rb.position.x <= minX && vel.x < 0) vel.x = 0;
        if (rb.position.x >= maxX && vel.x > 0) vel.x = 0;

        if (rb.position.y <= minY && vel.y < 0) vel.y = 0;
        if (rb.position.y >= maxY && vel.y > 0) vel.y = 0;

        rb.linearVelocity = vel;

        float posXClamped = Mathf.Clamp(rb.position.x, minX, maxX);
        float posYClamped = Mathf.Clamp(rb.position.y, minY, maxY);
        
        rb.position = new Vector2(posXClamped, posYClamped);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centroLimites, new Vector3(tamanoLimites.x, tamanoLimites.y, 0f));
    }
}