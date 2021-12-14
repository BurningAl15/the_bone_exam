using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivityUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI activityText;
    [SerializeField] TextMeshProUGUI descriptionText;

    public void UpdateActivityInfo(string _activityName, string _description)
    {
        activityText.text = _activityName;
        descriptionText.text = _description;
    }
}
