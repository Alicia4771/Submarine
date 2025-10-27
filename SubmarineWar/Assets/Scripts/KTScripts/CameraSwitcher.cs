using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    // ==========================================================
    // 外部連携用の変数（Inspectorで設定）
    // ==========================================================

    public Transform xrRig;
    public Transform interiorAnchor;
    public Transform periscopeAnchor;

    // 💡 内部へ戻るためのボタン (中指ボタン/Grab/Activate アクションを設定)
    public InputActionReference returnAction;

    // 既存の2カメラシステム（非VR視点切り替え用）
    public GameObject existingInteriorCamera;
    public GameObject existingPeriscopeCamera;

    // TorpedoLauncherから状態を参照できるように公開
    [HideInInspector] public bool isPeriscopeView = false;

    // ... (Start()とOnDestroy()は、returnActionの接続ロジックをそのまま使用) ...

    void Start()
    {
        // 中指ボタンアクションの接続 (戻る機能)
        if (returnAction != null && returnAction.action != null)
        {
            // 💡 潜望鏡から内部へ戻る機能として接続
            returnAction.action.performed += OnReturnActionPerformed;
            returnAction.action.Enable();
        }
    }

    void OnDestroy()
    {
        if (returnAction != null && returnAction.action != null)
        {
            returnAction.action.performed -= OnReturnActionPerformed;
        }
    }


    // ==========================================================
    // 💡 センサー班の仕事: イベントトリガー
    // ==========================================================

    // 1. 潜望鏡をタップした際に呼ばれるメソッド (XR Simple Interactableから接続)
    public void SwitchToPeriscopeByTap() // メソッド名を分かりやすく変更
    {
        // 内部視点にいる場合のみ、潜望鏡視点へ切り替え
        if (!isPeriscopeView)
        {
            ToggleCameraLogic(true); // true = 潜望鏡視点へ
        }
    }

    // 2. 中指ボタン（Performed）で呼ばれるメソッド
    private void OnReturnActionPerformed(InputAction.CallbackContext context)
    {
        // 潜望鏡視点にいる場合のみ、内部視点へ戻す
        if (isPeriscopeView)
        {
            ToggleCameraLogic(false); // false = 内部視点へ
        }
    }

    // ==========================================================
    // 💡 統合されたカメラ切り替えロジック
    // ==========================================================

    private void ToggleCameraLogic(bool toPeriscope)
    {
        isPeriscopeView = toPeriscope; // 状態を設定

        Transform targetAnchor = isPeriscopeView ? periscopeAnchor : interiorAnchor;

        // 1. プレイヤーのXR Rigの位置を移動させる（VR視点の切り替え）
        xrRig.position = targetAnchor.position;
        xrRig.rotation = targetAnchor.rotation;

        // 2. 既存の2カメラのSetActiveを反転させる（非VR視点/モニター表示に影響）
        if (existingInteriorCamera != null && existingPeriscopeCamera != null)
        {
            existingInteriorCamera.SetActive(!isPeriscopeView);
            existingPeriscopeCamera.SetActive(isPeriscopeView);
        }

        Debug.Log("カメラが " + (isPeriscopeView ? "潜望鏡" : "内部") + " 視点に切り替わりました。");
    }
}