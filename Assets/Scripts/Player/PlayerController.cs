using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private InputSystem_Actions inputActions;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool inputEnabled = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void Update()
    {
        if (!inputEnabled) return;
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Input travado: não dirigir a velocidade aqui — ou está zerada (minigame),
        // ou o ForceMoveTo está no controle (ImpulseWalk). Sem esse guard, o moveInput
        // congelado faria o player deslizar pra sempre / ignorar o alvo do impulso.
        if (!inputEnabled) return;
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void SetInputEnabled(bool enabled)
    {
        inputEnabled = enabled;
        if (!enabled)
        {
            moveInput = Vector2.zero;
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void ForceMoveTo(Vector2 target, float maxDuration)
    {
        StartCoroutine(ForceMoveToCoroutine(target, maxDuration));
    }

    private System.Collections.IEnumerator ForceMoveToCoroutine(Vector2 target, float maxDuration)
    {
        inputEnabled = false;
        moveInput = Vector2.zero;
        float elapsed = 0f;
        const float arriveThreshold = 0.15f;

        // Mira o alvo a cada frame e para ao chegar (ou quando o tempo do evento
        // estoura). Direção fixa fazia o player ultrapassar o ponto e seguir reto.
        while (elapsed < maxDuration && Vector2.Distance(rb.position, target) > arriveThreshold)
        {
            Vector2 dir = (target - rb.position).normalized;
            rb.linearVelocity = dir * moveSpeed;
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        inputEnabled = true;
    }
}
