using System;
using System.Collections;
using System.Collections.Generic;

namespace Varollo.MyTween
{
    /// <summary>
    /// Implement this interface to create a Tweening effect of a Type [T]
    /// </summary>
    /// <typeparam name="T">Type to be tweened</typeparam>
    public interface IMyTweenEffect<T>
    {
        /// <summary>
        /// All queued Tween Effects in this class
        /// </summary>
        Queue<TweenEffectParameters<T>> EffectQueue { get; }

        /// <summary>
        /// Is tweening right now?
        /// </summary>
        bool IsTweening { get; }

        /// <summary>
        /// Access this propperty to read the tweened value
        /// </summary>
        T ResultValue { get; }

        /// <summary>
        /// The change in value from start to target
        /// </summary>
        T ChangeInValue(T startValue, T targetValue);

        /// <summary>
        /// Time since the begining of the tween
        /// </summary>
        float ElapsedTime(float startTime);

        /// <summary>
        /// Courotine to tween a value of Type [T]
        /// </summary>
        IEnumerator ExecuteEffect(T startValue, T targetValue, float duration, TweeningFunctions.TweenType tweenType = TweeningFunctions.TweenType.QuadraticInOut, Action onFinishTweeningCallback = null);
    }
}