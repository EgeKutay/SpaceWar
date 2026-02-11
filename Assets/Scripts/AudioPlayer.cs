using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Taking Damage")]
    [SerializeField] AudioClip damageTakeClip;
    [SerializeField][Range(0f, 1f)] float damageTakeVolume = 1f;
    static AudioPlayer instance;
    public AudioPlayer GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        ManageSingleton();
    }
    void ManageSingleton()
    {

        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlayShootingClip()
    {
        if (shootingClip != null)
        {
            PlayClip(shootingClip, shootingVolume);
        }

    }
    public void PlayDamageTakeClip()
    {
        if (damageTakeClip != null)
        {
            PlayClip(damageTakeClip, damageTakeVolume);
        }

    }
    void PlayClip(AudioClip clip, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }
}
