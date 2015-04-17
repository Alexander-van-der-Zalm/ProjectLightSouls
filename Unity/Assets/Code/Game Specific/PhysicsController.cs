using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsController : MonoBehaviour 
{
    private float m_accel;
    private float m_friction;

    public float MovementMaxSpeed;
    public float TimeToMaxSpeed;

    public float RollMaxSpeed;
    public float RollDistance;

    public Vector2 Input;

    private Rigidbody2D rb;

    //private 
    private bool rolling = false;

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

        recalculateAccelFriction();
        
        float dt = Time.deltaTime;

        Vector2 v = rb.velocity;
        Vector2 a = Vector2.zero;
        
        if(!rolling)
            a = Input * m_accel;

        Vector2 friction = v * m_friction;

        rb.velocity += dt * (a - friction);
    }

    private void recalculateAccelFriction()
    {
        // Inefficient recalculate each fram, but who cares
        m_friction = TimeToMaxSpeed != 0 ? 2 / TimeToMaxSpeed : 0;
        m_accel = m_friction * MovementMaxSpeed;
    }

    Coroutine rollCR;

    public void Roll()
    {
        rollCR = StartCoroutine(RollCR());
    }

    public void StopRoll()
    {
        StopCoroutine(rollCR);
        rolling = false;
    }

    private IEnumerator RollCR()
    {
        rolling = true;

        float distance = 0;

        float speed = 0;
        float time = 0;
        
        float timePassed = 0;

        while(timePassed < time)
        {
            
            timePassed += Time.deltaTime;
            yield return null;
        }


        rolling = false;
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
