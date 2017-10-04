using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
	public bool solid = true;
	public bool visible = false;

	void Start()
	{
		solid = Random.Range(0.0f, 1.0f) < 0.2f;
		visible = true;

		if (!visible || !solid)
		{
			Transform child = transform.Find("marker");
			if (child)
			{
				child.gameObject.SetActive(false);
			}
		}
	}

	public void SetVisible(bool visibleState)
	{
		visible = visibleState;
		if (solid)
		{
			Transform child = transform.Find("marker");
			if (child)
			{
				child.gameObject.SetActive(visibleState);
			}
		}
	}
}
