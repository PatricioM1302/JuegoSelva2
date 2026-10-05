using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    [SerializeField] private int puntos = 1;
    [SerializeField] private float velocidadRotacion = 80f;

    private void Update()
    {
        transform.Rotate(0f, velocidadRotacion * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instancia != null)
            {
                GameManager.instancia.AgregarPuntos(puntos);
            }
            else
            {
                Debug.LogError("No se encontró el GameManager.");
            }

            Destroy(gameObject);
        }
    }
}