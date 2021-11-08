using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using static NativeGallery;

public class BattlefieldOverviewCapturer : MonoBehaviour
{
    public Image minimapImage;
    public Image actualMinimapImage;
    Texture2D view;
    public void GetPhoto()
    {
        Texture2D texture2D = ScreenshotHelper.CurrentTexture;
        Sprite sprite = ScreenshotHelper.CurrentSprite;
        RenderTexture renderTexture = ScreenshotHelper.CurrentRenderTexture;
        actualMinimapImage.GetComponent<Image> ().overrideSprite = Sprite.Create (texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0f, 0f), 100f);
        minimapImage.gameObject.SetActive(false);
    }
    private IEnumerator TakeScreenshot(){
        minimapImage.gameObject.SetActive(true);
        ScreenshotHelper.iCaptureScreen();
        yield return new WaitForEndOfFrame ();
        yield return new WaitForSecondsRealtime(3f);    
        GetPhoto();
    }
    private void TakeScreenshotNow(){
        StartCoroutine(TakeScreenshot());
    }

    public void TakeScreenshot_Static(){
        TakeScreenshotNow();
    }
}
