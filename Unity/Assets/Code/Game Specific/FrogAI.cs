using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AIBehaviorController), typeof(PhysicsController), typeof(Animator))]
public class FrogAI : MonoBehaviour 
{
    public List<AIBehaviorTrigger> StandardBehaviors;
    // Think about interupting?

    //public float MinWait;
    //public float MaxWait;
    public float IdleTime;
    [Range(0.0f,1.0f)]
    public float RotationChance;

    private AIBehaviorController AIController;
    private PhysicsController ph;
    private Animator anim;

    private Coroutine update;
    private Coroutine currentBehavior;
    
	// Use this for initialization
	void Start () 
    {
        AIController = GetComponent<AIBehaviorController>();
        ph = GetComponent<PhysicsController>();
        anim = GetComponent<Animator>();

        // Add all the standard behaviors to the functionpool
        for (int i = 0; i < StandardBehaviors.Count; i++)
            AIController.AddBehavior(StandardBehaviors[i].Behavior, true);

        // The core behavior loop
        update = StartCoroutine(CoreLogicLoop());

        //currentRoutine = StartCoroutine(RandomMoveCR());
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if(Input.GetKeyUp(KeyCode.I))
        {
            AIController.PrintBehaviors();
        }

        AIController.ClearBehaviors();
	}



    #region Boss Logic

    private IEnumerator CoreLogicLoop()
    {
        // Core logic
        // Action
        // Find a new action to do
        string functionName = AIController.FindNewBehavior();

        Debug.Log("Action: " + functionName);

        yield return currentBehavior = StartCoroutine(GetFunction(functionName));

        Debug.Log("Action Finished " + functionName);
        
        // *Rotate 
        // * = optional
        float roll = Random.Range(0.0f, 1.0f);

        Debug.Log("Rotate? " + (roll > RotationChance));

        if(roll > RotationChance)
            currentBehavior = StartCoroutine(RotateCR());

        update = StartCoroutine(CoreLogicLoop());
    }

    private IEnumerator GetFunction(string name)
    {
        switch(name)
        {
            case "Idle":
                return Idle();
            case "Rotate":
                return RotateCR();
            case "FrontHop":
            case "BackHop":
            case "SideHop":
            case "SideSweep":
            case "BacklegSweep":
            case "Tongue":
            case "Secrete":
            default:                break;
        }
        return null;
    }

    #region Shared Functions

    private void JumpCR()
    {

    }

    private void MoveCR()
    {

    }

    private IEnumerator RotateCR()
    {
        yield return null;
    }

    private IEnumerator RandomMoveCR()
    {
        Vector2 randomDir = new Vector2();
        while(randomDir.magnitude == 0)
        {
            randomDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        }
        randomDir.Normalize();

        ph.Input = randomDir;

        yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));

        ph.Input = Vector2.zero;

        yield return new WaitForSeconds(Random.Range(0.0f, 3.0f));

        currentBehavior =  StartCoroutine(RandomMoveCR());
    }

    #endregion

    #region AnimationFunctions

    private IEnumerator Idle()
    {
        Debug.Log("IDLE 1");
        yield return new WaitForSeconds(IdleTime);
        Debug.Log("IDLE 2");
        yield break;
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
