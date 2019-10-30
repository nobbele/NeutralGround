using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerColor
{
    GREEN, BLUE, PINK, ORANGE
}

public static class PowerColorHelper
{
    public static string GetCamelCaseName(PowerColor color) {
        string LowerSelfName = System.Enum.GetName(typeof(PowerColor), color).ToLower();
        char[] buffer = LowerSelfName.ToCharArray();
        buffer[0] = char.ToUpper(buffer[0]);
        return new string(buffer);
    }
}

public static class PowerHolder {

    private static Dictionary<PowerColor, bool> States = new Dictionary<PowerColor, bool>();

    public static void SetState(PowerColor color, bool state) {
        if (!States.ContainsKey(color))
            States.Add(color, state);
        else
            States[color] = state;
    }
    public static bool IsOn(PowerColor color) {
        if (!States.ContainsKey(color))
            return false;
        else
            return States[color];
    }
}
