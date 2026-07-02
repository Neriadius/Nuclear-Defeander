using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class AmmoSpawner : MonoBehaviour
{
    [SerializeField] private GameObject AmmoPrefab;
    private XRSocketInteractor socketInteractor;
    private Vector3 AmmoPos;
    private int SpawnerMagCount = 3;
    private int UnusedMagCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AmmoPos = transform.position;
        AmmoPos.y += 0.4f;
        if (PlayerPrefs.HasKey("SpawnerMagCount"))
        {
            SpawnerMagCount = PlayerPrefs.GetInt("SpawnerMagCount");
        }
        else
        {
            Debug.Log("SpawnerMagCount not found");
        }
        if (SpawnerMagCount > 0){
            UnusedMagCount = SpawnerMagCount;
            Instantiate(AmmoPrefab,AmmoPos,Quaternion.Euler(0f,0f,90f));
            SpawnerMagCount--;
            Debug.Log("SpawnerMagCount:" + SpawnerMagCount);
        }
    }

    void Awake()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
    }

    void OnEnable()
    {
        socketInteractor.selectExited.AddListener(OnSelectExited);
        socketInteractor.selectEntered.AddListener(OnSelectEntered);
    }

    void OnDsable()
    {
        socketInteractor.selectExited.RemoveListener(OnSelectExited);
        socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Hide when released back into the world or into a socket
        //if (args.interactorObject is not UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor)
        //    laserSight.HideLine();
        AmmoPos = transform.position;
        AmmoPos.y += 0.4f;
        if (SpawnerMagCount > 0){
            Instantiate(AmmoPrefab,AmmoPos,transform.rotation);
            SpawnerMagCount--;
            Debug.Log("SpawnerMagCount:" + SpawnerMagCount);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Ammo selected by socket");
    }

    public void MagUsed()
    {
        UnusedMagCount--;
        PlayerPrefs.SetInt("SpawnerMagCount",UnusedMagCount);
        Debug.Log("UnusedMagCount:" + UnusedMagCount);
    }
}
