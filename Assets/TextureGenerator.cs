using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator : MonoBehaviour
{
    public Texture2D GenerateTexture(int resolution, int frequency)
    {
        Texture2D texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
        texture.name = "Procedural Texture";
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;

        float stepSize = 1f / (resolution * resolution);
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                texture.SetPixel(x, y, Color.white * Noise.valueMethods[1](new Vector3(x, y, 0), frequency));
            }
        }
        texture.Apply(true);

        return texture;
    }
}
