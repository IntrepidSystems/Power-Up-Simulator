using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeIntakeTest : MonoBehaviour {

    Rigidbody body;
    public GameObject wristCube;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerEnter(Collider other) {
        if(other.tag.Equals("Intake") && !wristCube.active) {
            wristCube.active = true;
            Destroy(this.gameObject);
        }
    }

}