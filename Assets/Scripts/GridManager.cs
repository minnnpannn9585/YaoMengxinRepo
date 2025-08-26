using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("网格设置")]
    public int gridWidth = 10;         // 网格宽度
    public int gridHeight = 10;        // 网格高度
    public float cellSize = 1f;        // 单元格大小
    public Color gridColor = Color.gray; // 网格线颜色
    public LayerMask obstacleLayer;    // 障碍物图层

    private Vector2 originPosition;    // 网格原点位置
    private float angle = 30f;         // 30度角（弧度）
    private float angleRad;

    void Start()
    {
        originPosition = transform.position;
        angleRad = angle * Mathf.Deg2Rad;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gridColor;
        originPosition = transform.position;
        angleRad = angle * Mathf.Deg2Rad;

        // 绘制菱形网格
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector2 cellCenter = GetWorldPosition(x, y);
                DrawDiamond(cellCenter, cellSize * 0.5f);
            }
        }
    }

    // 绘制30度角的菱形
    void DrawDiamond(Vector2 center, float halfSize)
    {
        // 计算30度角菱形的四个顶点
        float xOffset = halfSize * Mathf.Cos(angleRad);
        float yOffset = halfSize * Mathf.Sin(angleRad);

        Vector2 top = center + new Vector2(0, yOffset);
        Vector2 right = center + new Vector2(xOffset, 0);
        Vector2 bottom = center + new Vector2(0, -yOffset);
        Vector2 left = center + new Vector2(-xOffset, 0);

        // 绘制菱形边
        Gizmos.DrawLine(top, right);
        Gizmos.DrawLine(right, bottom);
        Gizmos.DrawLine(bottom, left);
        Gizmos.DrawLine(left, top);
    }

    // 将网格坐标转换为世界坐标
    public Vector2 GetWorldPosition(int x, int y)
    {
        // 30度角菱形网格坐标转换公式
        float xFactor = cellSize * Mathf.Cos(angleRad) * 0.5f;
        float yFactor = cellSize * Mathf.Sin(angleRad) * 0.5f;

        float worldX = originPosition.x + (x - y) * xFactor;
        float worldY = originPosition.y + (x + y) * yFactor;

        return new Vector2(worldX, worldY);
    }

    // 将世界坐标转换为网格坐标
    public Vector2Int GetGridPosition(Vector2 worldPosition)
    {
        // 计算相对于原点的偏移
        float offsetX = worldPosition.x - originPosition.x;
        float offsetY = worldPosition.y - originPosition.y;

        float xFactor = cellSize * Mathf.Cos(angleRad) * 0.5f;
        float yFactor = cellSize * Mathf.Sin(angleRad) * 0.5f;

        // 30度角菱形网格坐标反向转换
        float x = (offsetX / xFactor + offsetY / yFactor) / 2;
        float y = (-offsetX / xFactor + offsetY / yFactor) / 2;

        // 四舍五入并限制在网格范围内
        int gridX = Mathf.RoundToInt(x);
        int gridY = Mathf.RoundToInt(y);

        gridX = Mathf.Clamp(gridX, 0, gridWidth - 1);
        gridY = Mathf.Clamp(gridY, 0, gridHeight - 1);

        return new Vector2Int(gridX, gridY);
    }

    // 检测鼠标点击的网格位置
    public Vector2Int? GetGridPositionFromMouse()
    {
        Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseWorldPos = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);

        return GetGridPosition(mouseWorldPos);
    }

    // 检查指定网格位置是否有障碍物
    public bool IsObstacle(Vector2Int gridPos)
    {
        Vector2 worldPos = GetWorldPosition(gridPos.x, gridPos.y);
        // 检测该位置是否有障碍物
        Collider2D collider = Physics2D.OverlapPoint(worldPos, obstacleLayer);
        return collider != null;
    }

    // 检查两点之间的路径是否通畅
    public bool IsPathClear(Vector2Int start, Vector2Int end)
    {
        // 直接点到点的情况
        if (start == end) return true;

        // 检查终点是否是障碍物
        if (IsObstacle(end)) return false;

        // 计算单步移动路径
        Vector2Int step = end - start;
        Vector2Int current = start;

        // 逐步检查路径上的每个点
        while (current != end)
        {
            // 向终点移动一步
            int xStep = step.x != 0 ? (step.x > 0 ? 1 : -1) : 0;
            int yStep = step.y != 0 ? (step.y > 0 ? 1 : -1) : 0;

            current += new Vector2Int(xStep, yStep);

            // 检查当前位置是否是障碍物
            if (IsObstacle(current))
                return false;
        }

        return true;
    }
}
