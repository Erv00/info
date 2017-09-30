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
        GenerateInbox();
    }

    public void GenerateInbox()
    {
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

        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<Destroy>().Kill();
        }

        for (int i = 0; i < inbox.Count; i++)
        {
            GameObject tmp = Instantiate(Element);
            tmp.GetComponent<Text>().text = inbox[i].GetValue();
            tmp.transform.SetParent(transform);
        }
        Debug.Log("Generated new inbox");
        GameObject.Find("Outbox").GetComponent<FlushBox>().Flush();
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
            Debug.Log("Got: " + x);
            if (x >= 26)
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
                tmp.Add(new BoxElement(alphabet[x]));
                continue;
            }
        }
        return tmp;
    }

    public BoxElement Next()
    {
        BoxElement tmp = null;
        try
        {
            tmp = inbox[index];
            index++;
            Destroy(transform.GetChild(0).gameObject);
        }
        catch (System.ArgumentOutOfRangeException)
        {
            Debug.LogWarning("No more items in inbox");
            return null;
        }
        catch (UnityException)
        {
            Debug.LogError("No more children");
        }
        return tmp;
    }

    public List<BoxElement> GetInbox()
    {
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
