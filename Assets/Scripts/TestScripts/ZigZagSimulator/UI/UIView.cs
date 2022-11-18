using UnityEngine;
using DG.Tweening;


public class UIView : AbstractUIView
{
    [SerializeField] private RectTransform viewTransform;
    [SerializeField] private float activationDuration = 1f;

    public override void ActivateView(bool on)
    {
        Debug.Log($"gameObject.name : {gameObject.name}");
        Debug.Log($"bool on : {on}");

        Vector3 scale = on == true ? Vector3.one : Vector3.zero;

        viewTransform.DOScale(scale, activationDuration)
            .SetRecyclable(true)
            .Play();
    }
}