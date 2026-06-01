using UnityEngine;

public class PhaseStarter : MonoBehaviour
{
    [SerializeField] private bool isPhase2;
    private void Start() => GameManager.Instance.StartPhase(isPhase2);
}
