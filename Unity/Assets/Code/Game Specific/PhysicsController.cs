using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsController : MonoBehaviour 
{
    private float m_accel;
    private float m_friction;

    public float MovementMaxSpeed;
    public float TimeToMaxSpeed;

    public float DodgeMaxSpeed;
    public float DodgeDistance;

    public Vector2 Input;

    private Rigidbody2D rb;

    private Vector2 airSpeed;
    public bool Airborne = false;

    private float speed;

	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate () 
    {
	    //Normalize input
        Input.Normalize();

        recalculateAccelFriction(TimeToMaxSpeed, MovementMaxSpeed);
        
        float dt = Time.deltaTime;

        Vector2 v = rb.velocity;
        Vector2 a = Vector2.zero;
        Vector2 friction = Vector2.zero;

        if (!Airborne)
        {
            a = Input * m_accel;
            friction = v * m_friction;
            rb.velocity += dt * (a - friction);
        }
        else
            rb.velocity = airSpeed;

        speed = rb.velocity.magnitude;
    }

    private void recalculateAccelFriction(float timeToMaxSpeed, float maxSpeed)
    {
        // Inefficient recalculate each fram, but who cares
        m_friction = timeToMaxSpeed != 0 ? 2 / TimeToMaxSpeed : 0;
        m_accel = m_friction * maxSpeed;
    }

    Coroutine dodgeCR;

    public void Dodge(Vector2 direction, float distance)
    {
        float time = distance / DodgeMaxSpeed;

        dodgeCR = StartCoroutine(DodgeCR(direction, DodgeMaxSpeed, time));
    }

    public void StopDodge()
    {
        StopCoroutine(dodgeCR);
        Airborne = false;
    }

    private IEnumerator DodgeCR(Vector2 direction, float speed, float time)
    {
        Airborne = true;

        airSpeed = direction.normalized * speed;

        yield return new WaitForSeconds(time);

        Airborne = false;
    }

    #region Debug
    //// for debug
    //private float t0;
    //private float RealTimeToMaxSpeed;
    //private float Speed;
    // float velocityRounded;
    //private float calculatedMaxSpeed;
    //private float calculatedTimeToMaxSpeed;

    //private void DebugStuff()
    //{
    //    calculatedMaxSpeed = m_friction != 0 ? m_accel / m_friction : -1;
    //    calculatedTimeToMaxSpeed = m_friction != 0 ? 2 / m_friction : -1;

    //    Speed = rb.velocity.magnitude;

    //    float order = 1;
    //    velocityRounded = Mathf.Round(v.magnitude * order);

    //    if (v.magnitude == 0)
    //    {
    //        t0 = Time.realtimeSinceStartup;
    //    }
    //    else if (velocityRounded == Mathf.Round(MaxSpeed * order) && t0 != 0)
    //    {
    //        RealTimeToMaxSpeed = Time.realtimeSinceStartup - t0;
    //        t0 = 0;
    //    }
    //}
    #endregion
}
