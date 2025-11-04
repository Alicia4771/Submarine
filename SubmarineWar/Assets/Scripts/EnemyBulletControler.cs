using UnityEngine;
using System.Collections.Generic;

public class EnemyBulletControler : MonoBehaviour
{

  // public static int enemyShipNumber = 0;
  // public GameObject enemyShipPrefab; // 敵船プレハブの用意

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  // オブジェクト衝突時に呼び出される関数
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("SubmarineBody"))
    {
      Debug.Log("敵の攻撃が潜水艦に衝突しました");
      // 残り時間を10秒減らす
      float currentTime = DataManager.GetTimeLimit();
      if (currentTime > 0f)
      {
        DataManager.SetTimeLimit(currentTime - 10f);
      }
      DeleteObject(gameObject, gameObject.name);
    }
  }
  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("EnemyShip"))
    {
      // Debug.Log("敵戦のたまが敵船（自分自身）に衝突しています");
      // 弾の発射時に自分自身と衝突して消えるのを防ぐ
    }
    else if (collision.gameObject.CompareTag("EnemyBullet"))
    {
      Debug.Log("敵戦のたま同士が衝突しています");
      // 衝突相手も削除
      DeleteObject(gameObject, gameObject.name);
      DeleteObject(collision.gameObject, collision.gameObject.name);
    }
    else
    {
      Debug.Log("壁に衝突しました。");
      DeleteObject(gameObject, gameObject.name);
    }
  }

  private void DeleteObject(GameObject deleteObject, string name){
    // オブジェクトの消去
    Destroy(deleteObject);
    // リストから消去
    DataManager.DeleteTorpedo(name);
  }
}

