using UnityEngine;

public class GameStatus : MonoBehaviour
{
    //is game active
    private static bool isActive = true;
    public static bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public static float TimeActive { get; set; }
}
