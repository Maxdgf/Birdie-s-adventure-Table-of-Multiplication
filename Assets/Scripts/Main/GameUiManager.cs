using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    [SerializeField] private GameObject answersPanel;
    [SerializeField] private float answersPanelMoveSpeed;

    private RectTransform answersPanelTransform;
    private GameObject blockAnswersPanel;

    void Start()
    {
        answersPanelTransform = answersPanel.GetComponent<RectTransform>();

        /*
         * Block panel prevents unnecessary button presses 
         * when the answer panel
         * slides smoothly.
         */
        // get block panel object
        Transform blockAnswersPanelTransform = answersPanel.transform.Find("Block Panel");
        blockAnswersPanel = blockAnswersPanelTransform.gameObject;
    }


    /// <summary>
    /// Hides answers panel to down.
    /// </summary>
    /// <param name="direction">Move direction state. If true, positive else negative.</param>
    public void MoveAnswersPanel(bool direction)
    {
        // set active block panel
        blockAnswersPanel.SetActive(true);

        // move answers panel by direction state, if direction is true - move up, else, down
        if (direction)
            answersPanelTransform.anchoredPosition = Vector2.Lerp(
                answersPanelTransform.anchoredPosition,
                new Vector2(0f, 1924f),
                answersPanelMoveSpeed * Time.deltaTime
            );
        else
            answersPanelTransform.anchoredPosition = Vector2.Lerp(
                answersPanelTransform.anchoredPosition,
                new Vector2(0f, 1179f),
                answersPanelMoveSpeed * Time.deltaTime
            );
    }
}
