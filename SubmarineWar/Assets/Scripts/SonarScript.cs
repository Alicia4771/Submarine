using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.VirtualTexturing;

public class SonarScript : MonoBehaviour
{
    [SerializeField]
    private GameObject sonar_point;

    private Vector3 center_point;

    private float sonar_interval = 1f;  // ソナーの情報を更新する間隔(s)
    private float acc;

    private float sonar_radius = 1; // 潜水艦のソナーの半径
    private float sonar_search_radius = 100;

    private List<Vector2> displayEnemyShipList;
    private List<Vector2> displayTorpedoList;



    void Start()
    {
        center_point = this.transform.position;
    }

    void Update()
    {
        acc += Time.deltaTime;
        if (acc < sonar_interval) return;
        acc = 0f;

        displayEnemyShipList = MakeDisplayList(DataManager.GetEnemyShipDistanceList());
        displayTorpedoList = MakeDisplayList(DataManager.GetTorpedoDistanceList());

        if (!GenerateSonarPoint(displayEnemyShipList, sonar_point)) Debug.Log("敵船のソナー表示失敗");
        if (!GenerateSonarPoint(displayTorpedoList, sonar_point)) Debug.Log("魚雷のソナー表示失敗");
    }



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

    private bool GenerateSonarPoint(List<Vector2> displayList, GameObject point)
    {
        if (displayList == null) return false;
        if (point == null) return false;

        for (int i = 0; i < displayList.Count; i++)
        {
            Vector3 position = new Vector3(center_point.x + displayList[i][0], center_point.y + displayList[i][1], center_point.z);
            Instantiate(point, position, Quaternion.identity);
        }

        return true;
    }
}
