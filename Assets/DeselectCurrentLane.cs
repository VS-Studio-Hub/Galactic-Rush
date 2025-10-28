using UnityEngine;

public class DeselectCurrentLane : MonoBehaviour
{
    private RotatingPlatform rotatingPlatform;

    void Start()
    {
        rotatingPlatform = GetComponentInParent<RotatingPlatform>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" && rotatingPlatform != null)
        {
            rotatingPlatform.Deselect = true;
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider, true);
        }
    }
}
