using Game.StateMachines;
using Game.StateMachines.PlayerMovement;
using Godot;

namespace Game.Characters;

public partial class Player : CharacterBody2D, IStateMachine
{
    public float Speed { get; } = 150.0f;
    public float JumpVelocity { get; } = -300.0f;
    private AnimatedSprite2D _sprite;
    protected State Curr_state { get; set; }
    State IStateMachine.Curr_state
    {
        get => Curr_state;
        set => Curr_state = value;
    }

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("Sprite");
        Curr_state = new Idle();
        Curr_state.SetStateMachine(this);
        Curr_state.Enter();
    }

    public override void _PhysicsProcess(double delta)
    {
        Curr_state.PhysicsProcess(delta);

        if (Velocity.X != 0)
        {
            if (Velocity.X < 0)
                _sprite.FlipH = true;
            else
                _sprite.FlipH = false;
        }

        MoveAndSlide();
    }

    public void PlayAnimation(string animation)
    {
        _sprite.Play(animation);
    }
}
