using System;
using Giroo.Core;
using IDisposable = Giroo.Core.IDisposable;

namespace _Scripts.Managers
{
    public class AdManager : IInitializable, IDisposable
    {
        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        public bool ShowRewardedAd(string from, Action OnSuccess, Action OnFail)
        {
            OnSuccess.Invoke();
            return true;
        }
    }
}