using System.Collections;
using UnityEngine;

public class Bombillos : MonoBehaviour
{   
    public Sprite bombilloEncendido;
    public float tiempoEncendido = 3f;
    private SpriteRenderer spriteRenderer;
    private Sprite bombilloApagado;     
    private bool isEncendido = false;
    private Coroutine corrutinaApagado; 
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            bombilloApagado = spriteRenderer.sprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BalaOnda>() != null)
        {
            Encender();
        }
    }
    
    private void Encender()
    {
        isEncendido = true;
        
        if (bombilloEncendido != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = bombilloEncendido;
        }

        if (corrutinaApagado != null)
        {
            StopCoroutine(corrutinaApagado);
        }

        corrutinaApagado = StartCoroutine(TemporizadorApagado());
    }

    private IEnumerator TemporizadorApagado()
    {
        yield return new WaitForSeconds(tiempoEncendido);
        Apagar();
    }

    private void Apagar()
    {
        isEncendido = false;
        
        if (bombilloApagado != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = bombilloApagado;
        }
    }
}