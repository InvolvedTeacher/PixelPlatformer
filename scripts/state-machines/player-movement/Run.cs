using Game.Characters;
using Godot;

namespace Game.StateMachines.PlayerMovement;

public class Run : State
{
    public override void Enter()
    {
        ((Player)_state_machine).PlayAnimation("run");
    }

    public override void PhysicsProcess(double delta)
    {
        Vector2 velocity = ((Player)_state_machine).Velocity;

        // Get the input direction and handle the movement/deceleration.
        float direction = Input.GetAxis("move_left", "move_right");
        if (direction != 0)
        {
            velocity.X = direction * ((Player)_state_machine).Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(
                ((Player)_state_machine).Velocity.X,
                0,
                ((Player)_state_machine).Speed
            );
        }

        ((Player)_state_machine).Velocity = velocity;

        // Handle Jump.
        if (Input.IsActionJustPressed("jump"))
        {
            _state_machine.TransitionTo(new Jump());
            return;
        }

        // Handle Fall.
        if (!((Player)_state_machine).IsOnFloor())
        {
            _state_machine.TransitionTo(new Fall());
            return;
        }

        // Handle Idle.
        if (velocity.X == 0)
        {
            _state_machine.TransitionTo(new Idle());
            return;
        }
    }
}
