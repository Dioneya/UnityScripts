using UnityEngine;
using System;
using System.Collections;
using Vuforia;
using ZXing;
using System.Text.RegularExpressions;
using System.IO;
using TMPro;
[AddComponentMenu("System/VuforiaScanner")]
public class QRScanner : MonoBehaviour
{

    public int checkedID; //переменная хранящая отсканированный ID комнаты
    public bool isIDChanged = false;
    string link = GlobalVariables.link+"/public/api/institution/";
    

    private bool mAccessCameraImage = true;

    // The desired camera image pixel format
    private PIXEL_FORMAT mPixelFormat = PIXEL_FORMAT.GRAYSCALE;// or RGBA8888, RGB888, RGB565, YUV
    // Boolean flag telling whether the pixel format has been registered
    private bool mFormatRegistered = false;
    private void Start()
    {
        GlobalVariables.cachePathFolder = Path.Combine(Application.persistentDataPath, @"Cache");
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