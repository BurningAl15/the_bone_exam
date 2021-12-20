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


    [Header("Slider Menu for Orbs")]
    [SerializeField] GameObject fusionContainer;
    [SerializeField] CanvasGroup fusionContainerCanvasGroup;

    [SerializeField] GameObject contentChemistryContainer;
    [SerializeField] CanvasGroup contentChemistryCanvasGroup;


    [Header("To check states Properties")]
    public MenuContainer currentMenu;

    public void Init()
    {
        menu_Activity.Init();
        menu_Chemical.Init();
        menu_Element.Init();
        Init_SliderContainer();
    }

    #region Update Menu
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

        menu_Element.roundMenuContentManager.OuterContentChange();
        menu_Element.roundMenuContentManager.InnerContentChange();
    }

    void Update_Menu_BG(Sprite _)
    {
        parentImg.sprite = _;
    }

    #endregion

    //From Chemistry to Element States
    #region Slider Container
    public void Init_SliderContainer()
    {
        if (fusionContainerCanvasGroup != null)
        {
            fusionContainerCanvasGroup.CanvasGroupFade(1);
            fusionContainerCanvasGroup.CanvasGroupInteractable(true);
        }
        if (fusionContainer != null)
            fusionContainer.SetActive(false);
    }

    public void CanvasGroupInteractable_SliderContainer(bool canInteract)
    {
        if (fusionContainerCanvasGroup != null)
            fusionContainerCanvasGroup.CanvasGroupInteractable(canInteract);
    }

    public void TurnOn_SliderContainer()
    {
        if (fusionContainerCanvasGroup != null)
            fusionContainerCanvasGroup.CanvasGroupInteractable(true);
        if (fusionContainer != null)
            fusionContainer.SetActive(true);
    }

    public void TurnOff_SliderContainer()
    {
        if (fusionContainerCanvasGroup != null)
            fusionContainerCanvasGroup.CanvasGroupInteractable(false);
        if (fusionContainer != null)
            fusionContainer.SetActive(false);
    }
    #endregion

    #region 6 Divisions Center
    public void TurnOn_6DivisionsCenter()
    {
        if (contentChemistryCanvasGroup != null)
            contentChemistryCanvasGroup.CanvasGroupInteractable(true);
        if (contentChemistryContainer != null)
            contentChemistryContainer.SetActive(true);
    }

    public void TurnOff_6DivisionsCenter()
    {
        if (contentChemistryCanvasGroup != null)
            contentChemistryCanvasGroup.CanvasGroupInteractable(false);
        if (contentChemistryContainer != null)
            contentChemistryContainer.SetActive(false);
    }
    #endregion

    //Getters
    #region Getters
    public Transform GetCurrentMenu()
    {
        return currentMenu.menuTransform;
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

    //Activity
    #region State Management
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

    //Element
    public ElementSelection GetCurrentElement()
    {
        return currentMenu.elementSelections[currentMenu.index];
    }

    public ElementSelection GetCurrentElement(int _index)
    {
        return currentMenu.elementSelections[_index];
    }

    public ElementType GetCurrentElementType()
    {
        return currentMenu.elementSelections[currentMenu.index].state;
    }

    public ElementSelection GetInitialValue()
    {
        return menu_Element.elementSelections[0];
    }

    #endregion

    //Setters

    #region Setters
    public void SetCurrentIndex(bool _)
    {
        currentMenu.index = _ ? currentMenu.index + 1 : currentMenu.index - 1;
        UpdateMenuInfo_FromCurrentMenu();
    }

    public void SetCurrentIndex_Reset(bool _)
    {
        currentMenu.index = _ ? 0 : currentMenu.angles.Count - 1;
        UpdateMenuInfo_FromCurrentMenu();
    }

    void UpdateMenuInfo_FromCurrentMenu()
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

    public void ResetMenuIndexes()
    {
        menu_Activity.index = 0;
        menu_Chemical.index = 0;
        menu_Element.index = 0;
    }

    #endregion
}
