using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string sceneBgmName; // ヒエラルキー上で設定するBGM名
    private SoundsManager soundsManager;

    private void Start()
    {
        //fpsを60に設定
        Application.targetFrameRate = 60;

        // ヒエラルキー上からSoundsManagerを取得
        soundsManager = FindObjectOfType<SoundsManager>();

        if (soundsManager != null)
        {
            // シーン開始時にBGMを再生
            soundsManager.PlayBGM(sceneBgmName);
        }
        else
        {
            Debug.Log("SoundsManagerが見つかりません！");
        }
    }

    private void Update()
    {
        // シーン遷移(TitleとResult用)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //現在のシーン名を取得し、切り替え先を決定する（TitleからGame、ResultからTitle）
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "Title")
            {
                LoadScene("Game");
            }
            else if (sceneName == "Result")
            {
                LoadScene("Title");
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        // シーン遷移前にBGMを停止
        if (soundsManager != null)
        {
            soundsManager.StopBGM();
        }
        else
        {
            Debug.Log("SoundsManagerが見つかりません！");
        }

        SceneManager.LoadScene(sceneName);

    }
}