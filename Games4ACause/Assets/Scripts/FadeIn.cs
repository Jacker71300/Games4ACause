using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float currentAlpha;
    private bool fading;
    public float fadeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        currentAlpha = 0;
        canvasGroup.alpha = 0;
        fading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            fading = true;
        }
        if(fading)
        {
            currentAlpha += Time.deltaTime * fadeSpeed;
            if(currentAlpha > 0.5f)
            {
                fading = false;
                currentAlpha = 0.5f;
            }
        }
        canvasGroup.alpha = currentAlpha;
    }
}
