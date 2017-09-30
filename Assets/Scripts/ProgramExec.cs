using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgramExec : MonoBehaviour {

    GameObject[] instructingGameObjs;
    private Human human;
    private List<string> expected = new List<string>();
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
                    if (!human.INBOX())
                    {
                        Debug.Log("Should Stop");
                        Check();
                        yield break;
                    }
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
                    i = human.JMP(inst);
                    break;
                case Instruction.Instructions.ERROR:
                    throw new System.NotImplementedException();
                    //break;
                default:
                    Debug.LogWarning("This my be a problem");
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
        List<BoxElement> inbox = GameObject.Find("Inbox").GetComponent<Inbox>().GetInbox();
        Dictionary<int, int> labels = GameObject.Find("Main Camera").GetComponent<Correct>().GetLabels();
        int inboxIndex = 0;
        int outboxIndex = 0;

        for (int i = 0; i < instructions.Length; i++)
        {
            inbox = GameObject.Find("Inbox").GetComponent<Inbox>().GetInbox();
            switch (instructions[i])
            {
                case Instruction.Instructions.INBOX:
                    Debug.Log(inboxIndex);
                    try
                    {
                        inHand = inbox[inboxIndex].GetValue();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Debug.LogWarning("No more elements in inbox");
                        return;
                    }
                    inboxIndex++;
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
                    string val = inHand;
                    try
                    {
                        int intVal = Int32.Parse(val);
                        intVal++;
                        inHand = (intVal.ToString());
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
                    i = labels[i];
                    break;
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
            NotGood();
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
                Debug.Log("Expected: " + ArrToStr(expected));
                Debug.Log("Got: " + ArrToStr(texts));
                Debug.LogError("Supplied Text Does Not Mach The Specified One");
                NotGood();
                return;
            }
        }
        Debug.Log("CORRECT");
        Good();

    }

    private void NotGood()
    {
        GameObject wrong = GameObject.Find("Wrong");
        for(int i = 0; i < wrong.transform.childCount; i++)
        {
            wrong.transform.GetChild(i).gameObject.SetActive(true);
        }
        GameObject.Find("Check").GetComponent<Button>().interactable = false;
    }

    private void Good()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 <= SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private string ArrToStr(List<string> strArr)
    {
        string tempo = "";

        foreach(string s in strArr)
        {
            tempo += s;
            tempo += "      ";
        }
        return tempo;
    }
    private string ArrToStr(string[] strArr)
    {
        string tempo = "";

        foreach (string s in strArr)
        {
            tempo += s;
            tempo += "      ";
        }
        return tempo;
    }
}
