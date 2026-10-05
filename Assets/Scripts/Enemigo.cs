using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Daño")]
    public int danio = 20;

    private bool puedeHacerDanio = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && puedeHacerDanio)
        {
            SaludJugador saludJugador = other.GetComponent<SaludJugador>();

            if (saludJugador != null)
            {
                saludJugador.RecibirDanio(danio);
                puedeHacerDanio = false;

                Debug.Log("El enemigo hizo " + danio + " de daño.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puedeHacerDanio = true;
        }
    }
}