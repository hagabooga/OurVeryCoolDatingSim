using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVars 
{
    // Character name types
    public static string MCName = "MC";
    public enum Character
    {
        Cam,
        Yun,
        MC,
    }
    public static string GetCharacterName(GlobalVars.Character character)
    {
        switch (character) {
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
