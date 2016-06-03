using UnityEngine;
using System.Collections;

public class LineRenderer : MonoBehaviour
{
    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 3, Screen.height), "");
        GUI.Box(new Rect(Screen.width / 4 - 1.5f, 0, 3, Screen.height), "");
        GUI.Box(new Rect(Screen.width / 2 - 1.5f, 0, 3, Screen.height), "");
        GUI.Box(new Rect(Screen.width * 3 / 4 - 1.5f, 0, 3, Screen.height), "");
        GUI.Box(new Rect(Screen.width - 3f, 0, 3, Screen.height), "");
    }
}
