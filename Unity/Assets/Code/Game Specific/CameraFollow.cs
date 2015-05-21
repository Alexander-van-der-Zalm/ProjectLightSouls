using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float LerpConstant;
    private Transform tr;
    public Vector3 offset = new Vector3(0, 0, -10);

    // Use this for initialization
    void Start ()
    {
        tr = transform;
        //offset = new Vector3(0, 0, -10); 
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 p1 = tr.position;
        Vector3 p2 = Target.position;
        p1.z = 0;
        p2.z = 0;

        Vector3 slerped = Vector3.Slerp(p1, p2, LerpConstant);

        //Debug.Log(string.Format("pos: {0} slerp: {1} target {2} ", tr.position, slerped,Target.position));
        tr.position = slerped + offset;
    }
}
