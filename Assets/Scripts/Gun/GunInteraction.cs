using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LaserSight laserSight;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    
    [SerializeField]private TMP_Text AmmoText;

    [Header("Gun Settings")]
    [SerializeField] private float damage = 25f;
    [SerializeField] private int maxAmmo = 6;
    [SerializeField]private int curAmmo = 6;
    //private float nextFireTime = 0f;

    //void Start()
    //{
    //    if (laserSight == null)
    //        laserSight = GetComponentInChildren<LaserSight>();
    //}

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (laserSight == null)
            laserSight = GetComponentInChildren<LaserSight>();
        AmmoText.text = curAmmo.ToString();
        
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    public void TryShoot()
    {
        if (curAmmo <= 0){
            //play emptyChamberSound
            Debug.Log("No Ammo");
            return;
        } else {
            curAmmo--;
            AmmoText.text = curAmmo.ToString();
        }

        // Checks if laser is hitting anything
        if (!laserSight.IsHitting) return;

        // Check if the object the laser is hitting has an Enemy component
        EnemyInteraction enemy = laserSight.CurrentHit.collider.GetComponent<EnemyInteraction>();
        if (enemy != null)
            enemy.TakeHit(damage);
    }

    public void Reload()
    {
        //play reload sound
        curAmmo = maxAmmo;
        AmmoText.text = curAmmo.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (curAmmo == maxAmmo) return;
        if (collision.gameObject.CompareTag("Ammo"))
        {
            Reload();
            Destroy(collision.gameObject);
        }
    }

    public void HideLaser()
    {
        Debug.Log("HideLaser in Gun called");
        laserSight.HideLine();
    }

    public void ShowLaser()
    {
        laserSight.ShowLine();
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Only show laser when grabbed by a controller, not a socket
        if (args.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor)
            laserSight.HideLine();
        else
            laserSight.ShowLine();
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Hide when released back into the world or into a socket
        if (args.interactorObject is not UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor)
            laserSight.HideLine();
    }
}
