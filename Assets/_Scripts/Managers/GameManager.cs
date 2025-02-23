using _Scripts.EventBus;
using Giroo.Core;

namespace _Scripts.Managers
{
    public class GameManager : IInitializable, IDisposable
    {
        private GameState _gameState;

        EventBinding<CoreEvents.LevelLoading> _levelLoadingEventBinding;
        EventBinding<CoreEvents.LevelLoaded> _levelLoadedEventBinding;
        
        
        public void Initialize()
        {
            _levelLoadingEventBinding = new EventBinding<CoreEvents.LevelLoading>(OnLevelLoading);
            EventBus<CoreEvents.LevelLoading>.Subscribe(_levelLoadingEventBinding);

            _levelLoadedEventBinding = new EventBinding<CoreEvents.LevelLoaded>(OnLevelLoaded);
            EventBus<CoreEvents.LevelLoaded>.Subscribe(_levelLoadedEventBinding);
        }

        private void OnLevelSuccess()
        {
            ChangeGameState(GameState.Success);
            EventBus<CoreEvents.LevelSuccess>.Publish(new CoreEvents.LevelSuccess());
        }

        private void OnLevelFail(CoreEvents.LevelFail obj)
        {
            ChangeGameState(GameState.Fail);
            EventBus<CoreEvents.LevelFail>.Publish(new CoreEvents.LevelFail());
        }

        private void OnLevelStart(CoreEvents.LevelStart obj)
        {
            ChangeGameState(GameState.Running);
            EventBus<CoreEvents.LevelStart>.Publish(new CoreEvents.LevelStart());
        }

        private void OnLevelLoaded(CoreEvents.LevelLoaded obj)
        {
            ChangeGameState(GameState.WaitForStart);
        }

        private void OnLevelLoading(CoreEvents.LevelLoading obj)
        {
            ChangeGameState(GameState.Loading);
        }

        public void Dispose()
        {
        }

        private void ChangeGameState(GameState gameState)
        {
            _gameState = gameState;
        }

        public GameState GetGameState()
        {
            return _gameState;
        }
    }

    public enum GameState
    {
        Loading,
        WaitForStart,
        Running,
        Success,
        Fail
    }
}