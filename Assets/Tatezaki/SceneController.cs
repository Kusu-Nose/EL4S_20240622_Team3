using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string sceneBgmName; // �q�G�����L�[��Őݒ肷��BGM��
    private SoundsManager soundsManager;

    private void Start()
    {
        //fps��60�ɐݒ�
        Application.targetFrameRate = 60;

        // �q�G�����L�[�ォ��SoundsManager���擾
        soundsManager = FindObjectOfType<SoundsManager>();

        if (soundsManager != null)
        {
            // �V�[���J�n����BGM���Đ�
            soundsManager.PlayBGM(sceneBgmName);
        }
        else
        {
            Debug.Log("SoundsManager��������܂���I");
        }
    }

    private void Update()
    {
        // �V�[���J��(Title��Result�p)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //���݂̃V�[�������擾���A�؂�ւ�������肷��iTitle����Game�AResult����Title�j
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
        // �V�[���J�ڑO��BGM���~
        if (soundsManager != null)
        {
            soundsManager.StopBGM();
        }
        else
        {
            Debug.Log("SoundsManager��������܂���I");
        }

        SceneManager.LoadScene(sceneName);

    }
}