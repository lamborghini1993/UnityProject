using UnityEngine;
using UnityEditor;

public class Dialog : ScriptableWizard {

    public int addHealth = 10;
    public int addSpeed = 1;

    [MenuItem("Tools/CreateWizard")]
    static void CreateWizard()
    {
        // 标题  按钮名
        DisplayWizard<Dialog>("统一修改敌人","改变值");
    }

    // 检查对话框create按钮的点击，按钮名更改但是检查的函数名还是固定为OnWizardCreate
    private void OnWizardCreate()
    {
        GameObject[] gos = Selection.gameObjects;
        foreach(GameObject go in gos)
        {
            CompleteProject.EnemyHealth hp = go.GetComponent<CompleteProject.EnemyHealth>();
            Undo.RecordObject(hp,"change health and speed");  //记录对象，可进行撤销操作
            hp.startingHealth += addHealth;
            hp.sinkSpeed += addSpeed;
        }
        ShowNotification(new GUIContent("有"+gos.Length+"个游戏物体值被改变"));

        EditorPrefs.SetInt("xx", addHealth);
        EditorPrefs.SetInt("yy", addSpeed);
    }

    private void SaveData()
    {
        EditorPrefs.SetInt("xx", addHealth);
        EditorPrefs.SetInt("yy", addSpeed);
    }

    private void OnEnable()
    {
        addHealth = EditorPrefs.GetInt("xx", addHealth);
        addSpeed = EditorPrefs.GetInt("yy", addSpeed);
    }
}
