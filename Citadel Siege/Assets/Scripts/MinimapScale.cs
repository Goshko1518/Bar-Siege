using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapScale : MonoBehaviour, IClickable
{
    private RectTransform rectTransform;
    private bool isFullScreen = false;
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }
    public void OnClick()
    {
        ScaleMap();
    }
    void ScaleMap(){
        isFullScreen = !isFullScreen;
        if(isFullScreen){
            rectTransform.localScale = rectTransform.localScale * 6.5f;
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        } 
        else{
            rectTransform.localScale = originalScale;
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);
        }  
    }
}
