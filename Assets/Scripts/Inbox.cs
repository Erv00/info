using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inbox : MonoBehaviour {

    private List<BoxElement> inbox;
    [SerializeField]
    private GameObject Element = null;
    private int index = 0;
    public enum InboxType { NUMERIC,ALPHABETIC,ALPHANUMERIC};
    [SerializeField]
    private InboxType Type = InboxType.NUMERIC;
    [SerializeField]
    private int min = 0;
    [SerializeField]
    private int max = 999;
    [SerializeField]
    private int length = 10;

    #region basic
    // Use this for initialization
    void Awake () {
        switch (Type)
        {
            case InboxType.NUMERIC:
                inbox = GenerateNumericInbox(length, min, max);
                break;
            case InboxType.ALPHABETIC:
                inbox = GenerateAlphabeticInbox(length);
                break;
            case InboxType.ALPHANUMERIC:
                inbox = GenerateAlphanumericInbox(length, min, max);
                break;
        }
        PrintArray(inbox);

        for(int i = 0; i < inbox.Count; i++)
        {
            GameObject tmp = Instantiate(Element);
            tmp.GetComponent<Text>().text = inbox[i].GetValue();
            tmp.transform.SetParent(transform);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    #endregion


    private List<BoxElement> GenerateNumericInbox(int length,int min,int max)
    {
        List<BoxElement> tmp = new List<BoxElement>();
        for(int i = 0; i < length; i++)
        {
            tmp.Add(new BoxElement(Random.Range(min, max + 1)));
        }
        //PrintArray(tmp);
        return(tmp);
    }

    private string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    private List<BoxElement> GenerateAlphabeticInbox(int length)
    {
        List<BoxElement> tmp = new List<BoxElement>();
        for (int i = 0; i < length; i++)
        {
            tmp.Add(new BoxElement(alphabet[Random.Range(1, 26 + 1)]));
        }
        return tmp;
    }

    private List<BoxElement> GenerateAlphanumericInbox(int length,int min,int max)
    {
        List<BoxElement> tmp = new List<BoxElement>();
        for (int i = 0; i < length; i++)
        {
            int x = Random.Range(min, max + 1 + 26);
            if (x > 26)
            {
                tmp.Add(new BoxElement(x-26));
                continue;
            }else if (x < 0)
            {
                tmp.Add(new BoxElement(x));
                continue;
            }
            else
            {
                tmp.Add(new BoxElement(alphabet[Random.Range(1, 26 + 1)]));
                continue;
            }
        }
        return tmp;
    }

    public BoxElement Next()
    {
        PrintArray(inbox);
        BoxElement tmp = inbox[index];
        DebugInbox("After getting tmp from inbox");
        index++;
        DebugInbox("After incrementing");
        Destroy(transform.GetChild(0).gameObject);
        Debug.Log("Returning " + tmp.GetValue());
        return tmp;
    }

    public List<BoxElement> GetInbox()
    {
        DebugInbox("Before giving inbox");
        return inbox;
    }

    #region debug
    private void PrintArray(List<BoxElement> arr)
    {
        for(int i = 0; i < arr.Count; i++)
        {
            Debug.Log(arr[i].GetValue());
        }
    }

    public void DebugInbox(string where)
    {
        Debug.LogWarning(where);
        PrintArray(inbox);
    }

    #endregion

}
