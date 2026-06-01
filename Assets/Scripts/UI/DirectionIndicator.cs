using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    [SerializeField] private RectTransform arrowImage;
    [SerializeField] private CanvasGroup canvasGroup;

    private Transform player;
    private Transform target;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        Hide();
    }

    private void Update()
    {
        if (player == null || target == null) return;
        Vector2 dir = ((Vector2)target.position - (Vector2)player.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        arrowImage.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void PointTo(Transform targetTransform)
    {
        target = targetTransform;
        if (canvasGroup) canvasGroup.alpha = 1f;
    }

    public void Hide()
    {
        target = null;
        if (canvasGroup) canvasGroup.alpha = 0f;
    }
}
