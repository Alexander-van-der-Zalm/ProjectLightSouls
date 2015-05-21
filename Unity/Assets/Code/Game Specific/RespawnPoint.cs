using UnityEngine;
using System.Collections;

public class RespawnPoint : MonoBehaviour
{
    //public enum RegistryMethod
    //{
    //    Auto,
    //    InteractButton
    //}

    public string Tag;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(Tag))
        {
            RespawnMe respawn = other.GetComponent<RespawnMe>();
            if(respawn != null)
            {
                respawn.RespawnPoint = transform;
            }
        }
    }
}
