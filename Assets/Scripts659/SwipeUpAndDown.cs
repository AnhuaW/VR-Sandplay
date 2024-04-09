using UnityEngine;
using Oculus.Interaction;
using TMPro;

public class SwipeUpAndDown: MonoBehaviour
{
    public OVRHand Hand;
    public TextMeshProUGUI debugUI;
    private Vector3 lastPalmPosition;
    private bool wasHandPreviouslyTracked = false;

    // Threshold to determine a swipe, adjust based on testing
    public float swipeThreshold = 0.2f;
    private float timeSinceLastSwipe = 0f;
    public float swipeCooldown = 0.5f; // Cooldown time between swipes to prevent multiple detections

    void Update()
    {
        if (Hand.IsTracked)
        {
            // If the hand was not previously tracked, update the last position and flag
            if (!wasHandPreviouslyTracked)
            {
                lastPalmPosition = Hand.PointerPose.position;
                wasHandPreviouslyTracked = true;
                return; // Skip the rest of this update to wait for the next frame
            }

            timeSinceLastSwipe += Time.deltaTime;
            if (timeSinceLastSwipe < swipeCooldown) return; // Ensure there's a cooldown period between swipes

            Vector3 currentPalmPosition = Hand.PointerPose.position;
            float verticalMovement = currentPalmPosition.y - lastPalmPosition.y;

            if (Mathf.Abs(verticalMovement) > swipeThreshold)
            {
                if (verticalMovement > 0)
                {
                    Debug.Log("Swiped Up");
                    debugUI.text = "Swiped Up";
                }
                else
                {
                    Debug.Log("Swiped Down");
                    debugUI.text = "Swiped Down";
                }
                timeSinceLastSwipe = 0; // Reset cooldown timer
            }

            // Update the last known position of the palm
            lastPalmPosition = currentPalmPosition;
        }
        else
        {
            wasHandPreviouslyTracked = false;
        }
    }
}

