﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgramExec : MonoBehaviour {

    GameObject[] instructingGameObjs;
    private Human human;
    private List<string> expected = new List<string>();
    [SerializeField]
    BoxElement InHand = new BoxElement("");
    private string inHand
    {
        set
        {
            InHand.SetValue(value);
        }
        get
        {
            return InHand.GetValue();
        }
    }

    void Start()
    {
        human = GameObject.Find("Man").GetComponent<Human>();
    }

    public void Ready()
    {
        instructingGameObjs = GetChildren();
        Exec();
    }

    private void Exec()
    {
        Execute();
        StartCoroutine(StandartExecute());
    }

    private GameObject[] GetChildren()
    {
        Transform prog = GameObject.Find("Program").transform;
        GameObject[] tmp = new GameObject[prog.childCount];
        for(int i = 0; i < prog.childCount; i++)
        {
            tmp[i] = prog.GetChild(i).gameObject;
        }
        return tmp;
    }

    IEnumerator StandartExecute()
    {
        for (int i = 0; i < instructingGameObjs.Length; i++)
        {
            GameObject obj = instructingGameObjs[i];
            Instruction inst = obj.GetComponent<Instruction>();
            switch (inst.Type)
            {
                case Instruction.Instructions.INBOX:
                    human.INBOX();
                    break;
                case Instruction.Instructions.OUTBOX:
                    human.OUTBOX();
                    break;
                case Instruction.Instructions.ADD:
                    throw new System.NotImplementedException();
                    //break;
                case Instruction.Instructions.SUB:
                    break;
                case Instruction.Instructions.INC:
                    human.INC();
                    break;
                case Instruction.Instructions.DEC:
                    human.DEC();
                    break;
                case Instruction.Instructions.JMP:
                    throw new System.NotImplementedException();
                    //break;
                case Instruction.Instructions.ERROR:
                    throw new System.NotImplementedException();
                    //break;
                default:
                    break;
            }
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Execution Finished");
        Check();
    }

    private void Execute()
    {
        GameObject.Find("Inbox").GetComponent<Inbox>().DebugInbox("Staring");
        Debug.Log("Starting");
        Instruction.Instructions[] instructions = GameObject.Find("Main Camera").GetComponent<Correct>().GetCommands();
        List<BoxElement> inbox = GameObject.Find("Inbox").GetComponent<Inbox>().GetInbox();
        GameObject.Find("Inbox").GetComponent<Inbox>().DebugInbox("When got inbox");
        int inboxIndex = 0;
        int outboxIndex = 0;

        for (int i = 0; i < instructions.Length; i++)
        {
            inbox = GameObject.Find("Inbox").GetComponent<Inbox>().GetInbox();
            switch (instructions[i])
            {
                case Instruction.Instructions.INBOX:
                    GameObject.Find("Inbox").GetComponent<Inbox>().DebugInbox("Before inboxing");
                    Debug.Log(inboxIndex);
                    inHand = inbox[inboxIndex].GetValue();
                    GameObject.Find("Inbox").GetComponent<Inbox>().DebugInbox("After Inboxing");
                    inboxIndex++;
                    GameObject.Find("Inbox").GetComponent<Inbox>().DebugInbox("After Incrementing");
                    break;
                case Instruction.Instructions.OUTBOX:
                    expected.Add(inHand);
                    outboxIndex++;
                    break;
                case Instruction.Instructions.ADD:
                    throw new NotImplementedException();
                    //break;
                case Instruction.Instructions.SUB:
                    throw new NotImplementedException();
                    //break;
                case Instruction.Instructions.INC:
                    GameObject.Find("Inbox").GetComponent<Inbox>().DebugInbox("Strating inc");
                    string val = inHand;
                    GameObject.Find("Inbox").GetComponent<Inbox>().DebugInbox("Got value");
                    try
                    {
                        int intVal = Int32.Parse(val);
                        GameObject.Find("Inbox").GetComponent<Inbox>().DebugInbox("Parsed hand value");
                        intVal++;
                        GameObject.Find("Inbox").GetComponent<Inbox>().DebugInbox("Incremented hand value");
                        inHand = (intVal.ToString());
                        GameObject.Find("Inbox").GetComponent<Inbox>().DebugInbox("Set hand value");
                    }
                    catch(FormatException)
                    {
                        Debug.LogError("Can't preform INC wit a non-num value in hand");
                    }
                    break;
                case Instruction.Instructions.DEC:
                    val = inHand;
                    try
                    {
                        int intVal = Int32.Parse(val);
                        intVal--;
                        inHand = (intVal.ToString());
                    }
                    catch (FormatException)
                    {
                        Debug.LogError("Can't preform DEC wit a non-num value in hand");
                    }
                    break;
                case Instruction.Instructions.JMP:
                    throw new NotImplementedException();
                //break;
                case Instruction.Instructions.ERROR:
                    throw new NotImplementedException();
                //break;
                default:
                    break;
            }
        }
        Debug.Log("Generated keys");
        Debug.Log("Printing Inbox");
        GameObject.Find("Inbox").GetComponent<Inbox>().DebugInbox("When done");
        foreach (string s in expected)
        {
            Debug.Log("EXPECT: " + s);
        }
    }

    private void Check()
    {
        string[] texts = new string[GameObject.Find("Outbox").transform.childCount];
        if(texts.Length != expected.Count)
        {
            Debug.LogError("Supplied Text Does Not Mach The Specified One");
            return;
        }
        for(int i=0;i< GameObject.Find("Outbox").transform.childCount; i++)
        {
            texts[i] = GameObject.Find("Outbox").transform.GetChild(i).GetComponent<Text>().text;
        }

        for(int i = 0; i < expected.Count; i++)
        {
            if(expected[i] != texts[i])
            {
                Debug.LogError("Supplied Text Does Not Mach The Specified One");
                return;
            }
        }
        Debug.Log("CORRECT");

    }
}
