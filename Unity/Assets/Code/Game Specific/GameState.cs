using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
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

    }
}
