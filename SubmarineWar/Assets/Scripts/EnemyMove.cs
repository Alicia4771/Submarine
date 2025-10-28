using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody rigidbody;

    private Vector3 direction;  // 方向単位ベクトル

    [SerializeField, Tooltip("最初に進む方向（設定したら）（デバッグ用）")]
    private Vector3 first_dir;

    [SerializeField, Tooltip("船が進む速度")]
    private float speed;

    [SerializeField, Tooltip("魚雷が進む速度")]
    private float torpedo_speed;

    private float detection_radius = 200.0f; // 敵が潜水艦を感知する半径

    private float discovery_point;

    private float torpedo_launch_point = 200.0f; // 魚雷を発射するのに必要な発見ポイント

    [SerializeField, Tooltip("速度調整用")]
    private float speed_adjustment = 1;
    
    [SerializeField, Tooltip("深度調整用")]
    private float depth_adjustment = 1;

    // [SerializeField, Tooltip("潜望鏡が上がっているときの加点")]
    private float scope_point = 50.0f; // 潜望鏡が上がっているときの加点


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        direction = SetDirection();

        if (first_dir != null && !(first_dir.x == 0 && first_dir.y == 0 && first_dir.z == 0))
        {
            direction = first_dir;
        }

        discovery_point = 0;
    }


    void Update()
    {
        rigidbody.AddForce(direction * speed, ForceMode.Force);

        Vector3 ship_pos = transform.position;          // 自身の座標を取得
        Vector3 submarine_pos = DataManager.GetSubmarinePosition(); // 潜水艦の座標を取得
        float submarine_speed = DataManager.GetSubmarineSpeed();    // 潜水艦の速度を取得
        float submarine_depth = DataManager.GetSubmarineDepth();    // 潜水艦の深度を取得
        bool is_periscope_up = DataManager.GetIsPeriscopeUp();    // 潜望鏡が上がっているかどうかを取得

        // 潜水艦との距離を計算（２次元）
        float distance = Mathf.Sqrt(Mathf.Pow(ship_pos.x - submarine_pos.x, 2) + Mathf.Pow(ship_pos.z - submarine_pos.z, 2));

        discovery_point = 0;
        discovery_point += (-1) * (1 / 2) * distance + detection_radius;
        discovery_point += submarine_speed * speed_adjustment;
        discovery_point += (-1) * submarine_depth * depth_adjustment;
        if (is_periscope_up) discovery_point += scope_point;


        if (discovery_point > torpedo_launch_point)
        {
            // 魚雷の進む方向を示す、方向単位ベクトル
            Vector3 torpedo_direction = (submarine_pos - ship_pos).normalized;

            LaunchTorpedo(torpedo_direction * torpedo_speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("衝突");
        if (collision.gameObject.CompareTag("EnemyShip") || collision.gameObject.CompareTag("FieldEdge"))
        {
            Debug.Log("壁に衝突しました。");
            direction = SetDirection();
        }
    }



    /**
     * ランダムに進行方向のなす角を決めて、その値を元に計算した方向単位ベクトルを返す。
     */
    private Vector3 SetDirection()
    {
        // ランダムで進行方向のなす角を決める
        float direction_angle = Random.Range(0.0f, 2 * Mathf.PI);
        // ランダムで決めた角度を元に方向ベクトルを求める
        Vector3 dir_vec = new Vector3(Mathf.Cos(direction_angle), 0, Mathf.Sin(direction_angle));
        // 求めた方向ベクトルを正規化
        dir_vec.Normalize();

        // オブジェクトの向きを設定する
        transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);

        return dir_vec;
    }

    /**
     * 敵が潜水艦に向かって魚雷を発射する
     */
    private void LaunchTorpedo(Vector3 dir)
    {
        // 未実装
    }
}