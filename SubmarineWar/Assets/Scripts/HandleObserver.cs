using UnityEngine;

public class HandleObserver : MonoBehaviour
{
    [SerializeField]
    private Transform Handle;

    private Quaternion handle_rotation_quaternion;
    private Vector3 handle_rotation;

    private float stop_range = 0.003f;


    void Start()
    {
        
    }

    void Update()
    {
        handle_rotation_quaternion = Handle.rotation;
        float rotation_x = handle_rotation_quaternion.x;
        //float rotation_x = Mathf.DeltaAngle(0f, Handle.localEulerAngles.x);

        if (!(Mathf.Abs(rotation_x) < stop_range))
        {
            if (rotation_x > 0)
            {
                // 右旋回
                Debug.Log("right" + rotation_x);
            } else
            {
                // 左旋回
                Debug.Log("left" + rotation_x);
            }
        } else
        {
            Debug.Log("stop" + rotation_x);
        }
    }
}
