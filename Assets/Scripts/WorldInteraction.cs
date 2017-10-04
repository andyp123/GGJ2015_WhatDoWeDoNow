using UnityEngine;
using System.Collections;

public class WorldInteraction : MonoBehaviour
{
	public Camera cam;
	public bool useCursor = true;

	Control hotControl = null;
	Control activeControl = null;
	
	void Start()
	{
		if (cam == null)
		{
			Debug.LogError("Camera not set");
		}
		else if (!useCursor)
		{
			cam.gameObject.GetComponent<MouseLook>().enabled = true;
			cam.gameObject.GetComponent<Crosshair>().enabled = true;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
	
	void Update()
	{
		hotControl = FindTargettedControl();

		// if hot control changes, or is lost, release active control
		if (activeControl != null && activeControl != hotControl)
		{
			activeControl.Release();
			activeControl = null;
		}

		if (hotControl && Input.GetButtonDown("Fire1"))
		{
			hotControl.Press();
			activeControl = hotControl;
		}
		if (activeControl && Input.GetButtonUp("Fire1"))
		{
			activeControl.Release();
			activeControl = null;
		}

		if (hotControl)
		{
			Debug.DrawLine (cam.transform.position, hotControl.transform.position, Color.yellow);
		}
	}

	// raycast from camera to find objects in the scene with Control component
	Control FindTargettedControl()
	{
		RaycastHit[] hits;
		Ray ray;

		if (!useCursor)
		{
			ray = new Ray(cam.transform.position, cam.transform.forward);
		}
		else
		{
			ray = cam.ScreenPointToRay(Input.mousePosition);
		}

		hits = Physics.RaycastAll(ray.origin, ray.direction, 100.0f);

		foreach (RaycastHit hit in hits)
		{
			Control control = (Control)hit.transform.gameObject.GetComponent<Control>();
			if (control != null)
			{
				return control;
			}
		}

		return null;
	}
	

}
