using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TriangleDraw:MeshDrawBase
{
    public List<Vector3> vts = new List<Vector3>();

    private void Start()
    {
        mh = new Mesh();
        
        tris = new int[3];
        for(int i = 0; i < vts.Count; i++)
        {
            //vts[i] = transform.InverseTransformPoint(vts[i]);   // 转为局部坐标
            tris[i] = i;
        }
        // 获取顶点
        mh.vertices = vts.ToArray();
        // 三角形 顺时针画
        mh.triangles = tris;

        mh.RecalculateBounds(); // 重新计算边界
        mh.RecalculateNormals();// 重新计算法线
        mh.RecalculateTangents();// 重新计算切线

        targetFilter.mesh = mh;
    }

    protected override void DrawMesh()
    {

    }

}
