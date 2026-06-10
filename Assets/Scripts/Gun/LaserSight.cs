using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserSight : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public bool IsHitting { get; private set; }
    public RaycastHit CurrentHit { get; private set; }

    [Header("Laser Settings")]
    [SerializeField] private float maxDistance = 50f;
    [SerializeField] private LayerMask layersToHit;

    [Header("Optional Dot Effect")]
    [SerializeField] private Transform laserDotPrefab;
    private Transform instantiatedDot;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Force world space so we can pass world positions directly
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 2;

        if (laserDotPrefab != null)
            instantiatedDot = Instantiate(laserDotPrefab);
    }

    void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        // Start point in world space
        lineRenderer.SetPosition(0, origin);

        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, layersToHit))
        {
            IsHitting = true;
            CurrentHit = hit;
            // End point is the world-space hit point
            lineRenderer.SetPosition(1, hit.point);

            if (instantiatedDot != null)
            {
                instantiatedDot.gameObject.SetActive(true);
                instantiatedDot.position = hit.point;
                // Rotate so the dot faces outward along the surface normal
                instantiatedDot.rotation = Quaternion.LookRotation(-hit.normal);
            }
        }
        else
        {
            IsHitting = false; 
            // Extend to max distance in world space
            lineRenderer.SetPosition(1, origin + direction * maxDistance);

            if (instantiatedDot != null)
                instantiatedDot.gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        if (instantiatedDot != null)
            Destroy(instantiatedDot.gameObject);
    }
}