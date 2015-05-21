using UnityEngine;
using System.Collections;

public class RespawnMe : MonoBehaviour
{
    public enum RespawnMethod
    {
        Origin,
        Transform
    }

    public enum Orientation
    {
        Original,
        FacingPlayer
    }

    public RespawnMethod Method;
    public Orientation OrientationMethod;

    public Transform RespawnPoint;

    public int Priority = 0;

    private Vector3 m_origin;
    private Quaternion m_orientation;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        // Save origin
        m_origin = transform.position;
        rb = GetComponent<Rigidbody2D>();
        m_orientation = transform.rotation;
	}
	
    public void Respawn()
    {
        // Reset animationcomponent
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            AnimatorResetter.ResetVariables(anim);
            AnimatorResetter.ResetStateToIdle(anim);
        }

        // Reset physicsController
        PhysicsController pc = GetComponent<PhysicsController>();
        if (pc != null)
        {
            pc.RestartPhysics();
        }

        // Reset Health
        HealthInfo hi = GetComponent<HealthInfo>();
        if(hi != null)
        {
            hi.ReBirth();
        }

        // Disable/reset rigidbody etc.
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
        }

        // Reset position
        if (Method == RespawnMethod.Origin)
            transform.position = m_origin;
        else
        {
            if(RespawnPoint != null)
                transform.position = RespawnPoint.position;
            else
                transform.position = m_origin;
        }
        
        // Reset orientation
        switch(OrientationMethod)
        {
            default:
                transform.rotation = m_orientation;
                break;
        }
    }

    // Register to gamestate/levelstate
    void OnEnable()
    {
        GameState gs = FindObjectOfType<GameState>();
        if(gs != null)
        {
            gs.RespawnRegister.Register(this);
        }
    }

    void OnDisable()
    {
        GameState gs = FindObjectOfType<GameState>();
        if (gs != null)
        {
            gs.RespawnRegister.UnRegister(this);
        }
    }
}
