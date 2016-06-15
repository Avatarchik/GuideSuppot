using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GyroSender : MonoBehaviour
{
    public static string toIP = "";
    public static bool isSend = false;

    void Awake()
    {
        Input.gyro.enabled = true;
    }

    void Start()
    {
    }

    void Update()
    {
        if (isSend)
        {
            SendGyro();
        }
    }

    public void SendInit()
    {
        OSCHandler.Instance.Init(toIP, 12000, 0);
        isSend = true;
    }

    void SendGyro()
    {
        Quaternion gyro = Input.gyro.attitude;
        string mes = gyro.x.ToString() + ',' +
                     gyro.y.ToString() + ',' +
                     gyro.z.ToString() + ',' +
                     gyro.w.ToString();
        OSCHandler.Instance.SendMessageToClient("myRemoteLocation", toIP + ":12000", mes);
    }
}

