using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    [SerializeField] DrawMode drawMode = DrawMode.ColourMap;
    [SerializeField] int mapWidth = 10;
    [SerializeField] int mapHeight = 10;
    [SerializeField] int seed = 0;
    [SerializeField] float noiseScale = 20;
    [SerializeField] Vector2 position;
    [SerializeField] int intensity = 1;

    [SerializeField] int octavesNumber = 3;
    [SerializeField] float persistance = 0.5f;
    [SerializeField] float lacunarity = 2;


    [SerializeField] TerrainType[] regions;

    public bool autoUpdate;


    public void generateMap()
    {
        float[,] noiseMap = NoiseGenerator.generateNoiseMap(mapWidth, mapHeight, seed, noiseScale, position, octavesNumber, persistance, lacunarity);
        Color[] colourMap = new Color[mapHeight * mapHeight];

        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                foreach(TerrainType r in regions)
                {
                    if(noiseMap[x,y] <= r.height)
                    {
                        colourMap[y * mapWidth + x] = r.colour;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();


        switch (drawMode)
        {
            case DrawMode.NoiseMap:
                display.drawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
                break;

            case DrawMode.ColourMap:
                display.drawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
                break;

            case DrawMode.Mesh:
                display.drawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, intensity), TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
                break;
        }

    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}

public enum DrawMode
{
    NoiseMap,
    ColourMap,
    Mesh
}