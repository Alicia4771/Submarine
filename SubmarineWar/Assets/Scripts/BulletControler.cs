using UnityEngine;

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
    if (collision.gameObject.CompareTag("EnemyShip"))
    {
      Debug.Log("EnemyShipに衝突しました。");
    }
    else
    {
      Debug.Log("壁に衝突しました。");
    }
    // 弾の消去
    Destroy(gameObject);
  }
}
