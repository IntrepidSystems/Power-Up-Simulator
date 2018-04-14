using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntakeManager : MonoBehaviour {

    public GameObject other;
    public GameObject wristCube;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        this.gameObject.transform.position = other.transform.position;
        this.gameObject.transform.rotation = other.transform.rotation;
    }

}