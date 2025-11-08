using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [Tooltip("是否限制在摄像机视野内")]
    public bool limitToScreen = true;

    [Tooltip("Z轴固定位置（2D游戏通常设为0）")]
    public float zPosition = 0f;

    private Camera mainCamera;

    void Start()
    {
        // 获取主摄像机
        mainCamera = Camera.main;
        
        // 如果没有指定主摄像机，尝试查找场景中的摄像机
        if (mainCamera == null)
        {
            mainCamera = FindObjectOfType<Camera>();
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // 获取鼠标在屏幕上的位置
            Vector3 mouseScreenPosition = Input.mousePosition;
            
            // 设置Z轴位置（确保物体在摄像机可见范围内）
            mouseScreenPosition.z = zPosition - mainCamera.transform.position.z;
            
            // 将屏幕坐标转换为世界坐标
            Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
            
            // 如果需要限制在屏幕内
            if (limitToScreen)
            {
                // 获取屏幕边界的世界坐标
                Vector3 screenMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mouseScreenPosition.z));
                Vector3 screenMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mouseScreenPosition.z));
                
                // 限制位置在屏幕范围内
                targetPosition.x = Mathf.Clamp(targetPosition.x, screenMin.x, screenMax.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, screenMin.y, screenMax.y);
            }
            
            // 设置物体位置
            transform.position = targetPosition;
        }
        else
        {
            Debug.LogWarning("没有找到摄像机，请确保场景中有摄像机存在");
        }
    }
}