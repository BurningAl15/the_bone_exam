using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIButtonActions : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buttonText;

    void UpdateButtonText(string _text)
    {
        buttonText.text = _text;
    }

    public void UpdateButtonAction()
    {
        UpdateButtonText(ButtonManager._instance.GetCurrentActivity().state.ToString());
    }

    public void ButtonAction()
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
}
