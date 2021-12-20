using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActivityManager : MonoBehaviour
{
    Coroutine currentCoroutine;


    [Header("Animation Properties")]
    [SerializeField] AnimationCurve animCurve;
    [SerializeField] float rotationTime = .5f;


    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.D))
    //     {
    //         Move(true);
    //     }
    //     else if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         Move(false);
    //     }
    // }

    public void Move(bool isRight)
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Rotate(isRight));
    }

    IEnumerator Rotate(bool isRight)
    {
        //Angle 
        // ! Just in case, replace all activityParents with activities to return to the working point
        SoundManager._instance.PlayMoveSound(SoundTypes.MOVE);

        float oldAngle = ButtonManager._instance.GetAngle();
        int oldIndex = ButtonManager._instance.GetCurrentIndex();
        if (Menu_StateManager._instance.GetState() == MenuState.ACTION_SELECTION)
            ButtonManager._instance.GetCurrentActivity(oldIndex).TurnOff();
        // else if (Menu_StateManager._instance.GetState() == MenuState.ELEMENT_SELECTION)
        //     ButtonManager._instance.GetCurrentElement(oldIndex).TurnOff();

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
            ButtonManager._instance.UpdateActivityInfo();
            if (ButtonManager._instance.GetCurrentActivity(oldIndex).info != null)
                ButtonManager._instance.GetCurrentActivity(oldIndex).info.SetActive(false);
            if (ButtonManager._instance.GetCurrentActivity().info != null)
                ButtonManager._instance.GetCurrentActivity().TurnOn();
            // ButtonManager._instance.UpdateButtons_ActionSelection();
            ButtonManager._instance.UpdateButtons();
        }
        else if (Menu_StateManager._instance.GetState() == MenuState.CHEMISTRY_SELECTION)
        {
            print("Updating Chemistry");
        }
        else if (Menu_StateManager._instance.GetState() == MenuState.ELEMENT_SELECTION)
        {
            ButtonManager._instance.UpdateElementInfo();
        }

        currentCoroutine = null;
    }
}

