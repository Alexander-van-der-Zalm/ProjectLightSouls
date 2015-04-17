using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour 
{
    public ControlScheme Controls;

    public enum PlayerActions
    {
        Dodge,
        Heal,
        Scan
    }

	// Use this for initialization
	void Start () 
    {
        #if UNITY_EDITOR
        if(Controls == null)
        {
            Controls = ControlScheme.CreateScheme<PlayerActions>();
            ScriptableObjectHelper.SaveAssetAutoNaming(Controls);
        }
        #endif  
	}
	
	// Update is called once per frame
	void Update () 
    {
        Controls.Update();
	}
}
