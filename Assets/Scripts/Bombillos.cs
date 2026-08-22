using UnityEngine;

public class Bombillos : MonoBehaviour
{   
    public Sprite bombilloEncendido;
    private SpriteRenderer spriteRenderer;
    private bool isEncendido = false;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isEncendido && collision.GetComponent<OndaExpansiva>() != null)
        {
            Encender();
        }
    }
    
    private void Encender()
    {
        isEncendido = true;
        if (bombilloEncendido != null)
        {
            spriteRenderer.sprite = bombilloEncendido;
        }
    }

    void Update()
    {
        
    }
}