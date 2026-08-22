using UnityEngine;

public class BalaOnda : MonoBehaviour
{
    public float velocidad = 5f;
    public float duracion = 2f;
    void Start()
    {
        Destroy(gameObject, duracion);
    }

    void Update()
    {
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Muro"))
        {
            Destroy(gameObject);
        }
    }
}
