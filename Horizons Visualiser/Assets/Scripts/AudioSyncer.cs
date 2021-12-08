using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    protected bool isBeat;                                          //protected means that the variable is accessible within its class and by derived class instances (in this case AudioSyncScale)

    public float bias, timeStep, timeToBeat, restSmoothTime;        //bias determines what spectrum value triggers a beat
                                                                    //timeStep determines minimum interval between each beat
                                                                    //timeToBeat determines how much time before the visualisation completes
                                                                    //restSmoothTime determines how fast the object goes to rest after a beat

    float previousAudioValue, audioValue, timer;

    void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()                                  //the method can be overriden in a derived class (in this case AudioSyncScale)
    {
        previousAudioValue = audioValue;                            //assign previous audio value
        audioValue = AudioSpectrum.spectrumValue;                   //assign audio value based on spectrumValue

        if(previousAudioValue > bias && audioValue <= bias)
        {
            if (timer > timeStep)
            {
                OnBeat();                                           //if audioValue is lower than bias, trigger a beat if timer is greater than timeStep
            }
        }

        if(previousAudioValue <= bias && audioValue > bias)
        {
            if(timer > timeStep)
            {
                OnBeat();                                           //if audioValue is greater than bias, trigger a beat if timer is greater than timeStep
            }
        }

        timer += Time.deltaTime;                                    //increment timer
    }

    public virtual void OnBeat()                                    //the method can be overriden in a derived class (in this case AudioSyncScale)
    {
        Debug.Log("Beat");
        timer = 0;                                                  //set timer to 0
        isBeat = true;                                              //beat has occurred
    }
}
