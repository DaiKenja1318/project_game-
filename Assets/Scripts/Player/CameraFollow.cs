using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                 // mục tiêu để theo (nhân vật)
    public float smoothTime = 0.2f;          // thời gian làm mượt
    public Vector2 offset;                   // offset so với vị trí nhân vật

    public BoxCollider2D bounds;             // vùng giới hạn mà camera được phép di chuyển
    private float minX, maxX, minY, maxY;

    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (bounds != null)
        {
            minX = bounds.bounds.min.x + cam.orthographicSize * cam.aspect;
            maxX = bounds.bounds.max.x - cam.orthographicSize * cam.aspect;
            minY = bounds.bounds.min.y + cam.orthographicSize;
            maxY = bounds.bounds.max.y - cam.orthographicSize;
        }
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        // vị trí mong muốn
        Vector3 desired = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);

        // làm mượt chuyển động camera
        Vector3 smoothed = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);

        float clampedX = smoothed.x;
        float clampedY = smoothed.y;

        // nếu có region bounds, clamp lại
        if (bounds != null)
        {
            clampedX = Mathf.Clamp(smoothed.x, minX, maxX);
            clampedY = Mathf.Clamp(smoothed.y, minY, maxY);
        }

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
