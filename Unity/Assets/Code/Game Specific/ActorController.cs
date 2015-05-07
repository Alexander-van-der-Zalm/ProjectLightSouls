using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhysicsController),typeof(PlayerData))]
public class ActorController : MonoBehaviour 
{
    private GameState gs;
    private PlayerData pd;
    private PhysicsController pc;
    private Vector2 lastInputDirection;

	// Use this for initialization
	void Start () 
    {
        pc = GetComponent<PhysicsController>();
        gs = FindObjectOfType<GameState>();
        pd = GetComponent<PlayerData>();
	}
	
    public void Move(float horizontal, float vertical)
    {
        pc.Input = new Vector2(horizontal, vertical);
    }

    public void Dodge(Vector2 dir)
    {
        pc.Dodge(dir, pc.DodgeDistance);
        // Change color temp
    }

    public void Heal()
    {
        Debug.Log("Heal");
    }

    public void Scan()
    {
        Debug.Log("Scan");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        gs.PlayerHit(pd);

        Debug.Log("Collision!");
    }
}
