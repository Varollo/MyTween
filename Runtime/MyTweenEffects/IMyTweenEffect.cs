using System;
using System.Collections;
using System.Collections.Generic;

namespace Varollo.MyTween
{
    public interface IMyTweenEffect<T>
    {
        struct EffectParameters
        {
            public T StartValue;
            public T TargetValue;
            public float Duration;
            public TweeningFunctions.TweenType TweenType;
            public Action OnFinishTweeningCallback;
        }

        Queue<EffectParameters> EffectQueue { get; }

        bool IsLocked { get; }

        T ResultValue { get; }

        T ChangeInValue(T startValue, T targetValue);

        float ElapsedTime(float startTime);

        IEnumerator ExecuteEffect(T startValue, T targetValue, float duration, TweeningFunctions.TweenType tweenType = TweeningFunctions.TweenType.QuadraticInOut, Action onFinishTweeningCallback = null);
    }
}