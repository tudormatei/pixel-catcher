using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboRemoverSpawner : MonoBehaviour
{
    [SerializeField] private GameObject comboRemoverPrefab;
    [SerializeField] private Vector2 xbounds;
    [SerializeField] private float yHeight;

    [SerializeField] private Vector2 randomPointSpeed;

    private float xleft;
    private float xRight;

    private float timeBetweenPointSpawn;
    private float startTime = 0f;

    private void Start()
    {
        timeBetweenPointSpawn = Random.Range(15f, 20f);

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

            GameObject comboRemover = Instantiate(comboRemoverPrefab, new Vector3(randomXPos, yHeight, 0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            comboRemover.GetComponent<Object>().rb.gravityScale = randomSpeed;
            comboRemover.transform.SetParent(transform);

            startTime = 0f;
        }
    }
}
