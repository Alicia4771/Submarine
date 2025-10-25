using UnityEngine;
using System.Collections.Generic;

public class BulletControler : MonoBehaviour
{
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
      // // 確認のため、敵船リストを表示
      // List<string> enemyShips = DataManager.GetEnemyShipList();
      // for (int i = 0; i < enemyShips.Count; i++)
      // {
      //   Debug.Log(enemyShips[i]);
      // }
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
