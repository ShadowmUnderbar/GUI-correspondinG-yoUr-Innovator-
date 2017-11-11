using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    [SerializeField]
    private Stick stick;
    private Rigidbody rigidBody;
    [SerializeField]
    private float speed = 1;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (stick)
        {
            rigidBody.velocity = new Vector3(stick.GetStickMove.x * speed, 0,stick.GetStickMove.y * speed);
        }
	}

    public float SpeedGetSet
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
    public Stick StickGetSet
    {
        get
        {
            return stick;
        }
        set
        {
            stick = value;
        }
    }
}
