using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SpatialTracking; // TrackedPoseDriverのために必要

public class SubmarineCameraControl : MonoBehaviour
{
    // ==========================================================
    // 外部連携用の変数（Inspectorで設定必須）
    // ==========================================================

    public CameraManager cameraManager; // 既存の CameraManager クラスへの参照 (切り替えロジック本体)

    // 💡 修正箇所: 左右両方の中指ボタンアクションを追加
    public InputActionReference returnActionRight;
    public InputActionReference returnActionLeft;

    // TorpedoLauncherから状態を参照できるように公開
    [HideInInspector] public bool isPeriscopeView = false;

    // 🚨 Step 3 統合: 必要な参照
    public TrackedPoseDriver mainCameraPoseDriver; // Main Camera の Tracked Pose Driver
    public GameObject leftControllerVisual;        // 左コントローラーの見た目
    public GameObject rightControllerVisual;       // 右コントローラーの見た目

    // 深度制限値 (Step 1のために残す)
    private const float MAX_DEPTH = -5.0f;


    // ==========================================================
    // ライフサイクルとアクション接続
    // ==========================================================

    void Start()
    {
        if (cameraManager == null)
        {
            Debug.LogError("Camera Managerが設定されていません。切り替えロジックが実行できません。");
            return;
        }

        // 💡 修正箇所: 右コントローラーの中指ボタンを接続
        if (returnActionRight != null && returnActionRight.action != null)
        {
            returnActionRight.action.performed += OnReturnActionPerformed;
            returnActionRight.action.Enable();
        }

        // 💡 修正箇所: 左コントローラーの中指ボタンを接続
        if (returnActionLeft != null && returnActionLeft.action != null)
        {
            returnActionLeft.action.performed += OnReturnActionPerformed;
            returnActionLeft.action.Enable();
        }

        isPeriscopeView = false;

        // 💡 初期状態ではトラッキングを有効にする
        if (mainCameraPoseDriver != null)
        {
            mainCameraPoseDriver.enabled = true;
        }
    }

    void OnDestroy()
    {
        // 💡 修正箇所: 右コントローラーの購読解除
        if (returnActionRight != null && returnActionRight.action != null)
        {
            returnActionRight.action.performed -= OnReturnActionPerformed;
        }

        // 💡 修正箇所: 左コントローラーの購読解除
        if (returnActionLeft != null && returnActionLeft.action != null)
        {
            returnActionLeft.action.performed -= OnReturnActionPerformed;
        }
    }

    // ==========================================================
    // 💡 VR入力トリガー
    // ==========================================================

    // 1. 潜望鏡をタップした際に呼ばれるメソッド (外部の XR Simple Interactableから接続)
    public void SwitchToPeriscopeByTap()
    {
        // 深度チェック (Step 1のロジック)
        if (DataManager.GetSubmarineDepth() <= MAX_DEPTH)
        {
            Debug.Log("潜望鏡使用不可：深度が深すぎます。");
            return;
        }

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
    // 💡 カメラ切り替えとトラッキング制御ロジック
    // ==========================================================

    private void ToggleViewAndCallManager(bool toPeriscope)
    {
        isPeriscopeView = toPeriscope; // 状態を設定

        // 🚨 Step 3: トラッキングの固定化とコントローラーのビジュアル制御 🚨

        // 1. ヘッドセットの回転を有効/無効化（固定化）
        if (mainCameraPoseDriver != null)
        {
            // 潜望鏡視点 (true) なら、HMDの動きを伝えるコンポーネントを無効化（画面固定）
            mainCameraPoseDriver.enabled = !toPeriscope;
        }

        // 2. コントローラーのビジュアルを非表示
        if (leftControllerVisual != null) leftControllerVisual.SetActive(!toPeriscope);
        if (rightControllerVisual != null) rightControllerVisual.SetActive(!toPeriscope);


        // 3. 既存の CameraManager の切り替えメソッドを呼び出し
        if (cameraManager != null)
        {
            cameraManager.ToggleCameraLogic();
        }

        Debug.Log("カメラが " + (isPeriscopeView ? "潜望鏡(固定)" : "内部(追従)") + " 視点に切り替わりました。");
    }
}