using UnityEngine;
using UnityEngine.SceneManagement;

public class Victoria : MonoBehaviour
{
    public static Victoria Instance;

    [Header("Configuración de Victoria")]
    public GameObject panelVictoria; 
    public string nombreSiguienteEscena = "tutorial 2";
    public float tiempoEsperaCambioEscena = 2f;

    private Bombillos[] listaBombillos;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        listaBombillos = FindObjectsOfType<Bombillos>();

        if (panelVictoria != null)
        {
            panelVictoria.SetActive(false);
        }
    }

    public void ComprobarVictoria()
    {
        foreach (Bombillos bombillo in listaBombillos)
        {
            if (!bombillo.EstaEncendido())
            {
                return;
            }
        }

        Ganaste();
    }

    void Ganaste()
    {
        Debug.Log("¡Todos los bombillos encendidos!");
        
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true); 
        }

        Time.timeScale = 0f;
    }

    public void CargarSiguienteNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreSiguienteEscena);
    }
}