using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PetController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SkinnedMeshRenderer meshTarget = null;
    void Awake()
    {
        string textureString = PlayerPrefs.GetString("colored");
        int w = PlayerPrefs.GetInt("w");
        int h = PlayerPrefs.GetInt("h");

        byte[] textureByte = System.Convert.FromBase64String(textureString);
        Texture2D coloredTexture = new Texture2D(w,h);
        coloredTexture.LoadImage(textureByte);
        coloredTexture.Apply();
        //File.WriteAllBytes(Application.dataPath + "test0" + ".png", textureByte);
        meshTarget.materials[0].mainTexture = coloredTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
