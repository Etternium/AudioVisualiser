using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int depth = 20;
    public int width = 256;
    public int height = 256;
    [Space]
    public float scale = 20f;                                                   //how zoomed in the terrain is
    public float offsetX = 100f;                                                //used to create random terrain each time the game is played
    public float offsetY = 100f;                                                //used to create random terrain each time the game is played
    [Space]
    public float speed = 2f;

    public ParticleSystem warpDrive;
    ParticleSystemRenderer systemRenderer;
    float duration, rateOverTime, startSpeed, lengthScale, power;

    void Start()
    {
        offsetX = Random.Range(0f, 9999f);                                      //set random value
        offsetY = Random.Range(0f, 9999f);                                      //set random value

        systemRenderer = warpDrive.GetComponent<ParticleSystemRenderer>();      //fetch particleSystemRenderer component
        power = 0f;
    }

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();                              //fetch Terrain component
        terrain.terrainData = GenerateTerrain(terrain.terrainData);             //set terrainData to a newly generated terrain based off current terrainData

        offsetX += Time.deltaTime * speed;                                      //move the offset continously

        SpeedChange();
        WarpDriveValues();
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;                            //set heightmapResolution

        terrainData.size = new Vector3(width, depth, height);                   //set width, depth and height of the terrain

        terrainData.SetHeights(0, 0, GenerateHeights());                        //set heights based off the two dimensional array
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];                            //populate two dimensional array with width and height
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);                          //generate perlin noise value for each element in the array
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCord = (float)x / width * scale + offsetX;                       //calculate x
        float yCord = (float)y / height * scale + offsetY;                      //calculate y

        return Mathf.PerlinNoise(xCord, yCord);                                 //return perlin noise value
    }

    void SpeedChange()
    {
        if (speed > 2)
            warpDrive.Play();                                                   //if speed is greater than default, enter warp drive

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            speed += 0.75f;
            power++;
            IncrementWarpDriveValues(7.5f, 8.5f, 1f);                           //increase speed, power and warp drive values if mouse scroll is positive
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            speed -= 0.75f;
            power--;
            IncrementWarpDriveValues(7.5f, 8.5f, -1f);                          //decrease speed, power and warp drive values if mouse scroll is negative
        }

        if (speed >= 10)
        {
            speed = 10;
            power = 11f;
            SetWarpDriveValues(0.5f, 500f, 100f, 100f);                         //if speed is at maximum value, set the speed, power and warp drive to maximum values
        }

        if (speed <= 2)
        {
            speed = 2;
            power = 0f;
            SetWarpDriveValues(5f, 35f, 25f, 15f);                              //if speed is at minimum value, set the speed, power and warp drive to minimum values

            warpDrive.Stop();                                                   //stop emitting particles form warp drive
        }

        if (speed != 10)
            duration = 5f;                                                      //warp drive play time duration is 5 if speed is not maximum
    }

    void WarpDriveValues()
    {
        var main = warpDrive.main;                                              //access main values from particle system
        var em = warpDrive.emission;                                            //access emission values from particle system


        main.duration = duration;                                               //access duration value
        em.rateOverTime = rateOverTime;                                         //access rate over time value
        main.startSpeed = startSpeed;                                           //access start speed value
        systemRenderer.lengthScale = lengthScale;                               //access length scale value
    }

    void SetWarpDriveValues(float D, float RoT, float sS, float lS)
    {
        duration = D;
        rateOverTime = RoT;
        startSpeed = sS;
        lengthScale = lS;                                                       //set values to their correspoding parameters
    }

    void IncrementWarpDriveValues(float sS, float lS, float factor)             //depending on factor value the values are either incremented or decremented
    {
        rateOverTime = Mathf.Pow(1.75f, power) + 20 * power;                    //change rate over time value based on an exponential equation (1.75^power + 20*power)
        startSpeed += sS * factor;
        lengthScale += lS * factor;                                             //change start speed and length scale values
    }
}
