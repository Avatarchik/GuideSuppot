using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RotateCamera : MonoBehaviour
{
    private Quaternion start_gyro;
    private Quaternion initialRotation; 
    private long _lastOscTimeStamp = -1;

    void Awake()
    {
        Input.gyro.enabled = true;
    }

    void Start()
    {
        OSCHandler.Instance.Init("", 0, 12000);
        initialRotation = transform.rotation;
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
                    Quaternion a = new Quaternion(
                        float.Parse(data[0]),
                        float.Parse(data[1]),
                        float.Parse(data[2]),
                        float.Parse(data[3])
                    );
                    if (start_gyro.x == 0.0)
                    {
                        Quaternion b = Quaternion.Euler(0, 0, 90) * a;
                        start_gyro = b;
                    }
                    else
                    {
                        Quaternion b = Quaternion.Inverse(start_gyro) * Quaternion.Euler(0, 0, 90) * a;
                        this.transform.localRotation = new Quaternion(-b.x, -b.y, -b.z, b.w);
                    }
                }
            }
        }
    }
}
