using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrequencyLine : MonoBehaviour
{
    int i;
    float[] spectrum;
    GameObject[] arrayOfLines;

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
        spectrum = new float[512];                                                              //initialise spectrum array
        arrayOfLines = new GameObject[linesToSpawn];                                            //initialise array with linesToSpawn elements

        for (i = 0; i < linesToSpawn; i++)
        {
            float x = ((float)Screen.width / linesToSpawn * i) + 6f;
            float y = Screen.height / 5f;
            Vector3 pos = new Vector3(x, y, 0f);                                                //set vector3 pos
            GameObject go = Instantiate(line, pos, Quaternion.identity);                        //instantiate line game object at position pos with no changes to rotation

            go.transform.SetParent(lineContainer.transform, true);                              //child the go to lineContainer
            arrayOfLines[i] = go;                                                               //populate the array with go at position i
        }
    }

    void Update()
    {
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);                      //fill spectrum array

        for (i = 0; i < arrayOfLines.Length; i++)
        {
            Vector2 newSize = GetComponent<RectTransform>().rect.size;                          //fetch size in rectTransform and assign it to newSize
            float arbitrary = (maxHeight - minHeight) * 30f * (2f * i / linesToSpawn + 1f);     //arbitrary value for better visualisation
            float lerp = Mathf.Lerp(newSize.y, minHeight + spectrum[i] * arbitrary, 0.25f);     //linearly interpolate between newSize.y and height that changes from spectrum[i] at the speed of 0.25

            newSize.y = Mathf.Clamp(lerp, minHeight, maxHeight);                                //y value of newSize can be lerped between minHeight and maxHeight values

            arrayOfLines[i].GetComponent<RectTransform>().sizeDelta = newSize;                  //update the rectTransform with respect to newSize
            arrayOfLines[i].GetComponent<Image>().color = Color.HSVToRGB(hueValue, 1f, 1f);     //fetch colour of ecah line in the array and assign it a hueValue
        }

        if(colorGradient)
            ColorGradient();
    }

    void ColorGradient()
    {
        hueValue += 0.05f / 20f;                                                                //continously increment hueValue

        if (hueValue >= 1f)
            hueValue = 0f;                                                                      //if hueValue is greater than 1, reset it to 0
    }

    public void ColorSwitch()
    {
        colorGradient = !colorGradient;                                                         //control whether the lines continously change colour or not
    }
}
