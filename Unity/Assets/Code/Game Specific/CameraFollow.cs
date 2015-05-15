using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float LerpConstant;
    private Transform tr;
    private Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        tr = transform;
        offset = new Vector3(0, 0, -10); 
	}
	
	// Update is called once per frame
	void Update ()
    {
        tr.position = Vector3.Slerp(tr.position, Target.position, LerpConstant) + offset;
    }
}
