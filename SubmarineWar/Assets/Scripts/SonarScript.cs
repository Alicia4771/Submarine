using UnityEngine;
using System.Collections.Generic;

public class SonarScript : MonoBehaviour
{

    private Vector3 center_point;

    private float sonar_radius = 1; // 潜水艦のソナーの半径
    private float sonar_search_radius = 100;

    private List<float[]> EnemyShipList;
    private List<float[]> TorpedoList;
    private List<Vector2> displayEnemyShipList;
    private List<Vector2> displayTorpedoList;



    void Start()
    {
        center_point = this.transform.position;
    }

    void Update()
    {
        EnemyShipList = DataManager.GetEnemyShipDistanceList();
        TorpedoList = DataManager.GetTorpedoDistanceList();

        for (int i = 0; i < EnemyShipList.Count; i++)
        {
            if (EnemyShipList[i][2] < sonar_search_radius)
            {
                Vector2 direction = new Vector2(EnemyShipList[i][0], EnemyShipList[i][1]).normalized; // 正規化して方角をセット
                direction = direction * (EnemyShipList[i][2] / sonar_search_radius);                // 潜水艦との距離で位置を調整
                direction *= sonar_radius;            // ソナーの半径に合わせる、

                displayEnemyShipList.Add(direction);
            }
        }


    }
}
