﻿using UnityEngine;
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
    private bool dodging = false;
    public bool Airborne = false;
    public bool Pause = false;

    private float speed;
    Coroutine dodgeCR;

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

        if (Pause)
            return;

        if (Input.magnitude == 0)
            recalculateAccelFriction(TimeToMaxSpeed, DodgeMaxSpeed);
        else
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
        else if(dodging)
            rb.velocity = airSpeed;

        speed = rb.velocity.magnitude;
    }

    public void RestartPhysics()
    {
        StopAllCoroutines();
        dodging = false;
        Airborne = false;
        Pause = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    private void recalculateAccelFriction(float timeToMaxSpeed, float maxSpeed)
    {
        // Inefficient recalculate each fram, but who cares
        m_friction = timeToMaxSpeed != 0 ? 2 / TimeToMaxSpeed : 0;
        m_accel = m_friction * maxSpeed;
    }

    public void Dodge(Vector2 direction, float distance, float speed)
    {
        float time = distance / speed;

        dodgeCR = StartCoroutine(DodgeCR(direction, speed, time));
    }

    public void DodgeTimed(Vector2 direction, float time, float speed)
    {
        dodgeCR = StartCoroutine(DodgeCR(direction, speed, time));
    }

    public void Dodge(Vector2 direction, float distance)
    {
        float time = distance / DodgeMaxSpeed;

        dodgeCR = StartCoroutine(DodgeCR(direction, DodgeMaxSpeed, time));
    }

    public void StopDodge()
    {
        StopCoroutine(dodgeCR);
        Airborne = false;
        dodging = false;
    }

    private IEnumerator DodgeCR(Vector2 direction, float speed, float time)
    {
        Airborne = true;
        dodging = true;

        airSpeed = direction.normalized * speed;

       // Debug.Log(string.Format("Dodge t: {0} d:{1} s: {2}", time, direction, speed));

        yield return new WaitForSeconds(time);

        Airborne = false;
        dodging = false;
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
