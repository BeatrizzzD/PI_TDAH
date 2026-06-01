using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private DirectionIndicator directionIndicator;
    [SerializeField] private GameObject hudRoot;

    public void Activate() => hudRoot.SetActive(true);
    public void Deactivate() => hudRoot.SetActive(false);

    public void UpdateTarget(TaskData activeTask)
    {
        if (activeTask == null) { directionIndicator.Hide(); return; }

        foreach (var marker in FindObjectsByType<NPCMarker>(FindObjectsSortMode.None))
        {
            if (marker.NpcId == activeTask.NpcId)
            {
                directionIndicator.PointTo(marker.transform);
                return;
            }
        }
        directionIndicator.Hide();
    }
}
