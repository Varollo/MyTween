using System;

namespace Varollo.MyTween
{
    /// <summary>
    /// Struct containing the parameters required to do a Tween Effect
    /// </summary>
    public struct TweenEffectParameters<T>
    {
        public T StartValue;
        public T TargetValue;
        public float Duration;
        public TweeningFunctions.TweenType TweenType;
        public Action OnFinishTweeningCallback;
    }
}