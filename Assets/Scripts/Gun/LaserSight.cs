using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserSight : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public bool IsHitting { get; private set; }
    public RaycastHit CurrentHit { get; private set; }
    private bool isHidden = false;

    [Header("Laser Settings")]
    [SerializeField] private float maxDistance = 50f;
    private float bufferMaxDistance;
    [SerializeField] private LayerMask layersToHit;

    [Header("Optional Dot Effect")]
    [SerializeField] private Transform laserDotPrefab;
    private Transform instantiatedDot;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 2;
        if (laserDotPrefab != null)
            instantiatedDot = Instantiate(laserDotPrefab);
        HideLine();

    }

    void Awake()
    {
        HideLine();
    }


    void Update()
    {
        if (isHidden)
        {
            //if(lineRenderer.enabled == false){return;}
            lineRenderer.enabled = false;
            IsHitting = false;
            if (instantiatedDot != null)
                instantiatedDot.gameObject.SetActive(false);
            return;
            
        }

        lineRenderer.enabled = true;
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

    /*public void HideLine()
    {
        lineRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0f));
    }

    public void ShowLine()
    {
        lineRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
    }*/

    public void HideLine()
    {
        isHidden = true;
    }

    public void ShowLine()
    {
        isHidden = false;
    }

    void OnDestroy()
    {
        if (instantiatedDot != null)
            Destroy(instantiatedDot.gameObject);
    }
}