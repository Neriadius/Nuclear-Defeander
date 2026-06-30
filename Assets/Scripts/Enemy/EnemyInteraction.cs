using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using Unity.Android.Gradle;

public class EnemyInteraction : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] public float speed = 5f;
    [SerializeField] private List<Transform> enemyPath;
    [SerializeField] private RagdollHandler _ragdollHandler;
    public float currentHealth;
    private float toTargetDistance = 0.8f;
    private bool isDead;
    private bool isDying;
    private int currentTargetIndex;
    private Vector3 target;
    private Animator _animator;
    private Collider collider;
    public scoreObject Operator;

    void Start()
    {
        Operator = FindFirstObjectByType<scoreObject>();
        _animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        _ragdollHandler = GetComponent<RagdollHandler>();

        _ragdollHandler.Initialize();
        currentHealth = maxHealth;
        currentTargetIndex = 0;
        isDead = false;
        isDying = false;
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (!isDead){
            bool playerTarget = false;

            if (currentTargetIndex == enemyPath.Count)
            {
                target = new Vector3(Camera.main.transform.position.x,transform.position.y,Camera.main.transform.position.z);
            } else {
                target = enemyPath[currentTargetIndex].position;
                target.y = transform.position.y;
                playerTarget = true;
            }

            toTargetDistance = (playerTarget) ? 2f : 0.8f;

            if (Vector3.Distance(transform.position, target) > toTargetDistance)
            {
                // Moves enemy towards main while maintaining the same position.y
                //target = new Vector3(Camera.main.transform.position.x,transform.position.y,Camera.main.transform.position.z);

                // Determine which direction to rotate towards
                Vector3 targetDirection = target - transform.position;


                if(Physics.Raycast(ray, out hit, 1.5f))
                {
                    if (hit.collider.CompareTag("Door"))
                    {
                        Debug.Log("Ray collided with door");
                        if(!isDying && !isDead)
                        {
                            StartCoroutine(DoorBlocked());
                        }
                        isDying = true;
                    }
                } else
                {
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        target,
                        speed * Time.deltaTime
                    );
                }



                // Only move if no wall is directly ahead
                /*if (!Physics.Raycast(transform.position, targetDirection, 0.48f))
                {
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        target,
                        speed * Time.deltaTime
                    );
                }*/

                //_rb.linearVelocity = targetDirection.normalized * speed;

                // The step size is equal to speed times frame time.
                float singleStep = speed * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

                // Draw a ray pointing at our target in
                Debug.DrawRay(transform.position, newDirection, Color.red);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                transform.rotation = Quaternion.LookRotation(newDirection);
            } else if (currentTargetIndex != enemyPath.Count){
                currentTargetIndex++;
            } else
            {
                AttackPlayer();
            }
        }
    }

    public void TakeHit(float damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} hit! HP: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
            StartCoroutine(Die());
    }

    private void AttackPlayer()
    {
        Debug.Log("Player Ded");
        SceneManager.LoadScene(2);
    }

    public void SetPath(List<Transform> newPath)
    {
        enemyPath = newPath;
        currentTargetIndex = 0;
    }

    IEnumerator Die()
    {
        Debug.Log($"{gameObject.name} died.");
        // Here should be death logic
        isDead = true;
        _animator.enabled = false;
        _ragdollHandler.Enable();
        collider.enabled = false;

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
        Operator.AddScore(new log("Walker felled", 50));
        
    }

    IEnumerator DoorBlocked()
    {
        Debug.Log("${gameObject.name} blocked by door");
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
        Debug.Log("${gameObject} died from blocked door");
    }
}