using UnityEngine;
using DG.Tweening;


public class UIView : AbstractUIView, ISetupable<SettingsObject>
{
    [SerializeField] private float _activationDuration = 1f;
    [SerializeField] private AbstractViewContent<SettingsObject> _viewContent;

    private RectTransform _viewTransform;


    void Awake()
    {
        _viewTransform = GetComponent<RectTransform>();
    }

    public override void ActivateView(bool on)
    {
        Vector3 scale = on == true ? Vector3.one : Vector3.zero;

        _viewTransform.DOScale(scale, _activationDuration)
            .SetRecyclable(true)
            .Play();
    }

    public void ResetContent()
    {
        if(_viewContent != null)
        {
            _viewContent.ResetContent();
        }
    }

    public void SetupContent(SettingsObject settings)
    {
        if(_viewContent == null)
        {
            _viewContent.SetupContent(settings);
        }
    }
}