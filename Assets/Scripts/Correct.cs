using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correct : MonoBehaviour {

    public string[] correctCommands;
    Dictionary<int, int> labels = new Dictionary<int, int>();

    public Instruction.Instructions[] GetCommands()
    {
        Instruction.Instructions[] tmp = new Instruction.Instructions[correctCommands.Length];

        for(int i=0;i<correctCommands.Length;i++)
        {
            string s = correctCommands[i];
            switch (s)
            {
                case "INBOX":
                    tmp[i] = Instruction.Instructions.INBOX;
                    break;
                case "OUTBOX":
                    tmp[i] = Instruction.Instructions.OUTBOX;
                    break;
                case "INC":
                    tmp[i] = Instruction.Instructions.INC;
                    break;
                case "JMP":
                    tmp[i] = Instruction.Instructions.JMP;
                    try
                    {
                        labels.Add(i, GetJumpAddress(i+1));
                    }
                    catch (FormatException)
                    {
                        Debug.LogError("After jump instruction there should be a number as a label");
                    }
                    break;
                default:
                    Debug.LogWarning("Correct commands line " + i + "is not a command");
                    tmp[i] = Instruction.Instructions.PLACEHOLDER;
                    break;
            }
        }
        return tmp;
    }

    public Dictionary<int,int> GetLabels()
    {
        return labels;
    }

    public int GetJumpAddress(int labelIndex)
    {
        for(int i = 0; i < correctCommands.Length; i++)
        {
            if (i == labelIndex)
            {
                continue;
            }
            if(correctCommands[i] == correctCommands[labelIndex])
            {
                return i;
            }
        }
        return -1;
    }
}
