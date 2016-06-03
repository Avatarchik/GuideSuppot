using UnityEngine;
using System.Collections;

public class GyroSender : MonoBehaviour
{
    private Quaternion formattedGyro;
    public string toIP = "0.0.0.0";

    void Awake()
    {
        Input.gyro.enabled = true;
        formatteGyro();
    }

    void Start()
    {
    }

    void Update()
    {
        formatteGyro();
    }

    void formatteGyro()
    {
        Quaternion gyro = Input.gyro.attitude;
        formattedGyro = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
    }

    void sendGyro()
    {

    }
}

