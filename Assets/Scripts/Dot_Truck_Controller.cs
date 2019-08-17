using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dot_Truck : System.Object {
	public WheelCollider leftWheel;
	public GameObject leftWheelMesh;
	public WheelCollider rightWheel;
	public GameObject rightWheelMesh;
	public bool motor;
	public bool steering;
	public bool reverseTurn;
}

public class Dot_Truck_Controller : MonoBehaviour {

	public float maxMotorTorque;
	public float maxSteeringAngle;
	public List<Dot_Truck> truckInfos;
	private Rigidbody rb;
	private GameObject gc;
	private GameObject aux;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		gc = GameObject.Find ("GameController");

	}

	public void VisualizaWheel (Dot_Truck wheelPair) {
		Quaternion rot;
		Vector3 pos;
		wheelPair.leftWheel.GetWorldPose (out pos, out rot);
		wheelPair.leftWheelMesh.transform.position = pos;
		wheelPair.leftWheelMesh.transform.rotation = rot;
		wheelPair.rightWheel.GetWorldPose (out pos, out rot);
		wheelPair.rightWheelMesh.transform.position = pos;
		wheelPair.rightWheelMesh.transform.rotation = rot;
	}

	void Update () {
		float motor = maxMotorTorque * Input.GetAxis ("Vertical");
		float steering = maxSteeringAngle * Input.GetAxis ("Horizontal");
		float brakeTorque = Mathf.Abs (Input.GetAxis ("Jump"));

		if (brakeTorque > 0.001) {
			brakeTorque = maxMotorTorque;
			motor = 0;
			rb.drag = 1.5f;
		} else {
			brakeTorque = 0;
			rb.drag = 0;
		}

		foreach (Dot_Truck truckInfo in truckInfos) {
			if (truckInfo.steering == true) {
				truckInfo.leftWheel.steerAngle = truckInfo.rightWheel.steerAngle = ((truckInfo.reverseTurn) ? -1 : 1) * steering;
			}
			if (truckInfo.motor == true) {
				truckInfo.leftWheel.motorTorque = motor;
				truckInfo.rightWheel.motorTorque = motor;
			}

			truckInfo.leftWheel.brakeTorque = brakeTorque;
			truckInfo.rightWheel.brakeTorque = brakeTorque;

			VisualizaWheel (truckInfo);
		}
	}

	IEnumerator RecuperarVeiculoTombado () {
		yield return new WaitForSeconds (5.0f);
		transform.rotation = Quaternion.Euler (0, 180, 0);
		transform.position = new Vector3 (transform.position.x, 3, transform.position.z);

		if (gc.GetComponent<GameController> ().numCheckPoint == 0 || gc.GetComponent<GameController> ().numCheckPoint == 4) {
			aux = GameObject.Find ("finish line(cp1)");
			transform.LookAt (aux.transform);
		} else if (gc.GetComponent<GameController> ().numCheckPoint == 1) {
			aux = GameObject.Find ("cp2");
			transform.LookAt (aux.transform);
		} else if (gc.GetComponent<GameController> ().numCheckPoint == 2) {
			aux = GameObject.Find ("cp3");
			transform.LookAt (aux.transform);
		} else if (gc.GetComponent<GameController> ().numCheckPoint == 3) {
			aux = GameObject.Find ("cp4");
			transform.LookAt (aux.transform);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "road") {
			StartCoroutine (RecuperarVeiculoTombado ());
		}
	}
}