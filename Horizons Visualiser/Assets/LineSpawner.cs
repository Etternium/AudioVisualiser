using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineSpawner : MonoBehaviour
{
    public int linesToSpawn;
    public float radius;

    public GameObject line;
    public Transform circleCentre, linesContainer;

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

            //color spectrum
            float colorNumber = 1f / linesToSpawn;
            Image lineColor = go.gameObject.GetComponent<Image>();

            lineColor.color = Color.HSVToRGB(colorNumber * i, 1, 1);
        }
    }
}
