using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private static PhaseResult _pendingResult;

    public static PhaseResult GetAndClearResult()
    {
        var r = _pendingResult;
        _pendingResult = null;
        return r;
    }

    public void GoToResult(PhaseResult result) { _pendingResult = result; SceneManager.LoadScene("Result"); }
    public void GoToMenu() => SceneManager.LoadScene("MenuInicial");
    public void GoToPhase1() => SceneManager.LoadScene("Phase1_Office");
    public void GoToPhase2() => SceneManager.LoadScene("Phase2_Office");
}
