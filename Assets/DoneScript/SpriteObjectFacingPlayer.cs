using UnityEngine;

public class SpriteObjectFacingPlayer : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] bool yawOnly = false;      // keep upright if true
    [SerializeField] bool invertForward = false; // flip if your quad faces -Z

    void LateUpdate()
    {
        Vector3 dir = target.position - transform.position;
        if (yawOnly) dir.y = 0f;
        if (dir.sqrMagnitude < 1e-6f) return;

        Quaternion look = Quaternion.LookRotation(dir.normalized, Vector3.up);
        if (invertForward) look *= Quaternion.Euler(0f, 180f, 0f);

        transform.rotation = look;
    }
}
