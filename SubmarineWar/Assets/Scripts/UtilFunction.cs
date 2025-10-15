using UnityEngine;

public class UtilFunction : MonoBehaviour
{
    [SerializeField, Tooltip("発射する玉のオブジェクト")]
    private GameObject torpedoPrefab;

    /**
     * 魚雷を発射する
     */
    public void LaunchTorpedo(Vector3 pos, Vector3 dir)
    {
        if (torpedoPrefab != null)
        {
            Instantiate(torpedoPrefab, pos, Quaternion.LookRotation(dir));
        }
    }

    public void LaunchTorpedo(Vector3 pos, Vector3 dir, float speed)
    {
        if (torpedoPrefab != null)
        {
            TorpedoScript torpedoScript = torpedoPrefab.GetComponent<TorpedoScript>();
            torpedoScript.SetSpeed(speed);

            Instantiate(torpedoPrefab, pos, Quaternion.LookRotation(dir));
        }
    }


}
