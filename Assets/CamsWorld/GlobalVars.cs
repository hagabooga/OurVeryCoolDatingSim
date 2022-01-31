using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVars
{
    // Character name types
    public static string MCName = "MC";
    public enum Character
    {
        Cam = 0,
        Yun = 1,
        MC = 2,
    }


    public enum Music
    {
        Stop,
        AlarmClock,
        CamsTheme,
        YunsTheme
    }

    public enum SpecialAction
    {
        Tutorial,
        TheClockIsTicking,
        RemoveCharacters
    }

    public enum Background
    {
        BlackScreen,
        AlarmClock,
        OutsideSchool,
        Bedroom,

    }


    public static string GetCharacterName(GlobalVars.Character? character)
    {
        switch (character)
        {
            case GlobalVars.Character.Cam:
                return "CAM";
            case GlobalVars.Character.Yun:
                return "YUN";
            case GlobalVars.Character.MC:
                return MCName;
            default:
                return "UNKNOWN CHAR";
        }
    }
}
