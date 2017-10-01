using UnityEngine;

public class Instruction : MonoBehaviour {

	public enum Instructions { INBOX,OUTBOX,ADD,SUB,INC,DEC,JMP,ERROR,PLACEHOLDER,COPYTO,COPYFROM,JMPZ}
    [SerializeField]
    Instructions type = Instructions.ERROR;
    public GameObject pair;
    public int index;
    public bool wasSet = false;

    public Instructions Type
    {
        get
        {
            return type;
        }
    }
}
