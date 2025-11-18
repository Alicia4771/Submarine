using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ScoreComponent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int finalScore = DataManager.GetScore();
        Debug.Log("Final Score: " + finalScore.ToString());

        // 3. Textコンポーネントの .text プロパティに代入します
        if (ScoreComponent != null)
        {
            ScoreComponent.text = "Score\n" + finalScore.ToString() + " pt";
        }
    }
}
