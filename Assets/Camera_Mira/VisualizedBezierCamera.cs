using UnityEngine;

public class VisualizedBezierCamera : MonoBehaviour
{
    // ベジェ曲線の制御点をVector3で設定（インスペクタ上で編集可能）
    public Vector3[] controlPoints;
    public float speed = 0.5f;
    public Vector3 LockUpPosition = Vector3.zero;

    private float t = 0f; // tの値
    private int numPoints; // 配列内のポイント数

    void Start()
    {
        numPoints = controlPoints.Length;

        if (numPoints < 2)
        {
            Debug.LogError("Control points must be at least 2 to form a Bezier curve.");
            enabled = false;
        }
    }

    void Update()
    {
        // tを速度に応じて増加させる
        t += Time.deltaTime * speed;
        t = Mathf.Clamp01(t);

        // ベジェ曲線上の新しい位置を計算
        Vector3 newPosition = CalculateBezierPoint(t, controlPoints);
        transform.position = newPosition;

        // カメラの向きを次の位置に向ける
        Vector3 nextPosition = CalculateBezierPoint(Mathf.Min(t + 0.01f, 1f), controlPoints);
        transform.LookAt(LockUpPosition);

        // tが1に達したら停止
        if (t >= 1f)
        {
            enabled = false;
        }
    }

    // 複数ポイントでベジェ曲線を計算するメソッド
    Vector3 CalculateBezierPoint(float t, Vector3[] points)
    {
        int n = points.Length - 1;
        Vector3 result = Vector3.zero;

        for (int i = 0; i <= n; i++)
        {
            float binomialCoeff = BinomialCoefficient(n, i);
            float term = binomialCoeff * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i);
            result += term * points[i];
        }

        return result;
    }

    // 二項係数を計算するメソッド
    int BinomialCoefficient(int n, int k)
    {
        int result = 1;
        for (int i = 1; i <= k; i++)
        {
            result *= n - (k - i);
            result /= i;
        }
        return result;
    }

    // ウェイポイントとベジェ曲線を視覚化
    private void OnDrawGizmos()
    {
        // ウェイポイントを表示
        Gizmos.color = Color.red;
        foreach (Vector3 point in controlPoints)
        {
            Gizmos.DrawSphere(transform.position + point, 0.1f);
        }

        // ベジェ曲線を描画
        Gizmos.color = Color.green;
        DrawBezierCurve();
    }

    // ベジェ曲線を描画するメソッド
    private void DrawBezierCurve()
    {
        if (controlPoints.Length < 2)
            return;

        Vector3 previousPoint = transform.position + controlPoints[0];
        int resolution = 20;

        for (int i = 1; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            Vector3 point = CalculateBezierPoint(t, controlPoints) + transform.position;
            Gizmos.DrawLine(previousPoint, point);
            previousPoint = point;
        }
    }
}