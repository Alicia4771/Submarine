using UnityEngine;

public class HandleObserver : MonoBehaviour
{
    [SerializeField]
    private Transform Handle;

    [SerializeField]
    private SubmarineMoveSample submarineMove;

    private Quaternion handle_rotation_quaternion;
    private Vector3 handle_rotation;

    private float stop_range = 0.003f;


    void Start()
    {

    }

    void Update()
    {
        handle_rotation_quaternion = Handle.rotation;
        float rotation_x = handle_rotation_quaternion.z;
        //float rotation_x = Mathf.DeltaAngle(0f, Handle.localEulerAngles.x);

        if (!(Mathf.Abs(rotation_x) < stop_range))
        {
            if (rotation_x < 0)
            {
                // 右旋回
                Debug.Log("right" + rotation_x);

            }
            else
            {
                // 左旋回
                Debug.Log("left" + rotation_x);
            }

            submarineMove.turn(rotation_x);
        }
        else
        {
            // 真っ直ぐ
            Debug.Log("stop" + rotation_x);

            submarineMove.turn(0);
        }
    }
}