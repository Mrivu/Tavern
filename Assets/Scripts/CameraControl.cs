using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.TMP_Compatibility;

public class CameraControl : MonoBehaviour
{
    // Controls the camera movement in a 3d space and zoom

    public float CameraSensitivity;
    public float ZoomSpeed;
    public float MaxZoom;
    public float MinZoom;
    
    public float MaximumOffset;

    public Transform CameraAnchor;
    public Transform MainCamera;

    // Update is called once per frame
    void Update()
    {
        if (!Global.CameraFreeze)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            CameraRotation(mouseX, mouseY);
            GetCameraZoom();
        }

        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += CameraAnchor.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= CameraAnchor.forward; 
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= CameraAnchor.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += CameraAnchor.right;
        }

        moveDirection.y = 0;
        CameraAnchor.position += moveDirection.normalized * (-MainCamera.localPosition.z * 0.45f) * Time.deltaTime;
    }

    private void CameraRotation(float MouseX, float MouseY)
    {
        float DragX = 0;
        float DragY = 0;
        if (Input.GetMouseButtonDown(0))
        {
            DragX = MouseX;
            DragY = MouseY;
        }

        if (Input.GetMouseButton(0))
        {
            float RotationX = CameraAnchor.rotation.eulerAngles.x + (DragY - MouseY) * CameraSensitivity * 100f * Time.deltaTime;
            float RotationY = CameraAnchor.rotation.eulerAngles.y + -(DragX - MouseX) * CameraSensitivity * 100f * Time.deltaTime;

            CameraAnchor.rotation = Quaternion.Euler(
                Mathf.Clamp(RotationX, 10f, 80f),
                RotationY,
                0
                );
        }
    }

    void GetCameraZoom()
    {
        float scroll = Input.mouseScrollDelta.y;

        Vector3 CameraPosition = gameObject.transform.position;
        Vector3 AnchorPosition = new Vector3(CameraAnchor.transform.position.x, CameraAnchor.transform.position.y, CameraAnchor.transform.position.z);

        if (!(Vector3.Distance(CameraPosition, AnchorPosition) < MinZoom && scroll > 0 || Vector3.Distance(CameraPosition, AnchorPosition) > MaxZoom && scroll < 0))
        {
            Vector3 zoom = new Vector3(0, 0, scroll);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                gameObject.transform.Translate(zoom * ZoomSpeed * 100f * Time.deltaTime);
            }
            else
            {
                gameObject.transform.Translate(zoom * ZoomSpeed * 20f * Time.deltaTime);
            }
        }
    }
}

