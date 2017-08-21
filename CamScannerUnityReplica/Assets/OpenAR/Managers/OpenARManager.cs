using System;
using OpenAR;
using System.Runtime.InteropServices;
using UnityEngine;

public class OpenARManager 
{
    // THIS NEED TO BE USED BY APPLICATION
    //public RawImage videoBackground;
    //public RectTransform videoParent;
    //public AspectRatioFitter videoAspectFitter;


    // intermediate
    //Texture2D my_generated_texture;
    //Color32[] pixels;

    // input
    //WebCamTexture webCamTexture;
    //GCHandle pixelsHandle;

    //void runProcessImage()
    //{
    //    OpenARDllManager.Image input_image;
    //    OpenARDllManager.Image output_image;
    //    input_image.cols = myTexture.width;
    //    input_image.rows = myTexture.height;
    //    input_image.type = 12;
    //    output_image.cols = 500;
    //    output_image.rows = 500;
    //    output_image.type = 12;
    //    byte[] input_image_data = myTexture.GetRawTextureData();


    //    byte[] output_image_data = new byte[output_image.rows * output_image.cols * 3];
    //    unsafe
    //    {
    //        fixed (byte* input_image_data_ptr = input_image_data)
    //        {
    //            fixed (byte* output_image_data_ptr = output_image_data)
    //            {
    //                input_image.data = input_image_data_ptr;
    //                output_image.data = output_image_data_ptr;
    //                OpenARDllManager.process_image(input_image, ref output_image);

    //            }

    //        }

    //    }

    //    Texture2D my_generated_texture = new Texture2D(output_image.cols, output_image.rows, TextureFormat.RGB24, false);
    //    my_generated_texture.LoadRawTextureData(output_image_data);
    //    my_generated_texture.Apply();
    //    Quad.GetComponent<MeshRenderer>().material.mainTexture = my_generated_texture;

    //}

    public Texture2D webcamTextureToTexture2D(WebCamTexture webCamTexture)
    {
        Texture2D my_generated_texture;
        Color32[] pixels;
        GCHandle pixelsHandle;
        pixels = webCamTexture.GetPixels32();
        pixelsHandle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
        byte[] output_data = new byte[webCamTexture.width * webCamTexture.height * 4];
        unsafe
        {
            fixed (byte* output_data_ptr = output_data)
            {
                System.IntPtr pixelsPtr = pixelsHandle.AddrOfPinnedObject();
                OpenARDllManager.display_webcam(webCamTexture.height, webCamTexture.width, pixelsPtr, output_data_ptr);
                pixelsPtr = System.IntPtr.Zero;
                pixelsHandle.Free();
                GC.Collect();
            }
        }

        my_generated_texture = new Texture2D(webCamTexture.width, webCamTexture.height, TextureFormat.RGBA32, false);
        my_generated_texture.LoadRawTextureData(output_data);
        my_generated_texture.Apply();
        pixels = null;
        output_data = null;
        return my_generated_texture;
    }


    public Texture2D Scan(Texture2D inputTexture)
    {
        Texture2D my_generated_texture;
        Color32[] pixels;
        GCHandle pixelsHandle;
        pixels = inputTexture.GetPixels32();
        pixelsHandle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
        byte[] output_data = new byte[inputTexture.width * inputTexture.height * 4];
        unsafe
        {
            fixed (byte* output_data_ptr = output_data)
            {
                System.IntPtr pixelsPtr = pixelsHandle.AddrOfPinnedObject();
                OpenARDllManager.display_webcam(inputTexture.height, inputTexture.width, pixelsPtr, output_data_ptr);
                pixelsPtr = System.IntPtr.Zero;
                pixelsHandle.Free();
                GC.Collect();
            }
        }

        my_generated_texture = new Texture2D(inputTexture.width, inputTexture.height, TextureFormat.RGBA32, false);
        my_generated_texture.LoadRawTextureData(output_data);
        my_generated_texture.Apply();
        pixels = null;
        output_data = null;
        return my_generated_texture;
    }

    //void Start()
    //{
    //    webCamTexture = new WebCamTexture();
    //    webCamTexture.Play();

    //}


    //public void freeMemory()
    //{
    //    foreach (Texture2D texture in FindObjectsOfType<Texture2D>())
    //    {
    //        Destroy(texture);
    //    }
    //}

    //void FixedUpdate()
    //{
    //    freeMemory();

    //}

    //void Update()
    //{
    //    if (webCamTexture.isPlaying)
    //    {
    //        pixels = webCamTexture.GetPixels32();
    //        pixelsHandle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
    //        byte[] output_data = new byte[webCamTexture.width * webCamTexture.height * 4];
    //        unsafe
    //        {
    //            fixed(byte* output_data_ptr = output_data)
    //            {
    //                System.IntPtr pixelsPtr =  pixelsHandle.AddrOfPinnedObject();
    //                OpenARDllManager.display_webcam(webCamTexture.height, webCamTexture.width, pixelsPtr, output_data_ptr);
    //                pixelsPtr = System.IntPtr.Zero;
    //                pixelsHandle.Free();
    //                GC.Collect();

    //            }

    //        }
    //        // DISPLAY THE FRAME

    //        // CORRECT THE ROTATION IF NEEDED
    //        int clockWiseRotationNedded = webCamTexture.videoRotationAngle;
    //        int counterClockWiseRotationNedded = -1 * clockWiseRotationNedded;

    //        // MIRROR IMAGE IF NEEDED
    //        counterClockWiseRotationNedded += (webCamTexture.videoVerticallyMirrored) ? 180 : 0;

    //        // ROTATE THE RAW IMAGE 
    //        videoBackground.GetComponent<RectTransform>().localEulerAngles = new Vector3(0f, 0f, counterClockWiseRotationNedded);

    //        // FETCH THE ACTAUL VIDEO RATION
    //        float videoRatio = (float)webCamTexture.width / (float)webCamTexture.height;

    //        my_generated_texture = new Texture2D(webCamTexture.width, webCamTexture.height , TextureFormat.RGBA32 , false);
    //        my_generated_texture.LoadRawTextureData(output_data);
    //        my_generated_texture.Apply();
    //        videoBackground.texture = my_generated_texture;

    //        pixels = null;
    //        output_data = null;




    //    }

    //}


    //    public void OnApplicationQuit()
    //    {
    //#if UNITY_EDITOR_WIN
    //        OpenARDllManager.destroy_windows();
    //        #endif
    //    }


}

