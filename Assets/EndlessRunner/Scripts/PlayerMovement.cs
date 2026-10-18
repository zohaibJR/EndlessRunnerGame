using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 5f;          // Base speed
    public float speedIncreaseRate = 0.1f;   // ✅ Speed gain per second
    public float maxSpeed = 20f;             // ✅ Limit the speed

    public float laneDistance = 2.5f;
    private Animator animator;
    private bool isRunning = false;

    private int currentLane = 1; // ✅ Start in middle lane
    public float laneSwitchSpeed = 10f;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    public float swipeThreshold = 50f;

    void Start()
    {
        animator = GetComponent<Animator>();

        // ✅ Force spawn at middle lane (X=0)
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (isRunning)
        {
            // ✅ Continuously increase speed
            forwardSpeed += speedIncreaseRate * Time.deltaTime;
            forwardSpeed = Mathf.Min(forwardSpeed, maxSpeed); // Cap at max speed

            // Move forward
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

            // Detect swipes
            DetectSwipe();
        }

        // Always smoothly move towards current lane (even before running)
        Vector3 targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSwitchSpeed * Time.deltaTime);
    }

    public void StartRunning()
    {
        isRunning = true;
        animator.SetBool("isRunning", true);

        // ✅ Make sure we are still in middle lane when run starts
        currentLane = 1;
    }

    private void ChangeLane(int direction)
    {
        int targetLane = currentLane + direction;
        if (targetLane < 0 || targetLane > 2) return;
        currentLane = targetLane;
    }

    private void DetectSwipe()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
            HandleSwipe(endTouchPosition - startTouchPosition);
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                startTouchPosition = touch.position;
            else if (touch.phase == TouchPhase.Ended)
                HandleSwipe(touch.position - startTouchPosition);
        }
#endif
    }

    private void HandleSwipe(Vector2 swipeDelta)
    {
        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y) && Mathf.Abs(swipeDelta.x) > swipeThreshold)
        {
            if (swipeDelta.x > 0) ChangeLane(1);   // Right
            else ChangeLane(-1);                   // Left
        }
        else if (Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x) && Mathf.Abs(swipeDelta.y) > swipeThreshold)
        {
            if (swipeDelta.y > 0)
            {
                // Swipe Up → trigger front flip
                animator.SetTrigger("doFrontFlip");
            }
        }
    }
}
