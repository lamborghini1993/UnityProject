using UnityEngine;
using UnityEditor;

public class MyWindow:EditorWindow
{

    [MenuItem("Tools/MyWindow &R")]
    static void ShowMyWindow()
    {
        MyWindow window = EditorWindow.GetWindow<MyWindow>();
        window.Show();
    }

    private string myName = "";
    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myName = GUILayout.TextField(myName);
        if(myName.Length > 0 && GUILayout.Button("创建"))
        {
            GameObject go = new GameObject(myName);
            Undo.RegisterCreatedObjectUndo(go, "Create_" + myName);
            myName = "";
        }
    }
}
