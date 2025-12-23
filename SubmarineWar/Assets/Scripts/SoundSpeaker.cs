using UnityEngine;

public class SoundSpeaker : MonoBehaviour
{

    [Header("音源ファイルの登録")]
    public AudioClip enemyDestroySound; // 敵が壊れる音 (Sound1)
    public AudioClip torpedoFireSound;  // 魚雷発射音 (Sound2)
    public AudioClip damegedSound;  // 敵船からの攻撃を受けた音 (Sound3)
    public AudioClip backgroundMusic1;   // 背景音楽1 (Sound4-1)
    public AudioClip backgroundMusic2;   // 背景音楽2 (Sound4-2)
    public AudioClip backgroundMusic3;   // 背景音楽3 (Sound4-3)
    public AudioClip switchSound;     // 潜望鏡を覗いた時の音 (Sound5)

    private AudioSource audioSource1;
    private AudioSource audioSource2;

    void Awake()
    {
        // AudioSourceコンポーネントを取得
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();
    }

    // --- ここからユーザーが定義したい関数 ---

    // Sound1: 敵船が壊れる音
    public void PlayEnemyDestroyed()
    {
        // PlayOneShotは、音が重なっても途切れずに再生されます（爆発音などに最適）
        audioSource1.PlayOneShot(enemyDestroySound);
    }

    // Sound2: 自分が魚雷を発射した音
    public void PlayTorpedoFire()
    {
        audioSource1.PlayOneShot(torpedoFireSound);
    }

    // Sound3: 敵船からの攻撃を受けた音
    public void PlayDamaged()
    {
        audioSource1.PlayOneShot(damegedSound);
    }

    // Sound4-1: 背景音楽の再生
    public void PlayBackgroundMusic1()
    {
        audioSource1.clip = backgroundMusic1;
        audioSource1.loop = true; // ループ再生を有効にする
        audioSource1.Play();
    }

    // Sound4-2: 背景音楽の再生
    public void PlayBackgroundMusic2()
    {
        audioSource2.clip = backgroundMusic2;
        audioSource2.loop = true; // ループ再生を有効にする
        audioSource2.Play();
    }

    // Sound4-3: 背景音楽の再生
    public void PlayBackgroundMusic3()
    {
        audioSource1.clip = backgroundMusic3;
        audioSource1.loop = true; // ループ再生を有効にする
        audioSource1.Play();
        audioSource2.Stop(); //backgroundMusic2を停止
    }

    // Sound5: 潜望鏡を覗いた時の音
    public void PlaySwitchSound()
    {
        audioSource1.PlayOneShot(switchSound);
    }
}