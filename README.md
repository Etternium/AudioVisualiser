# Starset Horizons - Audio-responsive visualiser

Name: Luka Mumelas

Student Number: C19331903

Class Group: Game Design

# Description of the project

For this assignment I created an interactive audio visualiser in which the viewer moves on a planet surface (infinitely generated) with the view of a planet in the distance and vastness of space above.

Interactive elements include:

- Moveable camera – the viewer can move their mouse to rotate the camera above, below, left and right
- Visual settings menu – on the right hand side of the screen the viewer can open a menu in order to turn on and off different visual settings as they see fit
- Song selector – in that same menu the viewer can manually select what song they would like to listen to, as well as choose a random song
- Speed of movement - the viewer can use scroll wheel to increase the speed at which they move across the planet surface with a little visual surprise


Visual settings include:
- Audio-responding line that changes colours
- Bass-responding circle that changes colours
- Warp Drive


![](Example%20Screenshot.png)

# Instructions for use

- Mouse to rotate the camera

- Scroll wheel to change speed and activate warp drive

- All audio-related settings can be found in the settings menu on the right hand side

# How it works

- The terrain is infinitely generated using perlin noise and is constantly moving the offsetX to give the illusion of movement. The speed at which it moves can be altered using mouse scroll which also activates the warp drive

```
    public int depth = 20;
    public int width = 256;
    public int height = 256;
    [Space]
    public float scale = 20f;                                                   //how zoomed in the terrain is
    public float offsetX = 100f;                                                //used to create random terrain each time the game is played
    public float offsetY = 100f;                                                //used to create random terrain each time the game is played
    [Space]
    public float speed = 2f;

    void Start()
    {
        offsetX = Random.Range(0f, 9999f);                                      //set random value
        offsetY = Random.Range(0f, 9999f);                                      //set random value
    }

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();                              //fetch Terrain component
        terrain.terrainData = GenerateTerrain(terrain.terrainData);             //set terrainData to a newly generated terrain based off current terrainData

        offsetX += Time.deltaTime * speed;                                      //move the offset continously
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
```

- Bass-responding circle has a script that is listenning for bass beats and changes scale whenever a bass beat occurs. It also spawns lines along its radius that continusly change colour. Colour changing and line meshes can be turned on and off in the settings panel

- - Beat detection

```
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
```

Colour changer

```
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
```

The for loop

```
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
```    

- Audio-responsive line generates lines in a for loop and each line takes in audio spectrum data, cuasing it to change its scale based on audio spectrum data. Lines constantly change colour. Colour changing and line meshes can be turned on and off in the settings panel

```
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
```

- Audio manager has a script thta cycles through the array of songs. If a song is skipped or chosen radnomly, it stops the current song and plays the next one. Songs can be paused and and can resume playing at button press in settings panel

- The planet in the background rotates around its y axis and has some ligthing (halo component and post processing) to make it cooler

```
    private void Start()
    {
        rotationSpeed -= 1f / 86400f * speed;
    }

    void Update()
    {
        transform.Rotate(0f, rotationSpeed, 0);
    }
```

# List of classes/assets with sources

| Class | Source |
|-----------|-----------|
| AudioManager.cs | Modified from [source](https://www.youtube.com/watch?v=zpiwhC8zp4A&ab_channel=xOctoManx) |
| AudioSpectrum.cs | Taken from [source](https://www.youtube.com/watch?v=PzVbaaxgPco&ab_channel=RenaissanceCoders) |
| AudioSyncer.cs | Taken from [source](https://www.youtube.com/watch?v=PzVbaaxgPco&ab_channel=RenaissanceCoders) |
| AudioSyncScale.cs | Taken from [source](https://www.youtube.com/watch?v=PzVbaaxgPco&ab_channel=RenaissanceCoders) |
| CameraRotation.cs | Modified from [source](https://www.youtube.com/watch?v=_QajrabyTJc&ab_channel=Brackeys) |
| ColourChanger.cs | Self Written |
| FrequencyLine.cs | Modified from [source](https://www.youtube.com/watch?v=PgXZsoslGsg&t=1s&ab_channel=MediaMax) |
| LineSpawner.cs | Self written |
| PlanetRotation.cs | Self written |
| TerrainGenerator.cs | Taken from [source](https://www.youtube.com/watch?v=vFvwyu_ZKfU&ab_channel=Brackeys) |

| Asset | Source |
|-----------|-----------|
| Warp Drive | Modified form [source](https://www.youtube.com/watch?v=4hlCOUoc6aQ&ab_channel=Mirza) |
| Skybox | Taken from [source](https://freepngimg.com/png/82984-skybox-blue-atmosphere-sky-space-hd-image-free-png) |
| Moon texture | Taken from [source](https://imgur.com/a/aw3nD) |
| Planet texture | Taken from [source](https://assetstore.unity.com/packages/tools/planet-texture-generator-51995) |
| UI font and star | Taken from [source](https://starsetonline.com/toolkit/) |

# References

[AudioManager.cs](https://www.youtube.com/watch?v=zpiwhC8zp4A&ab_channel=xOctoManx)

[AudioSpectrum.cs & AudioSyncer.cs & AudioSyncScale.cs](https://www.youtube.com/watch?v=PzVbaaxgPco&ab_channel=RenaissanceCoders)

[CameraRotation.cs](https://www.youtube.com/watch?v=_QajrabyTJc&ab_channel=Brackeys)

[FrequencyLine.cs](https://www.youtube.com/watch?v=PgXZsoslGsg&t=1s&ab_channel=MediaMax)

[TerrainGenerator.cs](https://www.youtube.com/watch?v=vFvwyu_ZKfU&ab_channel=Brackeys)

[Warp Drive](https://www.youtube.com/watch?v=4hlCOUoc6aQ&ab_channel=Mirza)

[Skybox](https://freepngimg.com/png/82984-skybox-blue-atmosphere-sky-space-hd-image-free-png)

[Moon texture](https://imgur.com/a/aw3nD)

[Planet texture](https://assetstore.unity.com/packages/tools/planet-texture-generator-51995)

[UI font and star](https://starsetonline.com/toolkit/)

# What I am most proud of in the assignment

I am very proud of how the the audio responisve elements and warp drive add so much more jazz and immersion to the project. Changing colours are beautiful and I was very proud when I figured how to implement the feature on the bass circle as it was tough at first.

# Proposal

For this assignment I aim to create an interactive audio visualiser in which the viewer moves on a planet surface (infinitely generated) with the view of a planet in the distance and vastness of space above.

Interactive elements include:

  - Moveable camera – the viewer can move their mouse to rotate the camera above, below, left and right
  - Visual settings menu – on the right hand side of the screen the viewer can open a menu in order to turn on and off different visual settings as they see fit
  - Song selector – in that same menu the viewer can manually select what song they would like to listen to, as well as choose a random song
  
  
Visual settings include:

  - Frequency-responding line that changes colours
  - Loudness-responding circle that changes colours
  - Stars that move towards the camera to give the impression of moving at high speed


![](Audio%20Visualiser%20vision.png)
