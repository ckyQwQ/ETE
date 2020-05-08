using UnityEngine;
using HedgehogTeam.EasyTouch;
using UnityStandardAssets.Vehicles.Aeroplane;
using ETModel;

public class CameraManager : MonoBehaviour
{
    public float rotateSpeed;
    public Vector3 offset;
    public Transform playerTransform;

    float _radius;
    Gesture _currentGesture;
    float belowMinXRotate;
    float belowMaxXRotate;
    float aboveMinXRotate;
    float aboveMaxXRotate;

    float maxOffsetLength;
    float minOffsetLength;
    float offsetLength;

    public static float swipeLock;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null && ETModelGameObject.aircraftJet == null)
        {
            return;
        }

        if (playerTransform == null && ETModelGameObject.aircraftJet != null)
        {
            playerTransform = ETModelGameObject.aircraftJet.transform;
            init();
        }
        

        modifyOffset();
        Gesture currentGesture = EasyTouch.current;

        if (currentGesture != null)
        {
            _currentGesture = currentGesture;
            if (EasyTouch.EvtType.On_TouchStart == currentGesture.type)
            {
            }

            if (EasyTouch.EvtType.On_TouchUp == currentGesture.type)
            {
            }


            if (EasyTouch.EvtType.On_Pinch == currentGesture.type && currentGesture.touchCount == 2)
            {
                zoom();
            }

            if (EasyTouch.EvtType.On_SwipeStart == currentGesture.type && currentGesture.touchCount == 1)
            {
                swipeLock = 10;
            }

            if (EasyTouch.EvtType.On_Swipe == currentGesture.type && currentGesture.touchCount == 1)
            {
                moveAround();
                pitch();
            }

            if (EasyTouch.EvtType.On_Twist == currentGesture.type)
            {
            }
        }
        swipeLock = swipeLock - Time.deltaTime;
        if (swipeLock <= 0)
        {
            cameraFdToPlayerFd();
        }
        transform.position = playerTransform.position + offset;

        if (swipeLock <= 0)
        {
            transform.forward = Vector3.Slerp(transform.forward, playerTransform.position - transform.position, Time.deltaTime * 20);
            //transform.LookAt(playerTransform.position);
        }

    }

    void init()
    {
        transform.position = playerTransform.position + offset;
        transform.LookAt(playerTransform);
        _radius = Mathf.Sqrt(offset.x * offset.x + offset.z * offset.z);

        minOffsetLength = 10f;
        maxOffsetLength = 30f;
        offsetLength = Mathf.Sqrt(offset.x * offset.x + offset.y * offset.y + offset.z * offset.z);

        belowMinXRotate = 0f;
        belowMaxXRotate = 80f;
        aboveMinXRotate = 280f;
        aboveMaxXRotate = 360f;

        swipeLock = 0;
    }

    void moveAround()
    {
        float deg = this._currentGesture.deltaPosition.x  * rotateSpeed / _radius;
        Quaternion angleAxis = Quaternion.AngleAxis(deg, Vector3.up);
        Vector3 vChanged = angleAxis * offset;
        transform.position = vChanged + playerTransform.position;
        offset = vChanged;
        transform.rotation = angleAxis * transform.rotation;
    }

    void pitch()
    {
        float xRotate = transform.localEulerAngles.x - this._currentGesture.deltaPosition.y / 50 * rotateSpeed;
        if (xRotate < 0)
        {
            xRotate = xRotate % 360.0f + 360.0f;
        }
        else if (xRotate > 360)
        {
            xRotate = xRotate % 360;
        }
        if (xRotate >= 0 && xRotate < 180)
        {
            xRotate = Mathf.Clamp(xRotate, belowMinXRotate, belowMaxXRotate);
        }
        else if (xRotate >= 180 && xRotate <= 360)
        {
            xRotate = Mathf.Clamp(xRotate, aboveMinXRotate, aboveMaxXRotate);
        }

        transform.localEulerAngles = new Vector3(xRotate, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    void zoom()
    {
        if (_currentGesture.deltaPinch > 0)
        {
            offset = Vector3.Lerp(offset, Vector3.Normalize(offset) * maxOffsetLength, Time.deltaTime * 2);
        }
        else
        {
            offset = Vector3.Lerp(offset, Vector3.Normalize(offset) * minOffsetLength, Time.deltaTime * 2);
        }
        offsetLength = offset.magnitude;
    }


    void modifyOffset()
    {
        offsetLength = Mathf.Clamp(offsetLength, minOffsetLength, maxOffsetLength);
        offset = Vector3.Normalize(offset) * offsetLength;
    }

    void cameraFdToPlayerFd()
    {
        Vector3 newVec = Quaternion.AngleAxis(30f, playerTransform.right) * playerTransform.forward;
        offset = Vector3.Slerp(offset, -Vector3.Normalize(newVec) * offsetLength, 25 * Time.deltaTime);
    }
}
