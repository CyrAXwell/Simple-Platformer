using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkinDisplayController : MonoBehaviour
{
    [SerializeField] private float displayTime;
    [SerializeField] private List<GameObject> skinsContainers;
    [SerializeField] private List<RenderTexture> skinTextures;
    [SerializeField] private bool saveSkinsImages;

    private void Awake()
    {
        foreach (GameObject container in skinsContainers)
        {
            container.SetActive(true);
        }
        StartCoroutine(HideSkinsContainers(displayTime));
    }

    private IEnumerator HideSkinsContainers(float time)
    {
        yield return new WaitForSeconds(time);

        foreach ( GameObject container in skinsContainers)
        {
            container.SetActive(false);
        }

        if (saveSkinsImages)
        {
            foreach ( RenderTexture texture in skinTextures)
            {
                SaveRTToFile(texture);
            }
        }
    }

    private void SaveRTToFile(RenderTexture skinTexture)
    {
        RenderTexture rt = skinTexture;

        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        RenderTexture.active = null;

        byte[] bytes;
        bytes = tex.EncodeToPNG();
        
        string path = AssetDatabase.GetAssetPath(rt) + ".png";
        System.IO.File.WriteAllBytes(path, bytes);
        AssetDatabase.ImportAsset(path);
        Debug.Log("Saved to " + path);
    }
}
