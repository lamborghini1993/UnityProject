using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeshDrawBase:MonoBehaviour
{
    protected MeshFilter targetFilter;
    protected Mesh mh;
    protected int[] tris;
    protected Vector2[] uvs;
    protected Vector3[] normals;

    private void Awake()
    {
        targetFilter = GetComponent<MeshFilter>();
    }
    protected virtual void Update()
    {
        DrawMesh();
    }

    protected abstract void DrawMesh();
}
