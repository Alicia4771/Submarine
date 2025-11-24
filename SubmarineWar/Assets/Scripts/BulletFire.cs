using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class BulletFire : MonoBehaviour // Unityのゲームオブジェクトにアタッチするための基本クラス
{
    // ==========================================================
    // 外部連携用の変数（Inspectorで設定）
    // ==========================================================

    // 💡 修正箇所: SubmarineCameraControl への参照を公開で宣言します
    public SubmarineCameraControl cameraSwitcher;

    // 💡 右コントローラーのデジタルアクションを設定
    public InputActionReference fireActionRight;

    // 💡 左コントローラーのデジタルアクションを設定
    public InputActionReference fireActionLeft;

    // 発射する弾（球）のモデルを設定 (Assets/Prefabsからドラッグ)
    public GameObject torpedoPrefab;

    // 弾が生成されるシーン内の位置と方向を示すオブジェクトを設定 (Hierarchyからドラッグ)
    public Transform firePoint;

    // 発射時に弾に与える力の強さ (数値で設定)
    public float fireForce = 50f;

    // 🚨 VR実機対応: 代替入力手段として直接的なコントローラーチェックを追加
    [Header("VR実機対応デバッグ")]
    public bool enableDirectControllerInput = true; // Inspector でON/OFF切り替え可能


    // ==========================================================
    // ライフサイクルメソッド: 入力アクションの接続と切断（イベントの購読）
    // ==========================================================

    void Start() // ゲーム開始時に一度だけ実行される
    {
        // 🚨 VR実機デバッグ用: 初期化状況をログ出力
        Debug.Log("=== BulletFire 初期化開始 ===");
        
        // 右コントローラーの接続とデバッグ
        if (fireActionRight != null && fireActionRight.action != null)
        {
            Debug.Log("右コントローラーのアクションを設定: " + fireActionRight.action.name);
            fireActionRight.action.performed += OnFireTorpedo;
            fireActionRight.action.Enable();
        }
        else
        {
            Debug.LogWarning("🚨 右コントローラーのfireActionRightが設定されていません！");
        }

        // 左コントローラーの接続とデバッグ
        if (fireActionLeft != null && fireActionLeft.action != null)
        {
            Debug.Log("左コントローラーのアクションを設定: " + fireActionLeft.action.name);
            fireActionLeft.action.performed += OnFireTorpedo;
            fireActionLeft.action.Enable();
        }
        else
        {
            Debug.LogWarning("🚨 左コントローラーのfireActionLeftが設定されていません！");
        }

        // その他の必要な参照もチェック
        if (cameraSwitcher == null)
        {
            Debug.LogError("🚨 SubmarineCameraControl (cameraSwitcher) が設定されていません！");
        }
        
        if (torpedoPrefab == null)
        {
            Debug.LogError("🚨 torpedoPrefabが設定されていません！");
        }
        
        if (firePoint == null)
        {
            Debug.LogError("🚨 firePointが設定されていません！");
        }

        Debug.Log("=== BulletFire 初期化完了 ===");
    }

    // 🚨 VR実機対応: 代替入力手段としてUpdateでの直接チェック
    void Update()
    {
        // enableDirectControllerInput が true の時のみ実行
        if (!enableDirectControllerInput) return;

        // XRコントローラーの直接的な入力チェック（Input Actionが動作しない場合の代替手段）
        if (UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool rightTrigger) && rightTrigger)
        {
            Debug.Log("🎯 右コントローラーのトリガーを直接検出！");
            TryFireTorpedo("右コントローラー(直接)");
        }
        
        if (UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool leftTrigger) && leftTrigger)
        {
            Debug.Log("🎯 左コントローラーのトリガーを直接検出！");
            TryFireTorpedo("左コントローラー(直接)");
        }
    }

    // 🚨 発射ロジックを共通化したメソッド
    private void TryFireTorpedo(string inputSource)
    {
        // cameraSwitcherの参照チェック
        if (cameraSwitcher == null)
        {
            Debug.LogError("🚨 cameraSwitcher が null です！Inspector で設定してください。");
            return;
        }
        
        Debug.Log($"{inputSource} からの発射要求 - 現在のカメラ状態 isPeriscopeView: {cameraSwitcher.isPeriscopeView}");

        // 潜望鏡視点の場合のみ、発射を許可
        if (cameraSwitcher.isPeriscopeView)
        {
            Debug.Log($"🎯 {inputSource} から潜望鏡視点で魚雷を発射！");

            if (torpedoPrefab != null && firePoint != null)
            {
                Debug.Log("魚雷プレハブと発射ポイントが設定済み - 魚雷生成開始");
                
                // 1. 弾の生成 (Instantiate)
                GameObject torpedo = Instantiate(torpedoPrefab, firePoint.position, firePoint.rotation);
                Debug.Log($"魚雷を生成: {torpedo.name} 位置: {firePoint.position}");

                // 2. 弾の発射 (AddForce)
                Rigidbody rb = torpedo.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 forceVector = firePoint.forward * fireForce;
                    rb.AddForce(forceVector, ForceMode.Impulse);
                    Debug.Log($"魚雷に力を加えました。力のベクトル: {forceVector} 強さ: {fireForce}");
                }
                else
                {
                    Debug.LogError("🚨 魚雷プレハブにRigidbodyがアタッチされていません！");
                }
            }
            else
            {
                Debug.LogError("🚨 torpedoPrefab または firePoint が設定されていません！");
            }
        }
        else
        {
            Debug.Log("❌ 潜望鏡視点ではありません。現在の視点では発射できません。");
        }
    }

    void OnDestroy() // このスクリプトを持つオブジェクトが破壊される直前に実行される
    {
        // ... (購読解除コードは省略) ...
        if (fireActionRight != null && fireActionRight.action != null)
        {
            fireActionRight.action.performed -= OnFireTorpedo;
        }
        if (fireActionLeft != null && fireActionLeft.action != null)
        {
            fireActionLeft.action.performed -= OnFireTorpedo;
        }
    }

    // ==========================================================
    // 💡 センサー班の仕事: 入力イベントを受け取り、発射ロジックを実行
    // ==========================================================

    // どちらのコントローラーのアクションにも接続されるメインのメソッド
    public void OnFireTorpedo(InputAction.CallbackContext context)
    {
        // 🚨 VR実機デバッグ用: 入力受信をログ出力
        Debug.Log("=== OnFireTorpedo メソッド呼び出し ===");
        Debug.Log("context.performed: " + context.performed);
        Debug.Log("入力デバイス: " + context.control.device.name);
        
        if (context.performed)
        {
            // 共通の発射メソッドを使用
            TryFireTorpedo($"Input Action ({context.control.device.name})");
        }
        else
        {
            Debug.Log("❌ context.performed が false - アクション未実行");
        }
    }
}



