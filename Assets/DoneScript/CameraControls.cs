using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] float sensitivity = 2f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mx = Input.GetAxisRaw("Mouse X") * sensitivity;
        transform.Rotate(0f, mx, 0f);
    }
}
