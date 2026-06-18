using UnityEngine;
using Unity.XR.CoreUtils;

public class FollowTransform : MonoBehaviour
{
    private XROrigin xrOrigin;
    [SerializeField] GameObject xrOr;
    private Camera mainXrCamera;
    private Transform curPosition;

    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;

    void Start()
    {
        // Get the XR Origin attached to your Rig
        xrOrigin = xrOr.GetComponent<XROrigin>();
        // Grab the specific camera used for XR rendering
        mainXrCamera = xrOrigin.Camera;
        curPosition = transform;
    }

    void Update()
    {
        // Use 'mainXrCamera' instead of Camera.main for your logic
        Vector3 camPos = mainXrCamera.transform.position;
        Vector3 newPos = new Vector3(camPos.x + xOffset,curPosition.position.y + yOffset,camPos.z + zOffset);
        transform.position = newPos;
    }
}
