using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourChanger : MonoBehaviour
{
    float hueValue;
    
    [HideInInspector]
    public int i;

    Image image;

    private void Start()
    {
        hueValue = 1f / LineSpawner.linesToSpawn * i;       //set starting hueValue depending on what line it is in the for loop from LineSpawner
        image = GetComponent<Image>();                      //fetch image component
    }

    void Update()
    {
        image.color = Color.HSVToRGB(hueValue, 1f, 1f);     //image colour is set to hueValue

        if(!LineSpawner.isStatic)
        {
            hueValue += 0.05f / 10f;                        //hueValue is incremented every frame

            if (hueValue >= 1f)
                hueValue = 0f;                              //if hueValue is 1, reset it to 0
        }
    }
}
