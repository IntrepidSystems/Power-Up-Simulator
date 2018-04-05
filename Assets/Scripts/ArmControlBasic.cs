using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmControlBasic : MonoBehaviour {

    public float armSpeed = 1.0f;
    Rigidbody body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float movement = 0;
        if(Input.GetKey("w")) {
            movement -= armSpeed;
        }
        if(Input.GetKey("s")) {
            movement += armSpeed;
        }
        body.transform.Rotate(new Vector3(movement, 0, 0));
	}

}