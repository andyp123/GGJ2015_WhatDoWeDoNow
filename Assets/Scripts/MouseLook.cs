using System;
using UnityEngine;

// http://answers.unity3d.com/questions/29741/mouse-look-script.html
// unity mlook script was not working properly
public class MouseLook : MonoBehaviour
{
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationX = 0F;
	float rotationY = 0F;

	Quaternion originalRotation;

	void Update ()
	{
		if (axes == RotationAxes.MouseXAndY)
		{
			// Read the mouse input axis
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			
			rotationX = ClampAngle (rotationX, minimumX, maximumX);
			rotationY = ClampAngle (rotationY, minimumY, maximumY);
			
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
			
			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		}
		else if (axes == RotationAxes.MouseX)
		{
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationX = ClampAngle (rotationX, minimumX, maximumX);
			
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = ClampAngle (rotationY, minimumY, maximumY);
			
			Quaternion yQuaternion = Quaternion.AngleAxis (-rotationY, Vector3.right);
			transform.localRotation = originalRotation * yQuaternion;
		}
	}

	void Start ()
	{
		// Make the rigid body not change rotation
		Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		if (rb)
			rb.freezeRotation = true;
		originalRotation = transform.localRotation;
	}

	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
}


//using System;
//using UnityEngine;
//
//[RequireComponent (typeof (Camera))]
//public class MouseLook : MonoBehaviour
//{
//	public float XSensitivity = 2f;
//	public float YSensitivity = 2f;
//	public bool clampVerticalRotation = true;
//	public float MinimumX = -90F;
//	public float MaximumX = 90F;
//	public bool smooth;
//	public float smoothTime = 5f;
//
//	Quaternion m_CameraTargetRot;
//	Camera m_Camera;
//	
//	void Start()
//	{
//		m_Camera = gameObject.GetComponent<Camera>();
//		if (m_Camera == null)
//		{
//			Debug.LogError("Camera component not found");
//		}
//	    m_CameraTargetRot = m_Camera.transform.localRotation;
//	}
//
//	void Update()
//	{
//		LookRotation(m_Camera.transform);
//	}
//
//	public void LookRotation(Transform camera)
//	{
//	    float yRot = Input.GetAxis("Mouse X") * XSensitivity;
//	    float xRot = Input.GetAxis("Mouse Y") * YSensitivity;
//
//		m_CameraTargetRot *= Quaternion.Euler (0f, yRot, 0f);
//	    m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);
//
//
//	    if(clampVerticalRotation)
//	        m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);
//
//	    if(smooth)
//	    {
//	        camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot, smoothTime * Time.deltaTime);
//	    }
//	    else
//	    {
//	        camera.localRotation = m_CameraTargetRot;
//		}
//		camera.eulerAngles.Set(camera.eulerAngles.x, camera.eulerAngles.y, 0.0f);
//	}
//	
//	Quaternion ClampRotationAroundXAxis(Quaternion q)
//	{
//	    q.x /= q.w;
//	    q.y /= q.w;
//	    q.z /= q.w;
//	    q.w = 1.0f;
//
//	    float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);
//
//	    angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);
//
//	    q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);
//
//	    return q;
//	}
//}
