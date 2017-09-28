using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramExec : MonoBehaviour {

    GameObject[] instructingGameObjs;
    string[] commands;
    private Human human;

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
        StartCoroutine(Execute());
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

    IEnumerator Execute()
    {
        commands = new string[instructingGameObjs.Length];
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
                    break;
                case Instruction.Instructions.SUB:
                    break;
                case Instruction.Instructions.INC:
                    break;
                case Instruction.Instructions.DEC:
                    break;
                case Instruction.Instructions.JMP:
                    break;
                case Instruction.Instructions.ERROR:
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(2);
        }
        Debug.Log("Execution Finished");
    }
}
