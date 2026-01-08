using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadNextPlay : MonoBehaviour
{
    public void LoadKeep()
    {
        SceneManager.LoadScene("KeepState");
        GameObject targetObj = GameObject.Find("bgm");

        if (targetObj != null)
        {
            // 销毁物体（立即销毁）
            Destroy(targetObj);
        }
    }

    public void LoadDest()
    {
        SceneManager.LoadScene("DestroyState01");
        GameObject targetObj = GameObject.Find("bgm");

        if (targetObj != null)
        {
            // 销毁物体（立即销毁）
            Destroy(targetObj);
        }
    }
}
