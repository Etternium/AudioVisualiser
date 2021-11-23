using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrequencyLine : MonoBehaviour
{
    int i;
    float[] spectrum;
    GameObject[] arrayOfLines;
    int visualiserSamples = 512;

    public int linesToSpawn = 60;
    public float minHeight = 10f, maxHeight = 500f;
    [Space]
    public GameObject line;
    public Transform lineContainer;
    [Space]
    [Range(0f, 1f)]
    public float hueValue;
    public bool colorGradient = true;

    void Start()
    {
        spectrum = new float[512];
        arrayOfLines = new GameObject[linesToSpawn];

        for (i = 0; i < linesToSpawn; i++)
        {
            Vector3 pos = new Vector3(1700f / linesToSpawn * i, 89f, 0f);
            GameObject go = Instantiate(line, pos, Quaternion.identity);

            go.transform.SetParent(lineContainer.transform, true);
            arrayOfLines[i] = go;
        }
    }

    void Update()
    {
        spectrum = AudioListener.GetSpectrumData(visualiserSamples, 0, FFTWindow.Rectangular);

        for (i = 0; i < arrayOfLines.Length; i++)
        {
            Vector2 newSize = GetComponent<RectTransform>().rect.size;
            newSize.y = Mathf.Clamp(Mathf.Lerp(newSize.y, minHeight + spectrum[i] * (maxHeight - minHeight) * 30f * (2f * i / linesToSpawn + 1f), 0.25f), minHeight, maxHeight);

            arrayOfLines[i].GetComponent<RectTransform>().sizeDelta = newSize;
            arrayOfLines[i].GetComponent<Image>().color = Color.HSVToRGB(hueValue, 1f, 1f);
        }

        if(colorGradient)
            ColorGradient();
    }

    void ColorGradient()
    {
        hueValue += 0.05f / 20f;

        if (hueValue >= 1f)
            hueValue = 0f;
    }

    public void ColorSwitch()
    {
        colorGradient = !colorGradient;
    }
}
