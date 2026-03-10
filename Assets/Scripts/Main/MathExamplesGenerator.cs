/*
 * Description
 * ----------------------------------------------------------------------------
 * This script generates multiplication math examples an manages game scene ui.
 * 
 * + Additional
 * + ----------------------------------------------------------------------
 * + Math examples generator adapted to work
 * + on a normal game level and in a boss fight, 
 * + some actions are performed on normal levels, and some in a boss fight.
 * 
 * ! Notes
 * ! -----------
 * ! # `isBossFightEnabled` bool needed to switch generator 
 * ! # state from normal mode to boss fight mode.
 * ! 
 * ! # In normal mode, examples are generated for a specific number 
 * ! # from the multiplication table (1...10) and with a multiplier 
 * ! # up to 10 (1...10), and in boss fight mode, 
 * ! # examples are generated randomly.
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MathExamplesGenerator : MonoBehaviour
{
    private const int MAX_ANSWER_NUM = 81; // max answer num
    private const float CORRECTHNESS_SYGNAL_DURATION = 0.3f; // answer correcthness sound signal duration
    private const float ANIMATION_PLAY_DIFFERENCE_FACTOR = 0.1f; // answer button text animation play difference factor

    [SerializeField] private bool isBossFightEnabled;
    [SerializeField] [Tooltip("Tmp text which displays math multiplication example.")] private TMP_Text exampleView; // example view
    [SerializeField] [Tooltip("Multiplication example answers buttons.")] private Button[] answerButtons; // answer buttons group
    [SerializeField] [Tooltip("Sound effects source.")] private AudioSource audioSource; // for sound effects
    [SerializeField] [Tooltip("Player object and ui game objects.")] private GameObject playerCharacter, exampleViewPanel, exampleAnwserCorrectionIcon, playerDataServer, boss; // important gameobjects
    [SerializeField] [Tooltip("Sound effects clips.")] private AudioClip wrongAnswer, correctAnswer; // wrong, correct answer sound effects
    [SerializeField] [Tooltip("Ui animations names.")] private string answerButtonsCaptionAnimation, exampleViewAnimation; // ui animations
    [SerializeField] [Tooltip("Multiplication example correcthness icon sprites.")] private Sprite correctIcon, incorrectIcon; // correct, wrong example answer icon

    private int firstNumber, currentMathExampleResult, playerScore, secondNumber;
    private AudioPlayer audioPlayer;
    private PlayerController playerController;
    private AnimationManager animationManager;
    private Animator exampleViewAnimator;
    private PlayerDataServer dataServer;
    private BossFightDataServer bossFightDataServer;
    private playerFightController playerFightController;
    private BossAI bossAI;

    void Start()
    {
        // set up audio player
        audioPlayer = gameObject.AddComponent<AudioPlayer>();
        audioPlayer.SetAudioSource(audioSource);

        // add or get required components for player interaction, animations 
        animationManager = gameObject.AddComponent<AnimationManager>();
        exampleViewAnimator = exampleView.GetComponent<Animator>();

        if (isBossFightEnabled)
        {
            firstNumber = Random.Range(0, 10);
            secondNumber = Random.Range(0, 10);

            playerFightController = playerCharacter.GetComponent<playerFightController>();
            bossFightDataServer = playerDataServer.GetComponent<BossFightDataServer>();
            bossAI = boss.GetComponent<BossAI>();
        }
        else
        {
            string data = PlayerPrefsManager.ExtractValueFromStringPref("SELECTED_LEVEL");
            LevelData levelData = JsonUtility.FromJson<LevelData>(data);

            firstNumber = levelData.targetNum;
            secondNumber = 0;

            playerController = playerCharacter.GetComponent<PlayerController>();
            dataServer = playerDataServer.GetComponent<PlayerDataServer>();
        }

        foreach (Button button in answerButtons)
            button.onClick.AddListener(
                delegate { 
                    OnClickAnswerButtonFunction(button.name); 
                }
            );

        GenerateMathExample();
        SetTextToAnswerButtons(answerButtons);
    }

    /// <summary>
    /// Finds TMP text in TMP button with specific name.
    /// </summary>
    /// <param name="buttonName">Button name.</param>
    /// <returns>TMP text object.</returns>
    private TMP_Text GetTmpTextFromButton(string buttonName)
    {
        GameObject textObject = GameObject.Find($"{buttonName}/caption");
        return textObject.GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Sets text to answer buttons.
    /// </summary>
    /// <param name="buttons">Buttons group.</param>
    private void SetTextToAnswerButtons(Button[] buttons)
    {
        int counter = 0;
        float animationDelay = 0f;
        int randomAnswerPosition = Random.Range(0, buttons.Length);
        List<int> answers = new List<int>();

        DisableButtons(); // disable buttons

        foreach (Button button in buttons)
        {
            Image buttonImage = button.GetComponent<Image>();
            buttonImage.color = new Color(1f, 1f, 1f, 1f);
            TMP_Text textView = GetTmpTextFromButton(button.name);
            
            // play text animation in normal game level, not bossfight
            if (!isBossFightEnabled)
            {
                animationDelay += ANIMATION_PLAY_DIFFERENCE_FACTOR;
                Animator textAnimator = textView.GetComponent<Animator>();
                StartCoroutine(animationManager.PlayAnimationAfterDelay(textAnimator, answerButtonsCaptionAnimation, animationDelay, false));
            }

            int numToSet = counter == randomAnswerPosition ? currentMathExampleResult : Random.Range(1, MAX_ANSWER_NUM);
            textView.text = numToSet.ToString();

            counter++;
            button.enabled = true; // make button enabled after disabled
        }
    }

    /// <summary>
    /// Sets onclick function to answer button.
    /// </summary>
    /// <param name="buttonName">Button name.</param>
    private void OnClickAnswerButtonFunction(string buttonName)
    {
        DisableButtons();
        if (!isBossFightEnabled) StartCoroutine(playerController.MoveWings());

        TMP_Text textView = GetTmpTextFromButton(buttonName);

        string value = textView.text;
        int answer = int.Parse(value);

        // answer check
        if (answer == currentMathExampleResult)
        {
            if (isBossFightEnabled)
            {
                playerFightController.PrepareToAttack();
                bossFightDataServer.ManagePlayerAttackState(true);
                GenerateMathExample(); // generate next example
                SetTextToAnswerButtons(answerButtons); // set answers to buttons
            }
            else
            {
                playerScore++;
                dataServer.UpdateScore(playerScore);
                playerController.AddYForceToPlayer(true);
                StartCoroutine(SignalizeExampleCorrecthness(true, value));
            }

            audioPlayer.PlayAudio(correctAnswer);
        }
        else
        {
            if (isBossFightEnabled)
            {
                bossAI.PrepareToAttack();
                bossFightDataServer.ManageBossAttackState(true);
                GenerateMathExample(); // generate next example
                SetTextToAnswerButtons(answerButtons); // set answers to buttons
            } else
                StartCoroutine(SignalizeExampleCorrecthness(false, value));

            audioPlayer.PlayAudio(wrongAnswer);
            Handheld.Vibrate();

        }
    }

    /// <summary>
    /// Generates a math example.
    /// </summary>
    private void GenerateMathExample()
    {
        if (isBossFightEnabled) // bossfight game
        {
            if (bossFightDataServer.bossHealth > 0)
            {
                firstNumber = Random.Range(0, 10);
                secondNumber = Random.Range(0, 10);
            } 
            else return;
        }
        else // normal game
        {
            if (secondNumber < Constants.EXAMPLES_COUNT)
            {
                secondNumber++;
            }
            else
            {
                dataServer.UpdateGameEndedState();
                return;
            }
        }

        string resultExample = string.Format("{0} x {1} = ?", firstNumber, secondNumber); // format string math example

        ExpressionEvaluator.Evaluate($"{firstNumber} * {secondNumber}", out currentMathExampleResult); // evaluate
        exampleView.text = resultExample;

        animationManager.PlayAnimation(exampleViewAnimator, exampleViewAnimation, false);
    }

    /// <summary>
    /// Disables answer buttons.
    /// </summary>
    private void DisableButtons()
    {
        foreach (Button button in answerButtons)
        {
            button.enabled = false; // disable button

            Image buttonImage = button.GetComponent<Image>();
            buttonImage.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    /// <summary>
    /// Signals the correctness of answer to current example and generates next example.
    /// </summary>
    /// <param name="isCorrect">Correct or not correct.</param>
    private IEnumerator SignalizeExampleCorrecthness(bool isCorrect, string answer)
    {
        int exampleStringLength = exampleView.text.Length;

        exampleView.text = exampleView.text.Remove(exampleStringLength - 1, 1) + answer;
        exampleAnwserCorrectionIcon.SetActive(true);

        Image image = exampleAnwserCorrectionIcon.GetComponent<Image>();
        Image exampleImage = exampleViewPanel.GetComponent<Image>();

        Color startColor = exampleImage.color;
        image.sprite = isCorrect ? correctIcon : incorrectIcon; // correct or incorrect icon
        exampleImage.color = isCorrect ? Color.green : Color.red; // green or red color

        yield return new WaitForSeconds(CORRECTHNESS_SYGNAL_DURATION);
        exampleImage.color = startColor;

        exampleAnwserCorrectionIcon.SetActive(false);

        GenerateMathExample(); // generate next example
        SetTextToAnswerButtons(answerButtons); // set answers to buttons
    }
}