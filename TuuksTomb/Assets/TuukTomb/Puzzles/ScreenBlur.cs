using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBlur : MonoBehaviour
{
    public DialogueManager manager;
    
    public void OnFadeOutComplete()
    {
        manager.StartText();
    }
}
