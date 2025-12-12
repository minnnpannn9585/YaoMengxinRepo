using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // 绕Z轴旋转（2D UI默认）
        float angle = eventData.button == PointerEventData.InputButton.Left ? 90f : -90f;
        transform.Rotate(Vector3.forward, angle);

        transform.parent.GetComponent<KeepPuzzle>().CheckFinish();
    }
}
