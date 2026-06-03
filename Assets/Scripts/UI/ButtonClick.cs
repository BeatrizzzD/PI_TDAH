using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip;

    public void OnClick()
    {
        if (clickClip == null) return;
        // Som de clique não pode depender de Camera.main: numa cena sem câmera
        // (ex.: Result) ele vinha null e o NullRef bloqueava a própria navegação.
        Vector3 pos = Camera.main != null ? Camera.main.transform.position : Vector3.zero;
        AudioSource.PlayClipAtPoint(clickClip, pos);
    }
}