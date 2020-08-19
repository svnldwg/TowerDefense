using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panSpeedTouch = 40f;
    public float panBorderThickness = 15f;
    public float scrollSpeed = 5f;
    public float zoomSpeedTouch = 8f;
    public float minY = 10f;
    public float maxY = 80f;

    public Camera cam;
    Vector3 touchStart;
    private bool touchPanActive = false;
    private float lastTouchZoomDistance = 0f;

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
            AddToCameraPosition(new Vector3(0, scroll * 1000 * scrollSpeed * Time.deltaTime * -1, 0));
        }
    }

    private void TouchControl()
    {
        PanByTouch();
        ZoomByTouch();
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
            
            Vector3 pos = direction * Time.deltaTime * panSpeedTouch;
            AddToCameraPosition(pos);

            touchStart = touch.position;
        }
    }

    private void ZoomByTouch()
    {
        if (Input.touchCount != 2) {
            lastTouchZoomDistance = 0f;
            return;
        }

        Vector2 touch0, touch1;
        float distance;
        touch0 = Input.GetTouch(0).position;
        touch1 = Input.GetTouch(1).position;
        distance = Vector2.Distance(touch0, touch1);

        if (lastTouchZoomDistance > 0f) {
            float diff = distance - lastTouchZoomDistance;
            AddToCameraPosition(new Vector3(0, diff * zoomSpeedTouch * Time.deltaTime * -1, 0));
        }

        lastTouchZoomDistance = distance;
    }

    private void AddToCameraPosition(Vector3 pos)
    {
        Vector3 cameraPos = transform.position;
        cameraPos += pos;
        cameraPos.y = Mathf.Clamp(cameraPos.y, minY, maxY);
        // TODO: clamp X and Z axis
        transform.position = cameraPos;
    }
}