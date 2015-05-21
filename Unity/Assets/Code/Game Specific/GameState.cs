using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(UIManager))]
public class GameState : MonoBehaviour 
{
    private UIManager ui;
    private InputController playerInput;

    public RespawnRegister RespawnRegister;

	// Use this for initialization
	void Start ()
    {
        ui = GetComponent<UIManager>();
        playerInput = GameObject.FindObjectOfType<InputController>();
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
            Application.OpenURL(webplayerQuitURL);
        #else
            Application.Quit();
        #endif
    }

    public void RestartGame()
    {
        Debug.Log("Restart game");
        RespawnRegister.RespawnAll();

        activatePlayer();
    }

    private void activatePlayer()
    {
        playerInput.enabled = true;
        ActorController actor = playerInput.GetComponent<ActorController>();
        if (actor != null)
            actor.RestartActor();
    }

    public void StartGame()
    {
        Debug.Log("Start game");
        activatePlayer();
    }

    public void PlayerHit(PlayerData player)
    {


        // Check for death:

        // No Death:
            // Hit sound

            // Screen Shake

        // Death:
            // Disable player input
            player.GetComponent<InputController>().enabled = false;
            // Play sound

            // Screen Shake

            // Fade in black

            // Put in the game over ui
            ui.ChangeState(UIManager.GameStates.GameOver);

    }
}
