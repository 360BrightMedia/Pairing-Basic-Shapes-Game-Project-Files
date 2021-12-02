using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBounds : MonoBehaviour
{

	float minX = -359f;
	float maxX = 359f;
	float minY = -170f;
	float maxY = 170f;
	Vector3 GameObjectPos;

	void OutOfBounds()
	{
		GameObjectPos = gameObject.transform.position;
		if(GameObjectPos.x < minX)
		{
			GameObjectPos.x = minX;
		}
		if(GameObjectPos.x > maxX)
		{
			GameObjectPos.x = maxX;
		}
		if(GameObjectPos.y < minY)
		{
			GameObjectPos.y = minY;
		}
		if(GameObjectPos.y > maxY)
		{
			GameObjectPos.y = maxY;
		}
	}

	private void Update()
	{
		OutOfBounds();
	}

}
