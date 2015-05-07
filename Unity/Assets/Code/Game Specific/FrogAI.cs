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

    public bool animationPlaying;
    public string animName;

    private AIBehaviorController AIController;
    private PhysicsController ph;
    private Animator anim;
	private Transform tr;

    private Coroutine update;
    private Coroutine currentBehavior;
    private string airborneStr = "AirBorne";
    private string takeOffStr = "TakeOff";
    private string landStr = "Landing";
    private string attackNrStr = "Attack";
    private string atackTrStr = "AttackTrigger";
    
	// Use this for initialization
	void Start () 
    {
        AIController = GetComponent<AIBehaviorController>();
        ph = GetComponent<PhysicsController>();
        anim = GetComponent<Animator>();
		tr = GetComponent<Transform>();

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

        //animNames();

        animName = getAnimName();

        AIController.ClearBehaviors();
	}

    private string getAnimName()
    {
        AnimatorClipInfo[] clipInfos = anim.GetCurrentAnimatorClipInfo(0);
        if (clipInfos.Length > 0)
            return clipInfos[0].clip.name;
        else
            return "";
        //anim.GetCurrentAnimationClipState(0).ToString();
    }

    #region Boss Logic

    private IEnumerator CoreLogicLoop()
    {
        // Core logic
        // Action
        // Find a new action to do
        string functionName = AIController.FindNewBehavior();

        Debug.Log("Action: " + functionName);

        IEnumerator func = GetFunction(functionName);

        if (func != null)
            yield return currentBehavior = StartCoroutine(func);
        else
            Debug.Log("Func is null");
        //Debug.Log("Action Finished " + functionName);
        
        // *Rotate 
        // * = optional
        float roll = Random.Range(0.0f, 1.0f);

        //Debug.Log("Rotate? " + (roll > RotationChance));

        if(roll > RotationChance)
            currentBehavior = StartCoroutine(RotateCR(new Vector2(0, 1)));

        update = StartCoroutine(CoreLogicLoop());
    }

    private IEnumerator GetFunction(string name)
    {
        switch(name)
        {
            case "Idle":
                return Idle();
            case "Rotate":
                return RotateCR(new Vector2(0, 1));
            case "JumpF":
				Vector2 randomDir = RandomDir();
				// Change to tr.forward
				return JumpCR(Vector2.up, 30.0f);
            case "SwipeNE":
                return Attack(1);
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

    private IEnumerator Attack(int attackNr)
    {
        anim.SetInteger(attackNrStr, attackNr);
        anim.SetTrigger(atackTrStr);
        yield return null;

        yield return StartCoroutine(waitTillAnimationChanges(getAnimName()));

        anim.SetInteger(attackNrStr, 0);
    }

    private IEnumerator waitTillAnimationChanges(string originalAnimName)
    {
        string curName = originalAnimName;

        while (originalAnimName == curName)
        {
            curName = getAnimName();
            yield return null;
        }
        Debug.Log(string.Format("Animation changed: {0} to {1}", originalAnimName, curName));
    }

    #region Shared Functions

	private IEnumerator JumpCR(Vector2 jumpDirection, float jumpDistance, string id = "Boss_Komba_JumpF")
    {
        // Find jump target
        // TakeOff
        // Trigger physics.dodge
        // 
        
        //anim.SetBool(airborneStr, true);
        anim.SetTrigger(takeOffStr);
        Debug.Log("0 " + getAnimName());

		yield return StartCoroutine(waitForAnimation(id+"_TakeOff"));

        //// Chargeup
        //// Stay charging til animation is finished

		yield return StartCoroutine(waitForAnimation(id+"_Airborne"));

        // Airborne
        // Stay airborne till dodge move has finished

        // Trigger dodge
        ph.Dodge(jumpDirection, jumpDistance);

        yield return null;

        // for as long as the physics controller sais its airborne
        while(ph.Airborne)
        {
            yield return null;
        }

        // Landing
        anim.SetTrigger(landStr);
        //anim.SetBool(airborneStr, false);

		yield return StartCoroutine(waitForAnimation(id+"_Land"));
		yield return StartCoroutine(waitForAnimationToFinish(id+"_Land"));
    }

    private IEnumerator waitForAnimation(string animToStop)
    {
        int i = 0;
        while(animToStop != getAnimName())
        {
            i++;
            yield return null;
        }
        Debug.Log("Stopped looking for " + animToStop + "  " + i);
    }

	private IEnumerator waitForAnimationToFinish(string animToStop)
	{
		int i = 0;
		while(animToStop == getAnimName())
		{
			i++;
			yield return null;
		}
		Debug.Log("Finished " + animToStop + " after " + i);
	}

    private void MoveCR()
    {

    }

    private IEnumerator RotateCR(Vector2 newForward)
    {      
        // Rotate left or rotate right

        yield return null;
    }

	private Vector2 RandomDir()
	{
		Vector2 randomDir = new Vector2();
		while(randomDir.magnitude == 0)
		{
			randomDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
		}
		randomDir.Normalize();
		return randomDir;
	}

    private IEnumerator RandomMoveCR()
    {
		Vector2 randomDir = RandomDir();

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
        //Debug.Log("IDLE 1");
        yield return new WaitForSeconds(IdleTime);
        //Debug.Log("IDLE 2");
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
