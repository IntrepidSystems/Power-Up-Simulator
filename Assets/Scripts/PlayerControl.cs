using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public Camera intakingCamera, scoringCamera;

    public GameObject arm, wrist, wristCube, field, cubePrefab;
    public GameObject intakeBack;
    private ArmControlBasic armControlScript;
    private WristControlBasic wristControlScript;

    private Rigidbody body;
	public float turnSpeed = 120.0f;
    public float maxSpeed = 120.0f;
    public float forwardAccel = 10.0f;
    public float forwardFriction;
    public float shotStrength;
	private bool move;
	private float movementInputValue;
	private float turnInputValue;

    enum ArmState {ZERO, INTAKE, SCALEBACK, SWITCH};
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
        if(Input.GetKey("up")) {
            movementInputValue += forwardAccel;
        }
        if (Input.GetKey("down")) {
            movementInputValue -= forwardAccel;
        }
        if(Mathf.Abs(movementInputValue) > maxSpeed) {
            movementInputValue = maxSpeed * Mathf.Sign(movementInputValue);
        }
        movementInputValue -= (forwardFriction * movementInputValue);

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
        } else if(Input.GetKey("d")) {
            currentArmState = ArmState.SWITCH;
        }
        switch (currentArmState) {
            case ArmState.ZERO:
                armControlScript.SetArmPositionPID(57.0f);
                wristControlScript.SetWristPositionPID(-65.0f);
                break;

            case ArmState.INTAKE:
                if (armControlScript.GetArmAngle() < 0) {
                    armControlScript.SetArmPositionPID(0.0f);
                    wristControlScript.SetWristPositionPID(-50.0f);
                }
                else {
                    armControlScript.SetArmPositionPID(57.0f);
                    wristControlScript.SetWristPositionPID(47.0f);
                }
                break;

            case ArmState.SCALEBACK:
                if (wristControlScript.GetWristAngle() > -20.0f) {
                    armControlScript.SetArmPositionPID(57.0f);
                    wristControlScript.SetWristPositionPID(-30.0f);
                } else {
                    armControlScript.SetArmPositionPID(-70.0f);
                    wristControlScript.SetWristPositionPID(-30.0f);
                }
                break;

            case ArmState.SWITCH:
                armControlScript.SetArmPositionPID(57.0f);
                wristControlScript.SetWristPositionPID(0.0f);
                break;

            default:
                armControlScript.SetArmPositionPID(57.0f);
                wristControlScript.SetWristPositionPID(-65.0f);
                break;
        }

        if(Input.GetKey("space") && wristCube.active) {
            wristCube.SetActive(false);
            GameObject newCube = Instantiate(cubePrefab, wristCube.transform.position + (1.5f * (wristCube.transform.position - intakeBack.transform.position)), wristCube.transform.rotation);
            newCube.GetComponent<CubeIntakeTest>().wristCube = wristCube;
            newCube.GetComponent<CubeIntakeTest>().enabled = true;
            newCube.GetComponent<Rigidbody>().AddForce((shotStrength * (wristCube.transform.position - intakeBack.transform.position)) + body.velocity, ForceMode.VelocityChange);
        }

        if((currentArmState == ArmState.INTAKE || currentArmState == ArmState.SWITCH) && armControlScript.GetArmAngle() > 30.0f && wristControlScript.GetWristAngle() > -1.0f) {
            intakingCamera.enabled = true;
        } else {
            intakingCamera.enabled = false;
        }
        if(currentArmState == ArmState.SCALEBACK && armControlScript.GetArmAngle() < -50.0f && wristControlScript.GetWristAngle() < -20.0f) {
            scoringCamera.enabled = true;
        } else {
            scoringCamera.enabled = false;
        }
    }

	void FixedUpdate() {
		Vector3 movement = transform.forward * movementInputValue * Time.deltaTime;
		float turn = turnInputValue * turnSpeed * Time.deltaTime;

		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

        Vector3 upDownForce = new Vector3(0, transform.position.y * -0.15f, 0);

		body.MoveRotation (body.rotation * turnRotation);
		body.MovePosition (body.position + movement + upDownForce);
        body.AddForce(new Vector3(0, transform.position.y * -100000.0f, 0));
	}

    public string GetArmWristReadout() {
        return "Arm Position: " + armControlScript.GetArmAngle().ToString() + " Wrist Position: " + wristControlScript.GetWristAngle().ToString();
    }

}