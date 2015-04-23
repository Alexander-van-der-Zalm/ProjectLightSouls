using UnityEngine;
using System.Collections;

// Make into scriptable object

[System.Serializable]
public class AIBehavior
{
    public float Weight;
    public string Type;
    public string Function;

   // public bool Finished;
}

[System.Serializable]
public class AIBehaviorTrigger
{
    public string Tag;
    public string AnimState;
    public AIBehavior Behavior;
}