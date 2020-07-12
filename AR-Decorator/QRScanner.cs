using UnityEngine;
using System;
using System.Collections;
using Vuforia;
using ZXing;
using System.Text.RegularExpressions;

using TMPro;
[AddComponentMenu("System/VuforiaScanner")]
public class QRScanner : MonoBehaviour
{

    public int checkedID; //переменная хранящая отсканированный ID комнаты
    public bool isIDChanged = false;
    string link = "http://likholetov.beget.tech/public/api/institution/";
    /*
private bool cameraInitialized;

PIXEL_FORMAT mPixelFormat = PIXEL_FORMAT.GRAYSCALE;
private BarcodeReader barCodeReader;


void Start()
{
    barCodeReader = new BarcodeReader();
    StartCoroutine(InitializeCamera());
    //mPixelFormat = PIXEL_FORMAT.GRAYSCALE;

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
            Debug.Log(cameraFeed);
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
}*/
    /*
    private bool cameraInitialized;

    private BarcodeReader barCodeReader;
    private bool isDecoding = false;

    IBarcodeReader reader = new BarcodeReader()
    {
        AutoRotate = false,
        TryInverted = false,
        Options =
            {
                PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE },
                TryHarder = true,
                ReturnCodabarStartEnd = true,
                PureBarcode = false
            }
    };
    void Start()
    {
        StartCoroutine(InitializeCamera());
    }

    private IEnumerator InitializeCamera()
    {
        // Waiting a little seem to avoid the Vuforia's crashes.
        yield return new WaitForSeconds(1.25f);

         var isFrameFormatSet = CameraDevice.Instance.SetFrameFormat(PIXEL_FORMAT.RGBA8888, true);
        // Debug.Log(String.Format("FormatSet : {0}", isFrameFormatSet));

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
        if (cameraInitialized && !isDecoding)
        {
            try
            {
                var cameraFeed = CameraDevice.Instance.GetCameraImage(PIXEL_FORMAT.RGBA8888);

                if (cameraFeed == null)
                {
                    return;
                }
                ThreadPool.QueueUserWorkItem(new WaitCallback(DecodeQr), cameraFeed);

            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }

    private void DecodeQr(object state)
    {
        isDecoding = true;
        var cameraFeed = (Image)state;
        var data = reader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.ARGB32);
        if (data != null)
        {
            // QRCode detected.
            Debug.Log("Detected !");
            isDecoding = false;
        }
        else
        {
            isDecoding = false;
            Debug.Log("No QR code detected !");
        }
    }
     
     */
    /*
    private WebCamTexture camTexture;
    private Rect screenRect;
    void Start()
    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = Screen.height;
        camTexture.requestedWidth = Screen.width;
        if (camTexture != null)
        {
            camTexture.Play();
        }
    }

    void OnGUI()
    {
        // drawing the camera on screen
        GUI.DrawTexture(screenRect, camTexture);
        // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            // decode the current frame
            var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);
            if (result != null)
            {
                Debug.Log("DECODED TEXT FROM QR: " +result.Text);
            }
        }
        catch (Exception ex) { Debug.LogWarning(ex.Message); }
    }
  //  private static Color32[] Encode(string textForEncoding,
  //int width, int height)
  //  {
  //      var writer = new BarcodeWriter
  //      {
  //          Format = BarcodeFormat.QR_CODE,
  //          Options = new QrCodeEncodingOptions
  //          {
  //              Height = height,
  //              Width = width
  //          }
  //      };
  //      return writer.Write(textForEncoding);
  //  }
  //  public Texture2D generateQR(string text)
  //  {
  //      var encoded = new Texture2D(256, 256);
  //      var color32 = Encode(text, encoded.width, encoded.height);
  //      encoded.SetPixels32(color32);
  //      encoded.Apply();
  //      return encoded;
  //  }*/

    private bool mAccessCameraImage = true;

