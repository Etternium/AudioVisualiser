using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public TextMeshProUGUI songNameText;
    public TextMeshProUGUI songLengthText;

    public AudioClip[] songs;

    AudioSource source;

    int currentTrack, fullLength, playTime, seconds, minutes;

    void Start()
    {
        source = GetComponent<AudioSource>();
        PlaySong();
    }

    void Update()
    {

    }

    public void PlaySong()
    {
        if(source.isPlaying)
        {
            return;
        }

        currentTrack--;
        if(currentTrack < 0)
        {
            currentTrack = songs.Length - 1;
        }

        StartCoroutine("SongEnd");
    }

    IEnumerator SongEnd()
    {
        while(source.isPlaying)
        {
            playTime = (int)source.time;
            ShowSongLength();
            yield return null;
        }

        PlayNext();
    }

    public void PlayNext()
    {
        source.Stop();
        currentTrack++;

        if(currentTrack > songs.Length - 1)
        {
            currentTrack = 0;
        }

        source.clip = songs[currentTrack];
        source.Play();

        ShowSongName();
        StartCoroutine("SongEnd");
    }

    public void PlayPrevious()
    {
        source.Stop();
        currentTrack--;

        if (currentTrack < 0)
        {
            currentTrack = songs.Length - 1;
        }

        source.clip = songs[currentTrack];
        source.Play();

        ShowSongName();
        StartCoroutine("SongEnd");
    }

    public void StopSong()
    {
        StopCoroutine("SongEnd");
        source.Stop();
    }

    void ShowSongName()
    {
        songNameText.text = source.clip.name;
        fullLength = (int)source.clip.length;
    }

    void ShowSongLength()
    {
        seconds = playTime % 60;
        minutes = (playTime / 60) % 60;
        songLengthText.text = minutes + ":" + seconds.ToString("D2") + " / " + (fullLength / 60) % 60 + ":" + (fullLength % 60).ToString("D2");
    }
}