//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;
//using UnityEngine.InputSystem;

//public class BulletFire : MonoBehaviour // Unityのゲームオブジェクトにアタッチするための基本クラス
//{
//    // ==========================================================
//    // 外部連携用の変数（Inspectorで設定）
//    // ==========================================================

//    // 💡 右コントローラーのデジタルアクションを設定
//    public InputActionReference fireActionRight;

//    // 💡 左コントローラーのデジタルアクションを設定
//    public InputActionReference fireActionLeft;

//    // 発射する弾（球）のモデルを設定 (Assets/Prefabsからドラッグ)
//    public GameObject torpedoPrefab;

//    // 弾が生成されるシーン内の位置と方向を示すオブジェクトを設定 (Hierarchyからドラッグ)
//    public Transform firePoint;

//    // 発射時に弾に与える力の強さ (数値で設定)
//    public float fireForce = 50f;


//    // ==========================================================
//    // ライフサイクルメソッド: 入力アクションの接続と切断（イベントの購読）
//    // ==========================================================

//    void Start() // ゲーム開始時に一度だけ実行される
//    {
//        // 右コントローラーの接続
//        if (fireActionRight != null && fireActionRight.action != null)
//        {
//            fireActionRight.action.performed += OnFireTorpedo;
//            fireActionRight.action.Enable();
//        }

//        // 💡 左コントローラーの接続
//        if (fireActionLeft != null && fireActionLeft.action != null)
//        {
//            fireActionLeft.action.performed += OnFireTorpedo;
//            fireActionLeft.action.Enable();
//        }
//    }

//    void OnDestroy() // このスクリプトを持つオブジェクトが破壊される直前に実行される
//    {
//        // 右コントローラーの購読解除
//        if (fireActionRight != null && fireActionRight.action != null)
//        {
//            fireActionRight.action.performed -= OnFireTorpedo;
//        }

//        // 💡 左コントローラーの購読解除
//        if (fireActionLeft != null && fireActionLeft.action != null)
//        {
//            fireActionLeft.action.performed -= OnFireTorpedo;
//        }
//    }

