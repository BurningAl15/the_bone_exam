using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundMenuManager : MonoBehaviour
{
    [SerializeField] Image parentImg;

    [Header("Action Menu Properties")]
    [SerializeField] Sprite actionState_BG;
    [SerializeField] MenuContainer menu_Activity;


    [Header("Chemical Menu Properties")]
    [SerializeField] Sprite chemicalState_BG;
    [SerializeField] MenuContainer menu_Chemical;


    [Header("Element Menu Properties")]
    [SerializeField] Sprite elementState_BG;
    [SerializeField] MenuContainer menu_Element;


    [Header("To check states Properties")]
    public MenuContainer currentMenu;

    public void Init()
    {
        menu_Activity.Init();
        menu_Chemical.Init();
        menu_Element.Init();
    }

    public void UpdateMenu()
    {
        switch (Menu_StateManager._instance.GetState())
        {
            case MenuState.ACTION_SELECTION:
                UpdateWholeElements();

                menu_Activity.menu.SetActive(true);
                menu_Chemical.menu.SetActive(false);
                menu_Element.menu.SetActive(false);

                currentMenu = menu_Activity;
                Update_Menu_BG(actionState_BG);
                break;

            case MenuState.CHEMISTRY_SELECTION:
                UpdateWholeElements();

                menu_Activity.menu.SetActive(false);
                menu_Chemical.menu.SetActive(true);
                menu_Element.menu.SetActive(false);

                currentMenu = menu_Chemical;
                Update_Menu_BG(chemicalState_BG);
                break;

            case MenuState.ELEMENT_SELECTION:
                UpdateWholeElements();

                menu_Activity.menu.SetActive(false);
                menu_Chemical.menu.SetActive(false);
                menu_Element.menu.SetActive(true);

                currentMenu = menu_Element;
                Update_Menu_BG(elementState_BG);
                break;
        }
    }

    void UpdateWholeElements()
    {
        menu_Activity.roundMenuContentManager.OuterContentChange();
        menu_Activity.roundMenuContentManager.InnerContentChange();

        menu_Chemical.roundMenuContentManager.OuterContentChange();
        menu_Chemical.roundMenuContentManager.InnerContentChange();
    }

    void Update_Menu_BG(Sprite _)
    {
        parentImg.sprite = _;
    }

    //Getters

    #region Getters

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
    #endregion

    //Setters

    #region Setters
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
                menu_Activity = currentMenu;
                break;
            case MenuState.CHEMISTRY_SELECTION:
                menu_Chemical = currentMenu;
                break;
            case MenuState.ELEMENT_SELECTION:
                menu_Element = currentMenu;
                break;
        }
    }
    #endregion
}
