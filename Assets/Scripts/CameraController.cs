using UnityEngine;
using System.Collections;

enum MouseButtonDown
{
    MBD_LEFT = 0,
    MBD_RIGHT,
    MBD_MIDDLE,
};

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 oldPos;
    private Camera mainCam;

    void Start()
    {
        mainCam = gameObject.GetComponent<Camera>();

        return;
    }

    void Update()
    {
        this.mouseEvent();

        return;
    }

    void mouseEvent()
    {
        float delta = Input.GetAxis("Mouse ScrollWheel");
        if (delta != 0.0f)
            this.mouseWheelEvent(delta);

        if (Input.GetMouseButtonDown((int)MouseButtonDown.MBD_LEFT) ||
        Input.GetMouseButtonDown((int)MouseButtonDown.MBD_MIDDLE) ||
        Input.GetMouseButtonDown((int)MouseButtonDown.MBD_RIGHT))
            this.oldPos = Input.mousePosition;

        this.mouseDragEvent(Input.mousePosition);

        return;
    }

    void mouseWheelEvent(float delta)
    {
        Vector3 toPos;

        delta *= 0.2f;
        if (delta > 0.0f)
        {
            toPos = mainCam.transform.TransformDirection(Vector3.forward) + new Vector3(0.0f, 0.0f, delta);
        }
        else
        {
            toPos = mainCam.transform.TransformDirection(Vector3.back) + new Vector3(0.0f, 0.0f, delta);
        }
        this.transform.position += toPos;

        return;
    }

    void mouseDragEvent(Vector3 mousePos)
    {
        Vector3 diff = mousePos - oldPos;

        if (Input.GetMouseButton((int)MouseButtonDown.MBD_RIGHT))
        {
            this.transform.Rotate(0.0f, -diff.x*0.2f, 0.0f);
        }

        this.oldPos = mousePos;

        return;
    }
}