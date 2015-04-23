using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBehaviorHitbox : MonoBehaviour 
{
    public List<AIBehaviorTrigger> behaviors;
    public AIBehaviorController controller;
    
    void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < behaviors.Count; i++)
        {
            // Add animation state check if prefered
            if(other.CompareTag(behaviors[i].Tag))
                controller.AddBehavior(behaviors[i].Behavior);
        }
            
    }
}
