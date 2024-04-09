using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeSkyBox : MonoBehaviour
{
    public Material skybox;
    public Image fadeImage;
    public float fadeSpeed = 0.8f;

    public IEnumerator FadeSkybox(Material newSkyboxMaterial)
    {
        yield return StartCoroutine(FadeTo(1)); // Fade to black
        RenderSettings.skybox = newSkyboxMaterial; // Change the skybox
        DynamicGI.UpdateEnvironment(); // Update GI to reflect new skybox
        yield return StartCoroutine(FadeTo(0)); // Fade back to clear
    }

    IEnumerator FadeTo(float targetAlpha)
    {
        Color color = fadeImage.color;
        float alphaDiff = Mathf.Abs(color.a - targetAlpha);
        while (alphaDiff > 0.01f)
        {
            color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeImage.color = color;
            alphaDiff = Mathf.Abs(color.a - targetAlpha);
            yield return null;
        }
    }

    public void ChangeSkyboxWithFade()
    {
        StartCoroutine(FadeSkybox(skybox));
    }
}
