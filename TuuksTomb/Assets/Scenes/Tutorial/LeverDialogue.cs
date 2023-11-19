using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDialogue : DialogueOptions
{
    public override void OnAccept()
    {
        Debug.Log("Accept");
    }
    public override void OnDecline()
    {
        Debug.Log("Decline");
    }
}
