using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour
{

    public GameObject touchBlocker;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Touch Count : " + Input.touchCount);
      if(Input.touchCount <= 1)
		{
            touchBlocker.SetActive(false);
		} 
      else if (Input.touchCount > 1)
		{
            touchBlocker.SetActive(true);
            Input.multiTouchEnabled = false;
        }
    }
}
