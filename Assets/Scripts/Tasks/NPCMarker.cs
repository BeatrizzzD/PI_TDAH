using UnityEngine;

public class NPCMarker : MonoBehaviour
{
    [SerializeField] private int npcId;
    [SerializeField] private GameObject taskIndicator;

    public int NpcId => npcId;
    public Vector2 Position => transform.position;

    private void Start() => HideIndicator();

    public void ShowIndicator() { if (taskIndicator) taskIndicator.SetActive(true); }
    public void HideIndicator() { if (taskIndicator) taskIndicator.SetActive(false); }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        TaskManager.Instance?.OnPlayerReachedNPC(npcId);

        var delivery = FindFirstObjectByType<DeliveryMinigame>();
        if (delivery != null && delivery.isActiveAndEnabled)
            delivery.OnPlayerReachedDestination(npcId);
    }
}
