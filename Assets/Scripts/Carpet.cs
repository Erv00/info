using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carpet : MonoBehaviour {

    #region basic
    // Use this for initialization
    void Start () {
        identifier = System.Int32.Parse(transform.GetChild(0).GetComponent<Text>().text);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    #endregion

    private Text onCarpetText;
    private BoxElement onCarpet;
    public string OnCarpet
    {
        get
        {
            return onCarpet.GetValue();
        }
        set
        {
            if(value == null)
            {
                onCarpetText.text = "";
            }
            onCarpet.SetValue(value);
            onCarpetText.text = onCarpet.GetValue();
        }
    }
    public int identifier;
}
