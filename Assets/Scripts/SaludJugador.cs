using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SaludJugador : MonoBehaviour
{
    [Header("Salud del jugador")]
    public int saludMaxima = 100;
    public int saludActual;

    [Header("Interfaz")]
    public TMP_Text textoSalud;

    private bool muerto = false;

    private void Start()
    {
        saludActual = saludMaxima;
        ActualizarInterfaz();
    }

    public void RecibirDanio(int cantidad)
    {
        if (muerto)
            return;

        saludActual -= cantidad;

        if (saludActual < 0)
        {
            saludActual = 0;
        }

        ActualizarInterfaz();

        Debug.Log("Salud del jugador: " + saludActual);

        if (saludActual <= 0)
        {
            Morir();
        }
    }

    private void ActualizarInterfaz()
    {
        if (textoSalud != null)
        {
            textoSalud.text = "Salud: " + saludActual;
        }
    }

    private void Morir()
    {
        muerto = true;

        Debug.Log("El jugador ha muerto. Reiniciando nivel...");

        Invoke(nameof(ReiniciarNivel), 2f);
    }

    private void ReiniciarNivel()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}