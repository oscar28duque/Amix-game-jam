using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class Victoria : MonoBehaviour
{
    public static Victoria Instance;

    [Header("Configuración de Victoria")]
    public GameObject panelVictoria;

    [Header("Configuración de Tiempo")]
    public float tiempoMaximo = 60f;
    public TextMeshProUGUI textoTiempo;

    [Header("Panel de Tiempo Agotado")]
    public GameObject panelTiempoAgotado;
    public float tiempoMostrarPanel = 2f;

    private float tiempoRestante;
    private Bombillos[] listaBombillos;
    private bool juegoTerminado = false;

    void Awake() { if (Instance == null) Instance = this; else Destroy(gameObject); }

    void Start()
    {
        Time.timeScale = 1f;
        tiempoRestante = tiempoMaximo;
        listaBombillos = FindObjectsOfType<Bombillos>();

        if (panelVictoria != null) panelVictoria.SetActive(false);
        if (panelTiempoAgotado != null) panelTiempoAgotado.SetActive(false);

        ActualizarTextoTiempo();
    }

    void Update()
    {
        if (juegoTerminado) return;

        tiempoRestante -= Time.deltaTime;
        ActualizarTextoTiempo();

        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            ActualizarTextoTiempo();
            StartCoroutine(TiempoAgotado());
        }
    }

    void ActualizarTextoTiempo()
    {
        if (textoTiempo != null)
        {
            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);
            textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }

    IEnumerator TiempoAgotado()
    {
        juegoTerminado = true;
        Debug.Log("¡Se acabó el tiempo!");

        if (panelTiempoAgotado != null)
            panelTiempoAgotado.SetActive(true);

        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(tiempoMostrarPanel);

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ComprobarVictoria()
    {
        foreach (Bombillos bombillo in listaBombillos)
        {
            if (!bombillo.EstaEncendido()) return;
        }

        Ganaste();
    }

    void Ganaste()
    {
        juegoTerminado = true;
        Debug.Log("¡Todos los bombillos encendidos!");

        if (panelVictoria != null)
            panelVictoria.SetActive(true);

        Time.timeScale = 0f;
    }

    public void CargarSiguienteNivel()
    {
        Time.timeScale = 1f;

        int siguienteEscena = SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(siguienteEscena);
    }
}