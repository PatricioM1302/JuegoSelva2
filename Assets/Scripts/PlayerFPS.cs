using UnityEngine;

/// <summary>
/// Controlador FPS del jugador.
/// Gestiona movimiento, carrera, salto, gravedad y cámara.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerFPS : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidadCaminar = 5f;
    [SerializeField] private float velocidadCorrer = 8f;
    [SerializeField] private float alturaSalto = 1.5f;
    [SerializeField] private float gravedad = -20f;

    [Header("Detección de suelo")]
    [SerializeField] private float distanciaSuelo = 0.3f;

    [Header("Cámara")]
    [SerializeField] private Camera camaraJugador;
    [SerializeField] private float sensibilidad = 2f;
    [SerializeField] private float limiteVertical = 80f;

    private CharacterController characterController;

    private float velocidadVertical;
    private float rotacionVertical;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if (camaraJugador == null)
        {
            camaraJugador = GetComponentInChildren<Camera>();
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MoverJugador();
        MirarConCamara();

        // Permite liberar el cursor durante las pruebas.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void MoverJugador()
    {
        // Movimiento WASD.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direccion =
            transform.right * horizontal +
            transform.forward * vertical;

        float velocidad = Input.GetKey(KeyCode.LeftShift)
            ? velocidadCorrer
            : velocidadCaminar;

        characterController.Move(
            direccion.normalized * velocidad * Time.deltaTime
        );

        // Comprueba si existe terreno debajo del jugador.
        bool estaEnSuelo = Physics.Raycast(
            transform.position + Vector3.up * 0.2f,
            Vector3.down,
            distanciaSuelo + 0.2f
        );

        if (estaEnSuelo && velocidadVertical < 0f)
        {
            velocidadVertical = -2f;
        }

        // Salto con barra espaciadora.
        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo)
        {
            velocidadVertical =
                Mathf.Sqrt(alturaSalto * -2f * gravedad);
        }

        // Aplicar gravedad.
        velocidadVertical += gravedad * Time.deltaTime;

        characterController.Move(
            Vector3.up * velocidadVertical * Time.deltaTime
        );
    }

    private void MirarConCamara()
    {
        if (camaraJugador == null)
            return;

        float mouseX =
            Input.GetAxis("Mouse X") * sensibilidad;

        float mouseY =
            Input.GetAxis("Mouse Y") * sensibilidad;

        // Giro horizontal del jugador.
        transform.Rotate(Vector3.up * mouseX);

        // Giro vertical de la cámara.
        rotacionVertical -= mouseY;

        rotacionVertical = Mathf.Clamp(
            rotacionVertical,
            -limiteVertical,
            limiteVertical
        );

        camaraJugador.transform.localRotation =
            Quaternion.Euler(rotacionVertical, 0f, 0f);
    }
}