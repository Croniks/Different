using System;
using System.Collections.Generic;

using UnityEngine;


public abstract class GenericUIViewSwitcher<ViewType, ViewBehaviour, View> : MonoBehaviour 
    where ViewType : Enum
    where ViewBehaviour : GenericUIViewBehaviour<ViewType>
    where View : AbstractUIView
{
    [Serializable]
    private class ViewInfo
    {
        public ViewType Type;
        public View View;
    }

    [SerializeField] protected Animator _uiAnimator;
    [SerializeField] private List<ViewInfo> _viewInfos;

    private Dictionary<ViewType, View> _viewsTypesAndViews = new Dictionary<ViewType, View>();
   
     
    private void Awake()
    {
        SetupTypesAndViews(_viewInfos, _viewsTypesAndViews);
    }

    private void OnEnable()
    {
        var viewsBehaviours = _uiAnimator.GetBehaviours<ViewBehaviour>();
        SubscribeToViewBehaviours(viewsBehaviours, true);
    }

    private void OnDisable()
    {
        var viewsBehaviours = _uiAnimator.GetBehaviours<ViewBehaviour>();
        SubscribeToViewBehaviours(viewsBehaviours, false);
    }

    private void SetupTypesAndViews(IEnumerable<ViewInfo> viewsInfos, IDictionary<ViewType, View> viewsTypesAndViews)
    {
        foreach (var viewsInfo in viewsInfos)
        {
            viewsTypesAndViews.Add(viewsInfo.Type, viewsInfo.View);
        }
    }

    private void SubscribeToViewBehaviours(IEnumerable<ViewBehaviour> behaviours, bool on) 
    {
        foreach(var behaviour in behaviours)
        {
            if(on == true)
            {
                behaviour.ViewActivated += OnViewActivated;
            }
            else
            {
                behaviour.ViewActivated -= OnViewActivated;
            }
        }
    }

    protected virtual void OnViewActivated(bool on, ViewType viewType)
    {
        _viewsTypesAndViews[viewType].ActivateView(on);
    }
}