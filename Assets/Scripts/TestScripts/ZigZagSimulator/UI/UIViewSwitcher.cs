public class UIViewSwitcher : GenericUIViewSwitcher<UIViewType, UIViewBehaviour, AbstractUIView>
{
    protected override void OnViewActivated(bool on, UIViewType viewType)
    {
        var view = _viewsTypesAndViews[viewType];
        view.ShowView(on);
    }
    
    public void OpenView(UIViewType type)
    {
        _uiAnimator.SetTrigger(type.ToString());
    }
}