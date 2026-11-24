namespace Game.StateMachines;

public abstract class State
{
	protected IStateMachine _state_machine = null;

	public virtual void Enter() { }

	public virtual void Exit() { }

	public virtual void Process(double delta) { }

	public virtual void PhysicsProcess(double delta) { }

	public void SetStateMachine(IStateMachine new_state_machine)
	{
		_state_machine = new_state_machine;
	}
}
