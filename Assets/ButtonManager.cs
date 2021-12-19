using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class ActionSelection
{
    [Header("Activity Properties")]
    public ActivityState state;
    public string description;
    public GameObject info;
    public CanvasGroup canvasGroup;

    public void Init()
    {
        if (canvasGroup != null)
        {
            canvasGroup.CanvasGroupFade(1);
            canvasGroup.CanvasGroupInteractable(true);
        }
        if (info != null)
            info.SetActive(false);
    }

    public void CanvasGroupInteractable(bool canInteract)
    {
        if (canvasGroup != null)
            canvasGroup.CanvasGroupInteractable(canInteract);
    }

    public void TurnOn()
    {
        if (canvasGroup != null)
            canvasGroup.CanvasGroupInteractable(true);
        if (info != null)
            info.SetActive(true);
    }

    public void TurnOff()
    {
        if (canvasGroup != null)
            canvasGroup.CanvasGroupInteractable(false);
    }
}

[Serializable]
public class ChemicalSelection
{
    public ChemistryType state;
}

[Serializable]
public class MenuContainer
{
    public GameObject menu;
    public Transform menuTransform;
    public RoundMenuContentManager roundMenuContentManager;

    public int index = 0;

    public List<ActionSelection> actionSelections = new List<ActionSelection>();
    public List<ChemicalSelection> chemicalSelections = new List<ChemicalSelection>();
    public List<float> angles = new List<float>();

    public void Init()
    {
        if (menuTransform == null)
            menuTransform = menu.transform;
        if (roundMenuContentManager == null)
            roundMenuContentManager = menu.GetComponent<RoundMenuContentManager>();

        for (int i = 1; i < actionSelections.Count; i++)
            actionSelections[i].Init();
    }

    public void LeftButton_Action()
    {
        if (Menu_StateManager._instance.GetState() == MenuState.CHEMISTRY_SELECTION)
            Menu_StateManager._instance.SetState_ActionSelection();
        else if (Menu_StateManager._instance.GetState() == MenuState.ELEMENT_SELECTION)
            Menu_StateManager._instance.SetState_ChemistrySelection();
        else
            Menu_StateManager._instance.SetState_ElementSelection();

        //2
        ButtonManager._instance.UpdateMenu();
        ButtonManager._instance.UpdateButtons();
    }
    public void RightButton_Action()
    {
        if (Menu_StateManager._instance.GetState() == MenuState.ACTION_SELECTION)
            Menu_StateManager._instance.SetState_ChemistrySelection();
        else if (Menu_StateManager._instance.GetState() == MenuState.CHEMISTRY_SELECTION)
            Menu_StateManager._instance.SetState_ElementSelection();
        else if (Menu_StateManager._instance.GetState() == MenuState.ELEMENT_SELECTION)
            Menu_StateManager._instance.SetState_Production();

        ButtonManager._instance.UpdateMenu();
        ButtonManager._instance.UpdateButtons();
    }
}

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager _instance;

    [Header("Buttons")]
    [SerializeField] Button btnLeft;
    [SerializeField] Button btnRight;
    [SerializeField] Button btnAction;

    [Header("Other Scripts")]
    [SerializeField] ActivityUIController activityUIController;
    [SerializeField] UIButtonActions buttonActions;
    [SerializeField] RoundMenuManager roundMenuManager;

    void Awake()
    {
        _instance = this;
        roundMenuManager.Init();
    }

    void Start()
    {
        roundMenuManager.UpdateMenu();
        UpdateInfo();
        UpdateButtons();
    }

    // Button Actions
    public void UpdateButtons()
    {
        switch (Menu_StateManager._instance.GetState())
        {
            case MenuState.ACTION_SELECTION:
                UpdateButtons_ActionSelection();
                break;

            case MenuState.CHEMISTRY_SELECTION:
                btnLeft.interactable = true;
                btnAction.interactable = false;
                btnRight.interactable = true;
                break;

            case MenuState.ELEMENT_SELECTION:
                btnLeft.interactable = true;
                btnRight.interactable = true;
                break;
        }
    }

    public void UpdateButtons_ActionSelection()
    {
        switch (roundMenuManager.currentMenu.actionSelections[roundMenuManager.currentMenu.index].state)
        {
            case ActivityState.FUSION:
                btnAction.interactable = true;
                btnLeft.interactable = false;
                btnRight.interactable = false;
                break;
            case ActivityState.DISCOVER:
                btnAction.interactable = false;
                btnLeft.interactable = false;
                btnRight.interactable = true;
                break;
            case ActivityState.FORGE:
                btnAction.interactable = false;
                btnLeft.interactable = false;
                btnRight.interactable = true;
                break;
            case ActivityState.MIX:
                btnAction.interactable = false;
                btnLeft.interactable = false;
                btnRight.interactable = true;
                break;
            case ActivityState.NONE:
                btnAction.interactable = false;
                btnLeft.interactable = false;
                btnRight.interactable = false;
                break;
        }
    }

    public void UpdateButtons_ChemicalSelection()
    {

    }

    // Round Menu Actions
    #region Round Menu Actions
    public void UpdateMenu()
    {
        roundMenuManager.UpdateMenu();
    }

    public Transform GetCurrentMenu()
    {
        return roundMenuManager.GetCurrentMenu();
    }

    public ActionSelection GetCurrentActivity()
    {
        return roundMenuManager.GetCurrentActivity();
    }

    public ActionSelection GetCurrentActivity(int _index)
    {
        return roundMenuManager.GetCurrentActivity(_index);
    }

    public ActivityState GetCurrentActivityState()
    {
        return roundMenuManager.GetCurrentActivityState();
    }

    public int GetCurrentIndex()
    {
        return roundMenuManager.GetCurrentIndex();
    }

    public List<float> GetAngles()
    {
        return roundMenuManager.GetAngles();
    }

    public float GetAngle()
    {
        return roundMenuManager.GetAngle();
    }

    public int GetSize()
    {
        return roundMenuManager.GetSize();
    }

    public bool GetCondition(bool _)
    {
        return roundMenuManager.GetCondition(_);
    }

    public void SetCurrentIndex(bool _)
    {
        roundMenuManager.SetCurrentIndex(_);
    }

    public void SetCurrentIndex_Reset(bool _)
    {
        roundMenuManager.SetCurrentIndex_Reset(_);
    }
    #endregion

    #region Update info in Activity Selection State
    public void UpdateInfo()
    {
        if (Menu_StateManager._instance.GetState() == MenuState.ACTION_SELECTION)
        {
            activityUIController.UpdateActivityInfo(GetCurrentActivityState().ToString(), GetCurrentActivity().description);
            buttonActions.UpdateButtonAction();
        }
    }

    public void LeftButton_Action()
    {
        roundMenuManager.currentMenu.LeftButton_Action();
    }

    public void RightButton_Action()
    {
        roundMenuManager.currentMenu.RightButton_Action();
    }

    public void ActionButton_Action()
    {
        switch (ButtonManager._instance.GetCurrentActivity().state)
        {
            case ActivityState.FUSION:
                print("Do Fusion Stuff");
                break;
            case ActivityState.DISCOVER:
                print("Do Discover Stuff");
                break;
            case ActivityState.FORGE:
                print("Do Forge Stuff");
                break;
            case ActivityState.MIX:
                print("Do Mix Stuff");
                break;
            case ActivityState.NONE:
                print("Doing Nothing");
                break;
        }
    }
    #endregion
}
