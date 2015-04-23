using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class AIBehaviorController : MonoBehaviour                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
{
    [SerializeField]
    private List<AIBehavior> m_tempBehaviors =  new List<AIBehavior>();
    [SerializeField]
    private List<AIBehavior> m_standardBehaviors = new List<AIBehavior>();

    #region Add & Clear

    public void AddBehavior(List<AIBehavior> toAdd, bool standardBehavior = false)
    {
        for (int i = 0; i < toAdd.Count; i++)
            AddBehavior(toAdd[i], standardBehavior);
    }
    public void AddBehavior(AIBehavior toAdd, bool standardBehavior = false)
    {
        if (standardBehavior)
            AddIfNotThere(toAdd, m_standardBehaviors);
        else
            AddIfNotThere(toAdd, m_tempBehaviors);
    }

    private void AddIfNotThere(AIBehavior toAdd, List<AIBehavior> list)
    {
        if (!list.Contains(toAdd))
            list.Add(toAdd);
    }

    public void ClearBehaviors(bool standardInclude = false)
    {
        m_tempBehaviors.Clear();
        if (standardInclude)
            m_standardBehaviors.Clear();
    }

    #endregion

    #region Find new Behavior

    public string FindNewBehavior(string type = "", float nonRandomValue = -1)
    {
        // combine the two lists
        List<AIBehavior> combined = m_standardBehaviors.Union(m_tempBehaviors).ToList();

        if (type != "")
            combined = combined.Where(a => a.Type == type).ToList();

        float totalWeight = combined.Sum(a => a.Weight);

        float randomValue = nonRandomValue != -1 ? nonRandomValue : Random.Range(0, totalWeight);

        float curValue = 0;
        for(int i = 0; i < combined.Count; i++)
        {
            curValue += combined[i].Weight;
            if(curValue >= randomValue)
            {
                return combined[i].Function;
            }
        }

        Debug.Log("AIBehaviorController.FindNewBehavior: No behavior found");

        return "";
    }

    #endregion

    internal void PrintBehaviors()
    {
        Debug.Log("Behaviors now:");
        for (int i = 0; i < m_standardBehaviors.Count; i++)
        {
            Debug.Log("Std " + m_standardBehaviors[i].Function);
        }

        for (int i = 0; i < m_tempBehaviors.Count; i++)
        {
            Debug.Log("Tmp " + m_tempBehaviors[i].Function);
        }
    }
}
