using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SurfaceCreator : MonoBehaviour
{
    [SerializeField]
    Color bottomColor;

    [SerializeField]
    Color topColor;

    [SerializeField]
    float heightStep = 1000;


    private Mesh mesh;

    public void GenerateTerrain(Texture2D noiseMap, float heightStrength)
    {
        if (mesh == null)
        {
            mesh = new Mesh();
            mesh.name = "Surface Mesh";
            GetComponent<MeshFilter>().mesh = mesh;
        }

        mesh.Clear();

        int resolution = noiseMap.width; // width is same as height
        Vector3[] vertices = new Vector3[(resolution + 1) * (resolution + 1)];
        Color[] colors = new Color[vertices.Length];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector3[] normals = new Vector3[vertices.Length];

        float heightAdjustedStrength = heightStrength / heightStep;
        float stepSize = 1f / resolution;

        int verticeIndex = 0;
        for (int y = 0; y <= resolution; y++)
        {
            for (int x = 0; x <= resolution; x++)
            {
                float height = Mathf.InverseLerp(0, 1, noiseMap.GetPixel(x, y).grayscale);
                vertices[verticeIndex] = new Vector3(x * stepSize, y * stepSize, height * heightAdjustedStrength);
                colors[verticeIndex] = Color.Lerp(topColor, bottomColor, height);
                normals[verticeIndex] = Vector3.up;
                uv[verticeIndex] = new Vector2(x * stepSize, y * stepSize);

                verticeIndex++;
            }
        }
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.colors = colors;
        mesh.uv = uv;

        int[] triangles = new int[resolution * resolution * 6];
        for (int t = 0, v = 0, y = 0; y < resolution; y++, v++)
        {
            for (int x = 0; x < resolution; x++, v++, t += 6)
            {
                triangles[t] = v;
                triangles[t + 1] = v + resolution + 1;
                triangles[t + 2] = v + 1;
                triangles[t + 3] = v + 1;
                triangles[t + 4] = v + resolution + 1;
                triangles[t + 5] = v + resolution + 2;
            }
        }
        mesh.triangles = triangles;
    }
}