using System;

using UnityEngine;


public class UIViewSwitcher : GenericUIViewSwitcher<UIViewType, UIViewBehaviour, UIView>
{
    public event Action TapToStart;
    public event Action ChangeSizePlatform;
    public event Action ChangeMoveSpeed;

    [SerializeField] private Game _game;

    public void SetTrigger(string triggerName)
    {
        _uiAnimator.SetTrigger(triggerName);

        if(triggerName == "GameView")
        {
            TapToStart?.Invoke();
        }
    }

    private void OnGameEnd()
    {
        SetTrigger("MenuView");
    }
}