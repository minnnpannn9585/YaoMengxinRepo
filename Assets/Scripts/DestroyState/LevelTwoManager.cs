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
        flameValue -= flameUse;

        //calculate residual value
        residualValue += minusChange;

        for (int i = 0; i < levelTwoFrags.Length; i++)
        {
            if (levelTwoFrags[i].gameObject.activeSelf)
            {
                residualValue += levelTwoFrags[i].noDestroyChange;
            }
        }
        print(residualValue);

        if (flameValue <= 0)
        {
            CheckEnding();
            return;
        }

        residualRenderer.sprite = residualSprites[residualValue + 12];

        if (flameValue > 0)
        {
            flameRenderer.sprite = flameSprites[flameValue - 1];
        }
        
    }

    public void CheckEnding()
    {
        if(residualValue <= 3)
        {
            //load next
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            //reload current
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
