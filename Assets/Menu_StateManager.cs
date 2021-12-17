using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_StateManager : MonoBehaviour
{
    public static Menu_StateManager _instance;
    [SerializeField] MenuState menuState;

    void Awake()
    {
        _instance = this;
        SetState_ActionSelection();
    }

    public void SetState_ByIndex(int index)
    {
        menuState = (MenuState)index;
    }

    public void SetState_ActionSelection()
    {
        menuState = MenuState.ACTION_SELECTION;
    }
    public void SetState_ChemistrySelection()
    {
        menuState = MenuState.CHEMISTRY_SELECTION;
    }
    public void SetState_ElementSelection()
    {
        menuState = MenuState.ELEMENT_SELECTION;
    }
    public void SetState_Production()
    {
        menuState = MenuState.PRODUCTION;
    }

    public MenuState GetState()
    {
        return menuState;
    }
}
