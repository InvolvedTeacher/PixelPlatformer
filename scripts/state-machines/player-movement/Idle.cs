using Game.Characters;
using Godot;

namespace Game.StateMachines.PlayerMovement;

public class Idle : State
{
	public override void Enter()
	{
		((Player)_state_machine).PlayAnimation("idle");
	}

	public override void PhysicsProcess(double delta)
	{
		// Handle Fall.
		if (!((Player)_state_machine).IsOnFloor())
		{
			_state_machine.TransitionTo(new Fall());
			return;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump"))
		{
			_state_machine.TransitionTo(new Jump());
			return;
		}

		// Handle Run.
		float direction = Input.GetAxis("move_left", "move_right");
		if (direction != 0)
		{
			_state_machine.TransitionTo(new Run());
			return;
		}
	}
}
