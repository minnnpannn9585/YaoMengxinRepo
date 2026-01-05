using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragOne : MonoBehaviour
{
    public int flameUse;
    public int destroyChange;
    public int noDestroyChange;
    public string burnParamName = "IsBurning";

    // 组件引用
    private Animator _animator;
    private Collider2D _collider;

    // 状态变量
    private bool _isPressing = false; // 是否正在长按
    private bool _isBurnCompleted = false; // 是否已完成烧毁


    private void Awake()
    {
        // 获取必要组件
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }


    #region 输入检测
    // 鼠标按下（或触摸开始）时触发
    private void OnMouseDown()
    {
        if (_isBurnCompleted) return; // 已烧毁的碎片不响应

        GetComponent<AudioSource>().Play(); // 播放音效
        _isPressing = true;
        _animator.SetBool(burnParamName, true); // 开始播放烧毁动画
    }

    // 鼠标松开（或触摸结束）时触发
    private void OnMouseUp()
    {
        ResetBurnState(); // 重置状态（停止动画）
    }

    // 鼠标移出碎片范围时触发（防止长按移出后继续动画）
    private void OnMouseExit()
    {
        if (_isPressing) ResetBurnState();
    }
    #endregion


    #region 状态管理
    // 重置烧毁状态：停止动画并回到初始状态
    private void ResetBurnState()
    {
        if (_isBurnCompleted) return;

        _isPressing = false;
        _animator.SetBool(burnParamName, false); // 停止动画
    }

    // 烧毁动画播放完成时调用（需在动画最后一帧设置动画事件）
    public void OnBurnAnimationFinished()
    {
        if (_isPressing)
        {
            // 长按到动画结束：标记为已完成并销毁碎片
            _isBurnCompleted = true;
            gameObject.SetActive(false);
            LevelOneManager.Instance.UseFlame(flameUse, destroyChange);
            
        }
        else
        {
            // 未长按完成却播放完动画（如误触）：重置状态
            ResetBurnState();
        }
    }
    #endregion
}
