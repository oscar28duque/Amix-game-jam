using UnityEngine;

public class playerscript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    Vector3 move;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Debug.Log(move);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (move * speed * Time.fixedDeltaTime));
    }
}
