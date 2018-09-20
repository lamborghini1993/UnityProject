using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class QuadDraw:MeshDrawBase
{
    public List<Vector3> vts = new List<Vector3>();

    protected override void DrawMesh()
    {
    }

    // Use this for initialization
    void Start()
    {
        mh = new Mesh();

        // 获取顶点
        mh.vertices = vts.ToArray();

        tris = new int[6];  //  两个三角形留个点
        tris[0] = tris[3] = 0;
        tris[1] = 1;
        tris[2] = tris[4] = 2;
        tris[5] = 3;
        mh.triangles = tris;

        uvs = new Vector2[vts.Count];
        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(0, 1);
        uvs[2] = new Vector2(1, 1);
        uvs[3] = new Vector2(1, 0);
        //uvs[3] = new Vector2(0.5f, 0);
        mh.uv = uvs;

        mh.RecalculateBounds(); // 重新计算边界
        mh.RecalculateNormals();// 重新计算法线
        mh.RecalculateTangents();// 重新计算切线

        targetFilter.mesh = mh;
    }

}
