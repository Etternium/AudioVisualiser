using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineSpawner : MonoBehaviour
{
    public static int linesToSpawn = 60;
    public float radius;

    public GameObject line;
    public Transform circleCentre, linesContainer;

    public static bool isStatic = false;

    void Start()
    {
        for (int i = 0; i < linesToSpawn; i++)
        {
            float theta = 360 / linesToSpawn;
            Quaternion rot = Quaternion.AngleAxis(theta * i, Vector3.forward);
            Vector3 dir = rot * Vector3.up;
            Vector3 pos = circleCentre.position + (dir * radius);

            GameObject go = Instantiate(line, pos, rot);
            go.transform.SetParent(linesContainer.transform, true);
            go.transform.localScale = new Vector3(0.01f, 0.04f);
            go.GetComponent<ColourChanger>().i = i;
        }
    }

    public void ColorSwitch()
    {
        isStatic = !isStatic;
    }
}
