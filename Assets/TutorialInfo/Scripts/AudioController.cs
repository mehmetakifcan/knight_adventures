using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioController instance;
    public Audio playerAudio;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void jumpSound(UnityEngine.Vector3 player)
    {
        AudioSource.PlayClipAtPoint(playerAudio.jumpSound, player);
    }

    public void attackSound(UnityEngine.Vector3 player)
    {
        AudioSource.PlayClipAtPoint(playerAudio.attackSound, player);
    }

    public void coinSound(UnityEngine.Vector3 player)
    {
        AudioSource.PlayClipAtPoint(playerAudio.coinSound, player);
    }

    public void waterSound(UnityEngine.Vector3 player)
    {
        AudioSource.PlayClipAtPoint(playerAudio.waterSound, player);
    }

    public void enemyDieSound(UnityEngine.Vector3 player)
    {
        AudioSource.PlayClipAtPoint(playerAudio.enemyDieSound, player);
    }

    public void playerDieSound(UnityEngine.Vector3 player)
    {
        AudioSource.PlayClipAtPoint(playerAudio.playerDieSound, player);
    }


}
[System.Serializable]
public class Audio
{
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip attackSound;
    public AudioClip waterSound;
    public AudioClip enemyDieSound;
    public AudioClip playerDieSound;
    

}