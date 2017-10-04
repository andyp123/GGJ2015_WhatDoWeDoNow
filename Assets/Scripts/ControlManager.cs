using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this gets kind of hacky!!

public class ControlManager : MonoBehaviour
{
	public GameObject[] controls;
	public GameObject rechargeBar;
	public Level level;

	bool controlsEnabled = true;
	float controlsDisabledUntilTime = 0.0f;

	void Start()
	{
		if (level == null)
		{
			Debug.LogError("Level not assigned");
		}

		foreach (GameObject go in controls)
		{
			if (go.GetComponent<Control>() == null)
			{
				Debug.LogError("Missing Control script");
			}
		}
	}

	void Update()
	{
		if (!controlsEnabled && Time.time > controlsDisabledUntilTime)
		{
			SetControlState(true);
		}
	}

	// check controls in late update to make sure the controls have been updated
	void LateUpdate()
	{
		Recharge progressBar = rechargeBar.gameObject.GetComponent<Recharge>();

		foreach (GameObject go in controls)
		{
			Control control = go.GetComponent<Control>();
			if (control.JustPressed())
			{
				//Debug.Log(control.id + " was just pressed.");
				float controlReenableDelay = 0.5f;

				switch(control.id)
				{
				case "move_left":
					level.TryMove(-1, 0);
					break;
				case "move_right":
					level.TryMove(1, 0);
					break;
				case "move_up":
					level.TryMove(0, 1);
					break;
				case "move_down":
					level.TryMove(0, -1);
					break;
				case "ping":
					controlReenableDelay = 1.0f;
					break;
				case "analyze":
					controlReenableDelay = 5.0f;
					break;
				case "send":
					controlReenableDelay = 4.0f;
					break;
				case "scan":
					controlReenableDelay = 2.0f;
					break;
				}

				SetControlState(false, controlReenableDelay);
				progressBar.StartRecharge(controlReenableDelay);
				// run anim
			}
		}
	}

	void SetControlState(bool state, float reenableDelay = 1.0f)
	{
		foreach (GameObject go in controls)
		{
			Control control = go.GetComponent<Control>();
			control.SetEnabled(state);
		}

		controlsEnabled = state;
		if (!state)
		{
			controlsDisabledUntilTime = Time.time + reenableDelay;
		}
	}
}
