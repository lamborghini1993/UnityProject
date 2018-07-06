using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    [MenuItem("CONTEXT/PlayerHealth/InitHealth")]
    static void InitHealth(MenuCommand cmd)
    {
        PlayerHealth health = cmd.context as PlayerHealth;  //获取对应的组件
        health.startingHealth = 200;    //设置组件对应的值
        Debug.Log("InitHealth");
    }


}
