using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueOptions : MonoBehaviour
{
    public GameObject acceptButton;
    public GameObject declineButton;
    public virtual void DialogueEnd()
    {
        Invoke(nameof(SetButtonsActive), 0.25f);
    }
    private void SetButtonsActive()
    {
        acceptButton.SetActive(true);
        declineButton.SetActive(true);
    }
    public abstract void OnAccept();
    public abstract void OnDecline();
}
