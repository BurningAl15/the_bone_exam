using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ContentParent
{
    public Transform innerTransform;
    public Transform midTransform;
    public Transform optionTransform;
}

[Serializable]
public class Content_5Divs : ContentParent
{
    public void SetOptionTransform(MenuState menuState)
    {
        switch (menuState)
        {
            case MenuState.ACTION_SELECTION:
                optionTransform.parent = innerTransform;
                optionTransform.localPosition = Vector3.zero;
                break;
            case MenuState.CHEMISTRY_SELECTION:
                optionTransform.parent = midTransform;
                optionTransform.localPosition = Vector3.zero;
                break;
            case MenuState.ELEMENT_SELECTION:
                // textTransform.parent = outerTransform;
                // textTransform.localPosition = Vector3.zero;
                break;
        }
    }
}

[Serializable]
public class Content_6Divs : ContentParent
{
    public void SetOptionTransform(MenuState menuState)
    {
        switch (menuState)
        {
            case MenuState.ACTION_SELECTION:
                optionTransform.parent = innerTransform;
                optionTransform.localPosition = Vector3.zero;
                break;
            case MenuState.CHEMISTRY_SELECTION:
                optionTransform.parent = midTransform;
                optionTransform.localPosition = Vector3.zero;
                break;
            case MenuState.ELEMENT_SELECTION:
                // textTransform.parent = outerTransform;
                // textTransform.localPosition = Vector3.zero;
                break;
        }
    }
}

public class RoundMenuContentManager : MonoBehaviour
{
    [SerializeField] List<Content_5Divs> innerContentList = new List<Content_5Divs>();

    // void Start()
    // {
    //     ButtonManager._instance.UpdateButtons(true);
    //     ButtonManager._instance.UpdateMenu();
    // }

    public void ChangeInnerContent(int _menuState)
    {
        Menu_StateManager._instance.SetState_ByIndex(_menuState);
        ButtonManager._instance.UpdateButtons();
        for (int i = 0; i < innerContentList.Count; i++)
            innerContentList[i].SetOptionTransform(Menu_StateManager._instance.GetState());
        ButtonManager._instance.UpdateMenu();
    }
}
