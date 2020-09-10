using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraControls : MonoBehaviour
{

    [SerializeField] float horizMoveSpeed = 100f;
    [SerializeField] float vertiMoveSpeed = 100f;

    [SerializeField] float minHorizontalAngle = -360;
    [SerializeField] float maxHorizontalAngle = 360;

    [SerializeField] float minVerticalAngleLower = 0f;
    [SerializeField] float maxVerticalAngleLower = 45f;
    [SerializeField] float minVerticalAngleUpper = 315f;
    [SerializeField] float maxVerticalAngleUpper = 370f;

    [SerializeField] float horizontalSensitivity = 0.1f;
    [SerializeField] float verticalSensitivity = 0.2f;


    float rotationX = 0f;
    float rotationY = 0f;
    float newX = 0f;
    float newY = 0f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            // Horizontal Motion
            float horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal");
            if(Mathf.Abs(horizontalMove) >= horizontalSensitivity)
            {
                newY = horizontalMove * horizMoveSpeed * Time.deltaTime;
                rotationY = transform.localEulerAngles.y + newY;
                rotationY = ClampAngle(rotationY, minHorizontalAngle, maxHorizontalAngle);
            }

            //Vertical Motion
            float verticalMove = CrossPlatformInputManager.GetAxis("Vertical");

            if (Mathf.Abs(verticalMove) >= verticalSensitivity)
            {
                newX = -verticalMove * vertiMoveSpeed * Time.deltaTime;
                rotationX = transform.localEulerAngles.x + newX;

                //Debug.Log("Rotation X: " + rotationX);
                if(rotationX >= 0f && rotationX <= 90f)
                {
                    rotationX = ClampAngle(rotationX, minVerticalAngleLower, maxVerticalAngleLower);

                }
                else if(rotationX >= 270f)
                {
                    rotationX = ClampAngle(rotationX, minVerticalAngleUpper, maxVerticalAngleUpper);

                }

                //Debug.Log("Rotation X(clamped): " + rotationX);

            

            Quaternion newQuat = Quaternion.Euler(rotationX, rotationY, 0f);
            transform.rotation = newQuat;
        }
    }

    public float ClampAngle(float angle, float min, float max)
    {
        //if (angle < -360f)
        //    angle += 360f;
        //if (angle > 360f)
        //    angle -= 360f;
        
        return Mathf.Clamp(angle, min, max);
    }
}
