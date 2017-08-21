using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamScannerExample : MonoBehaviour
{
    public Texture2D input_texture;
    public RawImage rawImage;

    void Start()
    {
        rawImage.texture = input_texture;
    }


	public void onRunMePress ()
    {
        OpenARManager openARManager = new OpenARManager() ;
        Texture2D output_texture = openARManager.Scan(input_texture);
        rawImage.texture = output_texture;


    }
	
	
}
