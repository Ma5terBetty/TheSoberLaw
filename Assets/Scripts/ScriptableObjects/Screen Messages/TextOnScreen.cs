using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextOnScreen", menuName = "Screen Messages/Message", order = 0)]
public class TextOnScreen : ScriptableObject
{
    [TextArea(3,10)]
    public string title;
    [TextArea(5, 10)]
    public string subtitle;
    [Header("Time to show the message before start fading")]
    public float timeOnScreen;
}
