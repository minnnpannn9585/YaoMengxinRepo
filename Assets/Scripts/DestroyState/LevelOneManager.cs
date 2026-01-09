using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneManager : MonoBehaviour
{
    public static LevelOneManager Instance;
    [HideInInspector]
    public int flameValue;
    [HideInInspector]
    public int residualValue;

    public FragOne[] levelOneFrags;

    public SpriteRenderer flameRenderer;
    public Sprite[] flameSprites;

    public SpriteRenderer residualRenderer;
    public Sprite[] residualSprites;
    
    public GameObject charaNormal;
    public GameObject charaAddict1;
    public GameObject charaAddict2;
    public GameObject charaLost1;
    public GameObject charaLost2;

    private bool levelEnd = false;

    public string successSubtitle;
    public string failureSubtitle;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        flameValue = 1;
        residualValue = 5;
        flameRenderer.sprite = flameSprites[flameValue - 1];
    }

    public void UseFlame(int flameUse, int minusChange)
    {
        if (levelEnd)
        {
            return;
        }
        flameValue -= flameUse;

        //calculate residual value
        residualValue += minusChange;

        for (int i = 0; i < levelOneFrags.Length; i++)
        {
            if(levelOneFrags[i].gameObject.activeSelf)
            {
                residualValue += levelOneFrags[i].noDestroyChange;
            }
        }
        SwitchImage(residualValue);
        print(residualValue);

        residualRenderer.sprite = residualSprites[residualValue - 1];

        if (flameValue > 0) 
        {
            flameRenderer.sprite = flameSprites[flameValue - 1];
        }
        else if(flameValue <= 0)
        {
            CheckEnding();
        }
    }

    public void CheckEnding()
    {
        if(residualValue <= 3)
        {
            SubtitleManager.Instance.ShowSubtitle(successSubtitle);
            //load next
            StartCoroutine(TimerLoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else
        {
            SubtitleManager.Instance.ShowSubtitle(failureSubtitle);
            //reload current
            StartCoroutine(TimerLoadScene(SceneManager.GetActiveScene().buildIndex));
        }

    }

    IEnumerator TimerLoadScene(int sceneIndex)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneIndex);
    }

    public void SwitchImage(int residualValue)
    {
        if (residualValue <= 3 && residualValue >= -2)
        {
            charaNormal.SetActive(true);
            charaAddict1.SetActive(false);
            charaAddict2.SetActive(false);
            charaLost1.SetActive(false);
            charaLost2.SetActive(false);
        }
        else if (residualValue >= 4 && residualValue <= 11)
        {
            charaNormal.SetActive(false);
            charaAddict1.SetActive(true);
            charaAddict2.SetActive(false);
            charaLost1.SetActive(false);
            charaLost2.SetActive(false);
        }
        else if (residualValue >= 12)
        {
            charaNormal.SetActive(false);
            charaAddict1.SetActive(false);
            charaAddict2.SetActive(true);
            charaLost1.SetActive(false);
            charaLost2.SetActive(false);
        }
        else if (residualValue >= -8 && residualValue <= -4)
        {
            charaNormal.SetActive(false);
            charaAddict1.SetActive(false);
            charaAddict2.SetActive(false);
            charaLost1.SetActive(true);
            charaLost2.SetActive(false);
        }
        else
        {
            charaNormal.SetActive(false);
            charaAddict1.SetActive(false);
            charaAddict2.SetActive(false);
            charaLost1.SetActive(false);
            charaLost2.SetActive(true);
        }
    }
}
