using UnityEngine;
using System.Collections;
using System;

public class UIManager : MonoBehaviour 
{
    public enum GameStates
    {
        Win, GameOver, Game, StartGame
    }

    public GameObject Win, GameOver, Game, StartGame;

    private GameObject current;

    public void ChangeState(GameStates newState)
    {
        GameObject newScreen = GetState(newState);

        if(current != null)
            current.SetActive(false);

        current = newScreen;
        current.SetActive(true);

        Debug.Log(current.name);
    }

    private GameObject GetState(GameStates newState)
    {
        switch(newState)
        {
            case GameStates.Game:
                return Game;
            case GameStates.GameOver:
                return GameOver;
            case GameStates.StartGame:
                return StartGame;
            case GameStates.Win:
                return Win;
            default:
                return Game;
        }
    }


    // Use this for initialization
    void Start () 
    {
        ChangeState(GameStates.StartGame);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
