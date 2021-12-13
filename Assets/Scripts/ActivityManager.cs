using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum State
{
    Fusion,
    Discover,
    Forge,
    Mix,
    None,
}

[Serializable]
public class Activity
{
    public State state;

    public string description;

    public float angle;
    public GameObject info;

    public void Init()
    {
        if (info != null)
            info.SetActive(false);
    }
}

public class ActivityManager : MonoBehaviour
{
    [Header("Logic Properties")]
    [SerializeField] Transform circleTransform;
    [SerializeField] List<Activity> activities;
    [SerializeField] Activity currentActivity;
    [SerializeField] Coroutine currentCoroutine;

    [SerializeField] TextMeshProUGUI activityText;
    [SerializeField] TextMeshProUGUI descriptionText;


    [Header("Animation Properties")]
    [SerializeField] AnimationCurve animCurve;
    [SerializeField] float rotationTime = .5f;
    int index = 0;

    void Awake()
    {
        for (int i = 1; i < activities.Count; i++)
            activities[i].Init();
        UpdateInfo();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Move(true);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Move(false);
        }
    }

    void Move(bool isRight)
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Rotate(isRight));
    }

    IEnumerator Rotate(bool isRight)
    {
        float oldAngle = activities[index].angle;
        int oldIndex = index;

        index = isRight ? index + 1 : index - 1;
        bool needToTransform = false;

        if (index > activities.Count - 1)
        {
            index = 0;
            needToTransform = true;
        }
        else if (index < 0)
        {
            index = activities.Count - 1;
            needToTransform = true;
        }


        if (needToTransform)
        {
            for (float i = 0; i < rotationTime; i += Time.deltaTime)
            {
                circleTransform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(oldAngle, isRight ? 360 : -75, animCurve.Evaluate(i / rotationTime)));
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i < rotationTime; i += Time.deltaTime)
            {
                circleTransform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(oldAngle, activities[index].angle, animCurve.Evaluate(i / rotationTime)));
                yield return null;
            }
        }

        UpdateInfo();
        if (activities[oldIndex].info != null)
            activities[oldIndex].info.SetActive(false);
        if (activities[index].info != null)
            activities[index].info.SetActive(true);

        currentCoroutine = null;
    }

    void UpdateInfo()
    {
        currentActivity = activities[index];
        activityText.text = currentActivity.state.ToString();
        descriptionText.text = currentActivity.description;
    }
}

