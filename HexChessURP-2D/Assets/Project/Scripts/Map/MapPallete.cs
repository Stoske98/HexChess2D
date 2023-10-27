using System.Collections.Generic;
using UnityEngine;

public class MapPallete
{
    List<string> colors;
    public List<Color> pallete = new List<Color>();
    public MapPallete(string color1str, string color2str, string color3str)
    {
        colors = new List<string>(3) { color1str, color2str, color3str };
        foreach (var color_str in colors)
        {
            if (UnityEngine.ColorUtility.TryParseHtmlString(color_str, out Color color))
            {
                pallete.Add(color);
            }
            else
            {
                Debug.LogError("Invalid hex color string: " + color_str);
            }
        }

    }
}
