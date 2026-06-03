using UnityEngine;

// Dirige o Animator do personagem a partir da velocidade do Rigidbody2D.
// Desacoplado do PlayerController de propósito: como lê a velocidade real,
// anima tanto o movimento WASD normal quanto o ForceMoveTo do ImpulseWalk.
[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerAnimator : MonoBehaviour
{
    // Abaixo disso o personagem é considerado parado (entra no estado Idle).
    [SerializeField] private float moveThreshold = 0.1f;

    private Animator animator;
    private Rigidbody2D rb;

    // Último facing cardinal não-nulo: mantém o Idle apontando pro lado certo
    // depois que o player para. Começa virado pra frente (baixo).
    private float lastMoveX = 0f;
    private float lastMoveY = -1f;

    private static readonly int MoveXHash = Animator.StringToHash("MoveX");
    private static readonly int MoveYHash = Animator.StringToHash("MoveY");
    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 velocity = rb.linearVelocity;
        float speed = velocity.magnitude;

        if (speed > moveThreshold)
        {
            // Trava em uma das 4 direções cardinais pelo eixo dominante,
            // para o blend tree não cair entre dois sprites na diagonal.
            if (Mathf.Abs(velocity.x) >= Mathf.Abs(velocity.y))
            {
                lastMoveX = Mathf.Sign(velocity.x);
                lastMoveY = 0f;
            }
            else
            {
                lastMoveX = 0f;
                lastMoveY = Mathf.Sign(velocity.y);
            }
        }

        animator.SetFloat(MoveXHash, lastMoveX);
        animator.SetFloat(MoveYHash, lastMoveY);
        animator.SetFloat(SpeedHash, speed);
    }
}
