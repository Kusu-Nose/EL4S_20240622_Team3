using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class EditableBezierCamera : MonoBehaviour
{
    // シーン内で編集可能なベジェ曲線の制御点
    public Vector3[] controlPoints;
    public float speed = 0.5f;
    public Vector3 LockUpPosition = Vector3.zero;

    // Gizmosと制御点の表示/非表示を制御するための変数
    public bool showGizmos = true;
    public bool showControlPoints = true;

    private float t = 0f;
    private int numPoints;

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
        if (Application.isPlaying)
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

    // シーンビューで制御点を視覚的に表示・編集する
    private void OnDrawGizmos()
    {
        if (!showGizmos) return;

        // 制御点の表示/非表示を切り替える
        if (showControlPoints)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < controlPoints.Length; i++)
            {
                controlPoints[i] = Handles.PositionHandle(controlPoints[i], Quaternion.identity);
                Gizmos.DrawSphere(controlPoints[i], 0.1f);
            }
        }

        // ベジェ曲線を描画
        Gizmos.color = Color.green;
        DrawBezierCurve();
    }

    private void DrawBezierCurve()
    {
        if (controlPoints.Length < 2)
            return;

        Vector3 previousPoint = controlPoints[0];
        int resolution = 20; // 曲線の分割数

        for (int i = 1; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            Vector3 point = CalculateBezierPoint(t, controlPoints);
            Gizmos.DrawLine(previousPoint, point);
            previousPoint = point;
        }
    }
}
