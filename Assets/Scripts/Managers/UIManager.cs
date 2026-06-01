using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HUDController hudController;

    private void Update()
    {
        if (GameManager.Instance?.State == null) return;
        hudController?.UpdateTarget(TaskManager.Instance?.GetActiveTask());
    }

    public void ActivateHUD() => hudController?.Activate();
    public void DeactivateHUD() => hudController?.Deactivate();
}
