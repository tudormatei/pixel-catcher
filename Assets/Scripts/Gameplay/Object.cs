using UnityEngine;

namespace Scripts.Gameplay
{
    public class Object : MonoBehaviour
    {
        public Rigidbody2D rb;

        [SerializeField] private ParticleSystem lavaEffect;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.tag == "Lava")
            {
                lavaEffect.Play();
                BoxCollider2D boxCol = GetComponent<BoxCollider2D>();
                CircleCollider2D circleCol = GetComponent<CircleCollider2D>();
                PolygonCollider2D polCol = GetComponent<PolygonCollider2D>();
                if (boxCol != null)
                {
                    boxCol.enabled = false;
                }
                else if (polCol != null)
                {

                    polCol.enabled = false;
                }
                else if (circleCol != null)
                {

                    circleCol.enabled = false;
                }

                rb.velocity = rb.velocity / 10;
                Destroy(gameObject, 3f);
            }
        }
    }
}
