using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bombillos : MonoBehaviour
{   
    public Sprite bombilloEncendido;
    public float tiempoEncendido = 3f;

    [Header("UI Indicador Radial")]
    public Image imagenProgreso;

    private SpriteRenderer spriteRenderer;
    private Sprite bombilloApagado;     
    private bool isEncendido = false;
    private float tiempoRestante = 0f;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            bombilloApagado = spriteRenderer.sprite;
        }

        if (imagenProgreso != null)
        {
            imagenProgreso.gameObject.SetActive(false);
            imagenProgreso.fillAmount = 0f;
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
        
        tiempoRestante = tiempoEncendido;
        
        if (imagenProgreso != null)
        {
            imagenProgreso.gameObject.SetActive(true);
            imagenProgreso.fillAmount = 1f;
        }
    }   

    void Update()
    {
        if(isEncendido)
        {
            tiempoRestante -= tiempoEncendido.deltaTime;
            if(imagenProgreso != null)
            {
                imagenProgreso.fillAmount = tiempoRestante/tiempoEncendido;
            }
            if(tiempoRestante <= 0)
            {
                Apagar();
            }
        }
    }
    private void Apagar()
    {
        isEncendido = false;
        
        if (bombilloApagado != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = bombilloApagado; 
        }
        if (imagenProgreso != null)
        {
            imagenProgreso.fillAmount = 0f;
            imagenProgreso.gameObject.SetActive(false);
        }
    }
}