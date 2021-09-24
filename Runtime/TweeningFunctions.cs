/* TWEENING FUNCTIONS TAKEN FROM:
 * https://github.com/danro/jquery-easing/blob/master/jquery.easing.js#L36
 * Adapted by Varollo
*/

using System;
using UnityEngine;

namespace Varollo.MyTween
{
    public static class TweeningFunctions
    {
        public enum TweenType
        {
            QuadraticIn,
            QuadraticOut,
            QuadraticInOut,
            CubicIn,
            CubicOut,
            CubicInOut,
            QuarticIn,
            QuarticOut,
            QuarticInOut,
            SinusoidalIn,
            SinusoidalOut,
            SinusoidalInOut,
            CircularIn,
            CircularOut,
            CircularInOut,
            ElasticIn,
            ElasticOut,
            ElasticInOut,
            BackIn,
            BackOut,
            BackInOut,
            BounceIn,
            BounceOut,
            BounceInOut
        }

        public static float Tween(float currentTime, float startValue, float changeInValue, float duration, TweenType tweenType = TweenType.QuadraticInOut)
        {
            return (float)typeof(TweeningFunctions).GetMethod(tweenType.ToString()).Invoke(null, new object[] { currentTime, startValue, changeInValue, duration });
        }

        #region Simple
        public static float Linear(float k)
        {
            return k;
        }
        #endregion

        #region Quadratic
        public static float QuadraticIn(float currentTime, float startValue, float changeInValue, float duration)
        {
            return changeInValue * (currentTime /= duration) * currentTime + startValue;
        }

        public static float QuadraticOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            return -changeInValue * (currentTime /= duration) * (currentTime - 2) + startValue;
        }

