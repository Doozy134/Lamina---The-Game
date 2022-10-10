using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSet : MonoBehaviour
{
    private int randomQ;
    private int numOfQ = 5;

    private void Awake()
    {
        
    }
    public void loadNextQuestion(out string Answer, out string QuestionText)
    {
        string newAns = null;
        string newQT = null;
        randomQ = Random.Range(1, numOfQ);
        Answer = newAns;
        QuestionText = newQT;
        Debug.Log(randomQ);
        if (randomQ == 1)
            q1(out Answer, out QuestionText);
        if(randomQ == 2)
            q2(out Answer, out QuestionText);
        if (randomQ == 3)
            q3(out Answer, out QuestionText);
        if(randomQ == 4)
            q4(out Answer, out QuestionText);
        if (randomQ == 5)
            q3(out Answer, out QuestionText);
    }



    private void q1(out string a, out string qText)
    {
        int angle1 = UnityEngine.Random.Range(1, 178);
        int angle2 = UnityEngine.Random.Range(1, (179 - angle1));
        int angle3 = 180 - angle1 - angle2;
        a = angle3.ToString();

        qText = "In triangle ABC, angle ABC=" + angle1 + ", angle ACB=" + angle2 + ". Calculate the size of the last angle";

    }

    private void q2(out string answer, out string qText)
    {
        int[] numbers = { 15, 20, 40, 28, 66, 42, 90 };
        int[] numbers2 = { 40, 44, 48, 50, 54, 58, 72, 80 };
        int a = Random.Range(0, numbers.Length);
        int b = Random.Range(0, numbers2.Length);
        a = numbers[a];
        b = numbers2[b];
        int ans = 0;

        qText = "Work out the highest common factor between " + a + " and " + b;

        while (a!=0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }
        ans = a | b;
        answer = ans.ToString();
        
    }

    private void q3(out string answer, out string qText)
    {
        float num = Random.Range(0.12345f, 5.6789f);
        int dp = Random.Range(1, 4);
        string roundingPoint = ("F"+dp).ToString();
        string roundedNum = num.ToString(roundingPoint);
        qText = "Round " + num + " to "+dp+" Decimal Point - including the point in your answer";
        answer = roundedNum;
    }

    private void q4(out string answer, out string qText)
    {
        float[] notes = {5, 10, 20, 50};

        float amountTendered = notes[Random.Range(0, notes.Length)];
        string aTendered = amountTendered.ToString("F2");

        float amount = Random.Range(0.5f, amountTendered-2.5f);
        string a = amount.ToString("F2");

        answer = (amountTendered - amount).ToString("F2");

        qText = "You buy £"+a+" of fruit from a shop, You have a £"+aTendered+" note. How much change do you get? {do not include £ in answer}";
    }
}
