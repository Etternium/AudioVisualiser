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

The terrain is infinitely generated using perlin noise and is constantly moving the offsetX to give the illusion of movement. The speed at which it moves can be altered using mouse scroll which also activates the warp drive

Bass-responding circle has a script that is listenning for bass beats and changes scale whenever a bass beat occurs. It also spawns lines along its radius that continusly change colour. Colour changing and line meshes can be turned on and off in the settings panel

Audio-responsive line generates lines in a for loop and each line takes in audio spectrum data, cuasing it to change its scale based on audio spectrum data. Lines constantly change colour. Colour changing and line meshes can be turned on and off in the settings panel

Audio manager has a script thta cycles through the array of songs. If a song is skipped or chosen radnomly, it stops the current song and plays the next one. Songs can be paused and and can resume playing at button press in settings panel

The planet in the background rotates around its y axis and has some ligthing (halo component and post processing) to make it cooler

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
