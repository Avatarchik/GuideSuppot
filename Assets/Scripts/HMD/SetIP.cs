using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetIP : MonoBehaviour
{
    public InputField inputField;
    private bool isReset = false;
    private GameObject canvas;

    void Awake()
    {
        canvas = GameObject.Find("Canvas");
    }

    void Start()
    {
        if (GyroSender.toIP == "")
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    public void Set()
    {
        string ip = inputField.text;

        GameObject obj = GameObject.Find("GyroSender");
        GyroSender.toIP = ip;
        GyroSender gyroSender = obj.GetComponent<GyroSender>();

        if (isReset)
        {
            OSCHandler.Instance.DestroyInstance();
        }
        gyroSender.SendInit();

        if (!isReset) isReset = true;
        canvas.SetActive(false);
    }
}
