using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Varollo.MyTween
{
    public class MyTweenVector3Effect : IMyTweenEffect<Vector3>
    {
        private Vector3 result;
        private bool locked;
        private Queue<IMyTweenEffect<Vector3>.EffectParameters> effectQueue = new Queue<IMyTweenEffect<Vector3>.EffectParameters>();

        public bool IsLocked => locked;

        public Vector3 ResultValue => result;

        public Queue<IMyTweenEffect<Vector3>.EffectParameters> EffectQueue => effectQueue;

        public Vector3 ChangeInValue(Vector3 startValue, Vector3 targetValue) => targetValue - startValue;

        public float ElapsedTime(float startTime) => Time.time - startTime;

        public IEnumerator ExecuteEffect(Vector3 startValue, Vector3 targetValue, float duration, TweeningFunctions.TweenType tweenType = TweeningFunctions.TweenType.QuadraticInOut, Action onFinishTweeningCallback = null)
        {
            if (IsLocked)
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

            locked = true;

            var startTime = Time.time;

            while (ElapsedTime(startTime) < duration)
            {
                result.x = TweeningFunctions.Tween(ElapsedTime(startTime), startValue.x, ChangeInValue(startValue, targetValue).x, duration, tweenType);
                result.y = TweeningFunctions.Tween(ElapsedTime(startTime), startValue.y, ChangeInValue(startValue, targetValue).y, duration, tweenType);
                result.z = TweeningFunctions.Tween(ElapsedTime(startTime), startValue.z, ChangeInValue(startValue, targetValue).z, duration, tweenType);

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