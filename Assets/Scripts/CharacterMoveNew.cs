using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterMoveNew : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 2f;               // 移动速度
    public float rotationSpeed = 10f;          // 旋转速度

    [Header("引用")]
    public GridManager gridManager;            // 网格管理器引用
    public TextMeshProUGUI messageText;        // 用于显示提示信息的UI文本

    private Vector2Int currentGridPosition;    // 当前网格位置
    private bool isMoving = false;             // 是否正在移动
    private List<Vector2Int> movePath;         // 移动路径

    // 四个30度移动方向（网格坐标变化）
    private Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(1, 0),    // 右上 (30度)
        new Vector2Int(0, -1),   // 右下 (-30度)
        new Vector2Int(0, 1),   // 左上 (150度)
        new Vector2Int(-1, 0)   // 左下 (-150度)
    };

    void Start()
    {
        // 初始化当前网格位置
        currentGridPosition = gridManager.GetGridPosition(transform.position);
        // 初始化消息文本
        if (messageText != null) messageText.text = "";
    }

    void Update()
    {
        // 处理鼠标点击
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Vector2Int? targetGridPos = gridManager.GetGridPositionFromMouse();
            if (targetGridPos.HasValue)
                MoveToGridPosition(targetGridPos.Value);
        }
    }

    // 移动到目标网格位置
    public void MoveToGridPosition(Vector2Int targetPos)
    {
        // 清除之前的消息
        if (messageText != null) messageText.text = "";
        // 如果已经在目标位置，不做任何操作
        if (currentGridPosition == targetPos) return;

        // 计算路径
        movePath = CalculatePath(currentGridPosition, targetPos);

        if (movePath != null && movePath.Count > 0)
        {
            // 检查路径是否通畅
            bool pathClear = true;

            // 检查第一步路径
            if (!gridManager.IsPathClear(currentGridPosition, movePath[0]))
                pathClear = false;
            // 检查第二步路径（如果有）
            else if (movePath.Count > 1 && !gridManager.IsPathClear(movePath[0], movePath[1]))
                pathClear = false;

            if (pathClear)
            {
                StartCoroutine(MoveAlongPath());
            }
            else
            {
                if (messageText != null)
                    messageText.text = "route block";
                Debug.Log("route block");
            }
        }
        else
        {
            if (messageText != null)
                messageText.text = "cannot reach target";
            Debug.Log("cannot reach target");
        }
    }

    // 计算路径（最多两步）
    private List<Vector2Int> CalculatePath(Vector2Int start, Vector2Int target)
    {
        List<Vector2Int> resultPath = new List<Vector2Int>();
        Vector2Int delta = target - start;

        // 检查是否可以一步到达（符合30度方向）
        foreach (var dir in directions)
        {
            // 计算需要多少步才能到达目标
            int steps = 0;
            bool isInDirection = false;

            if (dir.x != 0)
            {
                steps = delta.x / dir.x;
                isInDirection = delta.x % dir.x == 0 && steps > 0 &&
                               start.y + dir.y * steps == target.y;
            }
            else if (dir.y != 0)
            {
                steps = delta.y / dir.y;
                isInDirection = delta.y % dir.y == 0 && steps > 0 &&
                               start.x + dir.x * steps == target.x;
            }

            if (isInDirection)
            {
                resultPath.Add(target);
                return resultPath;
            }
        }

        // 计算两步路径
        // 找到中间点，使两段路径都符合30度方向
        Vector2Int midPoint = FindMidPoint(start, target);
        if (midPoint != Vector2Int.one * -1) // 检查是否找到有效中间点
        {
            resultPath.Add(midPoint);
            resultPath.Add(target);
            return resultPath;
        }

        return null; // 无法找到有效路径
    }

    // 寻找两步路径的中间点
    private Vector2Int FindMidPoint(Vector2Int start, Vector2Int target)
    {
        Vector2Int delta = target - start;

        // 尝试所有可能的中间点组合
        // 先沿x方向移动，再沿y方向移动
        if (delta.x != 0 && delta.y != 0)
        {
            Vector2Int midPoint = new Vector2Int(target.x, start.y);
            if (IsValidDirection(start, midPoint) && IsValidDirection(midPoint, target))
            {
                return midPoint;
            }

            // 先沿y方向移动，再沿x方向移动
            midPoint = new Vector2Int(start.x, target.y);
            if (IsValidDirection(start, midPoint) && IsValidDirection(midPoint, target))
            {
                return midPoint;
            }
        }

        return new Vector2Int(-1, -1); // 未找到有效中间点
    }

    // 检查两点之间是否为有效方向（30度斜角）
    private bool IsValidDirection(Vector2Int start, Vector2Int end)
    {
        if (start == end) return false;

        Vector2Int delta = end - start;

        foreach (var dir in directions)
        {
            if (dir.x != 0)
            {
                if (delta.x % dir.x == 0)
                {
                    int steps = delta.x / dir.x;
                    if (steps > 0 && start.y + dir.y * steps == end.y)
                    {
                        return true;
                    }
                }
            }
            else if (dir.y != 0)
            {
                if (delta.y % dir.y == 0)
                {
                    int steps = delta.y / dir.y;
                    if (steps > 0 && start.x + dir.x * steps == end.x)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    // 沿路径移动
    private IEnumerator MoveAlongPath()
    {
        isMoving = true;

        foreach (var targetGridPos in movePath)
        {
            Vector2 targetWorldPos = gridManager.GetWorldPosition(targetGridPos.x, targetGridPos.y);
            Vector2 startPos = transform.position;

            // 计算移动方向，用于旋转
            Vector2 moveDir = (targetWorldPos - startPos).normalized;

            // 计算30度角对应的旋转角度
            float targetAngle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // 移动过程
            float elapsedTime = 0f;
            float distance = Vector2.Distance(startPos, targetWorldPos);
            float moveDuration = distance / moveSpeed;

            while (elapsedTime < moveDuration)
            {
                // 移动
                transform.position = Vector2.Lerp(startPos, targetWorldPos, elapsedTime / moveDuration);

                // 旋转
                //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, elapsedTime / moveDuration * rotationSpeed);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 确保精确到达目标位置
            transform.position = targetWorldPos;
            //transform.rotation = targetRotation;

            // 更新当前网格位置
            currentGridPosition = targetGridPos;
        }

        isMoving = false;
        movePath.Clear();
    }
}
