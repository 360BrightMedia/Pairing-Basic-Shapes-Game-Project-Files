using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBounds : MonoBehaviour
{

	private void Update()
	{
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -357f, 357f), Mathf.Clamp(transform.position.y, -171f, 171f), transform.position.z);
	}

}
