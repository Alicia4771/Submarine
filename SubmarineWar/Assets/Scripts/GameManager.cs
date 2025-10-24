using UnityEngine;

public class GameManager : MonoBehaviour
{
  // 敵船のプレハブをInspectorからアサイン
  public GameObject enemyShipPrefab;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    Debug.Log("ゲーム開始");

    // 敵船を出現させる（３つ）
    // １つ目
    Vector3 spawnPosition_1 = new Vector3(0, 0, 0); // 設置する座標
    GameObject EnemyShip_1 = Instantiate(enemyShipPrefab, spawnPosition_1, Quaternion.identity); // プレハブの作成
    EnemyShip_1.name = "EnemyShip_1"; // 名前の決定
    // ２つ目
    Vector3 spawnPosition_2 = new Vector3(0, 0, 10); // 設置する座標
    GameObject EnemyShip_2 = Instantiate(enemyShipPrefab, spawnPosition_2, Quaternion.identity); // プレハブの作成
    EnemyShip_2.name = "EnemyShip_2"; // 名前の決定
    // ３つ目
    Vector3 spawnPosition_3 = new Vector3(0, 0, 20); // 設置する座標
    GameObject EnemyShip_3 = Instantiate(enemyShipPrefab, spawnPosition_3, Quaternion.identity); // プレハブの作成
    EnemyShip_3.name = "EnemyShip_3"; // 名前の決定

    // DataManagerの敵船リストに追加
    DataManager.AddEnemyShip("EnemyShip_1");
    DataManager.AddEnemyShip("EnemyShip_2");
    DataManager.AddEnemyShip("EnemyShip_3");
  }

  // Update is called once per frame
  void Update()
  {
  }
}
