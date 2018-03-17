using UnityEngine;
using System.Collections;

public class RobotMovement : MonoBehaviour {

	public float moveSpeed;
	public float Acceleration;

	void Start ()
	{
		moveSpeed = 10f;
	}

	void Update ()
	{
		transform.Translate (moveSpeed*Input.GetAxis ("Horizontal") * Time.deltaTime, 0f, moveSpeed*Input.GetAxis ("Vertical") * Time.deltaTime);
	}
}