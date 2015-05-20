﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIManager))]
public class GameState : MonoBehaviour 
{
    private UIManager ui;

    public RespawnRegister RespawnRegister;

	// Use this for initialization
	void Start ()
    {
        ui = GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ExitGame()
    {
        Debug.Log("Exit game");
    }

    public void RestartGame()
    {
        Debug.Log("Restart game");
        RespawnRegister.RespawnAll();
    }

    public void StartGame()
    {
        Debug.Log("Start game");
    }

    public void PlayerHit(PlayerData player)
    {
        // Play sound

        // Fade in black

        // Put in the game over ui
        ui.ChangeState(UIManager.GameStates.GameOver);

    }
}
