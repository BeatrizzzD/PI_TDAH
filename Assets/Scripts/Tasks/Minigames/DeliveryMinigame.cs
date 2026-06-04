using UnityEngine;
using TMPro;

public class DeliveryMinigame : MinigameBase
{
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private RectTransform directionArrow;

    private Transform player;
    private Transform destinationTransform;
    private int destinationNpcId;

    protected override void OnMinigameStart()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        destinationNpcId = FindFarthestNpcId();
        destinationTransform = FindDestinationNPC(destinationNpcId);

        instructionText.text = $"Leve o documento para o colega {destinationNpcId + 1}!\nSiga a seta.";

        // Delivery é o único minigame em que o jogador continua andando
        player?.GetComponent<PlayerController>()?.SetInputEnabled(true);
    }

    protected override void Update()
    {
        base.Update();
        if (timerText) timerText.text = Mathf.Ceil(timeRemaining).ToString();
        UpdateArrow();
    }

    private void UpdateArrow()
    {
        if (directionArrow == null || player == null || destinationTransform == null) return;
        Vector2 dir = ((Vector2)destinationTransform.position - (Vector2)player.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        directionArrow.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void OnPlayerReachedDestination(int npcId)
    {
        if (!isActiveAndEnabled) return;
        if (npcId == destinationNpcId) Complete(true);
    }

    private int FindFarthestNpcId()
    {
        var markers = FindObjectsByType<NPCMarker>(FindObjectsSortMode.None);
        NPCMarker farthest = null;
        float maxDist = -1f;
        Vector2 origin = player != null ? (Vector2)player.position : Vector2.zero;

        foreach (var m in markers)
        {
            if (m.NpcId == taskData.NpcId) continue;
            float dist = Vector2.Distance(origin, m.Position);
            if (dist > maxDist) { maxDist = dist; farthest = m; }
        }

        return farthest?.NpcId ?? (taskData.NpcId + 1) % Constants.TotalTasks;
    }

    private Transform FindDestinationNPC(int id)
    {
        foreach (var marker in FindObjectsByType<NPCMarker>(FindObjectsSortMode.None))
            if (marker.NpcId == id) return marker.transform;
        return null;
    }
}
