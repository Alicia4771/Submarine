using UnityEngine;

public class TorpedoScript : MonoBehaviour
{
    private Rigidbody rigidbody;

    private Vector3 direction;  // 進む方向
    private float speed;        // 魚雷が進む速さ
    private float max_speed;    // 魚雷の最大速度

    private float default_speed = 1;
    private float default_maxSpeed_agnification = 2;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        direction = transform.forward;

        if (speed <= 0) speed = default_speed;
        if (max_speed <= 0) max_speed = speed * default_maxSpeed_agnification;
    }

    void Update()
    {
        if (direction != null)
        {
            rigidbody.AddForce(direction * speed, ForceMode.Force);

            // 速度制限
            Vector3 v = rigidbody.linearVelocity;
            if (v.sqrMagnitude > max_speed * max_speed)
            {
                rigidbody.linearVelocity = v.normalized * max_speed;
            }
        }
    }

    public void SetSpeed(float speed)
    {
        if (speed > 0) this.speed = speed;
    }

    public void SetSpeed(float speed, float speedMax)
    {
        if (speed > 0) this.speed = speed;
        if (speed >= speedMax) this.max_speed = speedMax;
    }
}
