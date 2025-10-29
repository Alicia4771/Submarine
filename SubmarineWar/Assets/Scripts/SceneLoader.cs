using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理に必要

// シーン遷移だけを担当するシンプルなクラス
public class SceneLoader : MonoBehaviour
{
  // Inspectorのイベントから呼び出せるように public にします
  // 引数(sceneName)で、どのシーンに飛ぶかを指定できるようにします
  public void LoadScene(string sceneName)
  {
    Debug.Log("LoadScene メソッドが呼び出されました。");
    if (string.IsNullOrEmpty(sceneName))
    {
      Debug.LogError("読み込むシーン名が指定されていません。");
      return;
    }

    Debug.Log($"シーン {sceneName} を読み込みます。");

    // VRではフリーズを防ぐため、非同期ロード(LoadSceneAsync)が推奨されます
    SceneManager.LoadSceneAsync(sceneName);
  }
  
  public void printLog()
  {
    Debug.Log("SceneLoaderのprintLogメソッドが呼び出されました。");
  }
}