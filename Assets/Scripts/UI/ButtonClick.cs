using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip;

    public void OnClick()
    {
        if (clickClip == null) return;
        AudioSource.PlayClipAtPoint(clickClip, Camera.main.transform.position);
    }
}