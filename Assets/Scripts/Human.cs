using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human : MonoBehaviour {

    private BoxElement inHand = null;
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
    }

    public void ADD(int carpetIndex)
    {
        throw new System.NotImplementedException();
    }

    public void SUB(int carpetIndex)
    {
        throw new System.NotImplementedException();
    }

    public void INC()
    {
        try
        {
            inHand.SetValue((Int32.Parse(inHand.GetValue()) + 1).ToString());
        }
        catch (FormatException)
        {
            Debug.LogError("Can't preform INC with a non-number value");
        }
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
    }

    public void OUTBOX()
    {
        GameObject tmp = Instantiate(Element);
        tmp.GetComponent<Text>().text = inHand.GetValue();
        tmp.transform.SetParent(Outbox);
        inHand = null;
    }

    public void INBOX()
    {
        inHand = InboxScr.Next();
    }
}
