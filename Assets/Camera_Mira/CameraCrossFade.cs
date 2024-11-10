using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カメラのクロスフェード演出
/// </summary>
public class CameraCrossFade : MonoBehaviour
{
    // 切り替え対象カメラ
    [SerializeField] private Camera[] _cameras;

    // クロスフェード演出を表示するためのRawImage
    [SerializeField] private RawImage _crossFadeImage;

    // フェード時間
    [SerializeField] private float _fadeDuration = 1;

    private RenderTexture _renderTexture;
    private int _currentIndex;
    private Coroutine _fadeCoroutine;

    // 指定されたパラメータは有効かどうか
    private bool IsValid => _cameras.Length >= 2 && _crossFadeImage != null;

    // フェード中かどうか
    private bool IsChanging => _fadeCoroutine != null;

    // 初期化
    private void Awake()
    {
        if (!IsValid) return;

        // クロスフェード用のRenderTexture作成
        _renderTexture = new RenderTexture(Screen.width, Screen.height, 0);

        // RawImage初期化
        _crossFadeImage.texture = _renderTexture;
        _crossFadeImage.gameObject.SetActive(false);

        // カメラ初期化
        for (var i = 0; i < _cameras.Length; ++i)
        {
            // 最初のカメラだけ有効にする
            _cameras[i].enabled = i == _currentIndex;
        }
    }

    // キー入力チェック・カメラ変更
    private void Update()
    {
        if (!IsValid || IsChanging || !Input.anyKeyDown) return;

        // 数字キー入力チェック
        if (!int.TryParse(Input.inputString, out var cameraNo))
            return;

        // 押されたカメラ番号に対応したカメラに切り替え
        ChangeCamera(cameraNo - 1);
    }

    // 指定されたインデックスのカメラにクロスフェードしながら切り替える
    public void ChangeCamera(int index)
    {
        if (!IsValid || IsChanging)
            return;

        if (index < 0 || index >= _cameras.Length)
            return;

        if (index == _currentIndex)
            return;

        // クロスフェード演出開始
        _fadeCoroutine = StartCoroutine(CrossFadeCoroutine(index));
    }

    // クロスフェード演出コルーチン
    private IEnumerator CrossFadeCoroutine(int index)
    {
        // フェード用のRawImage表示
        _crossFadeImage.gameObject.SetActive(true);

        // フェード中のみ、切り替え後カメラの描画先をRenderTextureに設定
        var nextCamera = _cameras[index];
        nextCamera.enabled = true;
        nextCamera.targetTexture = _renderTexture;

        // RawImageのα値を徐々に変更（フェード）
        var startTime = Time.time;

        while (true)
        {
            var time = Time.time - startTime;
            if (time > _fadeDuration)
                break;

            var alpha = time / _fadeDuration;
            _crossFadeImage.color = new Color(1, 1, 1, alpha);

            yield return null;
        }

        // 切り替え後カメラを有効化
        nextCamera.targetTexture = null;
        _cameras[_currentIndex].enabled = false;

        // フェード用のRawImage非表示
        _crossFadeImage.gameObject.SetActive(false);

        _currentIndex = index;
        _fadeCoroutine = null;
    }
}