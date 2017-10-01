using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human : MonoBehaviour {

    private BoxElement InHand = null;
    private Text inHandText;
    private BoxElement inHand
    {
        get
        {
            return InHand;
        }

        set
        {
            if(value == null)
            {
                InHand = null;
                inHandText.text = "";
                return;
            }
            InHand = value;
            inHandText.text = InHand.GetValue();
        }
    }
    private Transform Outbox;
    [SerializeField]
    private Transform InboxObj;
    [SerializeField]
    private GameObject Element;
    [SerializeField]
    private Inbox InboxScr;

    void Awake()
    {
        Outbox = GameObject.Find("Outbox").transform;
        InboxObj = GameObject.Find("Inbox").transform;
        InboxScr = InboxObj.gameObject.GetComponent<Inbox>();
        inHandText = GetComponentInChildren<Text>();
    }

    public void SUB(int carpetIndex)
    {
        throw new System.NotImplementedException();
    }

    public void INC()
    {
        try
        {
            int x = Int32.Parse(inHand.GetValue());
            x = x + 1;
            inHand.SetValue(x.ToString());
        }
        catch (FormatException)
        {
            Debug.LogError("Can't preform INC with a non-number value");
        }
        UpdateText();
    }

    public void DEC()
    {
        try
        {
            inHand.SetValue((Int32.Parse(inHand.GetValue()) - 1).ToString());
        }catch(FormatException)
        {
            Debug.LogError("Can't preform DEC with a non-number value");
        }
        UpdateText();
    }

    public void OUTBOX()
    {
        GameObject tmp = Instantiate(Element);
        try
        {
            tmp.GetComponent<Text>().text = inHand.GetValue();
        }catch (NullReferenceException)
        {
            Debug.LogError("Can't prefor OUTBOX with empty hand");
        }
        tmp.transform.SetParent(Outbox);
        inHand = null;
    }

    public bool INBOX()
    {
        BoxElement temp = InboxScr.Next();
        if(temp != null)
        {
            inHand = temp;
            return true;
        }
        return false;
    }

    public int JMP(Instruction ins)
    {
        GameObject pair = ins.pair;
        return pair.transform.GetSiblingIndex();
    }

    public int JMPZ(Instruction ins)
    {
        if (inHand.GetValue() == "0")
        {
            GameObject pair = ins.pair;
            return pair.transform.GetSiblingIndex();
        }
        return -1;
    }

    public void COPYTO(int carpetID,Dictionary<int,Carpet> carpets)
    {
        Carpet car = carpets[carpetID];
        car.OnCarpet = inHand.GetValue();
    }

    public void COPYFROM(int carpetID, Dictionary<int, Carpet> carpets)
    {
        Carpet car = carpets[carpetID];
        inHand = new BoxElement(car.OnCarpet);
    }

    public void ADD(int carpetID,Dictionary<int,Carpet> carpets)
    {
        Carpet car = carpets[carpetID];
        try
        {
            int handVal = Int32.Parse(inHand.GetValue());
            int carpetVal = Int32.Parse(car.OnCarpet);
            inHand = new BoxElement(handVal + carpetVal);
        }
        catch
        {
            Debug.LogError("The supplied values are not numbers");
        }

    }

    private void UpdateText()
    {
        inHandText.text = InHand.GetValue();
    }
}
