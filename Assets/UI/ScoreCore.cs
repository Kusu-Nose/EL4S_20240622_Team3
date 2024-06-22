using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCore : MonoBehaviour
{
    //シングルトン化
    public static ScoreCore instance;
    private int score = 0;

    private void Awake()
    {
        //もしSoundマネージャーが入ったオブジェクトがない場合
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);    //このオブジェクトを消す
            return;
        }

        //このオブジェクトをロードしても消さないようにする
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int add)
    {
        //スコアに加える
        score += add;
    }

    public int GetScore()
    {
        return score;
    }

    //ゲームスタートで初期化する
    public void InitS()
    {
        score = 0;
    }
}
