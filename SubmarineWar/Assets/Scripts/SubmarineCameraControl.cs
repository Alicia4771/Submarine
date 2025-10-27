using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

// 💡 クラス名をユニークな名前に変更
public class SubmarineCameraControl : MonoBehaviour
{
    // ==========================================================
    // 外部連携用の変数（Inspectorで設定）
    // ==========================================================

    // 既存の CameraManager クラスへの参照を追加
    public CameraManager cameraManager;

    // 中指ボタンのアクション（内部へ戻る）
    public InputActionReference returnAction;

    // TorpedoLauncherから状態を参照できるように公開 (XR Rig移動は不要なため削除)
    [HideInInspector] public bool isPeriscopeView = false;

    // (アンカー、XR Rig、ExistingCameraの変数は、XR Rig移動が不要なため全て削除)

    // ==========================================================
    // ライフサイクルとアクション接続
    // ==========================================================

    void Start()
    {
        // 💡 既存の CameraManager が有効か確認し、無効なら警告
        if (cameraManager == null)
        {
            Debug.LogError("Camera Managerが設定されていません。切り替えロジックが実行できません。");
            return;
        }

        // 中指ボタンアクションの接続 (戻る機能)
        if (returnAction != null && returnAction.action != null)
        {
            returnAction.action.performed += OnReturnActionPerformed;
            returnAction.action.Enable();
        }
        isPeriscopeView = false;
    }

    void OnDestroy()
    {
        if (returnAction != null && returnAction.action != null)
        {
            returnAction.action.performed -= OnReturnActionPerformed;
        }
    }

    // ==========================================================
    // 💡 VR入力トリガー
    // ==========================================================

    // 1. 潜望鏡をタップした際に呼ばれるメソッド (XR Simple Interactableから接続)
    public void SwitchToPeriscopeByTap()
    {
        // 内部視点にいる場合のみ、潜望鏡視点へ切り替え
        if (!isPeriscopeView)
        {
            ToggleViewAndCallManager(true); // true = 潜望鏡視点へ
        }
    }

    // 2. 中指ボタン（Performed）で呼ばれるメソッド
    private void OnReturnActionPerformed(InputAction.CallbackContext context)
    {
        // 潜望鏡視点にいる場合のみ、内部視点へ戻す
        if (isPeriscopeView)
        {
            ToggleViewAndCallManager(false); // false = 内部視点へ
        }
    }

    // ==========================================================
    // 💡 カメラ切り替えと状態更新のロジック（既存ロジックの呼び出し）
    // ==========================================================

    private void ToggleViewAndCallManager(bool toPeriscope)
    {
        isPeriscopeView = toPeriscope; // 状態を設定

        // 💡 既存の CameraManager の切り替えメソッドを呼び出す
        // CameraManager.csの修正版（ToggleCameraLogic()を追加したもの）が必要
        cameraManager.ToggleCameraLogic();

        Debug.Log("カメラが " + (isPeriscopeView ? "潜望鏡" : "内部") + " 視点に切り替わりました。");
    }
}