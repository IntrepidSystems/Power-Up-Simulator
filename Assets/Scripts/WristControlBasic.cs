using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristControlBasic : MonoBehaviour
{

    public float wristSpeed = 1.0f;
    Rigidbody body;

    // Use this for initialization
    void Start() {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        float movement = 0;
        if (Input.GetKey("d")) {
            movement += wristSpeed;
        }
        if (Input.GetKey("a")) {
            movement -= wristSpeed;
        }
        body.transform.Rotate(new Vector3(movement, 0, 0));
    }

}