using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Puntuación")]
    public int puntos = 0;
    public int totalColeccionables = 6;

    [Header("Llave")]
    public bool tieneLlave = false;

    [Header("Interfaz")]
    public TMP_Text textoPuntos;
    public TMP_Text textoLlave;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
    }

    private void Start()
    {
        ActualizarInterfaz();
    }

    public void AgregarPuntos(int cantidad)
    {
        puntos += cantidad;
        ActualizarInterfaz();

        Debug.Log("Puntos totales: " + puntos);
    }

    public void ObtenerLlave()
    {
        tieneLlave = true;
        ActualizarInterfaz();

        Debug.Log("¡El jugador obtuvo la llave!");
    }

    private void ActualizarInterfaz()
    {
        if (textoPuntos != null)
        {
            textoPuntos.text =
                "Puntos: " + puntos + " / " + totalColeccionables;
        }

        if (textoLlave != null)
        {
            textoLlave.text = tieneLlave ? "Llave: Sí" : "Llave: No";
        }
    }
}