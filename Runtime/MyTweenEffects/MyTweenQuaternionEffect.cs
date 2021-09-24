using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Varollo.MyTween
{
    public class MyTweenQuaternionEffect : IMyTweenEffect<Quaternion>
    {
        private Quaternion result;
        private bool locked;
        private Queue<IMyTweenEffect<Quaternion>.EffectParameters> effectQueue = new Queue<IMyTweenEffect<Quaternion>.EffectParameters>();

        public bool IsLocked => locked;

        public Quaternion ResultValue => result;

        public Queue<IMyTweenEffect<Quaternion>.EffectParameters> EffectQueue => effectQueue;

        public Quaternion ChangeInValue(Quaternion startValue, Quaternion targetValue) => targetValue * Quaternion.Inverse(startValue);

        public float ElapsedTime(float startTime) => Time.time - startTime;

        public IEnumerator ExecuteEffect(Quaternion startValue, Quaternion targetValue, float duration, TweeningFunctions.TweenType tweenType = TweeningFunctions.TweenType.QuadraticInOut, Action onFinishTweeningCallback = null)
        {
            if (IsLocked)
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

            locked = true;

            var startTime = Time.time;

            while (ElapsedTime(startTime) < duration)
            {
                Vector3 change = ChangeInValue(startValue, targetValue).eulerAngles;
                Vector3 euler = startValue.eulerAngles;
                Vector3 startEuler = startValue.eulerAngles;

                euler.x = TweeningFunctions.Tween(ElapsedTime(startTime), startEuler.x, change.x, duration, tweenType);
                euler.y = TweeningFunctions.Tween(ElapsedTime(startTime), startEuler.y, change.y, duration, tweenType);
                euler.z = TweeningFunctions.Tween(ElapsedTime(startTime), startEuler.z, change.z, duration, tweenType);

                result = Quaternion.Euler(euler);

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