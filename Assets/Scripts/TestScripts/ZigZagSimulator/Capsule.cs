using System;

using UnityEngine;

using DG.Tweening;


public class Capsule : MonoBehaviour
{
    public event Action<int> ScoresReceived;

    [SerializeField] private int _scores = 1;

    [SerializeField] private float _animationDuration = 1f;

    [SerializeField] private float _initialScaleAnimationFactor = 0.9f;
    [SerializeField] private float _endScaleAnimationFactor = 1.1f;
    [SerializeField] private float _moveAnimationYOffset = 0.2f;
    
    private Vector3 _initialScale;
    private Vector3 _initialPosition;
    
    private Tween _seq = null;

    private CapsulePlacer _capsulePlacer = null;


    private void Awake()
    {
        _initialScale = transform.localScale;
        _initialPosition = transform.position; 

        //LaunchAnimation();
    }
    
    public Capsule SetupCapsule(CapsulePlacer capsulePlacer)
    {
        _capsulePlacer = capsulePlacer;
        return this;
    }

    public Capsule PlaceCapsule(Vector3 position)
    {
        transform.position = _initialPosition = position;

        return this;
    }

    public Capsule LaunchAnimation()
    {
        transform.position = _initialPosition;
        Vector3 endMoveAnimationValue = _initialPosition + new Vector3(0f, _moveAnimationYOffset, 0f);

        var moveAnimation = transform.DOMove(endMoveAnimationValue, _animationDuration)
            .SetEase(Ease.Linear);
        
        transform.localScale = _initialScale * _initialScaleAnimationFactor;
        Vector3 endScaleAnimationValue = _initialScale * _endScaleAnimationFactor;

        var scaleAnimation = transform.DOScale(endScaleAnimationValue, _animationDuration)
            .SetEase(Ease.Linear);

        _seq = DOTween.Sequence()
            .Insert(0f, moveAnimation)
            .Insert(0f, scaleAnimation)
            .SetRecyclable(true)
            .SetLoops(-1, LoopType.Yoyo)
            .SetUpdate(UpdateType.Fixed)
            .SetEase(Ease.Linear)
            .Play();

        return this;
    }

    private void OnTriggerEnter(Collider other)
    {
        ScoresReceived?.Invoke(_scores);
        ReturnToCapsulePlacer();
    }

    public void ReturnToCapsulePlacer()
    {
        if (_seq != null && _seq.IsActive() && _seq.IsPlaying())
        {
            _seq.Kill();
            _seq = null;
        }

        if(_capsulePlacer != null)
            _capsulePlacer.ReturnCaplsule(this);
    }
}