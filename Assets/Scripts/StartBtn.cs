using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
    public void StartRoom()
    {
        GetComponent<AudioSource>().Play();
        Invoke("LoadScene", 1f);
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
