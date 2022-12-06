using UnityEngine;
public class Yay 
{
    public int height = 1;
    public YayColor color;
    public Yay(int _height,YayColor _color)
    {
        color = _color;
        height = _height;
    }
    public static Color GetColorFromEnum(YayColor color)
    {
        switch (color)
        {
            case YayColor.Red:
                return Color.red;
            case YayColor.Green:
                return Color.green;
            case YayColor.Blue:
                return Color.blue;
            default:
                return Color.white;
        }
    }
}
public enum YayColor{
    Red,
    Green,
    Blue
}
