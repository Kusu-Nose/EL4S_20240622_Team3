using UnityEngine;

public class BezierCameraMovement : MonoBehaviour
{
    // インスペクタで設定するためのポイント配列
    public Transform[] controlPoints;
    public float speed = 0.5f;
    public Vector3 LockUpPosition = Vector3.zero;

    private float t = 0f; // tの値
    private int numPoints; // 配列内のポイント数

    void Start()
    {
        // ポイント数を取得
        numPoints = controlPoints.Length;

        // ポイントが2つ未満の場合はエラーメッセージを表示
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

        // カメラの向きを次の位置に向ける（滑らかな移動を実現）
        //Vector3 nextPosition = CalculateBezierPoint(Mathf.Min(t + 0.01f, 1f), controlPoints);
        transform.LookAt(LockUpPosition);

        // tが1に達したら停止（必要に応じてループさせることも可能）
        if (t >= 1f)
        {
            enabled = false;
        }
    }

    // 3次ベジェ曲線の計算メソッド
    Vector3 CalculateBezierPoint(float t, Transform[] points)
    {
        // ポイント数
        int n = points.Length - 1;
        Vector3 result = Vector3.zero;

        // ベルヌーイ係数とポイントの加重和を計算
        for (int i = 0; i <= n; i++)
        {
            float binomialCoeff = BinomialCoefficient(n, i);
            float term = binomialCoeff * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i);
            result += term * points[i].position;
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
}
