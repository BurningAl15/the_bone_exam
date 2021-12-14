using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// [ExecuteInEditMode]
public class ElementContainer : MonoBehaviour
{
    public ChemistryType chemistryType;

    [Header("Logic Properties")]
    public float maxValue;
    public float currentValue;

    [Header("UI Components")]
    public TextMeshProUGUI text;
    public Image img;

    // void OnRenderObject()
    // {
    //     img.color = ColorManager._instance.GetColor(chemistryType);
    // }

    public void UpdateChemistryValue(float updateValue)
    {
        currentValue = Mathf.Lerp(0, maxValue, updateValue);
        text.text = FormatValue();
    }

    string FormatValue()
    {
        return KParse(maxValue) + "/" + KParse(currentValue);
    }
    string KParse(float value)
    {
        return value > 100 ? (value / 1000).ToString("F1") + "K" : (value / 1000).ToString("F1");
    }
}
