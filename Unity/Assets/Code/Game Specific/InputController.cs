using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ActorController))]
public class InputController : MonoBehaviour 
{
    public ControlScheme Controls;
    private ActorController actor;

    public enum PlayerActions
    {
        Dodge = 0,
        Heal = 1,
        Scan = 2
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

        actor = GetComponent<ActorController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Controls.Update();

        if (Controls.Actions[(int)PlayerActions.Dodge].IsPressed())
            actor.Dodge();

        if (Controls.Actions[(int)PlayerActions.Heal].IsPressed())
            actor.Heal();

        if (Controls.Actions[(int)PlayerActions.Scan].IsPressed())
            actor.Scan();

        actor.Move(Controls.Horizontal.Value(), Controls.Vertical.Value());
	}
}
