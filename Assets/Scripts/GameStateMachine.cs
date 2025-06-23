using DI;
using Enums;
using Managers;

public class GameStateMachine
{
    private GameState _currentState = GameState.None;
    private readonly EventManager _eventManager;

    [Inject]
    public GameStateMachine(EventManager eventManager)
    {
        _eventManager = eventManager;
    }

    public void ChangeState(GameState state)
    {
        _currentState = state;
        _eventManager.PublishGameStateChanged(_currentState);
    }
}
