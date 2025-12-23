using UnityEngine;
using System.Collections.Generic;

public class BulletControler : MonoBehaviour
{

  public static int enemyShipNumber = 0;
  public GameObject enemyShipPrefab; // 敵船プレハブの用意

  // 敵船出現範囲（直接コードで指定）
  private float spawnXMin = -200f;
  private float spawnXMax = 200f;
  private float spawnZMin = -200f;
  private float spawnZMax = 200f;
  private float spawnY = -5f;

  // サウンド用
  public SoundSpeaker soundSpeaker;

  // 爆発エフェクト
  public GameObject explosionPrefab; // 爆発エフェクト

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
    {
      if (soundSpeaker == null)
        {
            // Unityのバージョンによってどちらか片方が使えます
            // 新しいUnity (2023以降):
            soundSpeaker = FindAnyObjectByType<SoundSpeaker>();
            
            // もしエラーが出るなら古い書き方 (2022以前):
            // soundSpeaker = FindObjectOfType<SoundSpeaker>();
        }
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
      // 魚雷を命中させた音
      soundSpeaker.PlayEnemyDestroyed();
      Debug.Log("魚雷を命中させました。音が鳴っているはずです。");
      // 爆発エフェクトの生成
        if (explosionPrefab != null)
        {
            Debug.Log("魚雷エフェクト発生");
            // 衝突した場所にエフェクトを出す
            GameObject effect = Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);
            // 💡 追加箇所: ここでサイズを大きくする（例：元の5倍にする）
            effect.transform.localScale = new Vector3(10f, 15f, 10f);
            
            // 3秒後に消す（出しっ放し防止）
            Destroy(effect, 10.0f);
        }
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
      float randX = Random.Range(spawnXMin, spawnXMax);
      float randZ = Random.Range(spawnZMin, spawnZMax);
      Vector3 spawnPosition = new Vector3(randX, spawnY, randZ); // ランダム座標
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
      // 弾の消去
      Destroy(gameObject);
    }
    else if (collision.gameObject.CompareTag("SubmarineBody"))
    {
      Debug.Log("自分の潜水艦にあたっています（問題なし）");
    }
    // 壁に衝突した時
    else if (collision.gameObject.CompareTag("StageWall"))
    {
      Debug.Log("壁に衝突しました。");
      // 弾の消去
      Destroy(gameObject);
    }
    // 敵の魚雷に衝突した時
    else if (collision.gameObject.CompareTag("EnemyBullet"))
    {
      Debug.Log("敵魚雷に衝突しました。");
      // 弾の消去
      Destroy(gameObject);
    }// 自分の魚雷に衝突した時
    else if (collision.gameObject.CompareTag("MyBullet"))
    {
      Debug.Log("自分の魚雷に衝突しました。");
      // 弾の消去
      Destroy(gameObject);
    }
    
  }
}
