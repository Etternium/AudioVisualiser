using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    protected bool isBeat;

    public float bias, timeStep, timeToBeat, restSmoothTime;

    float previousAudioValue, audioValue, timer;

    void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {
        previousAudioValue = audioValue;
        audioValue = AudioSpectrum.spectrumValue;

        if(previousAudioValue > bias && audioValue <= bias)
        {
            if (timer > timeStep)
            {
                OnBeat();
            }
        }

        if(previousAudioValue <= bias && audioValue > bias)
        {
            if(timer > timeStep)
            {
                OnBeat();
            }
        }

        timer += Time.deltaTime;
    }

    public virtual void OnBeat()
    {
        Debug.Log("Beat");
        timer = 0;
        isBeat = true;
    }
}
