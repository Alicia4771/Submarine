using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    // [SerializeField] を付けると、privateでもインスペクターに表示される
    [SerializeField]
    private TextMeshProUGUI timeTextComponent;
    [SerializeField]
    private TextMeshProUGUI speedTextComponent;
    [SerializeField]
    private TextMeshProUGUI depthTextComponent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      // 1. DataManagerから残り時間、スピード、深度を取得します
      float remainingTime = DataManager.GetTimeLimit();
      float currentSpeed = DataManager.GetSubmarineSpeed();
      float currentDepth = DataManager.GetSubmarineDepth();

      if (remainingTime < 0f)
      {
          remainingTime = 0f; // 残り時間がマイナスにならないようにする
      }

      currentSpeed *= 3000f / 50f; // 単位変換: Unityの単位(m/s) -> ゲーム内表示の単位(m/s)
      // 2. 取得したfloat型の値を、string型（文字列）に変換します
      // "F2" は小数点以下2桁まで表示するフォーマット指定子です
      string timeString = remainingTime.ToString("F2");
      string speedString = currentSpeed.ToString("F2");
      string depthString = currentDepth.ToString("F2");

      // 3. Textコンポーネントの .text プロパティに代入します
      if (timeTextComponent != null)
      {
          // 例: "Time: 98.50" のように表示する
          timeTextComponent.text = "TIME\n" + timeString + " s";
      }

      if (speedTextComponent != null)
      {
          speedTextComponent.text = "SPEED\n" + speedString + " Knot";
      }

      if (depthTextComponent != null)
      {
          depthTextComponent.text = "DEPTH\n" + depthString + " m";
      }

    }
}
