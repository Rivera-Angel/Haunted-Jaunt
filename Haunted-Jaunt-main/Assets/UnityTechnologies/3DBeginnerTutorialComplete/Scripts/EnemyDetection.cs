using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class EnemyDetection : MonoBehaviour
{
    [Header("Dimming Settings")]
    public Image dimmingImage;
    public float maxDimmingAlpha = 0.4f;
    public float dimmingSpeed = 10f;
    private float currentDimmingAlpha = 0f;

    [Header("Enemy Detection")]
    public float detectionRadius = 10f;
    public string enemyTag = "Enemy";
    public float visionCone = 30f;
    //public Transform player; // Point from which to cast the ray (e.g., player)
    public LayerMask obstructionLayers; // Layers that can block line of sight

    private List<Transform> nearbyEnemies = new List<Transform>();

    private void Start()
    {
        // converts degrees to rads for later calc on visionCone
        visionCone = Mathf.Cos(Mathf.Deg2Rad * (visionCone / 2.0f));
    }

    void Update()
    {
        // Create list of nearby enemies if present. If not, exit
        if (FindNearbyEnemies())
        {
            Transform enemy = closestVisibleEnemy();

            if (enemy != null)
            {
                float dimMod = FindDimModifier(enemy);
                currentDimmingAlpha = Mathf.Lerp(currentDimmingAlpha, dimMod, Time.deltaTime * dimmingSpeed);
            }
        }
        else
        {
            currentDimmingAlpha = Mathf.Lerp(currentDimmingAlpha, 0f, Time.deltaTime * dimmingSpeed);
        }

        Color currentColor = dimmingImage.color;
        currentColor.a = currentDimmingAlpha;
        dimmingImage.color = currentColor;
    }

    // creates list of nearby enemies if present
    bool FindNearbyEnemies()
    {
        nearbyEnemies.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        bool found = false;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(enemyTag))
            {
                nearbyEnemies.Add(hitCollider.transform);
                found = true;
            }
        }
        return found;
    }

    // checks 
    Transform closestVisibleEnemy()
    {
        // variables
        Transform closestEnemy = null;
        float minDistance = detectionRadius;

        // for each enemy nearby, check:
        //      - if you can see it
        //      - if its closer than the rest
        // if so, mark as such
        foreach (Transform enemy in nearbyEnemies)
        {
            float distance = HasLineOfSight(enemy);
            if (minDistance > distance && distance != 0)
            {
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    // checks if line of sight & if so, returns distance to enemy
    float HasLineOfSight(Transform enemy)
    {
        Vector3 direction = enemy.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction.normalized, out hit, detectionRadius, ~obstructionLayers))
        {
            if (hit.collider != null && hit.collider.CompareTag(enemyTag))
            {
                // return distance
                return Vector3.Magnitude(direction);
            }
        }
        return 0;
    }

    float FindDimModifier(Transform enemy)
    {
        // direction from enemy to player
        Vector3 directionToPlayer = transform.position - enemy.position;
        // where enemy is facing
        Vector3 enemyFacing = enemy.forward;


        // calc distance to enemy and how much to modify dimming
        float distance = Vector3.Magnitude(transform.position - enemy.position);
        // modify dimming relative to closeness
        // EX if detRad is 6, and 6 away, 6-6+1=1. 1/6 times the dimming makes it smaller
        // closing the gap increases the numerator, which makes the dimming larger
        // EX if detRad is 6, 1 away, 6-1+1=6. 6/6 times dimming makes dimming max
        float dimMod = (detectionRadius - distance + 1) / detectionRadius;

        float dimAlpha = maxDimmingAlpha * dimMod;

        // half dimming if not facing player
        if (!IsFacing(directionToPlayer, enemyFacing))
        {
            dimAlpha = dimAlpha * 0.5f;
        }

        return dimAlpha;
    }

    // check if facing
    bool IsFacing(Vector3 direction, Vector3 forward)
    {
        // product that says if player is facing same direction towards enemy
        float dotProduct = Vector3.Dot(forward, direction);
        float angle = dotProduct / (Vector3.Magnitude(forward) * Vector3.Magnitude(direction));

        if (angle > visionCone)
        {
            return true;
        }
        else return false;
    }



    // Optional: Visualize the detection radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}