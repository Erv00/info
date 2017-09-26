using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inbox : MonoBehaviour {

    int[] inbox;
    #region basic
    // Use this for initialization
    void Start () {
        inbox = GenerateNumericInbox(5, 1, 10); 

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    #endregion


    private int[] GenerateNumericInbox(int length,int min,int max)
    {
        Random r = new Random();
        int[] tmp = new int[length];
        for(int i = 0; i < length; i++)
        {
            tmp[i] = Random.Range(min, max + 1);
        }
        PrintArray(tmp);
        return(tmp);
    }

    #region debug
    private void PrintArray(int[] arr)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            Debug.Log(arr[i]);
        }
    }
    #endregion

}
