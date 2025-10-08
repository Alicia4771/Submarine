using UnityEngine;

public static class DataManager
{
    private static Vector3 submarine_position; // 潜水艦の座標
    private static float submarine_speed;    // 潜水艦の速度
    private static float submarine_depth;    // 潜水艦の深度
    private static bool is_periscope_up;   // 潜望鏡が上がっているかどうか

    static void Initialize()
    {
        is_periscope_up = false;
    }


    /**
     * 潜水艦の現在の座標を返す
     * @return Vector3 潜水艦の座標
     */
    public static Vector3 GetSubmarinePosition()
    {
        return submarine_position;
    }

    /**
     * 潜水艦の現在の座標を設定する
     * @param Vector3 position 潜水艦の座標
     */
    public static void SetSubmarinePosition(Vector3 position)
    {
        submarine_position = position;
    }

    /**
     * 潜水艦の現在の速度を返す
     * @return float 潜水艦の速度
     */
    public static float GetSubmarineSpeed()
    {
        return submarine_speed;
    }

    /**
     * 潜水艦の現在の速度を設定する
     * @param float speed 潜水艦の速度
     */
    public static void SetSubmarineSpeed(float speed)
    {
        submarine_speed = speed;
    }

    /**
     * 潜水艦の現在の深度を返す
     * @return float 潜水艦の深度
     */
    public static float GetSubmarineDepth()
    {
        return submarine_depth;
    }
    /**
     * 潜水艦の現在の深度を設定する
     * @param float depth 潜水艦の深度
     */
    public static void SetSubmarineDepth(float depth)
    {
        submarine_depth = depth;
    }

    /**
     * 潜望鏡が上がっているかどうかを返す
     * @return bool 潜望鏡が上がっているかどうか
     */
    public static bool GetIsPeriscopeUp()
    {
        return is_periscope_up;
    }
}