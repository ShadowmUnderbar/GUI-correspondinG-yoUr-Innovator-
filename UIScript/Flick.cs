using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Flick : MonoBehaviour {
    private bool isRunning = true;
    private bool touch = false;
    private int touchCount;
    private float flickRotate;
    private Vector2[] touchPosition = new Vector2[2];
    private float time;
	
	void Update () {
        if (touch) time += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (isRunning)
            {
#if UNITY_EDITOR
                if (EventSystem.current.IsPointerOverGameObject()) return;
                touchPosition[0] = Input.mousePosition;
#elif UNITY_ANDROID
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
                touchCount = Input.touchCount;
                touchPosition[0] = Input.touches[touchCount].position;
#endif
                touch = true;
            }
        }
        if (touch)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonUp(0))
            {
                touchPosition[1] = Input.mousePosition;
                touchend();
#elif UNITY_ANDROID
            switch (Input.GetTouch(touchCount).phase)
            {
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                touchPosition[1] = Input.touches[touchCount].position;
                    touchend();
                    break;
            }
#endif
            }
        }
    }

    public void touchend()
    {
        if (time <= 1)
        {
            if( (touchPosition[0] -touchPosition[1] ).magnitude>100)
            {
                Vector2 rotate_ = touchPosition[0] - touchPosition[1];
                flickRotate = Mathf.Atan2(rotate_.x, rotate_.y);
            }
        }
        touch = false;
        time = 0;
    }

    public bool IsRuuning
    {
        set
        {
            isRunning = value;
        }
        get
        {
            return isRunning;
        }
    }

    public float GetFlickRotate
    {
        get
        {
            return flickRotate;
        }
    }

    public void ImageSet(Sprite UIImage)
    {
        transform.GetComponent<Image>().sprite = UIImage;
    }
}
