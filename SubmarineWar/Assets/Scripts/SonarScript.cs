using UnityEngine;

public class SonarScript : MonoBehaviour
{

    private Vector3 center_point;

    private float sonar_radius = 1;


    void Start()
    {
        center_point = this.transform.position;
    }

    void Update()
    {
        DataManager.GetEnemyShipDistanceList();
    }
}
