using UnityEngine;

public class ImpulseWalkEvent : GameEventBase
{
    [SerializeField] private Transform impulseTarget;

    private PlayerController player;

    protected override void OnEventStart()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null || impulseTarget == null) return;

        player = playerObj.GetComponent<PlayerController>();
        if (player == null) return;

        player.ForceMoveTo(impulseTarget.position, currentEvent.Duration);
    }

    protected override void OnEventEnd()
    {
        player?.SetInputEnabled(true);
    }
}
