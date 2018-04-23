using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public Camera intakingCamera, scoringCamera;

    public GameObject fakeArm, fakeWrist, wristCube, field, cubePrefab;
    public GameObject frontWheel, backWheel;
    private ArmControlBasic armControlScript;
    private WristControlBasic wristControlScript;
    
    private Rigidbody body;
	public float turnSpeed = 120.0f;
    public float maxSpeed = 120.0f;
    public float shotStrength;

    public WheelCollider fl, ml, rl, fr, mr, rr;

    enum ArmState {ZERO, INTAKE, SCALEBACK, SWITCH};
    private ArmState currentArmState = ArmState.ZERO;

    public Rigidbody leftBumper, rightBumper, frontBumper, backBumper;

	void Start() {
		body = GetComponent<Rigidbody>();
        armControlScript = fakeArm.GetComponent<ArmControlBasic>();
        wristControlScript = fakeWrist.GetComponent<WristControlBasic>();

        armControlScript.armSpeed = 100.0f;
        armControlScript.armP = 1.0f;

        wristControlScript.wristSpeed = 150.0f;
        wristControlScript.wristP = 1.0f;
	}


	// Update is called once per frame
	void Update() {
        if(Input.GetKey(KeyCode.Joystick1Button1)) {
            currentArmState = ArmState.ZERO;
        } else if(Input.GetKey(KeyCode.Joystick1Button0)) {
            currentArmState = ArmState.INTAKE;
        } else if(Input.GetKey(KeyCode.Joystick1Button3)) {
            currentArmState = ArmState.SCALEBACK;
        } else if(Input.GetKey(KeyCode.Joystick1Button2)) {
            currentArmState = ArmState.SWITCH;
        }
        switch (currentArmState) {
            case ArmState.ZERO:
                armControlScript.SetArmPositionPID(-63.0f);
                wristControlScript.SetWristPositionPID(70.0f);
                break;

            case ArmState.INTAKE:
                if (armControlScript.GetArmAngle() > -20.0f) {
                    armControlScript.SetArmPositionPID(-63.0f);
                    wristControlScript.SetWristPositionPID(70.0f);
                }
                else {
                    armControlScript.SetArmPositionPID(-63.0f);
                    wristControlScript.SetWristPositionPID(-42.5f);
                }
                break;

            case ArmState.SCALEBACK:
                if (wristControlScript.GetWristAngle() < 0.0f) {
                    armControlScript.SetArmPositionPID(-63.0f);
                    wristControlScript.SetWristPositionPID(70.0f);
                } else {
                    armControlScript.SetArmPositionPID(70.0f);
                    wristControlScript.SetWristPositionPID(35.0f);
                }
                break;

            case ArmState.SWITCH:
                armControlScript.SetArmPositionPID(-63.0f);
                wristControlScript.SetWristPositionPID(10.0f);
                break;

            default:
                armControlScript.SetArmPositionPID(-63.0f);
                wristControlScript.SetWristPositionPID(70.0f);
                break;
        }

        if(Input.GetAxis("Fire1") > 0.25 && wristCube.active && !Input.GetKey(KeyCode.Joystick1Button0)) {
            wristCube.SetActive(false);
            GameObject newCube = Instantiate(cubePrefab, wristCube.transform.position + (2.25f * (frontWheel.transform.position - backWheel.transform.position)), wristCube.transform.rotation);
            newCube.GetComponent<CubeIntakeTest>().enabled = true;
            newCube.GetComponent<Rigidbody>().AddForce((shotStrength * (frontWheel.transform.position - backWheel.transform.position) + body.velocity), ForceMode.VelocityChange);
        }

        if(Input.GetKey(KeyCode.Joystick1Button7)) {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (leftBumper.transform.position.y > 6) {
            leftBumper.AddForce(new Vector3(0, -100000.0f * (leftBumper.transform.position.y - 6.0f), 0));
        }
        if (rightBumper.transform.position.y > 6) {
            rightBumper.AddForce(new Vector3(0, -100000.0f * (rightBumper.transform.position.y - 6.0f), 0));
        }
        if (frontBumper.transform.position.y > 6) {
            frontBumper.AddForce(new Vector3(0, -100000.0f * (frontBumper.transform.position.y - 6.0f), 0));
        }
        if (backBumper.transform.position.y > 6) {
            backBumper.AddForce(new Vector3(0, -100000.0f * (backBumper.transform.position.y - 6.0f), 0));
        }

        if((currentArmState == ArmState.INTAKE || currentArmState == ArmState.SWITCH || currentArmState == ArmState.ZERO)) {
            intakingCamera.enabled = true;
        } else {
            intakingCamera.enabled = false;
        }
        if(currentArmState == ArmState.SCALEBACK) {
             scoringCamera.enabled = true;
         } else {
             scoringCamera.enabled = false;
         }
    }

    void FixedUpdate() {
        /* Vector3 movement = transform.forward * Input.GetAxis("Vertical") * Time.deltaTime;
		float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;

		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

        Vector3 upDownForce = new Vector3(0, transform.position.y * -0.15f, 0);

		body.MoveRotation (body.rotation * turnRotation);
		body.MovePosition (body.position + movement + upDownForce);
        body.AddForce(new Vector3(0, transform.position.y * -100000.0f, 0)); */

        Vector3 movement = Vector3.zero;
        float turn = 0;
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f) {
            movement = (new Vector3(transform.forward.x, transform.forward.y, transform.forward.z)) * (1 * (Mathf.Sqrt(Mathf.Abs(Input.GetAxis("Vertical"))) * Mathf.Sign(Input.GetAxis("Vertical")))) * maxSpeed * Time.deltaTime;
        } else {
            movement = Vector3.zero;
        }
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f) {
            turn = (Mathf.Pow((Mathf.Abs(Input.GetAxis("Horizontal"))), 4) * Mathf.Sign(Input.GetAxis("Horizontal"))) * turnSpeed * Time.deltaTime;
        } else {
            turn = 0;
        }
        Vector3 upDownForce = new Vector3(0, transform.position.y * -0.15f, 0);

        body.AddRelativeTorque(new Vector3(0, turn, 0));
        body.AddForceAtPosition(0.166f * movement, fl.transform.position, ForceMode.VelocityChange);
        body.AddForceAtPosition(0.166f * movement, ml.transform.position, ForceMode.VelocityChange);
        body.AddForceAtPosition(0.166f * movement, rl.transform.position, ForceMode.VelocityChange);
        body.AddForceAtPosition(0.166f * movement, fr.transform.position, ForceMode.VelocityChange);
        body.AddForceAtPosition(0.166f * movement, mr.transform.position, ForceMode.VelocityChange);
        body.AddForceAtPosition(0.166f * movement, rr.transform.position, ForceMode.VelocityChange);

        body.AddForce(-130.0f * body.velocity);
        body.AddTorque(-60000.0f * body.angularVelocity);
    }

    public string GetArmWristReadout() {
        return "Arm Position: " + armControlScript.GetArmAngle().ToString() + " Wrist Position: " + wristControlScript.GetWristAngle().ToString();
    }

}