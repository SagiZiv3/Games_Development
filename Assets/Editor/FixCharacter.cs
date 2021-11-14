using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FixCharacter
{
    [MenuItem("GameObject/Fix Animation")]
    public static void FixAnimation()
    {
        Transform transform = Selection.activeTransform;
        DoTheFix(transform);
    }

    private static void DoTheFix(Transform transform)
    {
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("mixamorig"))
            {
                int realNameStart = child.name.IndexOf(":", StringComparison.Ordinal) + 1;
                child.name = child.name.Substring(realNameStart);
            }
            DoTheFix(child);
        }
    }
}
