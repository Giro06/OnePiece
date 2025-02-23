using _Scripts.EventBus.Interface;

namespace Giroo.Core
{
    public class CoreEvents
    {
        public struct GameInitialized : IEvent
        {
        }

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
    }
}