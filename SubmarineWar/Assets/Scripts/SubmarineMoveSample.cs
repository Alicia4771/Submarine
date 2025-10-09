using UnityEngine;

public class SubmarineMoveSample : MonoBehaviour
{

    private float speed;    // 潜水艦の速度
    private float depth;    // 潜水艦の深さ
    private float torpedo_speed;

    void Start()
    {
        
    }

    void Update()
    {
        // `DataManager`に値をセット
        DataManager.SetSubmarinePosition(this.transform.position);
        DataManager.SetSubmarineSpeed(speed);
        DataManager.SetSubmarineDepth(depth);
    }
}
