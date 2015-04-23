using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AIBehaviorController), typeof(PhysicsController))]
public class FrogAI : MonoBehaviour 
{
    
    public List<AIBehaviorTrigger> StandardBehaviors;
    // Think about interupting?

    private AIBehaviorController AIController;
    private PhysicsController ph;

	// Use this for initialization
	void Start () 
    {
        AIController = GetComponent<AIBehaviorController>();
        ph = GetComponent<PhysicsController>();

        for (int i = 0; i < StandardBehaviors.Count; i++)
            AIController.AddBehavior(StandardBehaviors[i].Behavior, true);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {


        AIController.ClearBehaviors();
	}



    #region Boss Logic

    // Core logic
    // Action
    // Wait
    // *Rotate 
    // * = optional

    private void StartFunction(string name)
    {
        switch(name)
        {
            case "Idle":            
            case "Rotate":
            case "FrontHop":
            case "BackHop":
            case "SideHop":
            case "SideSweep":
            case "BacklegSweep":
            case "Tongue":
            case "Secrete":
            default:                break;
        }
    }

    #region Shared Functions

    private void JumpCR()
    {

    }

    private void MoveCR()
    {

    }

    private void RotateCR()
    {

    }

    #endregion

    #region AnimationFunctions

    private void Idle(float time)
    {

    }

    private void Rotate(float maxTime, Transform target = null)
    {

    }

    private void BackHop(Vector2 target)
    {

    }

    private void SideHop(Vector2 target)
    {

    }

    private void FrontHop(Vector2 target)
    {

    }

    private void SideSweep()
    {

    }

    private void BackLegSweep()
    {

    }

    private void TongueSweep()
    {

    }

    private void TongueStretch()
    {

    }

    #endregion

    #endregion
}
