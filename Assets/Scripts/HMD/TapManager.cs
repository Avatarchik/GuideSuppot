using UnityEngine;
using System.Collections;

public class TapManager : MonoBehaviour
{
    private bool isDoubleTapStart;
    private float doubleTapTime;
    private GameObject canvas;

    void Awake()
    {
        canvas = GameObject.Find("Canvas");
    }

    void Update()
    {
        bool isDoubleTapped = DetectDoubleTapped();
        if (isDoubleTapped)
        {
            canvas.SetActive(!canvas.activeSelf);
        }
    }

    bool DetectDoubleTapped()
    {
        if (isDoubleTapStart)
        {
            doubleTapTime += Time.deltaTime;
            if (doubleTapTime < 0.3f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isDoubleTapStart = false;
                    doubleTapTime = 0.0f;

                    return true;
                }
            }
            else
            {
                isDoubleTapStart = false;
                doubleTapTime = 0.0f;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDoubleTapStart = true;
            }
        }

        return false;
    }
}
