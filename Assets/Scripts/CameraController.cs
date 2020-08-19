using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panSpeedTouch = 40f;
    public float panBorderThickness = 15f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    public Camera cam;
    Vector3 touchStart;
    private bool touchPanActive = false;

    private void Update() 
    {
        if (GameManager.gameIsOver) {
            this.enabled = false;
            return;
        }

        #if UNITY_IOS || UNITY_ANDROID 
            TouchControl();
        #endif

        KeyboardMouseControl(); // TODO: do not compile this for mobile
    }

    private void KeyboardMouseControl()
    {
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

    private void TouchControl()
    {
        PanByTouch();
    }
    private void PanByTouch()
    {
        if (Input.touchCount != 1) {
            touchPanActive = false;
            return;
        }

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began) {
            touchStart = touch.position;
            touchPanActive = true;
        }

        if (touchPanActive && touch.phase == TouchPhase.Moved) {
            Vector3 touchPosition = touch.position;
            touchPosition.z = touchStart.z = transform.position.y;
            Vector3 direction = cam.ScreenToWorldPoint(touchStart) - cam.ScreenToWorldPoint(touchPosition);

            transform.Translate(direction * Time.deltaTime * panSpeedTouch, Space.World);
            //transform.position += direction * Time.deltaTime * panSpeedTouch;

            touchStart = touch.position;
        }
    }
}