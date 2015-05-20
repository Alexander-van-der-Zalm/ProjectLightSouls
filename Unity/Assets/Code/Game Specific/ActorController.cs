using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(PhysicsController),typeof(PlayerData))]
public class ActorController : MonoBehaviour 
{
    private GameState gs;
    private PlayerData pd;
    private PhysicsController pc;
    private Animator anim;
    private Rigidbody2D rb;

    private string velStr = "Velocity";
    private string velNStr = "VelocityNormalized";
    private string attackTriggerStr = "AttackTrigger";
    private string attackStr = "Attack";
    private string DodgeTriggerStr = "DodgeTrigger";
    private string DodgeStr = "Dodge";

    // Use this for initialization
    void Start () 
    {
        pc = GetComponent<PhysicsController>();
        gs = FindObjectOfType<GameState>();
        pd = GetComponent<PlayerData>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
    public void FixedUpdate()
    {
        anim.SetFloat(velStr, rb.velocity.magnitude);
        anim.SetFloat(velNStr, rb.velocity.magnitude / pc.MovementMaxSpeed);
    }

    public void Move(float horizontal, float vertical)
    {
        pc.Input = new Vector2(horizontal, vertical);
    }

    public void Dodge(Vector2 dir)
    {
        pc.Dodge(dir, pc.DodgeDistance);
        // Change color temp
        StartCoroutine(DodgeRoutine());
    }

    private IEnumerator DodgeRoutine()
    {
        anim.SetTrigger(DodgeTriggerStr);
        anim.SetBool(DodgeStr, true);
        while (pc.Airborne)
        {
            yield return null;
        }

        anim.SetBool(DodgeStr, false);
    }

    public void RestartActor()
    {
        anim.Play("Idle", 0, 0);
        pc.RestartPhysics();
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
