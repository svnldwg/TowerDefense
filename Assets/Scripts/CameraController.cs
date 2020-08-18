using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 15f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    Vector3 touchStart;

    private void Update() 
    {
        if (GameManager.gameIsOver) {
            this.enabled = false;
            return;
        }

        if (Input.touchCount == 1) {
            Touch currentTouch = Input.GetTouch(0);

            if (currentTouch.phase == TouchPhase.Began) {
                touchStart = currentTouch.position;
            }

            if (currentTouch.phase == TouchPhase.Moved || currentTouch.phase == TouchPhase.Stationary) {
                Vector3 touchPosition = currentTouch.position;
                touchPosition.z = touchStart.z = transform.position.y;
                Vector3 direction = Camera.main.ScreenToWorldPoint(touchStart) - Camera.main.ScreenToWorldPoint(touchPosition);
                Debug.Log("Direction: " + direction.ToString());
                Debug.Log("Direction normalized: " + direction.normalized.ToString());

                transform.Translate(direction.normalized * Time.deltaTime * panSpeed, Space.World);
            }
        }

        if (Input.mousePosition.x <= 0 
            || Input.mousePosition.y <= 0
            || Input.mousePosition.x >= Screen.width
            || Input.mousePosition.y >= Screen.height) {
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0) {
            Vector3 pos = transform.position;
            pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            transform.position = pos;
        }
    } 
}
