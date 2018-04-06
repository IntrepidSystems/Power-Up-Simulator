using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public GameObject arm, wrist, wristCube;
    private ArmControlBasic armControlScript;
    private WristControlBasic wristControlScript;

    private Rigidbody body;
	public float speed = 12f;
	public float turnSpeed = 120;
	private bool move;
	private float movementInputValue;
	private float turnInputValue;

    enum ArmState {ZERO, INTAKE, SCALEBACK, MANUAL};
    private ArmState currentArmState = ArmState.ZERO;

	void Start() {
		body = GetComponent<Rigidbody>();
        armControlScript = arm.GetComponent<ArmControlBasic>();
        wristControlScript = wrist.GetComponent<WristControlBasic>();

        armControlScript.armSpeed = 100.0f;
        armControlScript.armP = 1.0f;

        wristControlScript.wristSpeed = 150.0f;
        wristControlScript.wristP = 1.0f;
	}


	// Update is called once per frame
	void Update() {
        movementInputValue = 0;
        if(Input.GetKey("up")) {
            movementInputValue += 10;
        }
        if (Input.GetKey("down")) {
            movementInputValue -= 10;
        }

        turnInputValue = 0;
        if (Input.GetKey("right")) {
            turnInputValue += 1;
        }
        if (Input.GetKey("left")) {
            turnInputValue -= 1;
        }

        if(Input.GetKey("a")) {
            currentArmState = ArmState.ZERO;
        } else if(Input.GetKey("s")) {
            currentArmState = ArmState.INTAKE;
        } else if(Input.GetKey("w")) {
            currentArmState = ArmState.SCALEBACK;
        }
        switch(currentArmState) {
            case ArmState.ZERO:
                armControlScript.SetArmPositionPID(57.0f);
                wristControlScript.SetWristPositionPID(-65.0f);
                break;

            case ArmState.INTAKE:
                armControlScript.SetArmPositionPID(57.0f);
                wristControlScript.SetWristPositionPID(47.0f);
                break;

            case ArmState.SCALEBACK:
                armControlScript.SetArmPositionPID(-31.0f);
                wristControlScript.SetWristPositionPID(-27.0f);
                break;

            default:
                armControlScript.SetArmPositionPID(57.0f);
                wristControlScript.SetWristPositionPID(-65.0f);
                break;
        }

        if(Input.GetKey("space") && wristCube.active) {
            wristCube.SetActive(false);
        }

        //armControlScript.ManualArm("w", "s");
        //wristControlScript.ManualWrist("a", "d");

        print(GetArmWristReadout());
    }

	void FixedUpdate() {
		Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;
		float turn = turnInputValue * turnSpeed * Time.deltaTime;

		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

		body.MoveRotation (body.rotation * turnRotation);
		body.MovePosition (body.position + movement);

        body.AddForce(new Vector3(0, (-1000 * body.transform.position.y) - 100, 0));
	}

    public string GetArmWristReadout() {
        return "Arm Position: " + armControlScript.GetArmAngle().ToString() + " Wrist Position: " + wristControlScript.GetWristAngle().ToString();
    }

}