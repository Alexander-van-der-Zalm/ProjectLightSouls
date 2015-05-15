using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float LerpConstant;
    private Transform tr;

	// Use this for initialization
	void Start ()
    {
        tr = transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        tr.position = Vector3.Slerp(tr.position, Target.position, LerpConstant);
    }
}
