using UnityEngine;

public class NPCInteraccion : MonoBehaviour
{
    private bool jugadorCerca = false;
    private bool dialogoAbierto = false;

    [Header("Interfaz de diálogo")]
    public GameObject panelDialogo;

    void Start()
    {
        // El diálogo comienza oculto
        if (panelDialogo != null)
        {
            panelDialogo.SetActive(false);
        }
    }

    void Update()
    {
        // Solo podemos interactuar si estamos cerca del NPC
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            dialogoAbierto = !dialogoAbierto;

            if (panelDialogo != null)
            {
                panelDialogo.SetActive(dialogoAbierto);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            Debug.Log("Presiona E para hablar");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            dialogoAbierto = false;

            // Cierra automáticamente el diálogo
            // si el jugador se aleja del NPC
            if (panelDialogo != null)
            {
                panelDialogo.SetActive(false);
            }
        }
    }
}