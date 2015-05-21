using UnityEngine;
using System.Collections.Generic;

public class IsCollisionDamage : MonoBehaviour
{
    #region Classes and enums

    [System.Serializable]
    public class DamageInfo
    {
        public float DamageAmount = 1.0f;
        public DamageFormula ResolveMethod = DamageFormula.SetAmount;
        public float VelocityNormalizeMagnitude = 3.0f;
    }

    // Damage Info
    public enum DamageFormula
    {
        SetAmount,
        VelocityBasedNormal,
        VelocityBasedFullyScaled//,
        //FullInfo
    }

    #endregion

    public bool DoesDamage = false;
    public float MinimumVelocity = 0.0f;

    public DamageInfo Damage;

    public List<string> TagsToHit;

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(string.Format("dmg {0} minV {1} {2} tags {3} other {4} named {5}", DoesDamage, MinimumVelocity, other.relativeVelocity, TagsToHit.Count, other.gameObject.tag, other.gameObject.name));

        if (!DoesDamage)
            return;

        // Measure relative velocity
        //if (other.relativeVelocity.magnitude < MinimumVelocity)
        //    return;

        // Maybe check/add specific direction??

        // Check Tag
        if(TagsToHit.Count > 0)
        {
            foreach (string tag in TagsToHit)
                if (other.gameObject.CompareTag(tag))
                    return;
        }

        // Calculate damage
        float damage = calculateDamage(other);

        // Resolve
        // Find the health info
        HealthInfo hi = HealthInfo.FindHealthInfo(other.gameObject);
        if (hi != null)
            hi.OnHit(damage);
        // Trigger resolution in HealthInfo
    }

    private float calculateDamage(Collision2D other)
    {
        // Todo other methods / switch
        return Damage.DamageAmount;
    }
}