    // The desired camera image pixel format
    private PIXEL_FORMAT mPixelFormat = PIXEL_FORMAT.GRAYSCALE;// or RGBA8888, RGB888, RGB565, YUV
    // Boolean flag telling whether the pixel format has been registered
    private bool mFormatRegistered = false;
    private void Start()
    {
        StartCoroutine(InitializeCamera());
    }
    void Update()
    {
        OnTrackablesUpdated();
    }
    /// <summary>
    /// Called when Vuforia is started
    /// </summary>
    private IEnumerator InitializeCamera()
    {
        yield return new WaitForSeconds(1.25f);
        var isFrameFormatSet = CameraDevice.Instance.SetFrameFormat(PIXEL_FORMAT.GRAYSCALE, true);
        // Debug.Log(String.Format("FormatSet : {0}", isFrameFormatSet));

        // Force autofocus.
        var isAutoFocus = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        if (!isAutoFocus)
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
        }
        // Try register camera image format
        if (isFrameFormatSet)
        {
            Debug.Log("Successfully registered pixel format " + mPixelFormat.ToString());
            mFormatRegistered = true;
        }
        else
        {
            Debug.LogError("Failed to register pixel format " + mPixelFormat.ToString() +
                "\n the format may be unsupported by your device;" +
                "\n consider using a different pixel format.");
            mFormatRegistered = false;
        }
    }
    /// <summary>
    /// Called when app is paused / resumed
    /// </summary>
    private void OnPause(bool paused)
    {
        if (paused)
        {
            Debug.Log("App was paused");
            UnregisterFormat();
        }
        else
        {
            Debug.Log("App was resumed");
            RegisterFormat();
        }
    }

    private bool CheckSite(string siteURL) {
        Regex regex = new Regex(string.Format(@"{0}(\d+)", Regex.Escape(link)));
        MatchCollection matches = regex.Matches(siteURL);
        if (matches.Count > 0)
        {
            foreach (Match match in matches)
                Debug.Log("Найдено совпадение");
            return true;
        }
        else
        {
            Debug.Log("Совпадений не найдено");
            return false;
        }
    }
    /// <summary>
    /// Called each time the Vuforia state is updated
    /// </summary>
    private void OnTrackablesUpdated()
    {
        if (mFormatRegistered)
        {
            if (mAccessCameraImage)
            {
                Vuforia.Image image = CameraDevice.Instance.GetCameraImage(mPixelFormat);
                if (image != null)
                {
                    string imageInfo = mPixelFormat + " image: \n";
                    imageInfo += " size: " + image.Width + " x " + image.Height + "\n";
                    imageInfo += " bufferSize: " + image.BufferWidth + " x " + image.BufferHeight + "\n";
                    imageInfo += " stride: " + image.Stride;
                    //Debug.Log(imageInfo);
                    byte[] pixels = image.Pixels;
                    if (pixels != null && pixels.Length > 0)
                    {
                        IBarcodeReader reader = new BarcodeReader();
                        var data = reader.Decode(pixels, image.Width, image.Height, RGBLuminanceSource.BitmapFormat.Gray8);
                        if (data != null)
                        {
                            // QRCode detected.
                            if (CheckSite(data.Text) == true) {
                                SceneLoader.link = data.Text;
                                SceneChanger.OpenScenLoader(2);
                            }
                        }
                        //Debug.Log("Image pixels: " + pixels[0] + "," + pixels[1] + "," + pixels[2] + ",...");
                    }
                }
            }
        }
    }
    /// <summary>
    /// Unregister the camera pixel format (e.g. call this when app is paused)
    /// </summary>
    private void UnregisterFormat()
    {
        Debug.Log("Unregistering camera pixel format " + mPixelFormat.ToString());
        CameraDevice.Instance.SetFrameFormat(mPixelFormat, false);
        mFormatRegistered = false;
    }
    /// <summary>
    /// Register the camera pixel format
    /// </summary>
    private void RegisterFormat()
    {
        if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true))
        {
            Debug.Log("Successfully registered camera pixel format " + mPixelFormat.ToString());
            mFormatRegistered = true;
        }
        else
        {
            Debug.LogError("Failed to register camera pixel format " + mPixelFormat.ToString());
            mFormatRegistered = false;
        }
    }
}