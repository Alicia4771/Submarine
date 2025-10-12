using UnityEngine;
using UnityEngine.InputSystem;

public class ProconReceiver : MonoBehaviour
{
    Rigidbody rigidbody;

    private float submarine_speed = 10;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
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
        Vector2 move = ctx.ReadValue<Vector2>();
        Vector3 move_direction = new Vector3(move.x, 0, move.y).normalized;

        //Debug.Log(move);

        rigidbody.AddForce(move_direction * submarine_speed, ForceMode.Impulse);
    }

    void Fire() { /* 任意の処理 */ }
}