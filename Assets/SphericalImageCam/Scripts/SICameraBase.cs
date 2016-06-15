//SICameraBase.cs
//
//Copyright (c) 2015 blkcatman
//
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SICameraBase : MonoBehaviour {
	protected bool canDraw = false;
	protected bool supportHDRTextures = true;
	protected bool supportDX11 = false;
	public Shader shader;
	protected Camera parentCamera;

	public RenderTexture targetTexture;

	protected void InitShader() {
		if (targetTexture == null) {
			targetTexture = new RenderTexture (1280, 720, 0, RenderTextureFormat.ARGB32);
			targetTexture.name = "temp";
		}
		Camera main = gameObject.GetComponent<Camera>();
		parentCamera = main;
		
		if(!CheckSupport(false, main.hdr)) {
			return;
		}
		if(!shader) {
			Debug.LogWarning("SICamera is used with no attached shaders!");
		}
		if(!shader.isSupported) {
			Debug.LogWarning("The shader \"" + shader.name + "\" is not supported on this platform!");
			return;
		}

		canDraw = true;
	}

	bool CheckSupport (bool needDepth, bool needHdr){
		supportHDRTextures = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf);
		supportDX11 = SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders;
		
		if (!SystemInfo.supportsImageEffects || !SystemInfo.supportsRenderTextures) {
			return false;
		}
		
		if(needHdr && !supportHDRTextures) {
			Debug.LogWarning("HDR Rendering is not supported on this platform!");
			return false;		
		}
		
		if(needDepth && !SystemInfo.SupportsRenderTextureFormat (RenderTextureFormat.Depth)) {
			return false;
		}
		
		if(needDepth)
			gameObject.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;	
		
		return true;
	}

	void OnDestroy() {
		for (int i = 0; i < transform.childCount; i++) {
			GameObject obj = transform.GetChild(i).gameObject;
			DestroyImmediate(obj);
		}
	}
}
