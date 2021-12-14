using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIButtonActions : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buttonText;

    Activity currentActivity;

    void UpdateButtonText(string _text)
    {
        buttonText.text = _text;
    }

    public void UpdateButtonAction(Activity activity)
    {
        UpdateButtonText(activity.state.ToString());
        currentActivity = activity;
    }

    public void ButtonAction()
    {
        switch (currentActivity.state)
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
