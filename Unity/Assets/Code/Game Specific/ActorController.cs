using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhysicsController))]
public class ActorController : MonoBehaviour 
{
    private PhysicsController pc;
    private Vector2 lastInputDirection;

	// Use this for initialization
	void Start () 
    {
        pc = GetComponent<PhysicsController>();
	}
	
    public void Move(float horizontal, float vertical)
    {
        pc.Input = new Vector2(horizontal, vertical);
    }

    public void Dodge(Vector2 dir)
    {
        pc.Dodge(dir);
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
