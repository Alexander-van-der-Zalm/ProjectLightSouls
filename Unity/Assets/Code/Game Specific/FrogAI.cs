using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
    public Vector2 RotationTarget;

    public float TakeOffSpeed = 3.0f;
    public float JumpSpeed = 20.0f;

    public bool animationPlaying;
    public string animName;

    private AIBehaviorController AIController;
    private PhysicsController ph;
    private Animator anim;
	private Transform tr;
    private Rigidbody2D rb;

    private Coroutine update;
    private Coroutine currentBehavior;
    private string airborneStr = "AirBorne";
    private string takeOffStr = "TakeOff";
    private string landStr = "Landing";
    private string attackNrStr = "Attack";
    private string attackTrStr = "AttackTrigger";
    private string rotateTrStr = "RotateTrigger";
    private string rotateStr = "Rotate";




    // Use this for initialization
    void Start () 
    {
        AIController = GetComponent<AIBehaviorController>();
        ph = GetComponent<PhysicsController>();
        anim = GetComponent<Animator>();
		tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

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
            //AIController.PrintBehaviors();
            getAnimCurves();
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
    }

    private List<AnimationCurve> getAnimCurves()
    {
        AnimatorClipInfo[] clipInfos = anim.GetCurrentAnimatorClipInfo(0);
        if (clipInfos.Length == 0)
            return null;

        List<AnimationCurve> curves = new List<AnimationCurve>();
        for(int i = 0; i < clipInfos.Length; i++)
        {
            AnimationClip clip = clipInfos[i].clip;
            foreach (var binding in UnityEditor.AnimationUtility.GetCurveBindings(clip))
            {
                curves.Add(UnityEditor.AnimationUtility.GetEditorCurve(clip, binding));
                Debug.Log(binding.propertyName + " " + binding.path);
            }
        }
        Debug.Break();
        return curves;
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
        float roll = UnityEngine.Random.Range(0.0f, 1.0f);

        //Debug.Log("Rotate? " + (roll > RotationChance));

        if(roll > 1-RotationChance)
            yield return currentBehavior = StartCoroutine(RotateCR(RotationTarget));

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
				return JumpCR(tr.up, 100.0f);
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
        anim.SetTrigger(attackTrStr);
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

        //anim.SetBool(airborneStr, true);

        Vector2 p1 = tr.position;
        float t1 = Time.realtimeSinceStartup;

        //// Chargeup
        //// Stay charging til animation is finished
        anim.SetTrigger(takeOffStr);
        yield return StartCoroutine(waitForAnimation(id+"_TakeOff"));

        ph.Dodge(jumpDirection, jumpDistance, JumpSpeed);

        //ph.Pause = true;
        ////yield return null;

        //// Add a small velocity if it isnt that speed already
        //if (rb.velocity.magnitude < TakeOffSpeed)
        //{
        //    rb.velocity = tr.up * TakeOffSpeed;
        //    Debug.Log("Vel "  + rb.velocity);
        //    //Debug.Break();
        //}
        
        // Now wait till charge anim has finished  
        yield return StartCoroutine(waitForAnimationToFinish(id + "_TakeOff"));

        //Trigger dodge
        //ph.Pause = false;
        //ph.Dodge(jumpDirection, jumpDistance, JumpSpeed);

        while(ph.Airborne)
        {
            yield return null;
        }

        // Landing
        anim.SetTrigger(landStr);
        //anim.SetBool(airborneStr, false);

        // Wait till the land animation is finished before starting the new action
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

        float deltaAngle = getAngle(tr.up, newForward);

        if (Mathf.Abs(deltaAngle) > 2)
        {
            anim.SetTrigger(rotateTrStr);
            anim.SetInteger(rotateStr, (int)Mathf.Sign(deltaAngle));
        }

        while (Mathf.Abs(deltaAngle) > 2)
        {
            deltaAngle = getAngle(tr.up, newForward);

            //Debug.Log(deltaAngle);
            yield return null;
        }

        anim.SetFloat(rotateStr, 0);
    }

    private float getAngle(Vector3 v1, Vector2 v2)
    {
        float angle = Vector2.Angle(v1, v2);

        bool left = Vector3.Cross(v1, v2).z > 0;

        if (left)
            angle = -angle;

        //Debug.Log(angle + " " + v1 + " " + v2);

        return angle;
    }

    private Vector2 RandomDir()
	{
		Vector2 randomDir = new Vector2();
		while(randomDir.magnitude == 0)
		{
			randomDir = new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f));
		}
		randomDir.Normalize();
		return randomDir;
	}

    private IEnumerator RandomMoveCR()
    {
		Vector2 randomDir = RandomDir();

        ph.Input = randomDir;

        yield return new WaitForSeconds(UnityEngine.Random.Range(2.0f, 5.0f));

        ph.Input = Vector2.zero;

        yield return new WaitForSeconds(UnityEngine.Random.Range(0.0f, 3.0f));

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
