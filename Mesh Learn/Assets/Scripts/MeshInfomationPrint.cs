using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshInfomationPrint : MeshDrawBase {

    private void Awake()
    {
    }
    protected override void DrawMesh()
    {
    }

    private void OnDrawGizmos()
    {
        targetFilter = GetComponent<MeshFilter>();
        mh = targetFilter.sharedMesh;
        Gizmos.color = Color.red;
        for(int i = 0; i < mh.vertices.Length; i++)
        {
            Vector3 tmp = transform.TransformPoint(mh.vertices[i]);
            Gizmos.DrawSphere(tmp, 0.2f);
        }
    }

}
