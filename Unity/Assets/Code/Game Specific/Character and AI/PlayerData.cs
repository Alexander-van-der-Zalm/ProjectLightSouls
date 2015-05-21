using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour 
{
    public float Health;
    public Vector3 Location;
    public Transform PlayerTransform;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Location = PlayerTransform.position;
	}
}
