using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	public float rotateSpeed = 10f;

	void Update () {
		transform.Rotate (Vector3.up * rotateSpeed * Time.deltaTime);
	}
}
