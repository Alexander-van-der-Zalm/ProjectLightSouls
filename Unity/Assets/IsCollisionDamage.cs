using UnityEngine;
using System.Collections;

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
        VelocityBased//,
        //FullInfo
    }

    #endregion

    public bool DoesDamage = false;
    public float MinimumVelocity = 0.0f;

    public DamageInfo Damage;
    
    public void CheckCollisionForDamage(Collision2D other)
    {
        if (!DoesDamage)
            return;

        if (other.relativeVelocity.magnitude < MinimumVelocity)
            return;

        // Maybe check/add specific direction??

        // Resolve
    }
}
