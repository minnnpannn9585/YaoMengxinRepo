using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTwoManager : MonoBehaviour
{
    public static LevelTwoManager Instance;
    [HideInInspector]
    public int flameValue;
    [HideInInspector]
    public int residualValue;

    public FragTwo[] levelTwoFrags;

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

    public GameObject keepCanv;
    public GameObject destCanv;

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

        flameValue = 3;
        residualValue = 3;
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
        residualValue = 3;

        for (int i = 0; i < levelTwoFrags.Length; i++)
        {
            if (levelTwoFrags[i].gameObject.activeSelf)
            {
                residualValue += levelTwoFrags[i].noDestroyChange;
            }
            if (!levelTwoFrags[i].gameObject.activeSelf)
            {
                residualValue += levelTwoFrags[i].destroyChange;
            }
        }
        print(residualValue);
        SwitchImage(residualValue);

        

        residualRenderer.sprite = residualSprites[residualValue + 12];

        if (flameValue > 0)
        {
            flameRenderer.sprite = flameSprites[flameValue - 1];
        }
        if (flameValue <= 0)
        {
            CheckEnding();
        }
    }

    public void CheckEnding()
    {
        print("finish");
        levelEnd = true;

        if (residualValue >= 4)
        {
            keepCanv.SetActive(true);
        }
        else
        {
            destCanv.SetActive(true);
        }
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
