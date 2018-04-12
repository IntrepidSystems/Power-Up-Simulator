using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManagingScript : MonoBehaviour {

    public GameObject scoringPlatform;
    public float minimumAngleForScored, maximumAngleForScored;
    public float minimumAngleForUnscored, maximumAngleForUnscored;
    public float minimumAngleForAntiscored, maximumAngleForAntiscored;
    public bool isBright;

    enum LightState {SCORED, UNSCORED, ANTISCORED};
    private LightState currentLightState = LightState.UNSCORED;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        double angle = scoringPlatform.transform.rotation.eulerAngles.z;
        while(angle > 180.0) {
            angle -= 360.0;
        }
        while(angle < -180.0) {
            angle += 360.0;
        }

        if(angle <= maximumAngleForScored && angle >= minimumAngleForScored) {
            currentLightState = LightState.SCORED;
        } else if(angle <= maximumAngleForUnscored && angle >= minimumAngleForUnscored) {
            currentLightState = LightState.UNSCORED;
        } else if(angle <= maximumAngleForAntiscored && angle >= minimumAngleForAntiscored) {
            currentLightState = LightState.ANTISCORED;
        } else {
            currentLightState = LightState.UNSCORED;
        }

        switch(currentLightState) {
            case LightState.SCORED:
                if(isBright) {
                    ((Behaviour)GetComponent("Halo")).enabled = false;
                } else {
                    ((Behaviour)GetComponent("Halo")).enabled = true;
                }
                break;

            case LightState.UNSCORED:
                if (isBright) {
                    ((Behaviour)GetComponent("Halo")).enabled = true;
                }
                else {
                    ((Behaviour)GetComponent("Halo")).enabled = false;
                }
                break;

            case LightState.ANTISCORED:
                ((Behaviour)GetComponent("Halo")).enabled = false;
                break;

            default:
                if (isBright) {
                    ((Behaviour)GetComponent("Halo")).enabled = true;
                }
                else {
                    ((Behaviour)GetComponent("Halo")).enabled = false;
                }
                break; 
        }

        print(angle);
    }



}