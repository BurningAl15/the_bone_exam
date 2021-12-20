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
        UpdateButtonText(ButtonManager._instance.GetCurrentActivityState().ToString());
    }
}
