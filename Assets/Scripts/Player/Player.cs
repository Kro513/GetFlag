using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	[field: Header("References")]
	[field: SerializeField] public PlayerSO Data { get; private set; }

	[field: Header("Animations")]
	[field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

	public Rigidbody Rigidbody { get; private set; }
	public Animator Animator { get; private set; }
	public PlayerInput Input { get; private set; }
	public CharacterController Controller { get; private set; }
	public ForceReceiver ForceReceiver { get; private set; }

	private PlayerStateMachine stateMachine;

	[field: SerializeField] public GameManager GameManager;

	//[SerializeField] private TextMeshProUGUI DeathCount;
	//[SerializeField] private int currentDeathCount = 0;

	//public Transform[] point;


	private void Awake()
	{
		AnimationData.Initialize();

		Rigidbody = GetComponent<Rigidbody>();
		Animator = GetComponentInChildren<Animator>();
		Input = GetComponent<PlayerInput>();
		Controller = GetComponent<CharacterController>();
		ForceReceiver = GetComponent<ForceReceiver>();
		stateMachine = new PlayerStateMachine(this);
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		stateMachine.ChangeState(stateMachine.IdleState);
	}

	private void Update()
	{
		stateMachine.HandleInput();
		stateMachine.Update();
	}

	private void FixedUpdate()
	{
		stateMachine.PhysicsUpdate();
	}

	void OnDie()
	{
		Animator.SetTrigger("Die");
		enabled = false;

		StartCoroutine(RespawnCoroutine());
	}

	void OnTriggerEnter (Collider collidedObject)
	{
		switch (collidedObject.tag)
		{
			case "Die":
				OnDie();
				break;

			case "GameClear":
				GameClear();
				break;
		}
	}

	private void GameClear()
	{
		enabled = false;

		StartCoroutine (GameEndCoroutine());
	}

	public IEnumerator GameEndCoroutine()
	{
		GameManager.OpenGameClearUI();

		yield return new WaitForSeconds(5);

		GameManager.CloseGameClearUI();

		Cursor.lockState = CursorLockMode.None;

		SceneManager.LoadScene("StartScene");
	}

	//public IEnumerator RespawnCoroutine()
	//{
	//	DeathCount.text = (currentDeathCount + 1).ToString();
	//	GameManager.OpenGameOverUI();

	//	yield return new WaitForSeconds(5);

	//	GameManager.CloseGameOverUI();

	//	Respawn();

	//	enabled = true;
	//}

	//private void Respawn()
	//{
	//	point = GameObject.Find("SpawnPoint").GetComponents<Transform>();

	//	if(point.Length > 0)
	//	{

	//	}
	//	transform.position = respawnPosition;
	//}
	public IEnumerator RespawnCoroutine()
	{
		GameManager.OpenGameOverUI();

		yield return new WaitForSeconds(5);

		GameManager.CloseGameOverUI();

		SceneManager.LoadScene("MainScene");
	}
}