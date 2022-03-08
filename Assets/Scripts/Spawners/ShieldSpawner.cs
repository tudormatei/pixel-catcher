using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private Vector2 xbounds;
    [SerializeField] private float yHeight;

    [SerializeField] private Vector2 randomTimeBetweenSpawn;
    [SerializeField] private Vector2 randomPointSpeed;

    private float xleft;
    private float xRight;

    private float timeBetweenPointSpawn;
    private float startTime = 0f;

    private void Start()
    {
        timeBetweenPointSpawn = Random.Range(randomTimeBetweenSpawn.x, randomTimeBetweenSpawn.y);

        startTime = 0f;
        xleft = xbounds.x;
        xRight = xbounds.y;
    }

    private void Update()
    {
        startTime += Time.deltaTime;
        if (startTime >= timeBetweenPointSpawn)
        {
            float randomXPos = Random.Range(xleft, xRight);
            float randomSpeed = Random.Range(randomPointSpeed.x, randomPointSpeed.y);

            GameObject shield = Instantiate(pointPrefab, new Vector3(randomXPos, yHeight, 0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            shield.GetComponent<Object>().rb.gravityScale = randomSpeed;
            shield.transform.SetParent(transform);

            startTime = 0f;
        }
    }
}
