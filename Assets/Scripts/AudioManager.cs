using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip correctSound;
    public AudioClip buzzerSound;
    public static AudioManager instance;
    public void PlaySoundSlotPlace()
    {
        audioSource.PlayOneShot(clickSound);
    }
    public void PlayCorrect()
    {
        audioSource.PlayOneShot(correctSound);
    }

    public void PlayBuzzer()
    {
        audioSource.PlayOneShot(buzzerSound);
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
