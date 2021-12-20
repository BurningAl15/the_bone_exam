using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Vector3 initialPoint;

    [SerializeField] ActivityManager activityManager;

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPoint = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 endPoint = eventData.position;
        if ((endPoint - initialPoint).normalized.x > 0)
        {
            print("Right");
            activityManager.Move(true);
        }
        else
        {
            print("Left");
            activityManager.Move(false);
        }
        print((endPoint - initialPoint).normalized);
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("Drag");
        // Vector3 endPoint = eventData.position;
        // print((endPoint - initialPoint).normalized);
    }
}
