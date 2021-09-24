using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Varollo.MyTween
{
    /// <summary>
    /// Tween effect for Quaternions
    /// </summary>
    public class MyTweenQuaternionEffect : IMyTweenEffect<Quaternion>
    {
        private Quaternion _resultValue;
        private bool _isTweening;
        private Queue<IMyTweenEffect<Quaternion>.EffectParameters> _effectQueue = new Queue<IMyTweenEffect<Quaternion>.EffectParameters>();

        public bool IsTweening => _isTweening;

        public Quaternion ResultValue => _resultValue;

        public Queue<IMyTweenEffect<Quaternion>.EffectParameters> EffectQueue => _effectQueue;

        public Quaternion ChangeInValue(Quaternion startValue, Quaternion targetValue) => targetValue * Quaternion.Inverse(startValue);

        public float ElapsedTime(float startTime) => Time.time - startTime;

        public IEnumerator ExecuteEffect(Quaternion startValue, Quaternion targetValue, float duration, TweeningFunctions.TweenType tweenType = TweeningFunctions.TweenType.QuadraticInOut, Action onFinishTweeningCallback = null)
        {
            if (IsTweening)
            {
                EffectQueue.Enqueue(new IMyTweenEffect<Quaternion>.EffectParameters
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
                Vector3 change = ChangeInValue(startValue, targetValue).eulerAngles;
                Vector3 euler = startValue.eulerAngles;
                Vector3 startEuler = startValue.eulerAngles;

                euler.x = TweeningFunctions.Tween(ElapsedTime(startTime), startEuler.x, change.x, duration, tweenType);
                euler.y = TweeningFunctions.Tween(ElapsedTime(startTime), startEuler.y, change.y, duration, tweenType);
                euler.z = TweeningFunctions.Tween(ElapsedTime(startTime), startEuler.z, change.z, duration, tweenType);

                _resultValue = Quaternion.Euler(euler);

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