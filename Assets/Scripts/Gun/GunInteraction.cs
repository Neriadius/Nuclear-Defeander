using UnityEngine;

public class GunInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LaserSight laserSight;

    [Header("Gun Settings")]
    [SerializeField] private float damage = 25f;       // seconds between shots
    //private float nextFireTime = 0f;

    void Start()
    {
        if (laserSight == null)
            laserSight = GetComponentInChildren<LaserSight>();
    }

    private void TryShoot()
    {
        if (!laserSight.IsHitting) return;

        // Check if the object the laser is hitting has an Enemy component
        EnemyInteraction enemy = laserSight.CurrentHit.collider.GetComponent<EnemyInteraction>();
        if (enemy != null)
            enemy.TakeHit(damage);
    }
}
