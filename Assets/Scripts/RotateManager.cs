using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RotateManager : MonoBehaviour
{
    public int port = 12000;
    private Quaternion start_gyro;
    private long _lastOscTimeStamp = -1;

    void Awake()
    {
        Input.gyro.enabled = true;
    }

    void Start()
    {
        OSCHandler.Instance.Init("", 0, port);
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

                    string address = (string)item.Value.packets[i].Data[0];
                    string[] data = address.Split(',');
                    Quaternion hmdGyro = new Quaternion(
                        float.Parse(data[0]),
                        float.Parse(data[1]),
                        float.Parse(data[2]),
                        float.Parse(data[3])
                    );

                    if (start_gyro.x == 0.0)
                    {
                        if (hmdGyro.x == 0.0f) return;
                        Quaternion b = Quaternion.Euler(0, 0, 90) * hmdGyro;
                        start_gyro = b;
                    }
                    else
                    {
                        Quaternion b = Quaternion.Inverse(start_gyro) * Quaternion.Euler(0, 0, 90) * hmdGyro;
                        b = new Quaternion(b.x, -b.y, b.z, b.w); ;
                        GameObject obj = GameObject.Find("Indicator");
                        obj.transform.localRotation = Quaternion.Euler(0, b.eulerAngles.y, 0);
                    }
                }
            }
        }

        SelfRotate();
    }

    void SelfRotate()
    {
        if (start_gyro.x == 0.0) return;
        Quaternion gyro = Input.gyro.attitude;
        Quaternion b = Quaternion.Inverse(start_gyro) * Quaternion.Euler(0, 0, 90) * gyro;
        b = new Quaternion(b.x, -b.y, b.z, b.w);
        Camera cam = Camera.main;
        cam.transform.localRotation = Quaternion.Euler(90, b.eulerAngles.y, 0);
    }
}
