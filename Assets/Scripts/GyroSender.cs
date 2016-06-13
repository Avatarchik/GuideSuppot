using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GyroSender : MonoBehaviour
{
    public string toIP = "192.168.10.6";
    private long _lastOscTimeStamp = -1;

    void Awake()
    {
        Input.gyro.enabled = true;
    }

    void Start()
    {
        OSCHandler.Instance.Init("192.168.10.8", 12000, 0);
        sendGyro();
    }

    void Update()
    {
        OSCHandler.Instance.UpdateLogs();
        foreach (KeyValuePair<string, ServerLog> item in OSCHandler.Instance.Servers)
        {
            for (int i = 0; i < item.Value.packets.Count; i++)
            {
                if (_lastOscTimeStamp < item.Value.packets[i].TimeStamp)
                {
                    _lastOscTimeStamp = item.Value.packets[i].TimeStamp;

                    string address = item.Value.packets[i].Address;

                    Debug.Log(address);
                }
            }
        }

        formatteGyro();
        sendGyro();
    }

    void formatteGyro()
    {
        Quaternion gyro = Input.gyro.attitude;
        //formattedGyro = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
    }

    void sendGyro()
    {
        Quaternion gyro = Input.gyro.attitude;
        string mes = gyro.x.ToString() + ',' +
                     gyro.y.ToString() + ',' +
                     gyro.z.ToString() + ',' +
                     gyro.w.ToString();
        OSCHandler.Instance.SendMessageToClient("myRemoteLocation", toIP + ":12000", mes);
    }
}

