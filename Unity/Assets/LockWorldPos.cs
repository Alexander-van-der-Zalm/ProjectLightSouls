using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LockWorldPos : MonoBehaviour 
{
	public bool LockWorldPosition = false;
	private Vector3 pos;
	private Transform tr;
    private Puppet2D_GlobalControl puppet2d;

	// Use this for initialization
	void Start () 
	{
		tr = transform;
        pos = tr.position;
        puppet2d = GetComponentInParent<Puppet2D_GlobalControl>();
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if(!LockWorldPosition)
			pos = tr.position;
		else
			tr.position = pos;

        if (puppet2d != null)
            puppet2d.Run();
	}
}
