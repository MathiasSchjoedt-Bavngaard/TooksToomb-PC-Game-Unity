using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueOptions : MonoBehaviour
{
    public abstract void OnAccept();
    public abstract void OnDecline();
}
