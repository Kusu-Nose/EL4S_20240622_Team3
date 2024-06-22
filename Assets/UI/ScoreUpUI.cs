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

    public RectTransform uiCanvas; // UI Canvas��RectTransform
    public Vector2 screenPosition; // ��ʍ��W�i�s�N�Z���P�ʁj


    private Coroutine scoreCoroutine;
    short stamina = 0;

    private void Start()
    {
        scoreCore = ScoreCore.instance;

        scoreUpdate();
    }

    public void CreatScoreUP(short score)
    {
        Camera camera = Camera.main;

        //�X�R�A�A�b�v�A�j���[�V����
        Vector2 spawnPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(uiCanvas, screenPosition, null, out spawnPosition);
        DamageNumber scoreNumber = Instantiate(numberPrefab, uiCanvas);
        scoreNumber.transform.localPosition = new Vector3(spawnPosition.x, spawnPosition.y, camera.transform.position.z + 20);
        scoreNumber.transform.localPosition += new Vector3(0, 0, 5);
        scoreNumber.number = score;

        //�X�R�A�X�V
        scoreCore.AddScore(score);

        //�����X�V
        scoreUpdate();
    }

    public void scoreUpdate()
    {
        // ���݂̃X�R�A���擾���ăe�L�X�g���X�V
        scoreText.text = scoreCore.GetScore().ToString("D6");
    }

    // �X�R�A�����Ԋu�ő���������R���[�`��
    public IEnumerator IncreaseScoreOverTime(short score, float interval)
    {
        while (true)
        {
            CreatScoreUP(score);
            yield return new WaitForSeconds(interval);
        }
    }

    // �X�R�A�������J�n���郁�\�b�h
    public void StartIncreasingScore(short score, float interval)
    {
        if (scoreCoroutine != null)
        {
            StopCoroutine(scoreCoroutine);
        }
        scoreCoroutine = StartCoroutine(IncreaseScoreOverTime(score, interval));
    }

    // �X�R�A�������~���郁�\�b�h
    public void StopIncreasingScore()
    {
        if (scoreCoroutine != null)
        {
            StopCoroutine(scoreCoroutine);
        }
    }
}


//�J�X�^��
#if UNITY_EDITOR
[CustomEditor(typeof(ScoreUpUI))]
class CustomScoreUPUI : Editor
{
   public override void OnInspectorGUI()
    {
        // �^�[�Q�b�g�̎Q�Ƃ��擾
        ScoreUpUI scoreUpUI = (ScoreUpUI)target;

        // �C���X�y�N�^�[�̕ύX���Ď�
        EditorGUI.BeginChangeCheck();

        // �_���[�W�i���o�[�v���n�u�̃t�B�[���h��\��
        scoreUpUI.numberPrefab = (DamageNumber)EditorGUILayout.ObjectField("Damage Number Prefab", scoreUpUI.numberPrefab, typeof(DamageNumber), true);

        // UI Canvas��RectTransform�t�B�[���h��\��
        scoreUpUI.uiCanvas = (RectTransform)EditorGUILayout.ObjectField("UI Canvas", scoreUpUI.uiCanvas, typeof(RectTransform), true);

        // ��ʍ��W�t�B�[���h��\��
        scoreUpUI.screenPosition = EditorGUILayout.Vector2Field("Screen Position", scoreUpUI.screenPosition);

        // TMP���擾
        scoreUpUI.scoreText = (TMP_Text)EditorGUILayout.ObjectField("TMPro", scoreUpUI.scoreText, typeof(TMP_Text), true);

        // �C���X�y�N�^�[�̕ύX��ۑ�
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(scoreUpUI);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Score����"))
        {
            DebugSpawn(scoreUpUI);
        }

        if (GUILayout.Button("Score����"))
        {
            scoreUpUI.StartIncreasingScore(10, 0.1f);

        }
        if (GUILayout.Button("�X�R�A������~"))
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
