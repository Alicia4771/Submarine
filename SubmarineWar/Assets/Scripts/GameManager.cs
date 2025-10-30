using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // 敵船のプレハブをInspectorからアサイン
    public GameObject enemyShipPrefab;
    // シーン遷移用のSceneLoader
    public SceneLoader sceneLoader;

  　// Start is called once before the first execution of Update after the MonoBehaviour is created
  　void Start()
  　{
        Debug.Log("ゲーム開始");

    　   // DataManagerを初期化
    　   Debug.Log("初期化します");
        DataManager.Initialize();

        // 敵船を出現させる（３つ）
        // １つ目
        Vector3 spawnPosition_1 = new Vector3(0, 0, 0); // 設置する座標
        GameObject EnemyShip_1 = Instantiate(enemyShipPrefab, spawnPosition_1, Quaternion.identity); // プレハブの作成
        BulletControler.enemyShipNumber += 1;
        EnemyShip_1.name = "EnemyShip_"+(BulletControler.enemyShipNumber).ToString(); // 名前の決定
        // ２つ目
        Vector3 spawnPosition_2 = new Vector3(0, 0, 10); // 設置する座標
        GameObject EnemyShip_2 = Instantiate(enemyShipPrefab, spawnPosition_2, Quaternion.identity); // プレハブの作成
        BulletControler.enemyShipNumber += 1;
        EnemyShip_2.name = "EnemyShip_"+(BulletControler.enemyShipNumber).ToString(); // 名前の決定
        // ３つ目
        Vector3 spawnPosition_3 = new Vector3(0, 0, 20); // 設置する座標
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
      Debug.Log(DataManager.GetTimeLimit());
      // 残り時間が0以下ならば、終了シーンに遷移
      if(DataManager.GetTimeLimit() <= 0)
      {
          Debug.Log("ゲーム終了。シーン遷移を行う。");
          if (sceneLoader != null)
          {
              sceneLoader.LoadScene("EndScene"); // 遷移先シーン名は適宜変更
          }
          else
          {
              Debug.LogError("SceneLoaderがアサインされていません。");
          }
        }
    }
}
