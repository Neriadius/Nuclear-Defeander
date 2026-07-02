using UnityEngine;

public class MacheteInteraction : MonoBehaviour
{
    [SerializeField] private int damage = 9999;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
            EnemyInteraction enemyScript = collision.gameObject.GetComponent<EnemyInteraction>();
            if (enemyScript != null) enemyScript.TakeHit(damage);
    }
}
