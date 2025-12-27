using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
        
    [SerializeField] private float sensitivityX = 750.0f;
    [SerializeField] private float sensitivityY = 750.0f;

    public Transform orientation;
    
    float xRotation;
    float yRotation;
    
    void Start()
    {
        
    }

    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (Time.timeScale == 0) return;
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivityY;
        
        yRotation += mouseX;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 65f);
        
        
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
