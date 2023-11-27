using UnityEngine;

public class CanvasToggle : MonoBehaviour
{
    public Canvas canvas;
    public SphinxDialogue _sphinxDialogue;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Z))
        {
            Close();
        }
    }

    private void Close()
    {
        if (_sphinxDialogue == null) return;
        _sphinxDialogue.CloseDialogue();
    }
}