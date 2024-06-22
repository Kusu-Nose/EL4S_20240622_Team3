using DamageNumbersPro;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ScoreUpUI : MonoBehaviour
{
    public ScoreCore scoreCore;
    public TMP_Text scoreText;
    public DamageNumber numberPrefab;
    public Vector4 position;

    private Coroutine scoreCoroutine;
    short stamina = 0;

    private void Start()
    {
        scoreCore = ScoreCore.instance;

        scoreUpdate();
    }

    public void CreatScoreUP(short score)
    {
        //スコアアップアニメーション
        DamageNumber scoreNumber = numberPrefab.Spawn(position);

        //スコア更新
        //scoreCore.AddScore(score);

        //文字更新
        //scoreUpdate();
    }

    public void scoreUpdate()
    {
        // 現在のスコアを取得してテキストを更新
        scoreText.text = scoreCore.GetScore().ToString("D6");
    }

    // スコアを一定間隔で増加させるコルーチン
    public IEnumerator IncreaseScoreOverTime(short score, float interval)
    {
        while (true)
        {
            CreatScoreUP(score);
            yield return new WaitForSeconds(interval);
        }
    }

    // スコア増加を開始するメソッド
    public void StartIncreasingScore(short score, float interval)
    {
        if (scoreCoroutine != null)
        {
            StopCoroutine(scoreCoroutine);
        }
        scoreCoroutine = StartCoroutine(IncreaseScoreOverTime(score, interval));
    }

    // スコア増加を停止するメソッド
    public void StopIncreasingScore()
    {
        if (scoreCoroutine != null)
        {
            StopCoroutine(scoreCoroutine);
        }
    }
}


//カスタム
#if UNITY_EDITOR
[CustomEditor(typeof(ScoreUpUI))]
class CustomScoreUPUI : Editor
{
   public override void OnInspectorGUI()
    {
        // ターゲットの参照を取得
        ScoreUpUI scoreUpUI = (ScoreUpUI)target;

        // インスペクターの変更を監視
        EditorGUI.BeginChangeCheck();

        // ダメージナンバープレハブのフィールドを表示
        scoreUpUI.numberPrefab = (DamageNumber)EditorGUILayout.ObjectField("Damage Number Prefab", scoreUpUI.numberPrefab, typeof(DamageNumber), true);

        // Vector4フィールドを表示
        scoreUpUI.position = EditorGUILayout.Vector4Field("Position", scoreUpUI.position);

        // TMPを取得
        scoreUpUI.scoreText = (TMP_Text)EditorGUILayout.ObjectField("TMPro", scoreUpUI.scoreText, typeof(TMP_Text), true);

        // インスペクターの変更を保存
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(scoreUpUI);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Score生成"))
        {
            DebugSpawn(scoreUpUI);
        }

        if (GUILayout.Button("Score生成"))
        {
            scoreUpUI.StartIncreasingScore(10, 0.1f);

        }
        if (GUILayout.Button("スコア増加停止"))
        {
            scoreUpUI.StopIncreasingScore();
        }
    }

    void DebugSpawn(ScoreUpUI sup)
    {
        sup.CreatScoreUP((short)Random.Range(1, 100));
    }
}
#endif
