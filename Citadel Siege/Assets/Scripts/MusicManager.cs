using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] clips;
    public int index = 0;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clips[index];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.clip = clips[index];
    }
    public void ChangeClip()
    {
        audioSource.Stop();
        if (index == clips.Length - 1)
            index = 0;
        else
        {
            index++;
        }
        audioSource.clip = clips[index];
        audioSource.Play();
    }
    public void MuteSound(){
        int volume = audioSource.volume == 1 ? 0 : 1;
        audioSource.volume = volume;
    }
}
