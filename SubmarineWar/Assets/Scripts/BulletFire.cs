using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem; // Input System (VRコントローラー入力) の機能を使うために必要

public class BulletFire : MonoBehaviour // Unityのゲームオブジェクトにアタッチするための基本クラス
{
    // ==========================================================
    // 外部連携用の変数（Inspectorで設定）
    // ==========================================================

    // 💡 どのVRコントローラーのデジタルアクション（Selectなど）に接続するかを設定
    public InputActionReference fireAction;

    // 発射する弾（球）のモデルを設定 (Assets/Prefabsからドラッグ)
    public GameObject torpedoPrefab;

    // 弾が生成されるシーン内の位置と方向を示すオブジェクトを設定 (Hierarchyからドラッグ)
    public Transform firePoint;

  // 発射時に弾に与える力の強さ (数値で設定)
  public float fireForce = 50f;
    

    // ==========================================================
    // ライフサイクルメソッド: 入力アクションの接続と切断（イベントの購読）
    // ==========================================================

    void Start() // ゲーム開始時に一度だけ実行される
    {
        // アクション参照が設定され、かつアクション自体が有効かを確認
        if (fireAction != null && fireAction.action != null)
        {
            // 💡 ボタンが押されてアクションが完了した（performed）瞬間に、
            //    OnFireTorpedo メソッドを呼び出すようシステムに「予約（購読）」する
            fireAction.action.performed += OnFireTorpedo;

            // アクションを有効化する（Input Systemにこの入力を監視させる）
            fireAction.action.Enable();
        }
    }

    void OnDestroy() // このスクリプトを持つオブジェクトが破壊される直前に実行される
    {
        // 💡 プログラムが終了する際、予約（購読）を解除する
        //    これを怠ると、メモリリークやエラーの原因になる可能性があるため、必須の処理
        if (fireAction != null && fireAction.action != null)
        {
            fireAction.action.performed -= OnFireTorpedo;
        }
    }

    // ==========================================================
    // 💡 センサー班の仕事: 入力イベントを受け取り、発射ロジックを実行
    // ==========================================================

    // VRコントローラーのInput Actionに接続されるメインのメソッド
    public void OnFireTorpedo(InputAction.CallbackContext context)
    {
        // context.performed はアクションが「実行された（ボタンが完全に押された）」状態を指す
        if (context.performed)
        {
            // ここで「ボタンが押された」という情報を取得できています（センサー班の目的達成）
            Debug.Log("発射ボタンが押されました！");

            // --- 続くUnity班の仕事（球の発射ロジック） ---

            // プレハブと発射位置が設定されているか確認
            if (torpedoPrefab != null && firePoint != null)
            {
                // 1. 弾の生成 (Instantiate)
                //    設定された TorpedoPrefab を firePoint の位置と回転でシーンに複製する
                GameObject torpedo = Instantiate(torpedoPrefab, firePoint.position, firePoint.rotation);
                

                // 2. 弾の発射 (AddForce)
                Rigidbody rb = torpedo.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // FirePointの正面方向（Transform.forward）に、
                    // FireForceで設定した強さの力を瞬間的に加える (ForceMode.Impulse)
                    rb.AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
                }
                
            }
        }
    }
}