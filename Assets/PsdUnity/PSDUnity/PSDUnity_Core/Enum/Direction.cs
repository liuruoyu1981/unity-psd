﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Sprites;
using UnityEngine.Scripting;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.Assertions.Must;
using UnityEngine.Assertions.Comparers;
using System.Collections;
namespace PSDUnity
{
    public enum Direction
    {
        None = 1,
        Horizontal = 1<<1,
        Vertical = 1<<2,
        LeftToRight = 1<<3,
        BottomToTop = 1<<4,
        TopToBottom = 1<<5,
        RightToLeft = 1<<6
    }
    public enum DirectionID
    {
        None = 0,
        Horizontal = 1,
        Vertical = 2,
        LeftToRight = 3,
        BottomToTop = 4,
        TopToBottom = 5,
        RightToLeft = 6
    }
}