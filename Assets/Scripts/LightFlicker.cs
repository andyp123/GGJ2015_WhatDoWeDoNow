using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{
	Light lightComponent;
	public float minIntensity = 0.0f;
	public float maxIntensity = 1.0f;
	public float lerpDelay = 2.5f;

	float startIntensity;
	float targetIntensity;
	float lerpTime;

	void Start()
	{
		lightComponent = gameObject.GetComponent<Light>();
		if (lightComponent == null)
		{
			Debug.Log("Light component not found");
		}
		lerpTime = lerpDelay;
		startIntensity = targetIntensity = lightComponent.intensity;
	}
	
	void Update()
	{
		lerpTime += Time.deltaTime;
		if (lerpTime >= lerpDelay)
		{
			lerpTime = 0.0f;
			lightComponent.intensity = targetIntensity;
			startIntensity = targetIntensity;
			targetIntensity = Random.Range(minIntensity, maxIntensity);
		}
		else
		{
			float lerpX = lerpTime / lerpDelay;
			lightComponent.intensity = startIntensity + (targetIntensity - startIntensity) * lerpX;
		}
	}
}
