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
        hueValue = 1f / LineSpawner.linesToSpawn * i;
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.color = Color.HSVToRGB(hueValue, 1f, 1f);

        if(!LineSpawner.isStatic)
        {
            hueValue += 0.05f / 10f;

            if (hueValue >= 1f)
                hueValue = 0f;
        }
    }
}
