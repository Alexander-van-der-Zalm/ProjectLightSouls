using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HealthInfo))]
public class PlayerData : MonoBehaviour
{
    public HealthInfo HealthInfo;
    public Vector3 Location;
    public Transform PlayerTransform;

	// Use this for initialization
	void Start ()
    {
        HealthInfo = GetComponent<HealthInfo>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Location = PlayerTransform.position;
	}
}
