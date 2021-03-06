//SphericalImageCam_Equi4.cs
//
//Copyright (c) 2015 blkcatman
//
using UnityEngine;

public class SphericalImageCam_Equi4 : SICameraCreator {
	private float[] rots = {0f, 0f, 109.471221f, 0f, 109.471221f, -120f, 109.471221f, 120f};
	private float[] ds = {0f, -0.5f, -0.5f, -0.5f};

	public bool drawImageOnGUI = true;
	
	void Start () {
		base.InitShader();
		
		for (int i = 0; i < 4; i++) {
			float rotX, rotZ;
			float fov = 142.0f;
			rotX = rots[i*2];
			rotZ = rots[i*2+1];

			CreateCamera(new Vector3(rotX, 0f, rotZ), fov, ds[i], "camera" + i.ToString());
		}

		if (drawImageOnGUI) {
			Camera main = gameObject.GetComponent<Camera>();
			main.nearClipPlane = 0.01f;
			main.farClipPlane = 0.02f;
		}
	}

	/*
	void OnGUI () {
		if (base.canDraw && drawImageOnGUI) {
			GUI.depth = 2;
			GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), base.targetTexture);
		}
	}*/
	
	void OnRenderImage(RenderTexture source, RenderTexture destination) {
		if(!base.canDraw && !drawImageOnGUI) {
			Graphics.Blit (source, destination);
			return;
		}
		
		Graphics.Blit (base.targetTexture, destination);
	}

}
