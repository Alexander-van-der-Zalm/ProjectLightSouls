using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIManager))]
public class GameState : MonoBehaviour 
{
    private UIManager ui;

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
    }

    public void StartGame()
    {
        Debug.Log("Start game");
    }

    public void PlayerHit(PlayerData player)
    {
        ui.ChangeState(UIManager.GameStates.GameOver);
    }
}
