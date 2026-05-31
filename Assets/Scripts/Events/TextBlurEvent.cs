using UnityEngine;

public class TextBlurEvent : GameEventBase
{
    [SerializeField] private CanvasGroup hudCanvasGroup;

    protected override void OnEventStart()
    {
        if (hudCanvasGroup) hudCanvasGroup.alpha = 0.25f;
    }

    protected override void OnEventEnd()
    {
        if (hudCanvasGroup) hudCanvasGroup.alpha = 1f;
    }
}
