using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorekeeper : MonoBehaviour {

    public GameObject scale, redSwitch, blueSwitch;
    public Text redScoreText, blueScoreText;

    private int redScore, blueScore;

	// Use this for initialization
	void Start () {
        redScore = 0;
        blueScore = 0;
        InvokeRepeating("UpdateScore", 0.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
        redScoreText.text = redScore.ToString();
        blueScoreText.text = blueScore.ToString();
	}

    void UpdateScore() {
        double scaleAngle = scale.transform.rotation.eulerAngles.z;
        while(scaleAngle < -180.0f) {
            scaleAngle += 360.0f;
        }
        while(scaleAngle > 180.0f) {
            scaleAngle -= 360.0f;
        }

        double redSwitchAngle = redSwitch.transform.rotation.eulerAngles.z;
        while (redSwitchAngle < -180.0f) {
            redSwitchAngle += 360.0f;
        }
        while (redSwitchAngle > 180.0f) {
            redSwitchAngle -= 360.0f;
        }

        double blueSwitchAngle = blueSwitch.transform.rotation.eulerAngles.z;
        while (blueSwitchAngle < -180.0f) {
            blueSwitchAngle += 360.0f;
        }
        while (blueSwitchAngle > 180.0f) {
            blueSwitchAngle -= 360.0f;
        }

        int totalRedAddition = 0;
        if(scaleAngle > -9.0f && scaleAngle < -4.0f) {
            totalRedAddition += 1;
        }
        if(redSwitchAngle > -4.0f && redSwitchAngle < -2.0f) {
            totalRedAddition += 1;
        }
        redScore += totalRedAddition;

        int totalBlueAddition = 0;
        if (scaleAngle < 9.0f && scaleAngle > 4.0f) {
            totalBlueAddition += 1;
        }
        if (blueSwitchAngle < 4.0f && blueSwitchAngle > 2.0f) {
            totalBlueAddition += 1;
        }
        blueScore += totalBlueAddition;
    }

}