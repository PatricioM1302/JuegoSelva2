using UnityEngine;

public class Llave : MonoBehaviour
{
    [Header("Configuración")]
    public float velocidadRotacion = 80f;

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
                GameManager.instancia.ObtenerLlave();
            }

            Destroy(gameObject);
        }
    }
}