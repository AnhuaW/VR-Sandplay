using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShelfMovement : MonoBehaviour
{
    [SerializeField]
    private float high = 0f;
    [SerializeField]
    private float low = -1.4f;
    [SerializeField]
    private float duration = 2f;
    public GameObject shelfVisual;

    private Coroutine movementCoroutine; // To track the currently running coroutine

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            // Move the shelf up
            if (this.transform.localPosition.y < high)
            {
                StartChangeHeight(high);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Move the shelf down
            if (this.transform.localPosition.y > low)
            {
                StartChangeHeight(low);
            }
        }
    }

    public void ShowShelf()
    {
        if (this.transform.localPosition.y < high)
        {
            StartChangeHeight(high);
        }
    }

    public void HideShelf()
    {
        if (this.transform.localPosition.y > low)
        {
            StartChangeHeight(low);
        }

    }

    public void StartChangeHeight(float target)
    {
        // Stop the current coroutine before starting a new one to avoid conflicts
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }
        movementCoroutine = StartCoroutine(ChangeHeight(target));
    }

    public IEnumerator ChangeHeight(float target)
    {
        if(target == high)
        {
            shelfVisual.SetActive(true);
        }

        float elapsedTime = 0f;
        Vector3 startPosition = transform.localPosition; // Capture the start position
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float ratio = Mathf.Clamp01(elapsedTime / duration);
            //quadratic ease in 
            ratio = Mathf.Pow(ratio, 2);
            transform.localPosition = Vector3.Lerp(startPosition, new Vector3(startPosition.x, target, startPosition.z), ratio);
            yield return null;
        }
        // Ensure the object reaches the exact end position
        transform.localPosition = new Vector3(startPosition.x, target, startPosition.z);
        Debug.Log("Movement completed!");

        if(target == low)
        {
            shelfVisual.SetActive(false);
        }
    }
}