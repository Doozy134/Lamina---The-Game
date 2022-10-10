using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Questions : MonoBehaviour
{
    //UI

    private string correctAns;
    [SerializeField] private GameObject Question;
    private TextMeshProUGUI questionText;
    private GameObject correctText;
    private GameObject incorrectText;
    //more UI
    private GameObject inputBox;
    private InputField inputfield;
    private Text answer;
    private GameObject textArea;
    private TextMeshProUGUI scoreText;
    private GameObject scoreMessage;
    private CanvasGroup cG;
    private Animator anim;
    private int scoreRewarded;
    //characters
    private GameObject enemy;
    private Rigidbody2D playerBody;
    //Scripts
    private QuestionSet QuestionSet;
    private Stopwatch stopWatch;
    private Score scoreScript;

    private bool bossQ1;
    private bool bossQ2;

    private void Start()
    {
        //setting all references
        questionText = Question.transform.Find("questionText").GetComponent<TextMeshProUGUI>();
        correctText = Question.transform.Find("correct").gameObject;
        incorrectText = Question.transform.Find("incorrect").gameObject;
        inputBox = Question.transform.Find("inputbox").gameObject;
        //textArea = inputBox.transform.Find("Text Area").gameObject;
        answer = inputBox.transform.Find("answer").GetComponent<Text>();
        inputfield = Question.transform.Find("inputbox").GetComponent<InputField>();
        scoreText = Question.transform.Find("score").GetComponent<TextMeshProUGUI>();
        scoreMessage = Question.transform.Find("score").gameObject;
        QuestionSet = gameObject.GetComponent<QuestionSet>();
        stopWatch = gameObject.GetComponent<Stopwatch>();
        scoreScript = gameObject.GetComponent<Score>();
        cG = Question.GetComponent<CanvasGroup>();
        anim = Question.GetComponent<Animator>();

        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        //starting off with all question elements inactive
        scoreMessage.SetActive(false);
        correctText.SetActive(false);
        incorrectText.SetActive(false);
        cG.interactable = false;
        cG.blocksRaycasts = false;

    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //When player enters answer check it
            checkAnswer();
        }
        

    }

    public void loadQuestion(GameObject currentEnemy)
    {
        //show question
        //Question.SetActive(true);
        anim.SetTrigger("start");
        
        playerBody.constraints = RigidbodyConstraints2D.FreezeAll;

        string newText = "";
        enemy = currentEnemy;

        //anglesintriangle(out correctAns, out newText);
        //calling function to pass in varaibles for next question
        QuestionSet.loadNextQuestion(out correctAns, out newText);

        //setting the new question
        questionText.text = newText;
        //make interactable
        cG.interactable = true;
        cG.blocksRaycasts = true;
        inputfield.Select();
        stopWatch.startStopwatch();

        Debug.Log(correctAns);

        inputfield.Select();



    }

    private void checkAnswer()
    {
        //different if enemy
        if (enemy.tag == "Boss")
        {
            if (!bossQ1)
            {
                if (answer.text.ToUpper() == correctAns)
                {
                    //calculate score

                    bossQ1 = true;
                    //calculate time
                    double timeTaken = stopWatch.stopStopwatch();
                    Debug.Log(timeTaken);
                    //get score
                    scoreRewarded = scoreScript.calcScore(timeTaken);
                    Debug.Log(scoreRewarded);
                    StartCoroutine(BosscorrectAnswer());
                    loadQuestion(enemy);

                }
                else if (answer.text == "")
                {
                    //setting exception for blank input
                    inputfield.Select();
                }
                else
                {
                    //deduct score
                    scoreScript.deductScore();

                    StartCoroutine(incorrectAnswer());
                    inputfield.Select();
                    inputfield.text = "";
                    inputfield.Select();
                }
            }
            else if (bossQ1 && !bossQ2)
            {
                if (answer.text.ToUpper() == correctAns)
                {
                    //calculate score

                    bossQ2 = true;
                    //calculate time
                    double timeTaken = stopWatch.stopStopwatch();
                    Debug.Log(timeTaken);
                    //get score
                    scoreRewarded = scoreScript.calcScore(timeTaken);
                    Debug.Log(scoreRewarded);
                    StartCoroutine(correctAnswer());

                }
                else if (answer.text == "")
                {
                    //setting exception for blank input
                    inputfield.Select();
                }
                else
                {
                    //deduct score
                    scoreScript.deductScore();

                    StartCoroutine(incorrectAnswer());
                    inputfield.Select();
                    inputfield.text = "";
                    inputfield.Select();
                    bossQ1 = false;
                }
            }
        }
        else
        {
            if (answer.text.ToUpper() == correctAns)
            {
                //calculate score

                //calculate time
                double timeTaken = stopWatch.stopStopwatch();
                Debug.Log(timeTaken);
                //get score
                scoreRewarded = scoreScript.calcScore(timeTaken);
                Debug.Log(scoreRewarded);
                StartCoroutine(correctAnswer());

            }
            else if (answer.text == "")
            {
                //setting exception for blank input
                inputfield.Select();
            }
            else
            {
                //deduct score
                scoreScript.deductScore();
                
                StartCoroutine(incorrectAnswer());
                inputfield.Select();
                inputfield.text = "";
                inputfield.Select();
            }
        }
    }

    private IEnumerator incorrectAnswer()
    {
        //show text
        incorrectText.SetActive(true);
        //wait
        FindObjectOfType<AudioManager>().Play("Incorrect");
        yield return new WaitForSeconds(2);
        //hide text
        incorrectText.SetActive(false);
    }

    private IEnumerator correctAnswer()
    {
        //show text
        correctText.SetActive(true);
        scoreMessage.SetActive(true);
        //play sound
        FindObjectOfType<AudioManager>().Play("Correct");
        //show score given
        scoreText.text = "+" + scoreRewarded.ToString();
        //make it non-interactable
        cG.interactable = false;
        cG.blocksRaycasts = false;
        //let the player move
        playerBody.constraints = RigidbodyConstraints2D.None;
        playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        yield return new WaitForSeconds(1.5f);

        //clear input box
        inputfield.Select();
        inputfield.text = "";
        //hide text
        correctText.SetActive(false);
        scoreMessage.SetActive(false);
        //animation to go away
        anim.SetTrigger("end");
        //disable enemy
        enemy.GetComponent<Enemy>().enemyExit();
    }

    private IEnumerator BosscorrectAnswer()
    {
        //show text
        correctText.SetActive(true);
        scoreMessage.SetActive(true);
        //play sound
        FindObjectOfType<AudioManager>().Play("Correct");
        //show score given
        scoreText.text = "+" + scoreRewarded.ToString();
        //make it non-interactable
        
        yield return new WaitForSeconds(1.5f);

        //clear input box
        inputfield.Select();
        inputfield.text = "";
        //hide text
        correctText.SetActive(false);
        scoreMessage.SetActive(false);
        //animation to go away
        
    }
}
