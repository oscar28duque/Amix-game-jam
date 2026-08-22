using UnityEngine;

public class playerscript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float maxspeed;
    [SerializeField] float deceleration;
    [SerializeField] float acceleration;
        
    Vector2 move;
    Vector2 targetVelocity;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Debug.Log(move);
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
        //rb.MovePosition(transform.position + (move * speed * Time.fixedDeltaTime));
    }
}
