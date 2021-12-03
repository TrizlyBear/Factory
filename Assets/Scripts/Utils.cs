using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2, bool clamp = false)
    {
        if (value > to1 && clamp)
            return to2;

        if (value < from1 && clamp)
            return from2;

        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}