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
