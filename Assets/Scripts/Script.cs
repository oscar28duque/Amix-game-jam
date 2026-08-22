using UnityEngine;
using UnityEngine.InputSystem;

public class Script : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb2D; 
    private Vector2 movementInput;

    public GameObject prefabOnda;
    public Transform puntoGeneracion;

    public int cantidadBalas = 36;
    public float anguloPaso = 5f;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        movementInput = movementInput.normalized;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            LanzarOnda();
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

    void FixedUpdate()
    {
        rb2D.linearVelocity = movementInput * speed;
    }
}

    
