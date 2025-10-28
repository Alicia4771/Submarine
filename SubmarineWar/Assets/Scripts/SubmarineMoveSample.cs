using UnityEngine;

public class SubmarineMoveSample : MonoBehaviour
{
    private Rigidbody rigidbody;

    private float speed;    // 潜水艦の速度
    private float depth;    // 潜水艦の深さ
    private float torpedo_speed;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // `DataManager`に値をセット
        DataManager.SetSubmarinePosition(this.transform.position);
        DataManager.SetSubmarineSpeed(speed);
        DataManager.SetSubmarineDepth(depth);
    }


    public void turn(float rotation_x)
    {
        if (rotation_x != 0)
        {
            Quaternion delta = Quaternion.AngleAxis(rotation_x, Vector3.up);
            rigidbody.MoveRotation(rigidbody.rotation * delta);
        }
        else
        {
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
}