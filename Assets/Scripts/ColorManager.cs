using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// [ExecuteInEditMode]
public class ColorManager : MonoBehaviour
{
    //This Script is just to make easier the change of colors in editor mode
    //Not working as expected
    //You must change the color of ElementContainer and Save with (Ctrl + S)
    //To see the change
    public static ColorManager _instance;
    [SerializeField] List<Color> colors = new List<Color>();
    Dictionary<ChemistryType, Color> colorDictionary = new Dictionary<ChemistryType, Color>();

    // void OnRenderObject()
    // {
    //     if (_instance == null)
    //     {
    //         _instance = this;
    //         for (int i = 0; i < colors.Count; i++)
    //         {
    //             print((ChemistryType)i);
    //             colorDictionary.Add((ChemistryType)i, colors[i]);
    //         }
    //     }
    // }


    public Color GetColor(ChemistryType type)
    {
        return colorDictionary[type];
    }
}
