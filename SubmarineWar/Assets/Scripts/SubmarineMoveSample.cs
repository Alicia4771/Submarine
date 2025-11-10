using UnityEngine;

public class SubmarineMoveSample : MonoBehaviour
{
    private Rigidbody rigidbody;

    private float speed;    // 潜水艦の速度
    private float depth;    // 潜水艦の深さ
    private float torpedo_speed;    // 魚雷の速度

    private float brake = 3;   // 入力なし時の減速量
    private float maxSpeed;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        maxSpeed = DataManager.GetSubmarineMaxSpeed();
    }

    void Update()
    {
        // `DataManager`に値をセット
        DataManager.SetSubmarinePosition(this.transform.position);
        //DataManager.SetSubmarineSpeed(speed);
        //DataManager.SetSubmarineDepth(depth);

        speed = DataManager.GetSubmarineSpeed();
        depth = DataManager.GetSubmarineDepth();

        streat();
    }



    private void streat()
    {
        Vector3 move_direction = transform.forward * speed;
        rigidbody.AddForce(move_direction, ForceMode.Impulse);

        // 速度を少し減衰させる
        rigidbody.linearVelocity = Vector3.MoveTowards(rigidbody.linearVelocity, Vector3.zero, brake * Time.fixedDeltaTime);

        // 速度上限を設定
        Vector3 v = rigidbody.linearVelocity;
        if (v.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rigidbody.linearVelocity = v.normalized * maxSpeed;
        }
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