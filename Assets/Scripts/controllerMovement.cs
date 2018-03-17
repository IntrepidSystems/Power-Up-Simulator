using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class controllerMovement : MonoBehaviour {

	public float moveSpeed;
	public float turnSpeed;

	void Start ()
	{
		moveSpeed = 10f;
		turnSpeed = 200f;
	}

	void Update ()
	{
		transform.Translate (moveSpeed*Input.GetAxis ("Horizontal") * Time.deltaTime, 0f, moveSpeed*Input.GetAxis ("Vertical") * Time.deltaTime, Space.World);

		Rotate (Input.GetAxis ("Rotate"));

	}
	void Rotate(float input)
	{
		if (Math.Abs (input) > 0.25)
			transform.Rotate (0f, turnSpeed * input * Time.deltaTime, 0f);
		else
			transform.Rotate (0f, 0f, 0f);
	}
}