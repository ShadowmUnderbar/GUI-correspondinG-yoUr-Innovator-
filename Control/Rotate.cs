using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    [SerializeField]
    private Stick stick;
    private Move move;
    [SerializeField]
    private float rotationSpeed;
    void Start () {
        move = GetComponent<Move>();
	}
	
	void Update () {
        if (stick)
        {
            if (stick.GetStickMove != new Vector2(0, 0))
            {
                RotateChange(stick.GetStickMove);
            }
        }
        else if (move.StickGetSet.GetStickMove != new Vector2(0, 0))
        {
            RotateChange(move.StickGetSet.GetStickMove);
        }
	}

    void RotateChange(Vector2 Rotate)
    {
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(Rotate.x,0,Rotate.y));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);   
    }

    public float SpeedGetSet
    {
        get
        {
            return rotationSpeed;
        }
        set
        {
            rotationSpeed = value;
        }
    }

    public void StickSet(GameObject MoveStick)
    {
        stick = MoveStick.GetComponent<Stick>();
    }
}
