using UnityEngine;

public class CanvasToggle : MonoBehaviour
{
    public Canvas canvas;
    public SphinxDialogue _sphinxDialogue;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    private void Close()
    {
        _sphinxDialogue.CloseDialogue();
    }
}