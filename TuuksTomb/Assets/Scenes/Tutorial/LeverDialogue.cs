using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDialogue : DialogueOptions
{
    public override void OnAccept()
    {
        Debug.Log("Accept");
        Debug.Log("Accept2");
    }
    public override void OnDecline()
    {
        Debug.Log("Decline");
    }
}
