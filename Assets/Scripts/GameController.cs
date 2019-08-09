using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public int numCheckPoint, numLapPlayer, lapsLimit;
	public bool playerLapStarted, playerWins;
	public float playerTime, lastTimePlayer, bestTimePlayer;

	[Space (20)]
	public bool EnemyVars;
	//	[Space (20)]

	public int numCheckPointEnemy, numLapEnemy, lapsLimitEnemy;
	public bool enemyLapStarted, enemyWins;
	public float enemyTime, lastTimeEnemy, bestTimeEnemy;

	void Start () {
		Time.timeScale = 1;

		numLapPlayer = 0;
		playerTime = 0f;
		playerWins = false;

		numLapEnemy = 0;
		enemyTime = 0f;
		enemyWins = false;

	}

	void Update () {
		PlayerLapsCounter ();
		EnemyLapsCounter ();
	}

	void PlayerLapsCounter () {
		if (playerLapStarted) {
			playerTime += Time.deltaTime;
		}
	}
	void EnemyLapsCounter () {
		if (playerLapStarted) {
			playerTime += Time.deltaTime;
		}
	}
}