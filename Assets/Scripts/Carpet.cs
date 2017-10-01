using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carpet : MonoBehaviour {

    #region basic
    // Use this for initialization
    void Start () {
        onCarpetText = transform.GetChild(1).GetComponent<Text>();
        identifier = System.Int32.Parse(transform.GetChild(0).GetComponent<Text>().text);
        Debug.Log("I " + this + " have the identifier " + identifier);
        OnCarpet = initialize;
        ProgramExec.carpets.Add(identifier, this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    #endregion

    [SerializeField]
    private string initialize = "";
    private Text onCarpetText;
    private BoxElement onCarpet = new BoxElement("");
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
