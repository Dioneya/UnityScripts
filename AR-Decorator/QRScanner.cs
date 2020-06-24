using UnityEngine;
using System;
using System.Collections;
using Vuforia;
using ZXing;
[AddComponentMenu("System/VuforiaScanner")]
public class QRScanner : MonoBehaviour
{
    public int checkedID; //переменная хранящая отсканированный ID комнаты
    public bool isIDChanged = false; 

    int previousID;

    private bool cameraInitialized;

    PIXEL_FORMAT mPixelFormat;
    private BarcodeReader barCodeReader;

    
    void Start()
    {
        barCodeReader = new BarcodeReader();
        StartCoroutine(InitializeCamera());
        mPixelFormat = PIXEL_FORMAT.GRAYSCALE;

    }

    private IEnumerator InitializeCamera()
    {
        // Waiting a little seem to avoid the Vuforia's crashes.
        yield return new WaitForSeconds(1.25f);

        var isFrameFormatSet = CameraDevice.Instance.SetFrameFormat(mPixelFormat, true);
        Debug.Log(String.Format("FormatSet : {0}", isFrameFormatSet));

        // Force autofocus.
        var isAutoFocus = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        if (!isAutoFocus)
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
        }
        Debug.Log(String.Format("AutoFocus : {0}", isAutoFocus));
        cameraInitialized = true;
    }

    private void Update()
    {
        if (cameraInitialized)
        {
            try
            {

                var cameraFeed = CameraDevice.Instance.GetCameraImage(mPixelFormat);
                if (cameraFeed == null)
                {
                    return;
                }
                var data = barCodeReader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.RGB24);
                if (data != null)
                {
                    // QRCode detected.
                    Debug.Log(data.Text);
                    try
                    {
                        
                        checkedID = Convert.ToInt32(data.Text);
                        GetComponent<JsonLoader>().StartLoad();
                    }
                    catch 
                    {
                        Debug.Log("Неверный формат QR");
                    }
                }
                else
                {
                    //Debug.Log("No QR code detected !");
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}