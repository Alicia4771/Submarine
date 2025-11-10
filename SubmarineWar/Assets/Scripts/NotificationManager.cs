using UnityEngine;
using TMPro; // TextMeshPro を使うために必要
using System.Collections; // Coroutine (コルーチン) を使うために必要

public class NotificationManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI notificationText; // 割り当てるText(TMP)

    [SerializeField]
    private CanvasGroup notificationCanvasGroup; // 割り当てるCanvas Group

    // Start is called before the first frame update
    void Start()
    {
        // 起動時は非表示（透明）にしておく
        if (notificationCanvasGroup != null)
        {
            notificationCanvasGroup.alpha = 0f; // 透明にする
            notificationCanvasGroup.interactable = false; // 操作不可にする
        }
    }

    // 外部からこの関数を呼び出してメッセージを表示する
    public void ShowMessage(string message, float duration)
    {
        // 既に表示中のメッセージがあれば、それを停止
        StopAllCoroutines();
        
        // テキストを設定
        if (notificationText != null)
        {
            notificationText.text = message;
        }

        // 表示・非表示のタイマーを開始
        StartCoroutine(ShowAndHideCoroutine(duration));
    }

    // 表示して、一定時間後に非表示にするコルーチン
    private IEnumerator ShowAndHideCoroutine(float duration)
    {
        // 1. 表示する (不透明にする)
        notificationCanvasGroup.alpha = 1f;

        // 2. 指定された秒数だけ待つ
        yield return new WaitForSeconds(duration);

        // 3. 非表示にする (透明にする)
        notificationCanvasGroup.alpha = 0f;
    }
}