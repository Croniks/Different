using System;
using UnityEngine;
using DG.Tweening;


namespace DOTweenBased
{
    public class DOTweenTimer : MonoBehaviour
    {
        [SerializeField] private int _secondsNumber;
        [SerializeField] private int _warningMoment;
        [SerializeField] private bool _launchOnStart = false;
        private Tweener timer;


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

                timer = DOVirtual.Int(seconds, 0, seconds, (seconds) =>
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
                }).SetAutoKill(false).SetEase(Ease.Linear);
            }
            else
            {
                timer.ChangeValues(seconds, 0, seconds);
                timer.Restart();
            }
        }
    }
}