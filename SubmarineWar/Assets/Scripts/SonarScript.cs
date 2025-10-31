using UnityEngine;
using System.Collections.Generic;

public class SonarScript : MonoBehaviour
{

    private Vector3 center_point;

    private float sonar_radius = 1; // 潜水艦のソナーの半径

    private List<float[]> EnemyShipList;
    private List<float[]> TorpedoList;



    void Start()
    {
        center_point = this.transform.position;
    }

    void Update()
    {
        EnemyShipList = DataManager.GetEnemyShipDistanceList();
        TorpedoList = DataManager.GetTorpedoList();
    }
}
