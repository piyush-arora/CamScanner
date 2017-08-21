using UnityEngine;
using UnityEngine.UI;
using OpenAR;


public class Test : MonoBehaviour
{
    public RawImage image;
    public RectTransform imageParent;
    public AspectRatioFitter imageFitter;

    OpenARManager openARManager;

    // Device cameras
    WebCamDevice activeCameraDevice;

    WebCamTexture activeCameraTexture;

    // Image rotation
    Vector3 rotationVector = new Vector3(0f, 0f, 0f);

    // Image uvRect
    Rect defaultRect = new Rect(0f, 0f, 1f, 1f);
    Rect fixedRect = new Rect(0f, 1f, 1f, -1f);

    // Image Parent's scale
    Vector3 defaultScale = new Vector3(1f, 1f, 1f);
    Vector3 fixedScale = new Vector3(-1f, 1f, 1f);


    void Start()
    {
        // Check for device cameras
        if (WebCamTexture.devices.Length == 0)
        {
            Debug.Log("No devices cameras found");
            return;
        }

        // Get the device's cameras and create WebCamTextures with them
        activeCameraDevice = WebCamTexture.devices[0];
        activeCameraTexture = new WebCamTexture(activeCameraDevice.name);

        
        // Set camera filter modes for a smoother looking image
        activeCameraTexture.filterMode = FilterMode.Trilinear;

        activeCameraTexture.Play();

        openARManager = new OpenARManager();
    }

    
    
    // Make adjustments to image every frame to be safe, since Unity isn't 
    // guaranteed to report correct data as soon as device camera is started
    void Update()
    {
        // Skip making adjustment for incorrect camera data
        if (activeCameraTexture.width < 100)
        {
            Debug.Log("Still waiting another frame for correct info...");
            return;
        }

        // Rotate image to show correct orientation 
        rotationVector.z = -activeCameraTexture.videoRotationAngle;
        image.rectTransform.localEulerAngles = rotationVector;

        // Set AspectRatioFitter's ratio
        float videoRatio =
            (float)activeCameraTexture.width / (float)activeCameraTexture.height;
        imageFitter.aspectRatio = videoRatio;

        // Unflip if vertically flipped
        image.uvRect =
            activeCameraTexture.videoVerticallyMirrored ? fixedRect : defaultRect;

        // Mirror front-facing camera's image horizontally to look more natural
        imageParent.localScale =
            activeCameraDevice.isFrontFacing ? fixedScale : defaultScale;

        image.texture = openARManager.webcamTextureToTexture2D(activeCameraTexture);
    }



    void FixedUpdate()
    {
        foreach (Texture2D texture in FindObjectsOfType<Texture2D>())
        {
            Destroy(texture);
        }
    }

	


    
}
