using UnityEngine;
using System.Collections.Generic;

public class BulletControler : MonoBehaviour
{

  public static int enemyShipNumber = 0;
  public GameObject enemyShipPrefab; // 敵船プレハブの用意

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
    {
        
    }

  // Update is called once per frame
  void Update()
  {

  }
    
  // オブジェクト衝突時に呼び出される関数
  private void OnCollisionEnter(Collision collision)
  {
    // EnemyShipに衝突した時
    if (collision.gameObject.CompareTag("EnemyShip"))
    {
      string enemyName = collision.gameObject.name;
      Debug.Log($"EnemyShipに衝突しました。名前: {enemyName}");
      // スコアを加算
      DataManager.AddScore(100);
      // スコアを出力
      Debug.Log($"現在のスコア:{DataManager.GetScore()}");
      // EnemyShipオブジェクトの削除
      Destroy(collision.gameObject);
      DataManager.DeleteEnemyShip(enemyName);
      // 新たにEnemyShipオブジェクトを作成
      Vector3 spawnPosition = new Vector3(0, 0, 0); // 設置する座標
      GameObject EnemyShip = Instantiate(enemyShipPrefab, spawnPosition, Quaternion.identity); // プレハブの作成
      enemyShipNumber += 1;
      EnemyShip.name = "EnemyShip_" + enemyShipNumber.ToString(); // 名前の決定
      DataManager.AddEnemyShip(EnemyShip.name);

      // 確認のため、敵船リストを表示
      List<string> enemyShips = DataManager.GetEnemyShipList();
      for (int i = 0; i < enemyShips.Count; i++)
      {
        Debug.Log(enemyShips[i]);
      }
    }
    // 壁に衝突した時
    else
    {
      Debug.Log("壁に衝突しました。");
    }
    // 弾の消去
    Destroy(gameObject);
  }
}
