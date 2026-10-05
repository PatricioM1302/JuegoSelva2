using UnityEngine;

public class SalidaNivel : MonoBehaviour
{
    [Header("Interfaz")]
    public GameObject panelVictoria;

    private void Start()
    {
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instancia != null &&
                GameManager.instancia.tieneLlave)
            {
                Debug.Log("¡Nivel completado!");

                if (panelVictoria != null)
                {
                    panelVictoria.SetActive(true);
                }

                Time.timeScale = 0f;
            }
            else
            {
                Debug.Log("Necesitas encontrar la llave para salir.");
            }
        }
    }
}