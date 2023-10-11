using UnityEngine;

public class PlayerStateMachine : StateMachine
{
	public Player Player { get; }

	public PlayerIdleState IdleState { get; }
	public PlayerWalkState WalkState { get; }
	public PlayerRunState RunState { get; }
	public PlayerJumpState JumpState { get; }
	public PlayerFallState FallState { get; }
 
	public Vector2 MovementInput { get; set; }
	public float MovementSpeed { get; private set; }
	public float RotationDamping { get; private set; }
	public float MovementSpeedModifier { get; set; } = 1f;

	public float JumpForce { get; set; }

	public Transform MainCameraTransform { get; set; }

	public PlayerStateMachine(Player player)
	{
		this.Player = player;

		IdleState = new PlayerIdleState(this);
		WalkState = new PlayerWalkState(this);
		RunState = new PlayerRunState(this);
		JumpState = new PlayerJumpState(this);
		FallState = new PlayerFallState(this);

		MainCameraTransform = Camera.main.transform;

		MovementSpeed = player.Data.GroundedData.BaseSpeed;
		RotationDamping = player.Data.GroundedData.BaseRotationDamping;
	}
}