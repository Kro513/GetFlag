using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
	[field: SerializeField] private GameManager instance = null;

	[SerializeField] private GameObject gameOverUI;

	[SerializeField] private GameObject gameClearUI;

	private string _PlayerPrefab;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);

		gameOverUI.SetActive(false);
		gameClearUI.SetActive(false);
	}
	public void OpenGameOverUI()
	{
		gameOverUI.SetActive(true);
	}

	public void CloseGameOverUI()
	{
		gameOverUI.SetActive(false);
	}

	public void OpenGameClearUI()
	{
		gameClearUI.SetActive(true);
	}

	public void CloseGameClearUI()
	{
		gameClearUI.SetActive(false);
	}
}