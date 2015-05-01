using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LockWorldPos : MonoBehaviour 
{
	public bool LockWorldPosition = false;
	private Vector3 pos;
	private Transform tr;

	// Use this for initialization
	void Start () 
	{
		tr = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!LockWorldPosition)
			pos = tr.position;
		else
			tr.position = pos;
	}
}
