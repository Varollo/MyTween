using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Varollo.MyTween
{
    public class MyTweenFloatEffect : IMyTweenEffect<float>
    {
        private float result;
        private bool locked;
        private Queue<IMyTweenEffect<float>.EffectParameters> effectQueue = new Queue<IMyTweenEffect<float>.EffectParameters>();

        public bool IsLocked => locked;

        public float ResultValue => result;

        public Queue<IMyTweenEffect<float>.EffectParameters> EffectQueue => effectQueue;

        public float ChangeInValue(float startValue, float targetValue) => targetValue - startValue;

        public float ElapsedTime(float startTime) => Time.time - startTime;

        public IEnumerator ExecuteEffect(float startValue, float targetValue, float duration, TweeningFunctions.TweenType tweenType = TweeningFunctions.TweenType.QuadraticInOut, Action onFinishTweeningCallback = null)
        {
            if (IsLocked)
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

            locked = true;

            var startTime = Time.time;

            while (ElapsedTime(startTime) < duration)
            {
                result = TweeningFunctions.Tween(ElapsedTime(startTime), startValue, ChangeInValue(startValue, targetValue), duration, tweenType);

                yield return new WaitForSeconds(Time.deltaTime);
            }

            result = targetValue;

            locked = false;

            onFinishTweeningCallback?.Invoke();

            if (effectQueue.Count > 0)
            {
                var nextEffect = EffectQueue.Dequeue();
                yield return ExecuteEffect(result, nextEffect.TargetValue, nextEffect.Duration, nextEffect.TweenType, nextEffect.OnFinishTweeningCallback);
            }
        }
    }
}