        public static float QuadraticInOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            if ((currentTime /= duration / 2) < 1) return changeInValue / 2 * currentTime * currentTime + startValue;
            return -changeInValue / 2 * ((--currentTime) * (currentTime - 2) - 1) + startValue;
        }
        #endregion

        #region Cubic
        public static float CubicIn(float currentTime, float startValue, float changeInValue, float duration)
        {
            return changeInValue * (currentTime /= duration) * currentTime * currentTime + startValue;
        }

        public static float CubicOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            return changeInValue * ((currentTime = currentTime / duration - 1) * currentTime * currentTime + 1) + startValue;
        }

        public static float CubicInOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            if ((currentTime /= duration / 2) < 1) return changeInValue / 2 * currentTime * currentTime * currentTime + startValue;
            return changeInValue / 2 * ((currentTime -= 2) * currentTime * currentTime + 2) + startValue;
        }
        #endregion

        #region Quartic
        public static float QuarticIn(float currentTime, float startValue, float changeInValue, float duration)
        {
            return changeInValue * (currentTime /= duration) * currentTime * currentTime * currentTime + startValue;
        }

        public static float QuarticOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            return -changeInValue * ((currentTime = currentTime / duration - 1) * currentTime * currentTime * currentTime - 1) + startValue;
        }

        public static float QuarticInOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            if ((currentTime /= duration / 2) < 1) return changeInValue / 2 * currentTime * currentTime * currentTime * currentTime + startValue;
            return -changeInValue / 2 * ((currentTime -= 2) * currentTime * currentTime * currentTime - 2) + startValue;
        }
        #endregion

        #region Quintic
        public static float QuinticIn(float currentTime, float startValue, float changeInValue, float duration)
        {
            return changeInValue * (currentTime /= duration) * currentTime * currentTime * currentTime * currentTime + startValue;
        }

        public static float QuinticOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            return changeInValue * ((currentTime = currentTime / duration - 1) * currentTime * currentTime * currentTime * currentTime + 1) + startValue;
        }

        public static float QuinticInOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            if ((currentTime /= duration / 2) < 1) return changeInValue / 2 * currentTime * currentTime * currentTime * currentTime * currentTime + startValue;
            return changeInValue / 2 * ((currentTime -= 2) * currentTime * currentTime * currentTime * currentTime + 2) + startValue;
        }
        #endregion

        #region Sinusoidal
        public static float SinusoidalIn(float currentTime, float startValue, float changeInValue, float duration)
        {
            return -changeInValue * Mathf.Cos(currentTime / duration * (Mathf.PI / 2)) + changeInValue + startValue;
        }

        public static float SinusoidalOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            return changeInValue * Mathf.Sin(currentTime / duration * (Mathf.PI / 2)) + startValue;
        }

        public static float SinusoidalInOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            return -changeInValue / 2 * (Mathf.Cos(Mathf.PI * currentTime / duration) - 1) + startValue;
        }
        #endregion

        #region Exponential
        public static float ExponentialIn(float currentTime, float startValue, float changeInValue, float duration)
        {
            return (currentTime == 0) ? startValue : changeInValue * Mathf.Pow(2, 10 * (currentTime / duration - 1)) + startValue;
        }

        public static float ExponentialOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            return (currentTime == duration) ? startValue + changeInValue : changeInValue * (-Mathf.Pow(2, -10 * currentTime / duration) + 1) + startValue;
        }

        public static float ExponentialInOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            if (currentTime == 0) return startValue;
            if (currentTime == duration) return startValue + changeInValue;
            if ((currentTime /= duration / 2) < 1) return changeInValue / 2 * Mathf.Pow(2, 10 * (currentTime - 1)) + startValue;
            return changeInValue / 2 * (-Mathf.Pow(2, -10 * --currentTime) + 2) + startValue;
        }
        #endregion

        #region Circular
        public static float CircularIn(float currentTime, float startValue, float changeInValue, float duration)
        {
            return -changeInValue * (Mathf.Sqrt(1 - (currentTime /= duration) * currentTime) - 1) + startValue;
        }

        public static float CircularOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            return changeInValue * Mathf.Sqrt(1 - (currentTime = currentTime / duration - 1) * currentTime) + startValue;
        }

        public static float CircularInOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            if ((currentTime /= duration / 2) < 1) return -changeInValue / 2 * (Mathf.Sqrt(1 - currentTime * currentTime) - 1) + startValue;
            return changeInValue / 2 * (Mathf.Sqrt(1 - (currentTime -= 2) * currentTime) + 1) + startValue;
        }
        #endregion

        #region Elastic
        public static float ElasticIn(float currentTime, float startValue, float changeInValue, float duration)
        {
            var s = 1.70158f; var p = 0f; var a = changeInValue;
            if (currentTime == 0) return startValue; if ((currentTime /= duration) == 1) return startValue + changeInValue; if (p == 0) p = duration * .3f;
            if (a < Mathf.Abs(changeInValue)) { a = changeInValue; s = p / 4; }
            else s = p / (2 * Mathf.PI) * a == 0 ? 0 : Mathf.Asin(changeInValue / a);
            return -(a * Mathf.Pow(2, 10 * (currentTime -= 1)) * Mathf.Sin((currentTime * duration - s) * (2 * Mathf.PI) / p)) + startValue;
        }

        public static float ElasticOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            var s = 1.70158f; var p = 0f; var a = changeInValue;
            if (currentTime == 0) return startValue; if ((currentTime /= duration) == 1) return startValue + changeInValue; if (p == 0) p = duration * .3f;
            if (a < Mathf.Abs(changeInValue)) { a = changeInValue; s = p / 4; }
            else s = p / (2 * Mathf.PI) * a == 0 ? 0 : Mathf.Asin(changeInValue / a);
            return a * Mathf.Pow(2, -10 * currentTime) * Mathf.Sin((currentTime * duration - s) * (2 * Mathf.PI) / p) + changeInValue + startValue;
        }

        public static float ElasticInOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            var s = 1.70158f; var p = 0f; var a = changeInValue;
            if (currentTime == 0) return startValue; if ((currentTime /= duration / 2) == 2) return startValue + changeInValue; if (p == 0) p = duration * (.3f * 1.5f);
            if (a < Mathf.Abs(changeInValue)) { a = changeInValue; s = p / 4; }
            else s = p / (2 * Mathf.PI) * a == 0 ? 0 : Mathf.Asin(changeInValue / a);
            if (currentTime < 1) return -.5f * (a * Mathf.Pow(2, 10 * (currentTime -= 1)) * Mathf.Sin((currentTime * duration - s) * (2 * Mathf.PI) / p)) + startValue;
            return a * Mathf.Pow(2, -10 * (currentTime -= 1)) * Mathf.Sin((currentTime * duration - s) * (2 * Mathf.PI) / p) * .5f + changeInValue + startValue;
        }
        #endregion

        #region Back

        public static float BackIn(float currentTime, float startValue, float changeInValue, float duration)
        {
            float s = 1.70158f;
            return changeInValue * (currentTime /= duration) * currentTime * ((s + 1) * currentTime - s) + startValue;
        }

        public static float BackOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            float s = 1.70158f;
            return changeInValue * ((currentTime = currentTime / duration - 1) * currentTime * ((s + 1) * currentTime + s) + 1) + startValue;
        }

        public static float BackInOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            float s = 1.70158f;
            if ((currentTime /= duration / 2) < 1) return changeInValue / 2 * (currentTime * currentTime * (((s *= (1.525f)) + 1) * currentTime - s)) + startValue;
            return changeInValue / 2 * ((currentTime -= 2) * currentTime * (((s *= (1.525f)) + 1) * currentTime + s) + 2) + startValue;
        }
        #endregion

        #region Bounce
        public static float BounceIn(float currentTime, float startValue, float changeInValue, float duration)
        {
            return changeInValue - BounceOut(duration - currentTime, 0, changeInValue, duration) + startValue;
        }

        public static float BounceOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            if ((currentTime /= duration) < (1f / 2.75f))
            {
                return changeInValue * (7.5625f * currentTime * currentTime) + startValue;
            }
            else if (currentTime < (2f / 2.75f))
            {
                return changeInValue * (7.5625f * (currentTime -= (1.5f / 2.75f)) * currentTime + .75f) + startValue;
            }
            else if (currentTime < (2.5f / 2.75f))
            {
                return changeInValue * (7.5625f * (currentTime -= (2.25f / 2.75f)) * currentTime + .9375f) + startValue;
            }
            else
            {
                return changeInValue * (7.5625f * (currentTime -= (2.625f / 2.75f)) * currentTime + .984375f) + startValue;
            }
        }

        public static float BounceInOut(float currentTime, float startValue, float changeInValue, float duration)
        {
            if (currentTime < duration / 2) return BounceIn(currentTime * 2, 0, changeInValue, duration) * 0.5f + startValue;
            return BounceOut(currentTime * 2 - duration, 0, changeInValue, duration) * 0.5f + changeInValue * 0.5f + startValue;
        }
        #endregion
    }
}