using System;

using UnityEngine;


public class UI : GenericUIViewSwitcher<UIViewType, UIViewBehaviour, UIView>
{
    public event Action TapedToStart;
    public event Action SizePlatformChanged;
    public event Action MoveSpeedChanged;

    [SerializeField] private Game _game;

    
    protected override void OnViewActivated(bool on, UIViewType viewType)
    {
        var view = _viewsTypesAndViews[viewType];
        view.ActivateView(on);
        
        if (viewType == UIViewType.Game)
        {
            view.ResetContent();
            TapedToStart?.Invoke();
        }
    }

    private void OnGameEnd()
    {
        _uiAnimator.SetTrigger(UIViewType.Menu.ToString());
    }
}