using UnityEngine;
using System.Collections.Generic;

public static class DataManager
{

    private static Vector3 submarine_position; // 潜水艦の座標
    private static float submarine_speed;    // 潜水艦の速度
    private static float submarine_depth;    // 潜水艦の深度
    private static bool is_periscope_up;   // 潜望鏡が上がっているかどうか
    private static int score;       // 現在のスコア

    private static List<string> enemy_ships_list = new();


    public static void Initialize()
    {
        is_periscope_up = false;
        enemy_ships_list = new List<string>();
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

    /**
     * 現在のスコアを返す
     * @return int スコア
     */
    public static int GetScore()
    {
        return score;
    }

    /**
     * スコアを加算する
     * @param int additional_score 加算するスコア
     */
    public static void AddScore(int additional_score)
    {
        score += additional_score;
    }

    /**
     * スコアを設定する
     * @param int new_score 現在のスコア
     */
    public static void SetScore(int new_score)
    {
        score = new_score;
    }


    /**
     * 敵船を追加する
     * @param string enemyShip_name 敵船の名前（例：EnemyShip_1, EnemyShip_21）
     * @return bool 追加成功：true, 追加失敗：false
     */
    public static bool AddEnemyShip(string enemyShip_name)
    {
        // nullチェック
        if (string.IsNullOrWhiteSpace(enemyShip_name)) return false;

        // 形式チェック
        string[] tokens = enemyShip_name.Split("_");
        if (tokens.Length != 2) return false;
        if (tokens[0] != "EnemyShip") return false;

        enemy_ships_list.Add(enemyShip_name);
        return true;
    }

    /**
     * 敵船を削除する
     * @param string enemyShip_name 敵船の名前（例：EnemyShip_1, EnemyShip_21）
     * @return bool 削除成功：true, 削除失敗：false
     */
    public static bool DeleteEnemyShip(string enemyShip_name)
    {
        // nullチェック
        if (string.IsNullOrWhiteSpace(enemyShip_name)) return false;

        return enemy_ships_list.Remove(enemyShip_name);
    }

    /**
     * 敵船の一覧をList<string>型で返す
     * @return List<string> 敵船の一覧
     */
    public static List<string> GetEnemyShipList()
    {
        return enemy_ships_list;
    }

    /**
     * 潜水艦と全ての敵船の距離と方角の情報をListにして返す
     * 
     * ## 情報の入り方
     * [(subm_x - ship_x), (subm_z - ship_z), (distance)]
     * - 潜水艦から敵船への方角：Vector2([0], [1])
     * - 潜水艦から敵船までの距離；[2]
     * 
     * @return List<float[]> 
     */
    public static List<float[]> GetEnemyShipDistanceList()
    {
        List<float[]> EnemyShipDistanceList = new();

        for (int i = 0; i < enemy_ships_list.Count; i++)
        {
            GameObject EnemyShip = GameObject.Find(enemy_ships_list[i]);
            if (EnemyShip == null) continue;

            float[] result = new float[3];

            Vector3 enemyShip_pos = EnemyShip.transform.position;
            float enemyShip_pos_x = enemyShip_pos.x;
            float enemyShip_pos_z = enemyShip_pos.z;

            result[0] = enemyShip_pos_x - submarine_position.x;
            result[1] = enemyShip_pos_z - submarine_position.z;
            result[2] = Mathf.Sqrt((result[0] * result[0]) + (result[1] * result[1]));

            EnemyShipDistanceList.Add(result);
        }

        return EnemyShipDistanceList;
    }
}
