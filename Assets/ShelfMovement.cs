using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfMovement : MonoBehaviour
{
    [SerializeField]
    public float high = 1.4f;
    public float low = 0f;
    public float duration = 2f;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow)) { 
            //Show Activate the shelf then move
            if(this.transform.localPosition.y < high)
            {
                //Coroutine
                StartCoroutine(ChangeHeight(high));
            }
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Move the shelf then disable
            if(this.transform.localPosition.y > low)
            {
                //Coroutine
                StartCoroutine(ChangeHeight(low));
            }
        }
    }

    public IEnumerator ChangeHeight(float end)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the fraction of time elapsed
            float ratio = (float) elapsedTime / duration;

            // Interpolate between start and end positions
            transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3(this.transform.localPosition.x, end, this.transform.localPosition.z), ratio);

            yield return null; // Yielding null allows Unity to wait for the next frame
        }

        // Ensure the object reaches the end position exactly
        transform.localPosition = new Vector3(this.transform.localPosition.x, end, this.transform.localPosition.z);
        Debug.Log("Movement completed!");
    }
}
