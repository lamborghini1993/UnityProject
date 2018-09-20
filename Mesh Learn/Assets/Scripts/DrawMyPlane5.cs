using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class DrawMyPlane5:MonoBehaviour
{

    public int xNum = 10, yNum = 10;
    public float xPix = 1f, yPix = 1f;
    public float swing = 1f;

    List<Vector3> vertex = new List<Vector3>(); // 顶点个数
    int[] triangleVettex;   // 三角形顶点
    Vector2[] uvs;
    MeshFilter meshFilter;
    Mesh mesh;
    float vPix, uPix;

    // Use this for initialization
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();
        for(int j = 0; j <= yNum; j++)
        {
            for(int i = 0; i <= xNum; i++)
            {
                Vector3 localPos = new Vector3(i * xPix, j * yPix, 0);
                vertex.Add(localPos);
            }
        }
        mesh.vertices = vertex.ToArray();

        triangleVettex = new int[xNum * yNum * 6];
        for(int j = 0, num = 0; j < yNum; j++)
        {
            for(int i = 0; i < xNum; i++, num += 6)
            {
                triangleVettex[num] = j * (yNum + 1) + i;
                triangleVettex[num + 1] = triangleVettex[num + 3] = (j + 1) * (yNum + 1) + i;
                triangleVettex[num + 2] = triangleVettex[num + 5] = triangleVettex[num] + 1;
                triangleVettex[num + 4] = triangleVettex[num + 1] + 1;
            }
        }
        mesh.triangles = triangleVettex;


        uvs = new Vector2[vertex.Count];
        vPix = 1f / xNum;
        uPix = 1f / yNum;
        for(int j = 0; j <= yNum; j++)
        {
            for(int i = 0; i <= xNum; i++)
            {
                // 这里一行有xNum+1个点
                uvs[j * (xNum + 1) + i] = new Vector2(i * vPix, j * uPix);
            }
        }
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        meshFilter.mesh = mesh;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for(int i = 0; i < vertex.Count; i++)
        {
            Vector3 worldPos = transform.TransformPoint(vertex[i]);
            Gizmos.DrawSphere(worldPos, 0.1f);
        }
    }

    private void Update()
    {
        // 顶点动画
        for(int j = 0; j <= yNum; j++)
        {
            for(int i = 0; i <= xNum; i++)
            {
                int num = j * (xNum + 1) + i;
                vertex[num] = new Vector3(vertex[num].x, vertex[num].y, Mathf.Sin(10 * Time.time + i) * swing);
            }
        }
        mesh.vertices = vertex.ToArray();
    }
}
