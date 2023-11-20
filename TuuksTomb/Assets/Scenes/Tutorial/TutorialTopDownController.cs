using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TutorialTopDownController : TutorialControllerBase
{
    public GameObject toggleSceneManager;
    private static bool _hasCompletedTutorial = false;

    protected override void Start()
    {
        if (_hasCompletedTutorial) return;

        var toggleSceneManagerScript = toggleSceneManager.GetComponent<ToggleScene>();

        var interactible = new RequiredControl(new KeyCode[] { KeyCode.E}," to interact with objects!", toggleSceneManagerScript);

        _requiredControls = new List<RequiredControl>() { interactible };

        base.Start();
    }

    protected override void EndTutorial()
    {
        _hasCompletedTutorial = true;
        base.EndTutorial();
    }
}