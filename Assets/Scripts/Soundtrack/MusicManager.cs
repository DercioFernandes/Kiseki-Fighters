using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audiosource;
    public AudioClip[] songs;
    public float volume;
    [SerializeField] private float trackTimer;

    void Start()
    {
        _audiosource = GetComponent<AudioSource>();
        if(!_audiosource.isPlaying)
            ChangeSong(Random.Range(0, songs.Length));
    }

    void Update()
    {
        _audiosource.volume = volume;

        if(_audiosource.isPlaying)
            trackTimer += 1 * Time.deltaTime;

        if(!_audiosource.isPlaying || trackTimer >= _audiosource.clip.length)
            ChangeSong(Random.Range(0, songs.Length));
    }

    public void ChangeSong(int songPicked){
        trackTimer = 0;
        _audiosource.clip = songs[songPicked];
        _audiosource.Play();
    }
}
