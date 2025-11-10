using UnityEngine;

public class LeverObserver : MonoBehaviour
{
    private enum Levertype
    {
        Speed,
        Depth
    }

    [SerializeField]
    private Levertype type;

    [SerializeField, Tooltip("レバーを最大まで倒した時の位置のオブジェクト")]
    private GameObject MaxLever;

    [SerializeField, Tooltip("レバーを手前に引ききった時の位置のオブジェクト(Levertype.Depthの時だけ必要)")]
    private GameObject MinDepthLever;

    private float lever_position;       // レバーの今の位置
    private float zero_position;        // レバーの初期位置
    private float max_position;         // レバーを最大まで倒した時の位置
    private float min_position;         // レバーを手前に引ききった時の位置(Levertype.Depthの時だけ)

    private float lever_movable_range;      // レバーを動かせる範囲(定数)
    private float lever_movable_range_p;    // レバーを正方向に動かせる範囲(定数)
    private float lever_movable_range_m;    // レバーを負方向に動かせる範囲(定数)

    private float defalte_lever_movable_range = 30;



    void Start()
    {
        zero_position = this.transform.rotation.x;

        if (MaxLever != null) max_position = MaxLever.transform.rotation.x;
        else max_position = zero_position + defalte_lever_movable_range;

        if (type == Levertype.Speed) lever_movable_range = zero_position - max_position;
        else if (type == Levertype.Depth)
        {
            if (MinDepthLever != null) min_position = MinDepthLever.transform.rotation.x;
            else min_position = zero_position - defalte_lever_movable_range;

            lever_movable_range = min_position - max_position;
            lever_movable_range_p = zero_position - max_position;
            lever_movable_range_m = min_position - zero_position;

            Destroy(MinDepthLever);
        }

        Destroy(MaxLever);
    }

    void Update()
    {
        lever_position = this.transform.rotation.x;

        if (type == Levertype.Speed)
        {
            // 可動範囲制限
            //if (lever_position > zero_position) this.transform.rotation = new Quaternion(zero_position, 0, 0, 0); lever_position = zero_position;
            //if (lever_position < max_position) this.transform.rotation = new Quaternion(max_position, 0, 0, 0); lever_position = max_position;

            float lever_ratio = Mathf.Abs((lever_position - zero_position) / lever_movable_range);

            //Debug.Log(lever_ratio);

            DataManager.SetSubmarineSpeedLeverRatio(lever_ratio);

        } else if (type == Levertype.Depth)
        {

            // 可動範囲制限
            //if (lever_position > min_position) this.transform.rotation = new Quaternion(min_position, 0, 0, 0); lever_position = min_position;
            //if (lever_position < max_position) this.transform.rotation = new Quaternion(max_position, 0, 0, 0); lever_position = max_position;

            float lever_position_diff = lever_position - zero_position;

            if (lever_position_diff < 0)    // 正方向
            {
                DataManager.MoveSubmarineDepth(Mathf.Abs(lever_position_diff) / lever_movable_range_p);

            }
            else
            {       // 負方向
                DataManager.MoveSubmarineDepth((-1) * (Mathf.Abs(lever_position_diff) / lever_movable_range_m));
            }
        }
    }
}
