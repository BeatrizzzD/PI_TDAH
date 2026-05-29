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
        destinationNpcId = (taskData.NpcId + 1) % Constants.TotalTasks;
        destinationTransform = FindDestinationNPC(destinationNpcId);
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

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

    private Transform FindDestinationNPC(int id)
    {
        foreach (var marker in FindObjectsByType<NPCMarker>(FindObjectsSortMode.None))
            if (marker.NpcId == id) return marker.transform;
        return null;
    }
}
