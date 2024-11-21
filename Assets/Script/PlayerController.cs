using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
public enum Choice
{
    None,
    Rock,
    Paper,
    Scissors
}

public class GameManager : MonoBehaviour
{
    public TMP_Text playerChoiceText;
    public TMP_Text computerChoiceText;
    public TMP_Text resultText;
    public TMP_Text playerScoreText;
    public TMP_Text computerScoreText;

    public Button Rock;
    public Button Paper;
    public Button Scissors;
    public Button shootButton;
    public Button replayButton;

    private Choice playerChoice = Choice.None;
    private Choice computerChoice = Choice.None;

    public int playerScore = 0;
    private int computerScore = 0;

    private bool gameInProgress = true;

    public void SetPlayerChoice(int choice)
    {
        if (!gameInProgress) return;

        playerChoice = (Choice)choice;
        Debug.Log("Player's choice: " + playerChoice);
        playerChoiceText.text = "Player: " + playerChoice.ToString();
    }

    public void Shoot()
    {
        if (playerChoice == Choice.None) return;

        gameInProgress = false;

        int choicelength = Enum.GetValues(typeof(Choice)).Length;
        // Generate random comp choice

        computerChoice = (Choice)UnityEngine.Random.Range(1, choicelength);
        Debug.Log("Computer's choice: " + computerChoice);
        computerChoiceText.text = "Computer: " + computerChoice.ToString();

        // Determine result
        string result = DetermineWinner(playerChoice, computerChoice);
        resultText.text = "Result: " + result;

        // Enable Replay button
        replayButton.gameObject.SetActive(true);
    }

    public void Replay()
    {
        playerChoice = Choice.None;
        computerChoice = Choice.None;
        gameInProgress = true;

        playerChoiceText.text = "Player: ";
        computerChoiceText.text = "Computer: ";
        resultText.text = "Result: ";

        replayButton.gameObject.SetActive(false);
    }

    private string DetermineWinner(Choice player, Choice computer)
    {
        if (player == computer) return "It's a Tie!";
        if ((player == Choice.Rock && computer == Choice.Scissors) ||
            (player == Choice.Scissors && computer == Choice.Paper) ||
            (player == Choice.Paper && computer == Choice.Rock))
        {
            playerScore++;
            UpdateScores();
            return "You Win!";
        }
        else
        {
            computerScore++;
            UpdateScores();
            return "Computer Wins!";
        }
    }

    private void UpdateScores()
    { 
        playerScoreText.text = "Player Score: " + playerScore.ToString();
        computerScoreText.text = "Comp Score: " + computerScore.ToString();
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
