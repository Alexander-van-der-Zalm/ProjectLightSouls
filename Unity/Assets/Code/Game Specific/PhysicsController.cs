using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsController : MonoBehaviour 
{
    private float m_accel;
    private float m_friction;

    public float MaxSpeed;
    public float TimeToMaxSpeed;

    public float RealTimeToMaxSpeed;
    public float Speed;

    public float calculatedMaxSpeed;
    public float calculatedTimeToMaxSpeed;

    public Vector2 Input;

    private Rigidbody2D rb;

    // for debug
    private float t0;

	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();

        
	}

    float velocityRounded;

	// Update is called once per frame
	void FixedUpdate () 
    {
	    //Normalize input
        Input.Normalize();

        calculatedMaxSpeed = m_friction != 0 ? m_accel / m_friction : -1;
        calculatedTimeToMaxSpeed = m_friction != 0 ? 2 / m_friction : -1;

        m_friction = TimeToMaxSpeed != 0 ? 2 / TimeToMaxSpeed : 0;
        m_accel = m_friction * MaxSpeed;

        float dt = Time.deltaTime;

        Vector2 v = rb.velocity;
        Vector2 a = Input * m_accel;
        Vector2 friction = v * m_friction;

        rb.velocity += dt * (a - friction);

        Speed = rb.velocity.magnitude;

        float order = 1;
        velocityRounded = Mathf.Round(v.magnitude * order);

        if(v.magnitude == 0)
        {
            t0 = Time.realtimeSinceStartup;
        }
        else if (velocityRounded == Mathf.Round(MaxSpeed * order)  && t0 != 0)
        {
            RealTimeToMaxSpeed = Time.realtimeSinceStartup - t0;
            t0 = 0;
        }
	}


}
