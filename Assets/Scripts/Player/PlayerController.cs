using UnityEngine;

namespace ExploradorSelva.Player
{
    /// <summary>
    /// Controla el movimiento del explorador en vista lateral 2D (plataformas).
    /// El eje horizontal lo maneja el jugador; el eje vertical queda a cargo
    /// de la física (gravedad + salto).
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movimiento")]
        [SerializeField] private float walkSpeed = 3f;
        [SerializeField] private float runSpeed = 6f;
        [SerializeField] private float jumpForce = 8f;

        [Header("Detección de suelo")]
        [SerializeField] private Transform groundCheck;   // GameObject vacío ubicado en los pies
        [SerializeField] private float groundCheckRadius = 0.15f;
        [SerializeField] private LayerMask groundLayer;   // Asignar el Layer "Ground"

        [Header("Referencias")]
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Rigidbody2D _rb;
        private float _moveInputX;
        private bool _isRunning;
        private bool _isGrounded;
        private bool _jumpRequested;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            ReadInput();
            FlipSprite();
            CheckGround();
        }

        private void FixedUpdate()
        {
            Move();
            if (_jumpRequested)
            {
                Jump();
                _jumpRequested = false;
            }
        }

        private void ReadInput()
        {
            _moveInputX = Input.GetAxisRaw("Horizontal");
            _isRunning = Input.GetKey(KeyCode.LeftShift);

            if (Input.GetButtonDown("Jump") && _isGrounded)
                _jumpRequested = true;
        }

        private void CheckGround()
        {
            if (groundCheck == null) return;
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        private void Move()
        {
            float speed = _isRunning ? runSpeed : walkSpeed;
            // El eje Y ya no lo tocamos: lo deja libre para la gravedad y el salto
            _rb.linearVelocity = new Vector2(_moveInputX * speed, _rb.linearVelocity.y);
        }

        private void Jump()
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
        }

        private void FlipSprite()
        {
            if (_moveInputX > 0.01f)
                spriteRenderer.flipX = false;
            else if (_moveInputX < -0.01f)
                spriteRenderer.flipX = true;
        }

        private void OnDrawGizmosSelected()
        {
            if (groundCheck == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
