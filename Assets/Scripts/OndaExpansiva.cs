using UnityEngine;

public class OndaExpansiva : MonoBehaviour
{
    public float velocidadExpansion = 5f;
    public float tamMax = 5f;
    public float duracion = 2f;

    private Vector3 escalaInicial;
    void Start()
    {
        escalaInicial = transform.localScale;
        Destroy(gameObject, duracion);
    }

    void Update()
    {
        transform.localScale += new Vector3(velocidadExpansion, velocidadExpansion, 0) * Time.deltaTime;

        if (transform.localScale.x >= tamMax)
        {
            Destroy(gameObject);
        }
    }
}
