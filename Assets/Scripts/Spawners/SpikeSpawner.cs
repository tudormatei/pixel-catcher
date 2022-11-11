using UnityEngine;

namespace Scripts.Gameplay
{
    public class SpikeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject spikePrefab;
        [SerializeField] private Vector2 xbounds;
        [SerializeField] private float yHeight;

        [SerializeField] private Vector2 randomPointSpeed;

        [SerializeField] float spawnInterval = 2f;
        [SerializeField] float currentSpawnTime = 0;

        [SerializeField] float bigCountdown = 120;
        [SerializeField] float currentBigTime = 0;

        private float xleft;
        private float xRight;

        private void Start()
        {
            xleft = xbounds.x;
            xRight = xbounds.y;
        }

        private void Update()
        {
            currentSpawnTime += Time.deltaTime;
            currentBigTime += Time.deltaTime;

            if (currentSpawnTime >= spawnInterval)
            {
                Spawn();
                currentSpawnTime = 0;
            }

            if (currentBigTime >= bigCountdown)
            {
                spawnInterval -= .1f;
                currentBigTime = 0;
            }
        }

        private void Spawn()
        {
            float randomXPos = Random.Range(xleft, xRight);
            float randomSpeed = Random.Range(randomPointSpeed.x, randomPointSpeed.y);

            GameObject spike = Instantiate(spikePrefab, new Vector3(randomXPos, yHeight, 0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            spike.GetComponent<Object>().rb.gravityScale = randomSpeed;
            spike.transform.SetParent(transform);
        }
    }

}
