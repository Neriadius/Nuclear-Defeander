using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserSight : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Renderer dotRenderer;
    public bool IsHitting { get; private set; }
    public RaycastHit CurrentHit { get; private set; }
    private bool isHidden = true;

    [Header("Laser Settings")]
    [SerializeField] private float maxDistance = 50f;
    private float bufferMaxDistance;
    [SerializeField] private LayerMask layersToHit;

    [Header("Optional Dot Effect")]
    [SerializeField] private GameObject laserDotPrefab;
    private GameObject instantiatedDot;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // Force world space so we can pass world positions directly
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 2;

        if (laserDotPrefab != null)
            instantiatedDot = Instantiate(laserDotPrefab);
        dotRenderer = instantiatedDot.GetComponent<Renderer>();
        HideLine();
    }

    void Awake()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        
        //Debug.Log("HideLine in LaserSight Awake called");
        //HideLine();
    }

    void OnEnable()
    {
        Debug.Log("LaserSight OnEnable called");
        if (lineRenderer != null && isHidden)
        HideLine();
    }

    void Update()
    {
        if(!isHidden){
            ShootLaser();
        }
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
                instantiatedDot.transform.position = hit.point;
                // Rotate so the dot faces outward along the surface normal
                instantiatedDot.transform.rotation = Quaternion.LookRotation(-hit.normal);
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


    public void HideLine()
    {
        isHidden = true;
        lineRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0f));
        dotRenderer.enabled = false;
        //instantiatedDot.transform.position += new Vector3(0f,-50f,0f);
    }

    public void ShowLine()
    {
        isHidden = false;
        lineRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
        dotRenderer.enabled = true;
       // instantiatedDot.transform.position += new Vector3(0f,50f,0f);
    }
}