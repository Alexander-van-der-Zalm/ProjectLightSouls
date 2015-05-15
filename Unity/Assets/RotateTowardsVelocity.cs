using UnityEngine;
using System.Collections;

public class RotateTowardsVelocity : MonoBehaviour
{
    private Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector2 dir = rb.velocity.normalized;
        rb.rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 270;
    }
}
