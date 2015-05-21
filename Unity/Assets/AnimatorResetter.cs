using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class AnimatorResetter
{
    public static void ResetStateToIdle(Animator anim)
    {
        int idleHash = Animator.StringToHash("Idle");
        if (anim.HasState(0, idleHash))
            anim.Play(idleHash, 0, 0);
    }

    public static void ResetVariables(Animator anim)
    {
        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            switch (param.type)
            {
                case AnimatorControllerParameterType.Bool:
                    anim.SetBool(param.nameHash, param.defaultBool);
                    break;
                case AnimatorControllerParameterType.Float:
                    anim.SetFloat(param.nameHash, param.defaultFloat);
                    break;
                case AnimatorControllerParameterType.Int:
                    anim.SetInteger(param.nameHash, param.defaultInt);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    //anim.SetTrigger(param.nameHash);
                    break;
                default:
                    Debug.LogError("TYPE NOT RECOGNIZED AnimatorControllerParameterType in AnimatorResetter");
                    return;
            }
        }
    }
}
