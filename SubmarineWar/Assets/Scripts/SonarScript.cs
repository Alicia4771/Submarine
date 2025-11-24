using System;
using UnityEngine;
using System.Collections.Generic;

public class SonarScript : MonoBehaviour
{
    [SerializeField]
    private GameObject sonar_point;

    private Vector3 center_point;

    private float sonar_interval = 1f;  // ソナーの情報を更新する間隔(s)
    private float acc;

    private float sonar_radius = 0.5f; // 潜水艦のソナーの半径
    private float sonar_search_radius = 100;

    private float submarine_rotation;

    private List<Vector2> displayEnemyShipList;
    private List<Vector2> displayTorpedoList;


    void Start()
    {
        center_point = this.transform.position;
    }

    void Update()
    {
        submarine_rotation = DataManager.GetSubmarineSpeed();

        acc += Time.deltaTime;
        if (acc < sonar_interval) return;
        acc = 0f;

        foreach (GameObject sonar_point in GameObject.FindGameObjectsWithTag("SonarPoint")) Destroy(sonar_point);

        displayEnemyShipList = MakeDisplayList(DataManager.GetEnemyShipDistanceList());
        displayTorpedoList = MakeDisplayList(DataManager.GetTorpedoDistanceList());

        if (!GenerateSonarPoint(displayEnemyShipList, sonar_point)) Debug.Log("敵船のソナー表示失敗");
        if (!GenerateSonarPoint(displayTorpedoList, sonar_point)) Debug.Log("魚雷のソナー表示失敗");
    }


    /**
     * 敵船や魚雷の方向と距離の情報がまとめられたListを受け取り、ソナーに表示する位置の情報に変換されたListを返す。
     * 
     * @param List<float[]> RawList 潜水艦からの方向と距離の情報を持った一覧のList
     * @return List<Vector2> ソナーに表示する座標の情報だけを持ったList
     */
    private List<Vector2> MakeDisplayList(List<float[]> RawList)
    {
        if (RawList == null) return null;

        List<Vector2> displayList = new();

        for (int i = 0; i < RawList.Count; i++)
        {
            if (RawList[i][2] < sonar_search_radius)
            {
                Vector2 direction = new Vector2(RawList[i][0], RawList[i][1]).normalized;
                direction = direction * (RawList[i][2] / sonar_search_radius);
                direction *= sonar_radius;

                displayList.Add(direction);
            }
        }
            
        return displayList;
    }


    /**
     * ソナーに表示する座標の情報だけを持ったListから、指定されたオブジェクトをその位置に生成する
     * 
     * @param List<Vector2> displayList ソナーに表示する座標の情報を持ったList
     * @param GameObject point 生成するオブジェクト
     * 
     * @return bool 追加成功：true, 追加失敗：false
     */
    private bool GenerateSonarPoint(List<Vector2> displayList, GameObject point)
    {
        if (displayList == null) return false;
        if (point == null) return false;

        float sin = Mathf.Sin(submarine_rotation * ((float)Math.PI / 180));
        float cos = Mathf.Cos(submarine_rotation * ((float)Math.PI / 180));

        for (int i = 0; i < displayList.Count; i++)
        {
            float x = displayList[i][0];
            float y = displayList[i][1];
            float result_x = x * cos - y * sin;
            float result_y = x * sin + y * cos;

            Vector3 position = new Vector3(center_point.x + result_x, center_point.y + result_y, center_point.z);
            Instantiate(point, position, Quaternion.identity);
        }

        return true;
    }
}
