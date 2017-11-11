using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slide : MonoBehaviour {
    private bool isRunning = true;
    private bool touch = false;
    private int touchCount;
    enum TapType
    {
        EnemyTap,
        PositionTap
    }
    [SerializeField]
    private TapType tapType;
    private List<Transform> enemyList;
    Ray ray;

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (isRunning)
            {
#if UNITY_EDITOR
                if (EventSystem.current.IsPointerOverGameObject()) return;

#elif UNITY_ANDROID
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
                touchCount = Input.touchCount;
#endif
                touch = true;
                enemyList.Clear();
            }
        }
        if (touch)
        {
            switch (Input.GetTouch(touchCount).phase)
            {
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    TapMove();
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    touch = false;
                    break;
            }
        }
    }

    void TapMove()
    {
        if (tapType == TapType.EnemyTap)
        {
            RaycastHit hit = new RaycastHit();
#if UNITY_EDITOR
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#elif UNITY_ANDROID
            ray = Camera.main.ScreenPointToRay(Input.touches[touchCount].position);
#endif
            if (Physics.Raycast(ray, out hit))
            {
                Transform obj = hit.collider.transform;
                if (obj.tag == "Enemy")
                {
                    enemyList.Add(obj);
                }
            }
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

    public List<Transform> GetEnemyList
    {
        get
        {
            if (!touch)
            {
                return enemyList;
            }
            else return null;
        }
        set
        {
            enemyList = value;
        }
    }

    public void ImageSet(Sprite UIImage)
    {
        transform.GetComponent<Image>().sprite = UIImage;
    }
}
