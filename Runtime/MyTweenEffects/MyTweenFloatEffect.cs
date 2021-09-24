using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Varollo.MyTween
{
    /// <summary>
    /// Tween effect for a float type
    /// </summary>
    public class MyTweenFloatEffect : IMyTweenEffect<float>
    {
        private float _resultValue;
        private bool _isTweening;
        private Queue<IMyTweenEffect<float>.EffectParameters> _effectQueue = new Queue<IMyTweenEffect<float>.EffectParameters>();

        public bool IsTweening => _isTweening;

        public float ResultValue => _resultValue;

        public Queue<IMyTweenEffect<float>.EffectParameters> EffectQueue => _effectQueue;

        public float ChangeInValue(float startValue, float targetValue) => targetValue - startValue;

        public float ElapsedTime(float startTime) => Time.time - startTime;

        public IEnumerator ExecuteEffect(float startValue, float targetValue, float duration, TweeningFunctions.TweenType tweenType = TweeningFunctions.TweenType.QuadraticInOut, Action onFinishTweeningCallback = null)
        {
            if (IsTweening)
            {
                EffectQueue.Enqueue(new IMyTweenEffect<float>.EffectParameters
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
                _resultValue = TweeningFunctions.Tween(ElapsedTime(startTime), startValue, ChangeInValue(startValue, targetValue), duration, tweenType);

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