using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Stick : MonoBehaviour {
    private bool isRunning = true;
    private bool touch = false;
    private int touchCount;
    private Vector2[] touchPosition = new Vector2[2];
    private RectTransform stick;

    [SerializeField]
    private KeyCode debugKeyCode;
    void Start() {
        stick = transform.GetChild(0) as RectTransform;
    }

    void Update() {
        stick.position = new Vector2(transform.position.x + touchPosition[1].x * transform.lossyScale.x,
                                     transform.position.y + touchPosition[1].y * transform.lossyScale.y);
#if UNITY_EDITOR
        int x;int y;
        if (Input.GetKey(KeyCode.A)) x = -30;
        else if (Input.GetKey(KeyCode.D)) x = 30;
        else x = 0;
        if (Input.GetKey(KeyCode.S)) y = -30;
        else if (Input.GetKey(KeyCode.W)) y = 30;
        else y = 0;
        touchPosition[1] = new Vector2(x,y);
#elif UNITY_ANDROID
        if (touch)
        {
            switch (Input.GetTouch(touchCount).phase)
            {
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    touchPosition[1] = new Vector2(Mathf.Clamp(transform.position.x + Input.touches[touchCount].position.x, -30, 30),
                                                   Mathf.Clamp(transform.position.y + Input.touches[touchCount].position.y, -30, 30));
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    touchend();
                    break;
            }
        }
#endif
    }

    public void touchend()
    {
        touch = false;
    }

    public void OnPointerDown(PointerEventData deta)
    {
        if (isRunning)
        {
            touch = true;
            touchCount = Input.touchCount;
            touchPosition[0] = Input.touches[touchCount].position;
        }
    }

    /// <summary>
    /// スティックの移動距離を-1~1までの間を返す
    /// </summary>
    public Vector2 GetStickMove
    {
        get
        {
            return touchPosition[1] / 30;
        }
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

    public void ImageSet(Sprite MainStickSprite, Sprite RoughImage)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = RoughImage;
        transform.GetComponent<Image>().sprite = MainStickSprite;
    }
}
