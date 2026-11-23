using System;

namespace Game.StateMachines;

public interface IStateMachine
{
	protected State Curr_state { get; set; }

	public void TransitionTo(State new_state)
	{
		Console.WriteLine($"{GetType().Name}: Transition to {new_state.GetType().Name}.");

		Curr_state.Exit();
		Curr_state = new_state;
		Curr_state.SetStateMachine(this);
		Curr_state.Enter();
	}

	public void Process(double delta)
	{
		Curr_state.Process(delta);
	}

	public void PhysicsProcess(double delta)
	{
		Curr_state.PhysicsProcess(delta);
	}
}
