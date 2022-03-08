using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAdderSpawner : MonoBehaviour
{
    [SerializeField] private GameObject comboAdderPrefab;
    [SerializeField] private Vector2 xbounds;
    [SerializeField] private float yHeight;

    [SerializeField] private Vector2 randomPointSpeed;

    private float xleft;
    private float xRight;

    private float timeBetweenPointSpawn;
    private float startTime = 0f;

    private void Start()
    {
        timeBetweenPointSpawn = Random.Range(20f, 25f);

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

            GameObject comboAdder = Instantiate(comboAdderPrefab, new Vector3(randomXPos, yHeight, 0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            comboAdder.GetComponent<Object>().rb.gravityScale = randomSpeed;
            comboAdder.transform.SetParent(transform);

            startTime = 0f;
        }
    }
}
