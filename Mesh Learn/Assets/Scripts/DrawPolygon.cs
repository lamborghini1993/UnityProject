using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPolygon:MonoBehaviour
{
    Mesh mh;
    int[] tris;
    Vector3[] normals;
    MeshFilter targetFilter;
    List<Vector3> vts = new List<Vector3>();

    private void Start()
    {
        targetFilter = GetComponent<MeshFilter>();
    }

    protected void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 localPoint = transform.InverseTransformPoint(hit.point);
                vts.Add(localPoint);
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
            Reset();

        if(Input.GetKeyDown(KeyCode.S))
            MyDrawPolygon();
    }

    protected void MyDrawPolygon()
    {
        mh = new Mesh();
        mh.vertices = vts.ToArray();

        int num = vts.Count - 2;
        tris = new int[num * 3];
        for(int i = 0; i < num; i++)
        {
            tris[i * 3] = 0;
            tris[i * 3 + 1] = i + 1;
            tris[i * 3 + 2] = i + 2;
        }
        mh.triangles = tris;

        // 法线
        normals = new Vector3[vts.Count];
        for(int i=0;i<vts.Count;i++)
        {
            normals[i] = new Vector3(0, 0, 1);
        }
        mh.normals = normals;

        mh.RecalculateBounds();
        mh.RecalculateNormals();
        mh.RecalculateTangents();
        targetFilter.mesh = mh;
    }

    protected void Reset()
    {
        vts.Clear();
        targetFilter.mesh = null;
    }

    private void OnDrawGizmos()
    {
        if(vts.Count == 0)
            return;
        Gizmos.color = Color.red;

        for(int i=0;i<vts.Count;i++)
        {
            Vector3 worldPoint = transform.TransformPoint(vts[i]);
            // 注意使用世界坐标
            Gizmos.DrawWireSphere(worldPoint, 0.2f);
        }
    }

    private void OnGUI()
    {
        if(vts.Count == 0)
            return;
        GUI.color = Color.yellow;
        for(int i =0;i<vts.Count;i++)
        {
            Vector3 worldPoint = transform.TransformPoint(vts[i]);
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPoint);
            // 注意GUI画y是从上往下画的，但是世界坐标y是从下往上
            screenPoint.y = Camera.main.pixelHeight - screenPoint.y;
            GUI.Label(new Rect(screenPoint, new Vector2(100, 80)), i.ToString());
        }

    }
}
