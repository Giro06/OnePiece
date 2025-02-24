using _Scripts.EventBus.Interface;
using Giro.Core.Enum;
using UnityEngine;

namespace Giroo.Core
{
    public class CoreEvents
    {
        public struct GameInitialized : IEvent
        {
        }

        #region LevelEvents

        public struct LevelLoading : IEvent
        {
        }

        public struct LevelLoaded : IEvent
        {
        }

        public struct LevelStart : IEvent
        {
        }

        public struct LevelSuccess : IEvent
        {
        }

        public struct LevelFail : IEvent
        {
        }

        public struct LevelComplete : IEvent
        {
        }

        public struct LevelRestart : IEvent
        {
        }

        #endregion

        #region CurrencyEvents

        public struct AddCurrencyEvent : IEvent
        {
            public int amount;
            public CurrencyType currencyType;

            public AddCurrencyEvent(CurrencyType currencyType, int amount)
            {
                this.currencyType = currencyType;
                this.amount = amount;
            }
        }
        
        public struct SpendCurrencyEvent : IEvent
        {
            public int amount;
            public CurrencyType currencyType;

            public SpendCurrencyEvent(CurrencyType currencyType, int amount)
            {
                this.currencyType = currencyType;
                this.amount = amount;
            }
        }
        
        public struct SetCurrencyEvent : IEvent
        {
            public int amount;
            public CurrencyType currencyType;

            public SetCurrencyEvent(CurrencyType currencyType, int amount)
            {
                this.currencyType = currencyType;
                this.amount = amount;
            }
        }
        
        public struct CurrencyAmountChangedEvent : IEvent
        {
            public int amount;
            public int dif;
            public int oldAmount;
            public CurrencyType currencyType;
        }
        
        #endregion

        #region FeelEvent

        public struct FeelEvent :IEvent
        {
            public Vector3 position;
            public Transform parent;
            public FeelType FeelType;

            public FeelEvent(FeelType feelType)
            {
                FeelType = feelType;
                position = Vector3.zero;
                parent = null;
            }

            public FeelEvent(Vector3 position, Transform parent, FeelType feelType)
            {
                this.position = position;
                this.parent = parent;
                FeelType = feelType;
            }
        }

        #endregion
    }
}