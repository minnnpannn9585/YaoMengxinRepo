using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSetFalse : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(SetFalseAfterTime());
    }

    IEnumerator SetFalseAfterTime()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
