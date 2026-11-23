using Godot;

namespace Game.Characters;

public partial class Player : CharacterBody2D
{
    public const float Speed = 150.0f;
    public const float JumpVelocity = -300.0f;
    private AnimatedSprite2D _sprite;

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("Sprite");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        }

        // Change player animations according to their movement
        if (IsOnFloor())
        {
            if (velocity.X == 0)
                _sprite.Play("idle");
            else
                _sprite.Play("run");
        }
        else
        {
            if (velocity.Y < 0)
                _sprite.Play("jump");
            else
                _sprite.Play("fall");
        }
        if (velocity.X != 0)
        {
            if (velocity.X < 0)
                _sprite.FlipH = true;
            else
                _sprite.FlipH = false;
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
