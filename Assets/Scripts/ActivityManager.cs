using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActivityManager : MonoBehaviour
{
    [Header("Logic Properties")]
    [SerializeField] Transform circleTransform;
    [SerializeField] Coroutine currentCoroutine;

    [SerializeField] ActivityUIController activityUIController;
    [SerializeField] UIButtonActions buttonActions;


    [Header("Animation Properties")]
    [SerializeField] AnimationCurve animCurve;
    [SerializeField] float rotationTime = .5f;
    int index = 0;


    // void Start()
    // {
    //     UpdateInfo();
    // }

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
        //Angle 
        // ! Just in case, replace all activityParents with activities to return to the working point
        float oldAngle = ButtonManager._instance.GetAngle();
        int oldIndex = ButtonManager._instance.GetCurrentIndex();
        if (Menu_StateManager._instance.GetState() == MenuState.ACTION_SELECTION)
            ButtonManager._instance.GetCurrentActivity(oldIndex).TurnOff();

        ButtonManager._instance.SetCurrentIndex(isRight);
        bool needToTransform = false;

        if (ButtonManager._instance.GetCondition(true))
        {
            ButtonManager._instance.SetCurrentIndex_Reset(true);
            needToTransform = true;
        }
        else if (ButtonManager._instance.GetCondition(false))
        {
            ButtonManager._instance.SetCurrentIndex_Reset(false);
            needToTransform = true;
        }

        if (needToTransform)
        {
            for (float i = 0; i < rotationTime; i += Time.deltaTime)
            {
                ButtonManager._instance.GetCurrentMenu().eulerAngles = new Vector3(0, 0, Mathf.Lerp(oldAngle, isRight ? 360 : -75, animCurve.Evaluate(i / rotationTime)));
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i < rotationTime; i += Time.deltaTime)
            {
                ButtonManager._instance.GetCurrentMenu().eulerAngles = new Vector3(0, 0, Mathf.Lerp(oldAngle, ButtonManager._instance.GetAngle(), animCurve.Evaluate(i / rotationTime)));
                yield return null;
            }
        }

        //Info
        if (Menu_StateManager._instance.GetState() == MenuState.ACTION_SELECTION)
        {
            ButtonManager._instance.UpdateInfo();
            if (ButtonManager._instance.GetCurrentActivity(oldIndex).info != null)
                ButtonManager._instance.GetCurrentActivity(oldIndex).info.SetActive(false);
            if (ButtonManager._instance.GetCurrentActivity().info != null)
                ButtonManager._instance.GetCurrentActivity().TurnOn();
            ButtonManager._instance.UpdateButtons_ActionSelection();
        }
        else if (Menu_StateManager._instance.GetState() == MenuState.CHEMISTRY_SELECTION)
        {
            print("Updating Chemistry");
        }
        else if (Menu_StateManager._instance.GetState() == MenuState.ELEMENT_SELECTION)
        {
            print("Updating Element");
        }

        currentCoroutine = null;
    }

    // IEnumerator JustInCase(bool isRight)
    // {
    //     List<Activity> activityParents = new List<Activity>();
    //     switch (Menu_StateManager._instance.GetState())
    //     {
    //         case MenuState.ACTION_SELECTION:
    //             activityParents = activities;
    //             break;
    //         case MenuState.CHEMISTRY_SELECTION:
    //             activityParents = chemistryActivities;
    //             break;
    //         case MenuState.ELEMENT_SELECTION:
    //             activityParents = elementActivities;
    //             break;
    //     }

    //     //Angle 
    //     // ! Just in case, replace all activityParents with activities to return to the working point
    //     float oldAngle = activityParents[index].angle;
    //     int oldIndex = index;
    //     activityParents[oldIndex].CanvasGroupInteractable(false);

    //     index = isRight ? index + 1 : index - 1;
    //     bool needToTransform = false;

    //     if (index > activityParents.Count - 1)
    //     {
    //         index = 0;
    //         needToTransform = true;
    //     }
    //     else if (index < 0)
    //     {
    //         index = activityParents.Count - 1;
    //         needToTransform = true;
    //     }


    //     if (needToTransform)
    //     {
    //         for (float i = 0; i < rotationTime; i += Time.deltaTime)
    //         {
    //             // circleTransform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(oldAngle, isRight ? 360 : -75, animCurve.Evaluate(i / rotationTime)));
    //             ButtonManager._instance.GetCurrentMenu().eulerAngles = new Vector3(0, 0, Mathf.Lerp(oldAngle, isRight ? 360 : -75, animCurve.Evaluate(i / rotationTime)));
    //             yield return null;
    //         }
    //     }
    //     else
    //     {
    //         for (float i = 0; i < rotationTime; i += Time.deltaTime)
    //         {
    //             ButtonManager._instance.GetCurrentMenu().eulerAngles = new Vector3(0, 0, Mathf.Lerp(oldAngle, activityParents[index].angle, animCurve.Evaluate(i / rotationTime)));
    //             yield return null;
    //         }
    //     }

    //     //Info
    //     UpdateInfo();
    //     if (activityParents[oldIndex].info != null)
    //         activityParents[oldIndex].info.SetActive(false);
    //     if (activityParents[index].info != null)
    //         activityParents[index].TurnOn();

    //     currentCoroutine = null;

    // }

    // void UpdateInfo()
    // {
    //     // currentActivity = activities[index];
    //     // activityUIController.UpdateActivityInfo(currentActivity.state.ToString(), currentActivity.description);
    //     // buttonActions.UpdateButtonAction(currentActivity);

    //     activityUIController.UpdateActivityInfo(ButtonManager._instance.GetCurrentActivity().ToString(), ButtonManager._instance.GetCurrentActivity().description);
    //     buttonActions.UpdateButtonAction();
    // }
}

