﻿using System.Text.RegularExpressions;

namespace AtlasEngine.Modelling.C5
{
    public class Color
    {

        public static bool IsHexColorCode(string colorAsString)
        {
            return colorAsString != null && Regex.IsMatch(colorAsString, "^#[A-Fa-f0-9]{6}");
        }


    }
}
