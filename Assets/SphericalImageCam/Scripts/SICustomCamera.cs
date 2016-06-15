//SICustomCamera.cs
//
//Copyright (c) 2015 blkcatman
//
using UnityEngine;

public class SICustomCamera : SICameraBase {

	void Start () {
		InitShader();

		Camera main = gameObject.GetComponent<Camera>();
		main.aspect = 1f;

		Material mat = new Material(base.shader);
		Quaternion quat = gameObject.transform.localRotation;
		mat.SetVector("_forward", quat * Vector3.forward);
		mat.SetVector("_right", quat * Vector3.right);
		mat.SetVector("_up", quat * Vector3.up);
		mat.SetFloat("_fov", main.fieldOfView);
		main.targetTexture = targetTexture;

		SIRenderEvent ev = gameObject.AddComponent<SIRenderEvent>();
		ev.ref_camera = main;
		ev.material = mat;
		ev.forceUpdateParameters = false;
	}
}
