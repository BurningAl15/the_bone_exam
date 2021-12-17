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
    public int index = 0;

    public List<ActionSelection> actionSelections = new List<ActionSelection>();
    public List<ChemicalSelection> chemicalSelections = new List<ChemicalSelection>();
    public List<float> angles = new List<float>();

    public void Init()
    {
        for (int i = 1; i < actionSelections.Count; i++)
            actionSelections[i].Init();
    }
}

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager _instance;
    [SerializeField] Button btnLeft, btnRight, btnAction;

    [SerializeField] Image parentImg;
    [SerializeField] Sprite actionState_BG, chemicalState_BG, elementState_BG;
    [SerializeField] MenuContainer menu_5, menu_6;

    [SerializeField] MenuContainer currentMenu;

    [SerializeField] ActivityUIController activityUIController;
    [SerializeField] UIButtonActions buttonActions;


    void Awake()
    {
        _instance = this;
        menu_5.Init();
        menu_6.Init();
    }

    void Start()
    {
        UpdateMenu();
        UpdateInfo();
        UpdateButtons();
        UpdateButtons_ActionSelection();
    }

    public void UpdateButtons()
    {
        switch (Menu_StateManager._instance.GetState())
        {
            case MenuState.ACTION_SELECTION:
                // UpdateButtons_ActionSelection(currentMenu.actionSelections[currentMenu.index].state);
                break;

            case MenuState.CHEMISTRY_SELECTION:
                btnLeft.interactable = true;
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
        switch (currentMenu.actionSelections[currentMenu.index].state)
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

    public void UpdateMenu()
    {
        switch (Menu_StateManager._instance.GetState())
        {
            case MenuState.ACTION_SELECTION:
                menu_5.menu.SetActive(true);
                menu_6.menu.SetActive(false);
                currentMenu = menu_5;
                parentImg.sprite = actionState_BG;
                break;

            case MenuState.CHEMISTRY_SELECTION:
                menu_5.menu.SetActive(false);
                menu_6.menu.SetActive(true);
                currentMenu = menu_6;
                parentImg.sprite = chemicalState_BG;
                break;

            case MenuState.ELEMENT_SELECTION:
                menu_5.menu.SetActive(false);
                menu_6.menu.SetActive(true);
                currentMenu = menu_6;
                parentImg.sprite = elementState_BG;
                break;
        }
    }

    public Transform GetCurrentMenu()
    {
        return currentMenu.menuTransform;
    }

    public ActionSelection GetCurrentActivity()
    {
        return currentMenu.actionSelections[currentMenu.index];
    }

    public ActionSelection GetCurrentActivity(int _index)
    {
        return currentMenu.actionSelections[_index];
    }

    public ActivityState GetCurrentActivityState()
    {
        return currentMenu.actionSelections[currentMenu.index].state;
    }


    public int GetCurrentIndex()
    {
        return currentMenu.index;
    }

    public List<float> GetAngles()
    {
        return currentMenu.angles;
    }

    public float GetAngle()
    {
        return currentMenu.angles[currentMenu.index];
    }

    public int GetSize()
    {
        return currentMenu.angles.Count;
    }

    public bool GetCondition(bool _)
    {
        return _ ? currentMenu.index > currentMenu.angles.Count - 1 : currentMenu.index < 0;
    }

    public void SetCurrentIndex(bool _)
    {
        currentMenu.index = _ ? currentMenu.index + 1 : currentMenu.index - 1;
        UpdateStuff();
    }

    public void SetCurrentIndex_Reset(bool _)
    {
        currentMenu.index = _ ? 0 : currentMenu.angles.Count - 1;
        UpdateStuff();
    }

    void UpdateStuff()
    {
        switch (Menu_StateManager._instance.GetState())
        {
            case MenuState.ACTION_SELECTION:
                menu_5 = currentMenu;
                break;
            case MenuState.CHEMISTRY_SELECTION:
            case MenuState.ELEMENT_SELECTION:
                menu_6 = currentMenu;
                break;
        }
    }

    public void UpdateInfo()
    {
        if (Menu_StateManager._instance.GetState() == MenuState.ACTION_SELECTION)
        {
            activityUIController.UpdateActivityInfo(GetCurrentActivity().ToString(), GetCurrentActivity().description);
            buttonActions.UpdateButtonAction();
        }
    }
}
