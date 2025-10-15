using UnityEngine;
using UnityEngine.InputSystem;

public class ProconReceiver : MonoBehaviour
{
    Rigidbody rigidbody;

    [SerializeField]
    private UtilFunction util;

    private float speed = 3;    // 潜水艦の速度
    private float maxSpeed = 20; // 潜水艦の最大速度
    private float depth;    // 潜水艦の深さ
    private float torpedo_speed;

    private float brake = 50;   // 入力なし時の減速量（大きいほどキュッと止まる）

    private Vector2 move;
    private Vector2 look;

    private void Start()
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

    private void FixedUpdate()
    {
        // 前への移動
        if (!((move.x == 0) && (move.y == 0)))
        {
            Vector3 move_direction = transform.forward * move.y;
            rigidbody.AddForce(move_direction * speed, ForceMode.Impulse);
        } else
        {
            // 入力がなければ減速
            rigidbody.linearVelocity = Vector3.MoveTowards(rigidbody.linearVelocity, Vector3.zero, brake * Time.fixedDeltaTime);
        }

        // 速度上限を設定
        Vector3 v = rigidbody.linearVelocity;
        if (v.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rigidbody.linearVelocity = v.normalized * maxSpeed;
        }

        // 方向転換
        if (!((look.x == 0) && (look.y == 0)))
        {
            Quaternion delta = Quaternion.AngleAxis(look.x, Vector3.up);
            rigidbody.MoveRotation(rigidbody.rotation * delta);
        }
    }

    public void OnAButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("A pressed (buttonEast)");
            Fire();
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        look = ctx.ReadValue<Vector2>();
    }

    void Fire()
    {
        Vector3 dir = this.transform.forward;

        util.LaunchTorpedo(transform.position, dir);
    }
}