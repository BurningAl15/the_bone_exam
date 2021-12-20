using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtend
{
    public static void ResetPosition(this Transform transform, Transform _)
    {
        transform.parent = _;
        transform.localPosition = Vector3.zero;
    }
}
