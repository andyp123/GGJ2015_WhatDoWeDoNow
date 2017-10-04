using UnityEngine;
using System.Collections;

public class Recharge : MonoBehaviour
{
	float startTime = 0.0f;
	float endTime = 0.1f;

	void Update()
	{
		if (Time.time > endTime)
		{
			transform.localScale = Vector3.one;
		}
		else
		{
			float scale = (Time.time - startTime) / (endTime - startTime);
			transform.localScale = new Vector3(1.0f, scale, 1.0f);
		}
	}

	public void StartRecharge(float rechargeDelay)
	{
		if (rechargeDelay < 0.1f) rechargeDelay = 0.1f; // avoid divide by 0

		startTime = Time.time;
		endTime = startTime + rechargeDelay;
	}
}
