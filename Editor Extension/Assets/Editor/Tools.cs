using UnityEngine;
using UnityEditor;

public class Tools
{

    [MenuItem("Tools/Test", false, priority = 9)]
    static void Test()
    {
        Debug.Log("Test");
    }

    // 每个菜单栏的priority默认值为1000
    [MenuItem("Tools/Test2",false,priority = 12)]
    static void Test2()
    {
        Debug.Log("test2");
    }

    [MenuItem("Tools/ShowInfo")]
    static void ShowInfo()
    {
        Debug.Log(Selection.activeGameObject.name); // 获取第一个选择的游戏物体名

    }

    [MenuItem("Tools/MyDelete %t", true)]
    static bool ValidMyDelete()
    {
        if (Selection.objects.Length > 0)
            return true;
        return false;
    }

    [MenuItem("Tools/MyDelete %t",false)]
    static void MyDelete()
    {
        foreach(GameObject o in Selection.objects)
        {
            // GameObject.DestroyImmediate(o); //脚本删除不能撤销
            Undo.DestroyObjectImmediate(o);     //注册的操作，删除了可以撤销
        }
    }
}
