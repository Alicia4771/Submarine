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

    [SerializeField, Tooltip("レバーを最大まで倒した時の位置")]
    private GameObject MaxSpeedLever;

    private float lever_position;
    private float zero_position;
    private float max_position;
    private float lever_movable_range;




    void Start()
    {
        zero_position = this.transform.rotation.x;
        max_position = MaxSpeedLever.transform.rotation.x;

        lever_movable_range = zero_position - max_position;

        Destroy(MaxSpeedLever);
    }

    void Update()
    {
        lever_position = this.transform.rotation.x;
        if (lever_position > zero_position) this.transform.rotation = new Quaternion(zero_position, 0, 0, 0); lever_position = zero_position;
        if (lever_position < max_position) this.transform.rotation = new Quaternion(max_position, 0, 0, 0); lever_position = max_position;

        float lever_ratio = Mathf.Abs((lever_position - zero_position) / lever_movable_range);

        if (type == Levertype.Speed) DataManager.SetSubmarineSpeedLeverRatio(lever_ratio);

    }
}
