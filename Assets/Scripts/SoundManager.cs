using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioClip buublePop; 

    public void PlayBubbleSound(Vector3Int position)
    {
        AudioSource.PlayClipAtPoint(buublePop, transform.position);
    }
}
