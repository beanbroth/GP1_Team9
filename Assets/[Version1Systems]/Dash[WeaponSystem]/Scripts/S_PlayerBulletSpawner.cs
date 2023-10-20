using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class S_PlayerBulletSpawner : MonoBehaviour
{
    [Header("Basic Settings")] public GameObject bulletPrefab;
    [FormerlySerializedAs("rateOfFire")] public float timeBetweenShots = 0.5f;
    public int bulletsPerShot = 1;
    [Header("Spacing Settings")] public SpacingType spacing;
    [Header("Angle Settings")] public float angleCenter = 0f;
    public float angleWidth = 90f;
    [HideInInspector] public Vector2 angleRange = new Vector2(-45f, 45f);
    public float minAngle => angleCenter + -angleWidth / 2;
    public float maxAngle => angleCenter + angleWidth / 2;
    private float timer;
    [Header("Gizmo Settings")] public bool gizmosEnabled = true;
    public Color gizmoColor = Color.green;
    public float gizmoRadius = 2f;
    public int gizmoSegments = 20;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenShots)
        {
            SpawnBullets();
            timer = 0;
        }
    }

    void SpawnBullets()
    {
        switch (spacing)
        {
            case SpacingType.Even:
                SpawnEvenBullets();
                break;
            case SpacingType.Random:
                SpawnRandomBullets();
                break;
            case SpacingType.PingPong:
                StartCoroutine(SpawnPingPongBullets());
                break;
        }
    }

    void SpawnEvenBullets()
    {
        float angleStep = (maxAngle - minAngle) / (bulletsPerShot - 1);
        if (bulletsPerShot <= 1)
        {
            SpawnPooledBullet(Quaternion.Euler(0f, angleCenter, 0f));
            return;
        }

        for (int i = 0; i < bulletsPerShot; i++)
        {
            float currentAngle = minAngle + angleStep * i;
            Quaternion rotation = Quaternion.Euler(new Vector3(0f, currentAngle, 0f));
            SpawnPooledBullet(rotation);
        }
    }

    private void SpawnPooledBullet(Quaternion rotation)
    {
        //GameObject pooledBullet = Instantiate(bulletPrefab);
        GameObject pooledBullet = S_ObjectPoolManager.Instance.GetObject(bulletPrefab);
        pooledBullet.transform.position = transform.position;
        pooledBullet.transform.rotation = rotation * transform.root.rotation;
        pooledBullet.gameObject.SetActive(true);
    }

    void SpawnRandomBullets()
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            float currentAngle = Random.Range(minAngle, maxAngle);
            Quaternion rotation = Quaternion.Euler(0f, currentAngle, 0f);
            SpawnPooledBullet(rotation);
        }
    }

    bool shootFromLeft = true;

    IEnumerator SpawnPingPongBullets()
    {
        float timeBetweenBulletSpawns = timeBetweenShots / (bulletsPerShot * 2);
        for (int i = 0; i < bulletsPerShot; i++)
        {
            float elapsedTime = i * timeBetweenBulletSpawns;
            float lerpValue = elapsedTime / timeBetweenShots;
            float currentAngle = Mathf.Lerp(0f, maxAngle - minAngle, lerpValue) + minAngle;
            if (!shootFromLeft)
            {
                currentAngle = maxAngle - (currentAngle - minAngle);
            }

            Quaternion rotation = Quaternion.Euler(0, currentAngle, 0f);
            SpawnPooledBullet(rotation);
            yield return new WaitForSeconds(timeBetweenBulletSpawns);
        }

        // Toggle the side variable for the next time the coroutine is called
        shootFromLeft = !shootFromLeft;
    }

    private void OnValidate()
    {
        angleCenter = Mathf.Clamp(angleCenter, -180f, 180f);
        angleWidth = Mathf.Clamp(angleWidth, 0f, 360f);
        bulletsPerShot = Mathf.Clamp(bulletsPerShot, 1, 1000);
    }

    void OnDrawGizmos()
    {
        if (gizmosEnabled == false || gizmoSegments == 0)
            return;
        Gizmos.color = gizmoColor;
        float angleStep = angleWidth / gizmoSegments;
        Vector3 previousPoint = transform.position + Quaternion.Euler(0, minAngle, 0) * Vector3.forward * gizmoRadius;
        Gizmos.DrawLine(transform.position, previousPoint);
        for (int i = 1; i <= gizmoSegments; i++)
        {
            float currentAngle = minAngle + angleStep * i;
            Vector3 currentPoint =
                transform.position + Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * gizmoRadius;
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }

        Gizmos.DrawLine(transform.position, previousPoint);
    }

    public enum SpacingType
    {
        Even,
        Random,
        PingPong
    }
}