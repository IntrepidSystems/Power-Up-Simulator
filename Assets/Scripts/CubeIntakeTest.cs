using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeIntakeTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerEnter(Collider other) {
        if(other.tag.Equals("Intake")) {
            this.gameObject.SetActive(false);
        }
    }

}