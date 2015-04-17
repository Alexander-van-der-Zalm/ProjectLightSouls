using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhysicsController))]
public class ActorController : MonoBehaviour 
{
    private PhysicsController pc;

	// Use this for initialization
	void Start () 
    {
        pc = GetComponent<PhysicsController>();
	}
	
    public void Move(float horizontal, float vertical)
    {
        pc.Input = new Vector2(horizontal, vertical);
    }

    public void Dodge()
    {
        Debug.Log("Dodge");
    }

    public void Heal()
    {
        Debug.Log("Heal");
    }

    public void Scan()
    {
        Debug.Log("Scan");
    }
}
