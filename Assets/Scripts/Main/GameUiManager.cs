using System.Collections;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    [SerializeField] private GameObject answersPanel, pauseButton;
    [SerializeField] private float answersPanelMoveSpeed, pauseButtonMoveSpeed;

    private RectTransform answersPanelTransform, pauseButtonTransform;
    private GameObject blockAnswersPanel;

    void Start()
    {
        answersPanelTransform = answersPanel.GetComponent<RectTransform>();
        pauseButtonTransform = pauseButton.GetComponent<RectTransform>();

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
        // move answers panel by direction state, if direction is true - move up, else, down
        answersPanelTransform.anchoredPosition = Vector2.Lerp(
            answersPanelTransform.anchoredPosition,
            direction ? new Vector2(0f, 1924f) : new Vector2(0f, 1179f),
            answersPanelMoveSpeed * Time.deltaTime
        );
    }

    /// <summary>
    /// Hides pause button to right.
    /// </summary>
    public void HidePauseButton()
    {
        pauseButtonTransform.anchoredPosition = Vector2.Lerp(
            pauseButtonTransform.anchoredPosition,
            new Vector2(260f, -208f),
            pauseButtonMoveSpeed * Time.deltaTime
        );
    }

    /// <summary>
    /// Enables and then disables block panel after delay.
    /// </summary>
    /// <param name="delay">Delay.</param>
    public IEnumerator DisableBlockPanelAfterDelay(float delay)
    {
        blockAnswersPanel.SetActive(true);
        yield return new WaitForSeconds(delay);
        blockAnswersPanel.SetActive(false);
    }
}
