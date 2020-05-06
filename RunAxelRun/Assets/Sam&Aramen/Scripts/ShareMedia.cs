using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters;
using VoxelBusters.NativePlugins;
using UnityEngine.UI;

public class ShareMedia : MonoBehaviour
{
    private bool isSharing = false;
    public AudioSource boton;
    public Button btn;



    public void ShareSocialMedia() {

        isSharing = true;
        boton.Play();
    }

    private void LateUpdate()
    {
        if (isSharing)
        {
            isSharing = false;
            StartCoroutine(CaptureScreenShot());
        }
    }

   

    IEnumerator CaptureScreenShot()
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
        ShareSheet(texture);
        Object.Destroy(texture);
    }

    private void ShareSheet(Texture2D texture)
    {
        ShareSheet _shareSheet = new ShareSheet();
        _shareSheet.Text = "Run Axel Run !";
        _shareSheet.AttachImage(texture);
        _shareSheet.URL = "www.axeleldragon.com";

        NPBinding.Sharing.ShowView(_shareSheet,ShareFinish);
    }
    private void ShareFinish(eShareResult _result)
    {
        Debug.Log(_result);
    }
    
}

