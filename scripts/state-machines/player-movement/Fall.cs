using Game.Characters;
using Godot;

namespace Game.StateMachines.PlayerMovement;

public class Fall : State
{
    public override void Enter()
    {
        ((Player)_state_machine).PlayAnimation("fall");
    }

    public override void PhysicsProcess(double delta)
    {
        Vector2 velocity = ((Player)_state_machine).Velocity;

        // Handle Idle, Run
        if (((Player)_state_machine).IsOnFloor())
        {
            if (velocity.X != 0)
                _state_machine.TransitionTo(new Run());
            else
                _state_machine.TransitionTo(new Idle());
            return;
        }

        // Add the gravity.
        velocity += ((Player)_state_machine).GetGravity() * (float)delta;

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
    }
}
