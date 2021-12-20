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
                optionTransform.ResetPosition(innerTransform);
                break;
            case MenuState.CHEMISTRY_SELECTION:
                optionTransform.ResetPosition(midTransform);
                break;
            case MenuState.ELEMENT_SELECTION:
                break;
        }
    }
}

[Serializable]
public class Content_6Divs : ContentParent
{
    public Transform outerTransform;

    public void SetOptionTransform(MenuState menuState)
    {
        switch (menuState)
        {
            case MenuState.ACTION_SELECTION:
                optionTransform.ResetPosition(innerTransform);
                break;
            case MenuState.CHEMISTRY_SELECTION:
                optionTransform.ResetPosition(midTransform);
                break;
            case MenuState.ELEMENT_SELECTION:
                optionTransform.ResetPosition(outerTransform);
                break;
            case MenuState.PRODUCTION:
                break;
        }
    }
}

[Serializable]
public class Content_Element : ContentParent
{
    public void SetOptionTransform(MenuState menuState)
    {
        switch (menuState)
        {
            case MenuState.ACTION_SELECTION:
                optionTransform.ResetPosition(innerTransform);
                break;
            case MenuState.CHEMISTRY_SELECTION:
                optionTransform.ResetPosition(innerTransform);
                break;
            case MenuState.ELEMENT_SELECTION:
                optionTransform.ResetPosition(midTransform);
                break;
            case MenuState.PRODUCTION:
                break;
        }
    }
}

public class RoundMenuContentManager : MonoBehaviour
{
    [SerializeField] List<Content_5Divs> innerContentList = new List<Content_5Divs>();
    [SerializeField] List<Content_6Divs> innerContentList_6 = new List<Content_6Divs>();
    [SerializeField] List<Content_Element> elementContentList = new List<Content_Element>();

    [Header("Inner and Outer Container")]
    [SerializeField] Transform innerContainer;
    [SerializeField] Transform outerContainer;
    [SerializeField] Transform outerOuterContainer;

    [SerializeField] Transform contentContainer;

    public void InnerContentChange()
    {
        for (int i = 0; i < innerContentList.Count; i++)
            innerContentList[i].SetOptionTransform(Menu_StateManager._instance.GetState());
        for (int i = 0; i < innerContentList_6.Count; i++)
            innerContentList_6[i].SetOptionTransform(Menu_StateManager._instance.GetState());
        for (int i = 0; i < elementContentList.Count; i++)
            elementContentList[i].SetOptionTransform(Menu_StateManager._instance.GetState());
    }

    public void OuterContentChange()
    {
        switch (Menu_StateManager._instance.GetState())
        {
            case MenuState.ACTION_SELECTION:
                contentContainer.ResetPosition(innerContainer);
                if (elementContentList.Count > 0)
                    contentContainer.ResetPosition(outerOuterContainer);
                break;
            case MenuState.CHEMISTRY_SELECTION:
                if (innerContentList.Count > 0)
                    contentContainer.ResetPosition(outerContainer);
                else if (innerContentList_6.Count > 0)
                    contentContainer.ResetPosition(innerContainer);
                else if (elementContentList.Count > 0)
                    contentContainer.ResetPosition(outerContainer);
                break;
            case MenuState.ELEMENT_SELECTION:
                if (innerContentList.Count > 0)
                    contentContainer.ResetPosition(outerOuterContainer);
                else if (innerContentList_6.Count > 0)
                    contentContainer.ResetPosition(outerContainer);
                else if (elementContentList.Count > 0)
                    contentContainer.ResetPosition(innerContainer);
                break;
        }
    }
}
