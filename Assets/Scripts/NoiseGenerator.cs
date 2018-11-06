using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Public function to generate noise  */
public static class NoiseGenerator {

    public static float[,] generateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, Vector2 position, int octaveNumber, float persistance, float lacunarity)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        float maxNoiseHeight = 0;
        float minNoiseHeight = 0;

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaveNumber];

        //set random seed with octave offset
        for(int i = 0; i < octaveNumber; i++)
        {
            //we clamp random, instead perlin noise repeat same values
            float offsetX = prng.Next(-100000, 100000) + position.x;
            float offsetY = prng.Next(-100000, 100000) + position.y;

            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }


        if (scale <= 0)
            scale = 0.0001f;


        for (int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                // amplitude of noise
                float amplitude = 1;
                //frenquency of noise
                float frequency = 1;
                //we start from 0 and add perlin value generated each octave
                float noiseHeight = 0;
                for (int oct = 0; oct < octaveNumber; oct++)
                {
                    float sampleX = (x - mapWidth/2) / scale * frequency + octaveOffsets[oct].x;
                    float sampleY = (y - mapHeight/2) / scale * frequency + octaveOffsets[oct].y;

                    //we generate perlin value and set them in range of -1 & 1 for noiseHeight can decrease
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 -1;
                    noiseHeight += perlinValue * amplitude;

                    //each octave we multiply amplitude & freq by perisitance and lacunarity values
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                //we store the max and min value for entire map
                if(noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                } else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        //noise map is not in 0-1 value, to create a height readeable me convert the actual range of value in range 0-1
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
        return noiseMap;
    }
}
