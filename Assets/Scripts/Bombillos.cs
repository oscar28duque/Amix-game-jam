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

    [Header("Efectos de Sonido")]
    public AudioClip sonidoEncender;
    private AudioSource audioSourceOn;

    public AudioClip sonidoApagar;
    private AudioSource audioSourceOff;

    private bool disponibleSonido = true;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSourceOn = GetComponent<AudioSource>();

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

    public bool EstaEncendido()
    {
        return isEncendido;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BalaOnda>() != null)
        {
            Encender();
        }
    }

    private void ActivarSonido()
    {
        disponibleSonido = true;
    }

    private void Encender()
    {
        isEncendido = true;
        if (isEncendido && audioSourceOn != null && sonidoEncender != null && disponibleSonido)
        {
            disponibleSonido = false;
            audioSourceOn.PlayOneShot(sonidoEncender);
            Invoke("ActivarSonido",1f); 
        }

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
        
        if (Victoria.Instance != null)
        {
            Victoria.Instance.ComprobarVictoria();
        }
    }   

    void Update()
    {
        if(isEncendido)
        {
            tiempoRestante -= Time.deltaTime;
            if(imagenProgreso != null)
            {
                imagenProgreso.fillAmount = tiempoRestante/tiempoEncendido;
            }
            if(tiempoRestante <= 0)
            {
                Apagar();
                
                audioSourceOff.PlayOneShot(sonidoApagar);

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