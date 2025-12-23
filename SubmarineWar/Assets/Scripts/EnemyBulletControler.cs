using UnityEngine;
using System.Collections.Generic;

public class EnemyBulletControler : MonoBehaviour
{

  // サウンド用
  public SoundSpeaker soundSpeaker;

  // public static int enemyShipNumber = 0;
  // public GameObject enemyShipPrefab; // 敵船プレハブの用意

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
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("SubmarineBody"))
    {
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
    else if (collision.gameObject.CompareTag("SubmarineBody"))
    {
       // 敵の魚雷の命中音
      soundSpeaker.PlayDamaged();
      Debug.Log("魚雷を打ち込まれました。音が鳴っているはずです。");
      Debug.Log("敵の攻撃が潜水艦に衝突しました");
      Debug.Log("敵の攻撃が潜水艦に衝突しました");
      // 残り時間を10秒減らす
      float currentTime = DataManager.GetTimeLimit();
      if (currentTime > 0f)
      {
        DataManager.SetTimeLimit(currentTime - 10f);
      }
      DeleteObject(gameObject, gameObject.name);
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

