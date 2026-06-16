using UnityEngine;
using System.Collections;
using System;

public class EnemyInteraction : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] public float speed = 5f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) > 0.001f)
        {
            // Moves enemy towards main while maintaining the same position.y
            Vector3 target = new Vector3(Camera.main.transform.position.x,transform.position.y,Camera.main.transform.position.z);
            transform.position = Vector3.MoveTowards(
                transform.position, 
                target,
                speed * Time.deltaTime
            );

            // Determine which direction to rotate towards
            Vector3 targetDirection = target - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        } else {
            AttackPlayer();
        }
    }

    public void TakeHit(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} hit! HP: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
            Die();
    }

    /*IEnumerator MoveToPlayer(Vector3 targetPosition, float moveSpeed)
    {
        // Keep looping until the distance to the target is effectively zero
        while (Vector3.Distance(transform.position, targetPosition) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                targetPosition, 
                moveSpeed * Time.deltaTime
            );
            
            yield return null;
        }

        AttackPlayer();
    }*/

    private void AttackPlayer()
    {
        Debug.Log("Player Ded");
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        // Here should be death logic
        Destroy(gameObject);
    }
}
