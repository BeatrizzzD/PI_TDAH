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
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void SetInputEnabled(bool enabled)
    {
        inputEnabled = enabled;
        if (!enabled) rb.linearVelocity = Vector2.zero;
    }

    public void ForceMove(Vector2 direction, float duration)
    {
        StartCoroutine(ForceMoveCoroutine(direction, duration));
    }

    private System.Collections.IEnumerator ForceMoveCoroutine(Vector2 direction, float duration)
    {
        inputEnabled = false;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            rb.linearVelocity = direction * moveSpeed;
            elapsed += Time.deltaTime;
            yield return null;
        }
        rb.linearVelocity = Vector2.zero;
        inputEnabled = true;
    }
}