//    // ==========================================================
//    // 💡 センサー班の仕事: 入力イベントを受け取り、発射ロジックを実行
//    // ==========================================================

//    // どちらのコントローラーのアクションにも接続されるメインのメソッド
//    public void OnFireTorpedo(InputAction.CallbackContext context)
//    {
//        if (context.performed && cameraSwitcher != null && cameraSwitcher.isPeriscopeView)
//        {
//            if (context.performed)
//            {
//                Debug.Log("発射ボタンが押されました！");

//                // --- 続くUnity班の仕事（球の発射ロジック） ---

//                if (torpedoPrefab != null && firePoint != null)
//                {
//                    // 1. 弾の生成 (Instantiate)
//                    GameObject torpedo = Instantiate(torpedoPrefab, firePoint.position, firePoint.rotation);

//                    // 2. 弾の発射 (AddForce)
//                    Rigidbody rb = torpedo.GetComponent<Rigidbody>();
//                    if (rb != null)
//                    {
//                        rb.AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
//                    }
//                }
//            }
//        }
//    }
//}

//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;
//using UnityEngine.InputSystem; // Input System (VRコントローラー入力) の機能を使うために必要

//public class BulletFire : MonoBehaviour // Unityのゲームオブジェクトにアタッチするための基本クラス
//{
//    // ==========================================================
//    // 外部連携用の変数（Inspectorで設定）
//    // ==========================================================

//    // 💡 どのVRコントローラーのデジタルアクション（Selectなど）に接続するかを設定
//    public InputActionReference fireAction;

//    // 発射する弾（球）のモデルを設定 (Assets/Prefabsからドラッグ)
//    public GameObject torpedoPrefab;

//    // 弾が生成されるシーン内の位置と方向を示すオブジェクトを設定 (Hierarchyからドラッグ)
//    public Transform firePoint;

//  // 発射時に弾に与える力の強さ (数値で設定)
//  public float fireForce = 50f;


//    // ==========================================================
//    // ライフサイクルメソッド: 入力アクションの接続と切断（イベントの購読）
//    // ==========================================================

//    void Start() // ゲーム開始時に一度だけ実行される
//    {
//        // アクション参照が設定され、かつアクション自体が有効かを確認
//        if (fireAction != null && fireAction.action != null)
//        {
//            // 💡 ボタンが押されてアクションが完了した（performed）瞬間に、
//            //    OnFireTorpedo メソッドを呼び出すようシステムに「予約（購読）」する
//            fireAction.action.performed += OnFireTorpedo;

//            // アクションを有効化する（Input Systemにこの入力を監視させる）
//            fireAction.action.Enable();
//        }
//    }

//    void OnDestroy() // このスクリプトを持つオブジェクトが破壊される直前に実行される
//    {
//        // 💡 プログラムが終了する際、予約（購読）を解除する
//        //    これを怠ると、メモリリークやエラーの原因になる可能性があるため、必須の処理
//        if (fireAction != null && fireAction.action != null)
//        {
//            fireAction.action.performed -= OnFireTorpedo;
//        }
//    }

//    // ==========================================================
//    // 💡 センサー班の仕事: 入力イベントを受け取り、発射ロジックを実行
//    // ==========================================================

//    // VRコントローラーのInput Actionに接続されるメインのメソッド
//    public void OnFireTorpedo(InputAction.CallbackContext context)
//    {
//        // context.performed はアクションが「実行された（ボタンが完全に押された）」状態を指す
//        if (context.performed)
//        {
//            // ここで「ボタンが押された」という情報を取得できています（センサー班の目的達成）
//            Debug.Log("発射ボタンが押されました！");

//            // --- 続くUnity班の仕事（球の発射ロジック） ---

//            // プレハブと発射位置が設定されているか確認
//            if (torpedoPrefab != null && firePoint != null)
//            {
//                // 1. 弾の生成 (Instantiate)
//                //    設定された TorpedoPrefab を firePoint の位置と回転でシーンに複製する
//                GameObject torpedo = Instantiate(torpedoPrefab, firePoint.position, firePoint.rotation);


//                // 2. 弾の発射 (AddForce)
//                Rigidbody rb = torpedo.GetComponent<Rigidbody>();
//                if (rb != null)
//                {
//                    // FirePointの正面方向（Transform.forward）に、
//                    // FireForceで設定した強さの力を瞬間的に加える (ForceMode.Impulse)
//                    rb.AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
//                }

//            }
//        }
//    }
//}