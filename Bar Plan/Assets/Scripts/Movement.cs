using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float fl_movementSpeed;
    public float fl_mouseSpeed;
    public float fl_mouseClamp;

    private float fl_rotation_x;

    [HideInInspector] public GameObject objectSelected;
    public GameObject overheadCamera;
    public bool bl_mainCamera = true;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 cam_rotation = transform.GetChild(0).transform.localRotation.eulerAngles;
        fl_rotation_x = cam_rotation.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        CameraChange();
        LockMouse();

        if (bl_mainCamera) SelectObject();
        else objectSelected = null;
    }

    void PlayerMovement()
    {
        Vector3 v3_movement = new Vector3((Input.GetAxis("Horizontal") * fl_movementSpeed) * Time.deltaTime, 0,
                                          (Input.GetAxis("Vertical") * fl_movementSpeed) * Time.deltaTime);
        Vector3 v3_mouseMovement = new Vector3(0, Input.GetAxis("Mouse X") * fl_mouseSpeed * Time.deltaTime, 0);

        float mouseY = Input.GetAxis("Mouse Y");

        fl_rotation_x -= mouseY * fl_mouseSpeed * Time.deltaTime;
        fl_rotation_x = Mathf.Clamp(fl_rotation_x, -fl_mouseClamp, fl_mouseClamp);

        Quaternion cameraRotation = Quaternion.Euler(fl_rotation_x, 0, 0);

        transform.Translate(v3_movement);
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            transform.Rotate(v3_mouseMovement);
            transform.GetChild(0).transform.localRotation = cameraRotation;
        }
    }

    void SelectObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2)), out hit,  Mathf.Infinity))
        {
            objectSelected = hit.transform.gameObject;
        }
    }

    void CameraChange()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bl_mainCamera) bl_mainCamera = false;
            else bl_mainCamera = true;
        }

        if (bl_mainCamera)
        {
            overheadCamera.transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
        }

        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            overheadCamera.transform.GetChild(0).gameObject.SetActive(true);
        }

        overheadCamera.transform.position = new Vector3(transform.position.x,
                                                        overheadCamera.transform.position.y,
                                                        transform.position.z);
    }

    void LockMouse()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
