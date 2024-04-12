using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Oculus;

public class ChangeSkyBox : MonoBehaviour
{
    public Material skybox;
    public float fadeSpeed = 0.8f;
    public OVRScreenFade screenFade;

    public IEnumerator FadeSkybox(Material newSkyboxMaterial)
    {
        //fade to black
        screenFade.FadeOut();
        yield return new WaitForSeconds(screenFade.fadeTime);
        RenderSettings.skybox = newSkyboxMaterial; // Change the skybox
        DynamicGI.UpdateEnvironment(); // Update GI to reflect new skybox
        //fade back in
        screenFade.FadeIn();
    }

    public void ChangeSkyboxWithFade()
    {
        StartCoroutine(FadeSkybox(skybox));
    }
}
