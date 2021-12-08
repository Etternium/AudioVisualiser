using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    float[] audioSpectrum;

    public static float spectrumValue                                           //public value that can be accessed from any scripts that needs it
    {
        get;
        private set;
    }

    void Start()
    {
        audioSpectrum = new float[128];                                         //intitialise the array
    }

    void Update()
    {
        AudioListener.GetSpectrumData(audioSpectrum, 0, FFTWindow.Hamming);     //fill audioSpectrum array

        if(audioSpectrum != null && audioSpectrum.Length > 0)
        {
            spectrumValue = audioSpectrum[0] * 100;                             //if audioSpectrum has elements, set spectrum value to the first element in the array
        }
    }
    
}
