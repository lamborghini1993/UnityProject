using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class DrawMyCube6:MonoBehaviour
{

    public float radius = 1;

    List<Vector3> vertex = new List<Vector3>();
    MeshFilter meshFilter;
    Mesh mesh;

    // Use this for initialization
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();
        for(int z = 0; z < 2; z++)
        {
            for(int y = 0; y < 2; y++)
            {
                for(int x = 0; x < 2; x++)
                {
                    Vector3 pos = new Vector3(x, y, z) * radius;
                    vertex.Add(pos);
                }
            }
        }
        mesh.vertices = vertex.ToArray();


    }

    // Update is called once per frame
    void Update()
    {

    }
}
