using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteData : MonoBehaviour
{
    
    public void DeleteSaveData()
	{
		Debug.Log("Deleting SavedScene...");
		PlayerPrefs.DeleteKey("SavedScene");
		Debug.Log("Deleted SavedScene");
	}

}
