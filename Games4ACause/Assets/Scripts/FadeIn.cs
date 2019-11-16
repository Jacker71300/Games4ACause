using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float currentAlpha;
    public bool fading;
    public float fadeSpeed;
    public float fadeStop;

    public static FadeIn fadeInstance;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        currentAlpha = 0;
        canvasGroup.alpha = 0;

        if (fadeInstance == null)
        {
            fadeInstance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(fading)
        {
            currentAlpha += Time.deltaTime * fadeSpeed;
            if(currentAlpha > fadeStop)
            {
                fading = false;
                currentAlpha = fadeStop;
            }
        }
        canvasGroup.alpha = currentAlpha;
    }
}
