using UnityEngine;
using UnityEngine.InputSystem;

public class Script : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb2D; 
    private Vector2 movementInput;

    public GameObject prefabOnda;
    public Transform puntoGeneracion;

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
        Instantiate(prefabOnda, posicion, Quaternion.identity);
    }

    void FixedUpdate()
    {
        rb2D.linearVelocity = movementInput * speed;
    }
}

    
