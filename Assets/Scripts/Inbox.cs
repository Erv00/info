using UnityEngine;
using UnityEngine.UI;

public class Inbox : MonoBehaviour {

    private BoxElement[] inbox;
    [SerializeField]
    private GameObject Element = null;
    private int index = 0;

    #region basic
    // Use this for initialization
    void Awake () {
        inbox = GenerateAlphanumericInbox(5, -30, 30);
        PrintArray(inbox);

        for(int i = 0; i < inbox.Length; i++)
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


    private BoxElement[] GenerateNumericInbox(int length,int min,int max)
    {
        BoxElement[] tmp = new BoxElement[length];
        for(int i = 0; i < length; i++)
        {
            tmp[i] = new BoxElement(Random.Range(min, max + 1));
        }
        //PrintArray(tmp);
        return(tmp);
    }

    private string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    private BoxElement[] GenerateAlphabeticInbox(int length)
    {
        BoxElement[] tmp = new BoxElement[length];
        for (int i = 0; i < length; i++)
        {
            tmp[i] = new BoxElement(alphabet[Random.Range(1, 26 + 1)]);
        }
        return tmp;
    }

    private BoxElement[] GenerateAlphanumericInbox(int length,int min,int max)
    {
        BoxElement[] tmp = new BoxElement[length];
        for (int i = 0; i < length; i++)
        {
            int x = Random.Range(min, max + 1 + 26);
            if (x > 26)
            {
                tmp[i] = new BoxElement(x-26);
                continue;
            }else if (x < 0)
            {
                tmp[i] = new BoxElement(x);
                continue;
            }
            else
            {
                tmp[i] = new BoxElement(alphabet[Random.Range(1, 26 + 1)]);
                continue;
            }
        }
        return tmp;
    }

    public BoxElement Next()
    {
        index++;
        Destroy(transform.GetChild(0).gameObject);
        return inbox[index - 1];
    }

    #region debug
    private void PrintArray(BoxElement[] arr)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            Debug.Log(arr[i].GetValue());
        }
    }
    #endregion

}
