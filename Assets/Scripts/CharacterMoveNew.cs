using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterMoveNew : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 10f;

    [Header("引用")]
    public GridManager gridManager;
    public TextMeshProUGUI messageText;
    public Animator animator; // 新增：角色动画控制器

    private Vector2Int currentGridPosition;
    private bool isMoving = false;
    private List<Vector2Int> movePath;

    private Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(0, 1),
        new Vector2Int(-1, 0)
    };

    private Vector2 lastMoveDir = Vector2.right; // 记录最后移动方向
    private int lastDirectionIndex = 0; // 记录最后方向索引

    void Start()
    {
        currentGridPosition = gridManager.GetGridPosition(transform.position);
        if (messageText != null) messageText.text = "";
        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Vector2Int? targetGridPos = gridManager.GetGridPositionFromMouse();
            if (targetGridPos.HasValue)
                MoveToGridPosition(targetGridPos.Value);
        }
        // 当角色未移动时，播放idle动画
        if (!isMoving && animator != null)
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public void MoveToGridPosition(Vector2Int targetPos)
    {
        if (messageText != null) messageText.text = "";
        if (currentGridPosition == targetPos) return;

        movePath = FindShortestPath(currentGridPosition, targetPos);

        if (movePath != null && movePath.Count > 0)
        {
            bool pathClear = true;
            for (int i = 0; i < movePath.Count - 1; i++)
            {
                if (!gridManager.IsPathClear(movePath[i], movePath[i + 1]))
                {
                    pathClear = false;
                    break;
                }
            }

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

    private List<Vector2Int> FindShortestPath(Vector2Int start, Vector2Int target)
    {
        var visited = new HashSet<Vector2Int>();
        var queue = new Queue<List<Vector2Int>>();
        queue.Enqueue(new List<Vector2Int> { start });
        visited.Add(start);

        while (queue.Count > 0)
        {
            var path = queue.Dequeue();
            var current = path[path.Count - 1];

            if (current == target)
            {
                path.RemoveAt(0);
                return path;
            }

            foreach (var dir in directions)
            {
                var next = current + dir;
                if (next.x < 0 || next.x >= gridManager.gridWidth ||
                    next.y < 0 || next.y >= gridManager.gridHeight)
                    continue;

                if (visited.Contains(next)) continue;
                if (gridManager.IsObstacle(next)) continue;

                visited.Add(next);
                var newPath = new List<Vector2Int>(path) { next };
                queue.Enqueue(newPath);
            }
        }

        return null;
    }

    private IEnumerator MoveAlongPath()
    {
        isMoving = true;

        foreach (var targetGridPos in movePath)
        {
            Vector2 targetWorldPos = gridManager.GetWorldPosition(targetGridPos.x, targetGridPos.y);
            Vector2 startPos = transform.position;

            Vector2 moveDir = (targetWorldPos - startPos).normalized;

            // 记录最后移动方向和索引
            if (moveDir != Vector2.zero)
            {
                lastMoveDir = moveDir;
                lastDirectionIndex = GetDirectionIndex(moveDir);
            }

            // 设置动画参数（只用整数方向）
            if (animator != null)
            {
                animator.SetBool("IsMoving", true);
                animator.SetInteger("MoveDirection", lastDirectionIndex);
            }

            float elapsedTime = 0f;
            float distance = Vector2.Distance(startPos, targetWorldPos);
            float moveDuration = distance / moveSpeed;

            while (elapsedTime < moveDuration)
            {
                transform.position = Vector2.Lerp(startPos, targetWorldPos, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetWorldPos;
            currentGridPosition = targetGridPos;
        }

        isMoving = false;
        movePath.Clear();

        // 移动结束，播放对应方向的idle动画
        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
            animator.SetInteger("IdleDirection", lastDirectionIndex);
        }
    }

    // 获取方向索引（0:右, 1:下, 2:上, 3:左）
    private int GetDirectionIndex(Vector2 dir)
    {
        if (Vector2.Dot(dir, Vector2.right) > 0.7f) return 0;    // 右
        if (Vector2.Dot(dir, Vector2.down) > 0.7f) return 1;     // 下
        if (Vector2.Dot(dir, Vector2.up) > 0.7f) return 2;       // 上
        if (Vector2.Dot(dir, Vector2.left) > 0.7f) return 3;     // 左
        return 0; // 默认右
    }
}
