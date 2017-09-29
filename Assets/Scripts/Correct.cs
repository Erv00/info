using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correct : MonoBehaviour {

    public Instruction.Instructions[] correctCommands;

    public Instruction.Instructions[] GetCommands()
    {
        return correctCommands;
    }
}
