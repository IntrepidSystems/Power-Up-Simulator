using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristControlBasic : MonoBehaviour
{

    public float wristSpeed;
    public float wristP;
    public GameObject arm;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void MoveWrist(float movementValue) {
        this.gameObject.transform.Rotate(new Vector3(movementValue, 0, 0));
    }

    public void ManualWrist(string upKey, string downKey) {
        float movement = 0;
        if (Input.GetKey(upKey)) {
            movement -= wristSpeed * Time.deltaTime;
        }
        if (Input.GetKey(downKey)) {
            movement += wristSpeed * Time.deltaTime;
        }
        MoveWrist(movement);
    }

    public float GetWristAngle() {
        float angle = this.gameObject.transform.localEulerAngles.x;
        while (angle > 180.0f) {
            angle -= 360.0f;
        }
        while (angle < -180.0f) {
            angle += 360.0f;
        }
        return angle;
    }

    public void SetWristPositionPID(float desiredAngle) {
        float error = desiredAngle - GetWristAngle();
        float output = error * wristP;

        if (output > wristSpeed * Time.deltaTime) {
            output = wristSpeed * Time.deltaTime;
        }
        else if (output < -wristSpeed * Time.deltaTime) {
            output = -wristSpeed * Time.deltaTime;
        }

        MoveWrist(output);
    }

}