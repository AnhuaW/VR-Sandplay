using UnityEngine;
using System.Collections;
using Wacki.IndentSurface;
using UnityEngine.Events;

/// <summary>
/// Simple control script for our sphere that leaves a track in the snow.
/// </summary>
/// 
public class IndentActor : MonoBehaviour
{
    private Vector3 lastPosition;
    private bool isMoving;
    [Range(0.0f, 0.2f)]
    // DrawDelta
    public float drawDelta = 0.01f;
    private Vector3 _prevDrawPos;
    public float interval = 0.3f;
    private Rigidbody rb;
    public AudioSource snow_audioSource;
    public UnityEvent StartDrawingSFX = new UnityEvent();
    public UnityEvent StopDrawingSFX = new UnityEvent();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        GetComponent<Rigidbody>().AddTorque(v, 0, -h);

        if (Vector3.Distance(_prevDrawPos, transform.position) < drawDelta)
            return;

        _prevDrawPos = transform.position;

        Debug.DrawLine(transform.position, transform.position + Vector3.down);

        //check raycast distance before creatng indentation
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit) && hit.distance <= 0.1f)
        {
            var texDraw = hit.collider.gameObject.GetComponent<IndentDraw>();
            if (texDraw == null)
                return;

            texDraw.IndentAt(hit);
            if (isMoving && !snow_audioSource.isPlaying)
            {
                StartDrawingSFX.Invoke();
            }
            else
            {
                StopDrawingSFX.Invoke();
            }
        }

        //Check if the object is moving
        if (transform.position != lastPosition)
        {
            // If they're different, the GameObject has moved.
            if (!isMoving)
            {
                isMoving = true;
                Debug.Log("Started Moving");
            }
        }
        else
        {
            // If they're the same, the GameObject is not moving.
            if (isMoving)
            {
                isMoving = false;
                Debug.Log("Stopped Moving");
            }
        }

        // Update lastPosition for the next frame's comparison
        lastPosition = transform.position;

    }
}