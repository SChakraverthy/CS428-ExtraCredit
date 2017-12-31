using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour {

    #region Vars

    public Material material;
    public GameObject plane;
    public InputField urlInputField;
    public InputField resourceInputField;

    private GameObject manager;

    #endregion

    #region ImageLoader
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadFromURL()
    {
        string url = urlInputField.text;
        this.StartCoroutine(this.LoadImage(url));
    }

    public void LoadFromResource()
    {

        string text = resourceInputField.text;

        Texture2D unreadableImg = (Texture2D)Resources.Load(text);
        Texture2D img = new Texture2D(unreadableImg.width, unreadableImg.height);

        // Copy the loaded image to the new texture. Makes it readable.
        if(unreadableImg != null){

            RenderTexture renderTex = RenderTexture.GetTemporary(unreadableImg.width, unreadableImg.height);
            Graphics.Blit(unreadableImg, renderTex);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            img.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            img.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);

        } else
        {
            Debug.Log("Unable to load image");
            return;
        }

        // Set the texture to a material.
        material = new Material(Shader.Find("Sprites/Default"));
        material.mainTexture = img;

        // Apply the material to the plane.
        plane.GetComponent<Renderer>().material = material;

        MeshRenderer mr = plane.GetComponent<MeshRenderer>();
        mr.material = material;
    }

    private IEnumerator LoadImage(string url)
    {

        using(WWW www = new WWW(url))
        {
            yield return www;

            // Set the texture to a material.
            Texture2D img = (Texture2D)www.texture;
            material.mainTexture = img;

            // Apply the material to the plane.
            plane.GetComponent<Renderer>().material = material;

            MeshRenderer mr = plane.GetComponent<MeshRenderer>();
            mr.material = material;
        }
    }

    #endregion
}
