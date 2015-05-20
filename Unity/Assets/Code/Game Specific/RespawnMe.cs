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
        // Disable/reset rigidbody etc.
        if(rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
        }
        if (Method == RespawnMethod.Origin)
            transform.position = m_origin;
        else
            transform.position = RespawnPoint.transform.position;

        switch(OrientationMethod)
        {
            default:
                transform.rotation = m_orientation;
                return;
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
