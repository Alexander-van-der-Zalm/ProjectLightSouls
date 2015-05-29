using UnityEngine;
using System.Collections.Generic;

public class AnimateAudio : MonoBehaviour
{
    public List<AudioClip> AudioClips;

	public void PlayClip(int index)
    {
        AudioSource.PlayClipAtPoint(AudioClips[index], transform.position);
    }
}
