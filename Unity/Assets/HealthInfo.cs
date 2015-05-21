using UnityEngine;
using System.Collections;
using System;

public class HealthInfo : MonoBehaviour
{
    public enum Type
    {
        Player,
        EnemyNPC,
        FriendlyNPC,
        Boss
    }
    //Resistance types??
    public float Health = 1.0f;
    public float MaxHealth = 1.0f;
    // Resistances etc.??

    public Type CharacterType = Type.EnemyNPC;

    private GameState gs;

    void Start()
    {
        gs = GameObject.FindObjectOfType<GameState>();
    }

    public void OnHit(float damage)
    {
        Health -= damage;
        if (Health < 0)
            Die();
        else
            Hit();
    }

    private void Hit()
    {
        // Play sound

        // ScreenShake
        // Where to define this
    }

    public void ReBirth()
    {
        Health = MaxHealth;
    }

    public void Die()
    {
        CommonDeath();
        switch (CharacterType)
        {
            case Type.Boss:
                BossDeath();
                break;
            case Type.EnemyNPC:
                EnemyNPCDeath();
                break;
            case Type.FriendlyNPC:
                FriendlyNPCDeath();
                break;
            case Type.Player:
                PlayerDeath();
                break;
        }
    }

    private void CommonDeath()// add xp check?
    { 
        // Respawn 
        
    }

    private void BossDeath()
    {

    }

    private void PlayerDeath()
    {
        gs.PlayerDeath(gameObject);
    }

    private void FriendlyNPCDeath()
    {

    }

    private void EnemyNPCDeath()
    {

    }

    public static HealthInfo FindHealthInfo(GameObject go)
    {
        // check current go level
        HealthInfo hi = go.GetComponent<HealthInfo>();

        if (hi == null) // Check parent
            hi = go.GetComponentInParent<HealthInfo>();

        if (hi == null) // Check children
            hi = go.GetComponentInChildren<HealthInfo>();

        return hi;
    }
}
