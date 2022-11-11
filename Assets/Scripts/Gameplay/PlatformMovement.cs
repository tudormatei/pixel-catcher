using UnityEngine;

namespace Scripts.Gameplay
{
    public class PlatformMovement : MonoBehaviour
    {
        private Vector3 mousePos;
        bool clicked = false;

        private void Update()
        {
            if (clicked)
            {
                mousePos = Input.mousePosition;
                mousePos.z = 17;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                transform.position = new Vector2(mousePos.x, transform.position.y);
            }
        }

        private void OnMouseDown()
        {
            clicked = true;
        }

        private void OnMouseUp()
        {
            clicked = false;
        }
    }
}
