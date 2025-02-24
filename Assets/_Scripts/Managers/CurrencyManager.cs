using _Scripts.EventBus;
using Giro.Core.Enum;
using Giroo.Core;
using UnityEngine;

namespace _Scripts.Managers
{
    public class CurrencyManager : IInitializable, IDisposable
    {
        EventBinding<CoreEvents.LevelComplete> _levelCompleteEventBinding;
        EventBinding<CoreEvents.AddCurrencyEvent> _addCurrencyEventBinding;
        EventBinding<CoreEvents.SpendCurrencyEvent> _spendCurrencyEventBinding;
        EventBinding<CoreEvents.SetCurrencyEvent> _setCurrencyEventBinding;
        EventBinding<CoreEvents.LevelLoaded> _levelLoadedEventBinding;

        private const string _currencySaveKey = "Currency";

        public void Initialize()
        {
            _levelCompleteEventBinding = new EventBinding<CoreEvents.LevelComplete>(OnLevelComplete);
            _addCurrencyEventBinding = new EventBinding<CoreEvents.AddCurrencyEvent>(OnAddCurrency);
            _spendCurrencyEventBinding = new EventBinding<CoreEvents.SpendCurrencyEvent>(OnSpendCurrency);
            _setCurrencyEventBinding = new EventBinding<CoreEvents.SetCurrencyEvent>(OnSetCurrency);
            _levelLoadedEventBinding = new EventBinding<CoreEvents.LevelLoaded>(OnLevelLoaded);

            EventBus<CoreEvents.LevelComplete>.Subscribe(_levelCompleteEventBinding);
            EventBus<CoreEvents.AddCurrencyEvent>.Subscribe(_addCurrencyEventBinding);
            EventBus<CoreEvents.SpendCurrencyEvent>.Subscribe(_spendCurrencyEventBinding);
            EventBus<CoreEvents.SetCurrencyEvent>.Subscribe(_setCurrencyEventBinding);
            EventBus<CoreEvents.LevelLoaded>.Subscribe(_levelLoadedEventBinding);

            CurrencyAmountChanged(CurrencyType.Coin, 0, GetCurrencyAmount(CurrencyType.Coin));
        }

        public void Dispose()
        {
            EventBus<CoreEvents.LevelComplete>.Unsubscribe(_levelCompleteEventBinding);
            EventBus<CoreEvents.AddCurrencyEvent>.Unsubscribe(_addCurrencyEventBinding);
            EventBus<CoreEvents.SpendCurrencyEvent>.Unsubscribe(_spendCurrencyEventBinding);
            EventBus<CoreEvents.SetCurrencyEvent>.Unsubscribe(_setCurrencyEventBinding);
            EventBus<CoreEvents.LevelLoaded>.Unsubscribe(_levelLoadedEventBinding);
        }

        private void OnLevelLoaded(CoreEvents.LevelLoaded levelLoadedEvent)
        {
        }


        private void OnAddCurrency(CoreEvents.AddCurrencyEvent addCurrencyEvent)
        {
            var oldAmount = GetCurrencyAmount(addCurrencyEvent.currencyType);
            AddCurrencyAmount(addCurrencyEvent.currencyType, addCurrencyEvent.amount);
            var newAmount = GetCurrencyAmount(addCurrencyEvent.currencyType);
            CurrencyAmountChanged(addCurrencyEvent.currencyType, oldAmount, newAmount);
        }

        private void OnSpendCurrency(CoreEvents.SpendCurrencyEvent spendCurrencyEvent)
        {
            var oldAmount = GetCurrencyAmount(spendCurrencyEvent.currencyType);
            SpendCurrencyAmount(spendCurrencyEvent.currencyType, spendCurrencyEvent.amount);
            var newAmount = GetCurrencyAmount(spendCurrencyEvent.currencyType);
            CurrencyAmountChanged(spendCurrencyEvent.currencyType, oldAmount, newAmount);
        }

        private void OnSetCurrency(CoreEvents.SetCurrencyEvent setCurrencyEvent)
        {
            var oldAmount = GetCurrencyAmount(setCurrencyEvent.currencyType);
            SetCurrencyAmount(setCurrencyEvent.currencyType, setCurrencyEvent.amount);
            var newAmount = GetCurrencyAmount(setCurrencyEvent.currencyType);
            CurrencyAmountChanged(setCurrencyEvent.currencyType, oldAmount, newAmount);
        }

        private void CurrencyAmountChanged(CurrencyType currencyType, int oldAmount, int newAmount)
        {
            var dif = newAmount - oldAmount;
            EventBus<CoreEvents.CurrencyAmountChangedEvent>.Publish(new CoreEvents.CurrencyAmountChangedEvent
            {
                currencyType = currencyType,
                amount = newAmount,
                oldAmount = oldAmount,
                dif = dif
            });
        }


        private void OnLevelComplete(CoreEvents.LevelComplete levelCompleteEvent)
        {
            var oldAmount = GetCurrencyAmount(CurrencyType.Coin);
            AddCurrencyAmount(CurrencyType.Coin, Game.Settings.levelCompleteEarnAmount);
            var newAmount = GetCurrencyAmount(CurrencyType.Coin);

            CurrencyAmountChanged(CurrencyType.Coin, oldAmount, newAmount);
        }

        public int GetCurrencyAmount(CurrencyType currencyType)
        {
            return PlayerPrefs.GetInt(_currencySaveKey + currencyType.ToString(), 0);
        }

        public void SetCurrencyAmount(CurrencyType currencyType, int amount)
        {
            PlayerPrefs.SetInt(_currencySaveKey + currencyType.ToString(), amount);
        }

        public void AddCurrencyAmount(CurrencyType currencyType, int amount)
        {
            var oldAmount = GetCurrencyAmount(currencyType);
            SetCurrencyAmount(currencyType, oldAmount + amount);
        }

        public void SpendCurrencyAmount(CurrencyType currencyType, int amount)
        {
            var oldAmount = GetCurrencyAmount(currencyType);
            SetCurrencyAmount(currencyType, oldAmount - amount);
        }

        public bool HasEnoughCurrency(CurrencyType currencyType, int amount)
        {
            return GetCurrencyAmount(currencyType) >= amount;
        }
    }
}