using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour {
    private bool isRunning = true;
    private bool touch = false;
    [SerializeField]
    private KeyCode debugKeyCode;
    enum PushMode
    {
        ShortPush,
        LongPush
    }
    [SerializeField]
    private PushMode pushMode;
	
	void Update () {
#if UNITY_EDITOR
        if (Input.GetKeyDown(debugKeyCode))
        {
            ButtonPush();
        }
#elif UNITY_ANDROID
        if (touch)
        {
            switch (Input.GetTouch(touchCount).phase)
            {
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    ButtonPush();
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    touchend();
                    break;
            }
        }
#endif
    }

    void ButtonPush()
    {
        if (pushMode == PushMode.ShortPush) touch = false;
    }

    public void touchend()
    {
        touch = false;
    }

    public void OnPointerDown(PointerEventData deta)
    {
        if (isRunning) touch = true;
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

    public void ImageSet(Sprite ButtonImage)
    {
        transform.GetComponent<Image>().sprite = ButtonImage;
    }
}
