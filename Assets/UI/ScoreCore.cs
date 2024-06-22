using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCore : MonoBehaviour
{
    //�V���O���g����
    public static ScoreCore instance;
    private int score = 0;

    private void Awake()
    {
        //����Sound�}�l�[�W���[���������I�u�W�F�N�g���Ȃ��ꍇ
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);    //���̃I�u�W�F�N�g������
            return;
        }

        //���̃I�u�W�F�N�g�����[�h���Ă������Ȃ��悤�ɂ���
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int add)
    {
        //�X�R�A�ɉ�����
        score += add;
    }

    public int GetScore()
    {
        return score;
    }

    //�Q�[���X�^�[�g�ŏ���������
    public void InitS()
    {
        score = 0;
    }
}
