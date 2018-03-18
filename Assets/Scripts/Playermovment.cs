using UnityEngine;
using System.Collections;

public class Playermovment : MonoBehaviour
{
	public float turnSpeed = 50f;

	public float _Velocity = 0.0f; //current velocity
	public float _MaxVelocity = 1.0f;
	public float _MinVelocity = -1.0f;
	public float _Acc = 0.0f; //current acceleration
	public float _AccSpeed = 0.1f; //Acceleration multiplier
	public float _MaxAcc = 1.0f; //max acceleration
	public float _MinAcc = -1.0f; //self explanatory


	void Update()
	{
		if (Input.GetButtonDown ("Vertical"))
			_Acc += _AccSpeed;

		if (Input.GetButtonUp ("Vertical"))
			_Velocity = 0f;
		
		if (Input.GetButtonDown("Horizontal"))
			_Acc -= _AccSpeed;

		if (Input.GetKey (KeyCode.LeftArrow))
			transform.Rotate (Vector3.up, -turnSpeed * Time.deltaTime);

		if (Input.GetKey (KeyCode.RightArrow))
			transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);

		if (_Acc > _MaxAcc)
			_Acc = _MaxAcc;
		else if (_Acc < _MinAcc)
			_Acc = _MinAcc;

		_Velocity += _Acc;

		if (_Velocity > _MaxVelocity)
			_Velocity = _MaxVelocity;
	
		if (_Velocity < _MinVelocity)
			_Velocity = _MinVelocity;

		transform.Translate (Vector3.forward * _Velocity * Time.deltaTime);
	}
}

