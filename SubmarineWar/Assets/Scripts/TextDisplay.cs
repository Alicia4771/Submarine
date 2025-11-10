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


      // 2. 取得したfloat型の値を、string型（文字列）に変換します
      // "F2" は小数点以下2桁まで表示するフォーマット指定子です
      string timeString = remainingTime.ToString("F2");
      string speedString = currentSpeed.ToString("F2");
      string depthString = currentDepth.ToString("F2");

      // 3. Textコンポーネントの .text プロパティに代入します
      if (timeTextComponent != null)
      {
          // 例: "Time: 98.50" のように表示する
          timeTextComponent.text = "TIME: " + timeString;
      }

      if (speedTextComponent != null)
      {
          speedTextComponent.text = "SPEED: " + speedString;
      }

      if (depthTextComponent != null)
      {
          depthTextComponent.text = "DEPTH: " + depthString;
      }

    }
}
