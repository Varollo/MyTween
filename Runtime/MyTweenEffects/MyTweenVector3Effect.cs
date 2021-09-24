using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Varollo.MyTween
{
    /// <summary>
    /// Tween effect for a Vector3
    /// </summary>
    public class MyTweenVector3Effect : IMyTweenEffect<Vector3>
    {
        private Vector3 _resultValue;
        private bool _isTweening;
        private Queue<IMyTweenEffect<Vector3>.EffectParameters> _effectQueue = new Queue<IMyTweenEffect<Vector3>.EffectParameters>();

        public bool IsTweening => _isTweening;

        public Vector3 ResultValue => _resultValue;

        public Queue<IMyTweenEffect<Vector3>.EffectParameters> EffectQueue => _effectQueue;

        public Vector3 ChangeInValue(Vector3 startValue, Vector3 targetValue) => targetValue - startValue;

        public float ElapsedTime(float startTime) => Time.time - startTime;

        public IEnumerator ExecuteEffect(Vector3 startValue, Vector3 targetValue, float duration, TweeningFunctions.TweenType tweenType = TweeningFunctions.TweenType.QuadraticInOut, Action onFinishTweeningCallback = null)
        {
            if (IsTweening)
            {
                EffectQueue.Enqueue(new IMyTweenEffect<Vector3>.EffectParameters
                {
                    TargetValue = targetValue,
                    Duration = duration,
                    TweenType = tweenType,
                    OnFinishTweeningCallback = onFinishTweeningCallback
                });

                yield break;
            }

            _isTweening = true;

            var startTime = Time.time;

            while (ElapsedTime(startTime) < duration)
            {
                _resultValue.x = TweeningFunctions.Tween(ElapsedTime(startTime), startValue.x, ChangeInValue(startValue, targetValue).x, duration, tweenType);
                _resultValue.y = TweeningFunctions.Tween(ElapsedTime(startTime), startValue.y, ChangeInValue(startValue, targetValue).y, duration, tweenType);
                _resultValue.z = TweeningFunctions.Tween(ElapsedTime(startTime), startValue.z, ChangeInValue(startValue, targetValue).z, duration, tweenType);

                yield return new WaitForSeconds(Time.deltaTime);
            }

            _resultValue = targetValue;

            _isTweening = false;

            onFinishTweeningCallback?.Invoke();

            if (_effectQueue.Count > 0)
            {
                var nextEffect = EffectQueue.Dequeue();
                yield return ExecuteEffect(_resultValue, nextEffect.TargetValue, nextEffect.Duration, nextEffect.TweenType, nextEffect.OnFinishTweeningCallback);
            }
        }
    }
}