using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeIntakeTest : MonoBehaviour {

    Rigidbody body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if(isContactingRight && isContactingLeft) {
            this.gameObject.SetActive(false);
            IntakeManager script = leftIntake.GetComponent<IntakeManager>();
            script.wristCube.SetActive(true);
        }
	}

    bool isContactingLeft = false, isContactingRight = false;
    GameObject leftIntake = null, rightIntake = null;

    private void OnCollisionEnter(Collision collision) {
        if(collision.other.gameObject.tag == "Left Intake Wheel") {
            isContactingLeft = true;
            leftIntake = collision.other.gameObject;
        }
        if(collision.other.gameObject.tag == "Right Intake Wheel") {
            isContactingRight = true;
            rightIntake = collision.other.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.other.gameObject.tag == "Left Intake Wheel") {
            isContactingLeft = false;
            leftIntake = null;
        }
        if (collision.other.gameObject.tag == "Right Intake Wheel") {
            isContactingRight = false;
            rightIntake = null;
        }
    }

}