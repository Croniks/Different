using System;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using System.Threading;
using UnityEngine.SocialPlatforms;


namespace UsefulScripts.DOTweenBased
{
    // Нельзя переиспользовать если при первом запуске указать 0 секунд
    // Разобраться почему !!!
    public class DOTweenTimer : MonoBehaviour
    {
        [SerializeField] private int _secondsNumber;
        [SerializeField] private int _warningMoment;
        [SerializeField] private bool _launchOnStart = false;
        private Tweener timer;
        private TweenerCore<int, int, DG.Tweening.Plugins.Options.NoOptions> timerCore;
        private TweenCallback<int> timerCallback;

        
        private void Start()
        {
            if(_launchOnStart == true)
            {
                LaunchTimer(_secondsNumber, _warningMoment);
            }   
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                LaunchTimer(_secondsNumber, _warningMoment);
            }
        }
        
        private void LaunchTimer(int seconds, int warningTime = 0)
        {
            if (seconds < 0) return;

            if (timer == null)
            {
                bool wasWarning = false;
                int previousSeconds = -1;

                timerCallback = (seconds) =>
                {
                    if (wasWarning == false && warningTime > 0f && seconds - warningTime <= 0f)
                    {
                        wasWarning = true;
                        Debug.Log($"Warning! {seconds} seconds left !!!");
                    }

                    if (previousSeconds != seconds)
                    {
                        if (seconds > 0)
                        {
                            Debug.Log(TimeSpan.FromSeconds(seconds).ToString("mm':'ss"));
                        }
                        if (seconds <= 0)
                        {
                            Debug.Log(TimeSpan.FromSeconds(0).ToString("mm':'ss"));
                        }

                        previousSeconds = seconds;
                    }
                };

                timer = DOVirtual.Int(seconds, 0, seconds, timerCallback)
                    .SetAutoKill(false)
                    .SetEase(Ease.Linear);

                timerCore = (TweenerCore<int, int, DG.Tweening.Plugins.Options.NoOptions>)timer;
            }
            else
            {
                Debug.Log($"timer.IsActive(): {timer.IsActive()}!!!");
                
                timer.ChangeValues(seconds, 0, seconds);
                timerCore.OnUpdate(delegate { timerCallback(seconds); });
                timer.Restart();
                
                Debug.Log($"timer.IsPlaying(): {timer.IsPlaying()}!!!");
                Debug.Log($"timerCore.startValue: {timerCore.startValue}!!!");
                Debug.Log($"timerCore.endValue: {timerCore.endValue}!!!");
            }
        }
    }
}