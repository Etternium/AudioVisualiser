using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineSpawner : MonoBehaviour
{
    public static int linesToSpawn = 60;

    public GameObject line;
    public Transform circleCentre, linesContainer;

    public static bool isStatic = false;

    void Start()
    {
        for (int i = 0; i < linesToSpawn; i++)
        {
            float theta = 360 / linesToSpawn;                                       //set angle theta
            Quaternion rot = Quaternion.AngleAxis(theta * i, Vector3.forward);      //rotate theta around the z axis
            Vector3 dir = rot * Vector3.up;                                         //orient the line to the camera
            Vector3 pos = circleCentre.position + (dir * Screen.height / 4.5f);     //set position of the line along the circle radius

            GameObject go = Instantiate(line, pos, rot);                            //create an instance of a line at pos position at rot rotation
            ColourChanger colourChanger = go.AddComponent<ColourChanger>();         //add ColourChanger component to the line

            go.transform.SetParent(linesContainer.transform, true);                 //child the line to the linesContainer game object
            go.transform.localScale = new Vector3(0.01f, 0.04f);                    //set the local scale of the line
            colourChanger.i = i;                                                    //this i = i in the ColourChanger
        }
    }

    public void ColorSwitch()
    {
        isStatic = !isStatic;                                                       //control whether the line continously changes colour or not
    }
}
