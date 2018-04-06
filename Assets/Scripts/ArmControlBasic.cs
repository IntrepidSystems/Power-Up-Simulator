using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmControlBasic : MonoBehaviour {

    public float armSpeed;
    public float armP;

    Rigidbody body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void MoveArm(float movementValue) {
        body.transform.Rotate(new Vector3(movementValue, 0, 0));
    }

    public void ManualArm(string upKey, string downKey) {
        float movement = 0;
        if (Input.GetKey(upKey)) {
            movement -= armSpeed * Time.deltaTime;
        }
        if (Input.GetKey(downKey)) {
            movement += armSpeed * Time.deltaTime;
        }
        MoveArm(movement);
    }

    public float GetArmAngle() {
        float angle = body.transform.localEulerAngles.x;
        while(angle > 180.0f) {
            angle -= 360.0f;
        }
        while(angle < -180.0f) {
            angle += 360.0f;
        }
        return angle;
    }

    public void SetArmPositionPID(float desiredAngle) {
        if(desiredAngle < -89.0f) {
            desiredAngle = -89.0f;
        } else if(desiredAngle > 57.0f) {
            desiredAngle = 57.0f;
        }

        float error = desiredAngle - GetArmAngle();
        float output = error * armP;
        
        if(output > armSpeed * Time.deltaTime) {
            output = armSpeed * Time.deltaTime;
        } else if(output < -armSpeed * Time.deltaTime) {
            output = -armSpeed * Time.deltaTime;
        }

        MoveArm(output);
    }

}