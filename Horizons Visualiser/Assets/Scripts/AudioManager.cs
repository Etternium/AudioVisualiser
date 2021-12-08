using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public TextMeshProUGUI songNameText;
    public TextMeshProUGUI songLengthText;

    public AudioClip[] songs;

    [HideInInspector]
    public static AudioSource source;

    int currentTrack, fullLength, playTime, seconds, minutes;

    void Start()
    {
        source = GetComponent<AudioSource>();           //fetch audio source component
        PlaySong();
    }

    public void PlaySong()
    {
        if(source.isPlaying)
        {
            return;                                     //if a song is playing then do nothing
        }

        currentTrack--;
        if(currentTrack < 0)
        {
            currentTrack = songs.Length - 1;            //if you skip backwards from the first song, play the last song in the array
        }

        StartCoroutine("SongEnd");                      //wait for the song to end
    }

    IEnumerator SongEnd()
    {
        while(source.isPlaying)
        {
            playTime = (int)source.time;                //variable playTime = current song length
            ShowSongLength();                           //display length of the current song
            yield return null;
        }

        PlayNext();                                     //when song ends, play next one
    }

    public void PlayNext()
    {
        source.Stop();                                  //stop playing the current song
        currentTrack++;                                 //skip to next song in the array

        if(currentTrack > songs.Length - 1)
        {
            currentTrack = 0;                           //if you skip the last song, play the first song in the array
        }

        source.clip = songs[currentTrack];              //song to be played is the next song
        source.Play();                                  //play next song

        ShowSongName();                                 //display next song name
        StartCoroutine("SongEnd");                      //wait for the song to end
    }

    public void PlayPrevious()
    {
        source.Stop();                                  //stop playing the current song
        currentTrack--;                                 //skip to previous song in the array

        if (currentTrack < 0)
        {
            currentTrack = songs.Length - 1;            //if you skip backwards from the first song, play the last song in the array
        }

        source.clip = songs[currentTrack];              //song to be played is the previous song
        source.Play();                                  //play previous song

        ShowSongName();                                 //display previous song name
        StartCoroutine("SongEnd");                      //wait for the song to end
    }

    public void StopSong()
    {
        StopCoroutine("SongEnd");                       //don't wait for the song end
        source.Stop();                                  //stop the song immediately
    }

    public void PlayRandom()
    {
        source.Stop();                                  //stop playing the current song
        int rnd = Random.Range(0, songs.Length);        //get a random integer value
        currentTrack = rnd;                             //song to be played in the array equals to the random integer value

        source.clip = songs[currentTrack];              //random song to be played
        source.Play();                                  //play random song

        ShowSongName();                                 //display song name
        StartCoroutine("SongEnd");                      //wait for the song to end
    }

    void ShowSongName()
    {
        songNameText.text = source.clip.name;           //song name is the same as the audio clip file name
        fullLength = (int)source.clip.length;           //fullLength is assigned the length value of the current song
    }

    void ShowSongLength()
    {
        seconds = playTime % 60;                        //when playTime reaches 60 seconds, seconds display resets to 0
        minutes = (playTime / 60) % 60;                 //when playTime reaches 60 minutes, minutes display resets to 0

        songLengthText.text = minutes + ":" + seconds.ToString("D2") + " / " + (fullLength / 60) % 60 + ":" + (fullLength % 60).ToString("D2");
            //minutes : seconds (format is 0:00) / song length in minutes and seconds (format is 0:00)
    }
}
