using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 敵船のプレハブをInspectorからアサイン
    public GameObject enemyShipPrefab;
    // シーン遷移用のSceneLoader
    public SceneLoader sceneLoader;
    
    // インスペクターで NotificationManager をアタッチする
    [SerializeField]
    private NotificationManager notificationManager;

  　// Start is called once before the first execution of Update after the MonoBehaviour is created
  　void Start()
  　{
        Debug.Log("ゲーム開始");
        
        if (notificationManager != null)
          {
              // 5秒間だけメッセージを表示する
              notificationManager.ShowMessage("Game start. Seek and destroy the enemy ships!", 7.0f);
          }

    　   // DataManagerを初期化
    　   Debug.Log("初期化します");
        DataManager.Initialize();

        // 敵船を出現させる（３つ）
        // １つ目
        Vector3 spawnPosition_1 = new Vector3(0, -5, 0); // 設置する座標
        GameObject EnemyShip_1 = Instantiate(enemyShipPrefab, spawnPosition_1, Quaternion.identity); // プレハブの作成
        BulletControler.enemyShipNumber += 1;
        EnemyShip_1.name = "EnemyShip_"+(BulletControler.enemyShipNumber).ToString(); // 名前の決定
        // ２つ目
        Vector3 spawnPosition_2 = new Vector3(0, -5, 10); // 設置する座標
        GameObject EnemyShip_2 = Instantiate(enemyShipPrefab, spawnPosition_2, Quaternion.identity); // プレハブの作成
        BulletControler.enemyShipNumber += 1;
        EnemyShip_2.name = "EnemyShip_"+(BulletControler.enemyShipNumber).ToString(); // 名前の決定
        // ３つ目
        Vector3 spawnPosition_3 = new Vector3(0, -5, 20); // 設置する座標
        GameObject EnemyShip_3 = Instantiate(enemyShipPrefab, spawnPosition_3, Quaternion.identity); // プレハブの作成
        BulletControler.enemyShipNumber += 1;
        EnemyShip_3.name = "EnemyShip_"+(BulletControler.enemyShipNumber).ToString(); // 名前の決定

        // DataManagerの敵船リストに追加
        DataManager.AddEnemyShip(EnemyShip_1.name);
        DataManager.AddEnemyShip(EnemyShip_2.name);
        DataManager.AddEnemyShip(EnemyShip_3.name);

        // 確認のため、敵船リストを表示
        List<string> enemyShips = DataManager.GetEnemyShipList();
        for (int i = 0; i < enemyShips.Count; i++)
        {
            Debug.Log(enemyShips[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
      // 残り時間を減らす
      float currentTime = DataManager.GetTimeLimit();
      if (currentTime > 0f)
      {
        DataManager.SetTimeLimit(currentTime - Time.deltaTime);
      }
      // Debug.Log("残り時間"+DataManager.GetTimeLimit());
      // 残り時間が0以下ならば、終了シーンに遷移
      if(DataManager.GetTimeLimit() <= 0)
      {
          Debug.Log("ゲーム終了。シーン遷移を行う。");
          if (sceneLoader != null)
          {
              // 10秒間だけメッセージを表示する
              notificationManager.ShowMessage("Mission complete. Moving to results.", 10.0f);
              // sceneLoader.LoadScene("EndScene");
              StartCoroutine(WaitAndLoadEndScene());
          }
          else
          {
              Debug.LogError("SceneLoaderがアサインされていません。");
          }
        return;
      }
    }
    private System.Collections.IEnumerator WaitAndLoadEndScene()
    {
      yield return new WaitForSeconds(10f);
      SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
    }

    }

