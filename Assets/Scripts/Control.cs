using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour
{
	public string id = "";

	bool controlEnabled = true;
	bool state = false;
	float stateChangeTime = 0.0f;

	public void Press()
	{
		if (!controlEnabled || state) return;

		state = true;
		stateChangeTime = Time.time;
		transform.Translate(new Vector3(-0.02f, 0.0f, 0.0f));
	}

	public void Release()
	{
		if (!state) return;

		state = false;
		stateChangeTime = Time.time;
		transform.Translate(new Vector3(0.02f, 0.0f, 0.0f));
	}

	public void SetEnabled(bool state)
	{
		controlEnabled = state;
	}

	public bool IsPressed()
	{
		return state;
	}

	public bool JustPressed()
	{
		return (state && stateChangeTime == Time.time);
	}

	public bool JustReleased()
	{
		return (!state && stateChangeTime == Time.time);
	}

	public float StateDuration()
	{
		return Time.time - stateChangeTime;
	}
}
