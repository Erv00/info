using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour {

	public enum Instructions { INBOX,OUTBOX,ADD,SUB,INC,DEC,JMP,ERROR,PLACEHOLDER}
    [SerializeField]
    Instructions type = Instructions.ERROR;
    public GameObject pair;

    public Instructions Type
    {
        get
        {
            return type;
        }
    }
}
