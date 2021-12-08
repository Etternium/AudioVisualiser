using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncScale : AudioSyncer                               //derive form AudioSyncer
{
    public Vector3 beatScale, restScale;

    public override void OnBeat()
    {
        base.OnBeat();                                                  //

        StopCoroutine("MoveToScale");                                   //stop current MoveToScale coroutine
        StartCoroutine("MoveToScale", beatScale);                       //start MoveToScale coroutine
    }

    public override void OnUpdate()
    {
        base.OnUpdate();                                                //fucntion called rom AudioSyncer

        if (isBeat)
            return;                                                     //if a beat has occurred return

        transform.localScale = Vector3.Lerp(transform.localScale, restScale, restSmoothTime * Time.deltaTime);
            //lineraly inerpolate the local scale of the game object from its scale to restScale at restSmoothTime speed
    }

    IEnumerator MoveToScale(Vector3 target)
    {
        Vector3 curr = transform.localScale;                            //current scale
        Vector3 initial = curr;                                         //set initial to curr
        float timer = 0;

        while(curr != target)
        {
            curr = Vector3.Lerp(initial, target, timer / timeToBeat);   //linearly interpolate from intial scale to target scale (beatScale) at (timer / timeToBeat) speed
            timer += Time.deltaTime;                                    //increment timer

            transform.localScale = curr;                                //set scale to curr

            yield return null;
        }

        isBeat = false;                                                 //beat is not occurring, lerp to restScale
    }
}
