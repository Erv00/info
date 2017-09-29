using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgramExec : MonoBehaviour {

    GameObject[] instructingGameObjs;
    private Human human;
    private List<string> expected = new List<string>();
    [SerializeField]
    BoxElement inHand = null;

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
        Debug.Log("Starting");
        Instruction.Instructions[] instructions = GameObject.Find("Main Camera").GetComponent<Correct>().GetCommands();
        BoxElement[] inbox = GameObject.Find("Inbox").GetComponent<Inbox>().GetInbox();
        int inboxIndex = 0;
        int outboxIndex = 0;

        for (int i = 0; i < instructions.Length; i++)
        {
            switch (instructions[i])
            {
                case Instruction.Instructions.INBOX:
                    inHand = inbox[inboxIndex];
                    inboxIndex++;
                    break;
                case Instruction.Instructions.OUTBOX:
                    expected.Add(inHand.GetValue());
                    outboxIndex++;
                    break;
                case Instruction.Instructions.ADD:
                    throw new NotImplementedException();
                    //break;
                case Instruction.Instructions.SUB:
                    throw new NotImplementedException();
                    //break;
                case Instruction.Instructions.INC:
                    string val = inHand.GetValue();
                    try
                    {
                        int intVal = Int32.Parse(val);
                        intVal++;
                        inHand.SetValue(intVal.ToString());
                    }
                    catch(FormatException)
                    {
                        Debug.LogError("Can't preform INC wit a non-num value in hand");
                    }
                    break;
                case Instruction.Instructions.DEC:
                    val = inHand.GetValue();
                    try
                    {
                        int intVal = Int32.Parse(val);
                        intVal--;
                        inHand.SetValue(intVal.ToString());
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
