using UnityEngine;

public class CameraScript : MonoBehaviour
{
    void Start()
    {
        if (transform.parent != null)
        {
            this.transform.position = transform.parent.position;
        }
    }
}
