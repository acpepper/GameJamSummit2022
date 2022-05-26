using UnityEngine;
using System.Collections;

namespace Shaders.Effects {
    [RequireComponent(typeof(Camera))]
    public class invertEffect : MonoBehaviour
    {
	public Material material;

	// // Creates a private material used to the effect
	// void Awake ()
	// {
	//     material = new Material(Shader.Find("Custom/InvertColor"));
	//     Debug.Log(material.ToString());
	// }
	
	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
	    Graphics.Blit(source, destination, material);
	}
    }
}
