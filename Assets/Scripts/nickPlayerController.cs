using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nickPlayerController : MonoBehaviour {

	private Rigidbody m_RigidBody;
	public float speed = 12f;
	public float turnSpeed = 120;
	private bool move;
	private float movementInputValue;
	private float turnInputValue;

	void Start (){
	  
		m_RigidBody = GetComponent<Rigidbody> ();
		m_RigidBody.isKinematic = false;
	}


	// Update is called once per frame
	void Update(){
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
    }

	void FixedUpdate () {

		Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;
		float turn = turnInputValue * turnSpeed * Time.deltaTime;

		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

		m_RigidBody.MoveRotation (m_RigidBody.rotation * turnRotation);
		m_RigidBody.MovePosition (m_RigidBody.position + movement);
	}	

}
