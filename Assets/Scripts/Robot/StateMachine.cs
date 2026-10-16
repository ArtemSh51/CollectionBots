public class StateMachine
{
    private State _state;

    public void ChangeState(State state)
    {
        _state?.Exit();
        _state = state;
        _state.Enter();
    }

    public void Update()
    {
        _state?.Update();
    }
}
