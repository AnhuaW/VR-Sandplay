using UnityEngine;
using System.Collections;
using Wacki.IndentSurface;

/// <summary>
/// Simple control script for our sphere that leaves a track in the snow.
/// </summary>
public class IndentActor : MonoBehaviour
{
    [Range(0.0f, 0.2f)]
    // DrawDelta
    public float drawDelta = 0.01f;
    private Vector3 _prevDrawPos;
        
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
        if (Physics.Raycast(transform.position, Vector3.down, out hit) && hit.distance < 0.1f)
        {
            var texDraw = hit.collider.gameObject.GetComponent<IndentDraw>();
            if (texDraw == null)
                return;

            texDraw.IndentAt(hit);
        }
    }
}