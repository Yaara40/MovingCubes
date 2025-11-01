using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using System.Collections;

public class GameController : MonoBehaviour
{
    private int cubesNumer = 5;
    public GameObject cubePrefab;
    private Rigidbody rb;
    public Vector3 startPosition;
    private float boundariesX = 6f;
    private float boundariesZ1 = -10;
    private float boundariesZ2 = 6f;
    List<Cube> availableCubes = new List<Cube>();
    
    // Next cube hint system
    public float hintDelay = 5f; // Seconds between each scale increase
    public float scaleIncrement = 0.5f; // How much bigger to make the cube each time
    private float lastClickTime;
    private int hintLevel = 0; // How many times the cube has been scaled
    
    // Timer and Game Over system
    public float gameTime = 20f; // 1 minute in seconds
    public TextMeshProUGUI timerText; // Reference to UI Text component
    public int score = 0;
    public TextMeshProUGUI scoreText; // Reference to UI Text component
    public GameObject gameOverPanel; // Reference to game over panel
    private float startTime;
    private bool gameOver = false;
    
    // Timer warning system
    public float warningTime = 10f; // Start warning at 10 seconds
    public Color normalTimerColor = Color.white;
    public Color warningTimerColor = Color.red;
    private bool isTimerFlashing = false;
    
    // Countdown audio system
    public AudioClip countdownSound; // Assign your countdown audio clip in Inspector
    public AudioClip victorySound; // Assign your victory audio clip in Inspector
    public AudioClip defeatSound; // Assign your defeat audio clip in Inspector
    private AudioSource audioSource;
    private bool countdownStarted = false;
    
    // Game Over UI elements
    public TextMeshProUGUI gameOverMessageText; // Reference to game over message text
    public TextMeshProUGUI finalScoreText; // Reference to final score display
    public GameObject celebrationParticles; // Reference to celebration particle system
    public GameObject nextLevelButton; // Reference to next level button
    void Start()
    {
        // Reset all game state variables
        gameOver = false;
        isTimerFlashing = false;
        countdownStarted = false;
        score = 0;
        hintLevel = 0;
        
        // Reset static cube counter
        Cube.nextCubeNumber = 1;
        
        // Hide game over panel at start
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        
        // Reset UI elements
        if (timerText != null)
        {
            timerText.color = normalTimerColor;
        }
        
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        
        // Clear any existing cubes and create new ones
        availableCubes.Clear();
        
        for (int i = 1; i <= cubesNumer; i++)
        {
            rb = GetComponent<Rigidbody>();
            startPosition = new Vector3(0, 3, 0);
            GameObject cube = Instantiate(cubePrefab, startPosition, Quaternion.identity);
            cube.GetComponent<Cube>().cubeID = i;
            availableCubes.Add(cube.GetComponent<Cube>());
        }
        
        // Start the hint timer
        lastClickTime = Time.time;
        StartCoroutine(CheckForHint());
        
        // Start the game timer
        startTime = Time.time;
        StartCoroutine(GameTimer());
        
        // Initialize AudioSource for countdown
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private IEnumerator CheckForHint()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(0.5f); // Check every half second
            
            // Calculate how many 5-second intervals have passed
            int intervalsPassedSinceLastClick = Mathf.FloorToInt((Time.time - lastClickTime) / hintDelay);
            
            // If more intervals have passed than our current hint level, make cube bigger
            if (intervalsPassedSinceLastClick > hintLevel)
            {
                hintLevel = intervalsPassedSinceLastClick;
                MakeNextCubeBigger();
            }
        }
    }
    
    private IEnumerator GameTimer()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(0.1f); // Update timer every 0.1 seconds
            
            float timeElapsed = Time.time - startTime;
            float timeRemaining = gameTime - timeElapsed;
            
            // Check for win condition first, before checking time
            if (Cube.nextCubeNumber > cubesNumer)
            {
                Debug.Log("WIN CONDITION MET in GameTimer! Calling GameOver('You Win!')");
                GameOver("You Win!");
                break;
            }
            
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                GameOver("Time's Up!");
            }
            
            UpdateTimerDisplay(timeRemaining);
        }
    }
    
    private void MakeNextCubeBigger()
    {
        // Find the cube with the next number
        Cube nextCube = GetNextCube();
        if (nextCube != null)
        {
            // Calculate the new scale based on hint level
            float newScale = 1f + (hintLevel * scaleIncrement);
            nextCube.transform.localScale = Vector3.one * newScale;
            Debug.Log($"Making cube {Cube.nextCubeNumber} bigger! Scale: {newScale}x (Hint level: {hintLevel})");
        }
    }
    
    private void ResetNextCubeSize()
    {
        // Find the cube and reset its size
        Cube nextCube = GetNextCube();
        if (nextCube != null)
        {
            nextCube.transform.localScale = Vector3.one;
        }
    }
    
    private Cube GetNextCube()
    {
        foreach (Cube cube in availableCubes)
        {
            if (cube != null && cube.cubeID == Cube.nextCubeNumber)
            {
                return cube;
            }
        }
        return null;
    }
    
    // Call this method when a cube is clicked correctly
    public void OnCubeClicked()
    {
        if (gameOver) return;
        
        lastClickTime = Time.time;
        hintLevel = 0; // Reset hint level for next cube
        ResetNextCubeSize();
        
        // Remove the clicked cube from available cubes
        availableCubes.RemoveAll(cube => cube == null);
        
        // Check if all cubes are clicked (win condition)
        if (Cube.nextCubeNumber > cubesNumer)
        {
            GameOver("You Win!");
        }
        else
        {
            Debug.Log("Win condition NOT met. Need nextCubeNumber (" + Cube.nextCubeNumber + ") to be > cubesNumer (" + cubesNumer + ")");
        }
    }
    
    private void UpdateTimerDisplay(float timeRemaining)
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            
            // Start flashing when timer reaches warning time
            if (timeRemaining <= warningTime && !isTimerFlashing)
            {
                isTimerFlashing = true;
                StartCoroutine(FlashTimer());
            }
            else if (timeRemaining > warningTime && isTimerFlashing)
            {
                isTimerFlashing = false;
                timerText.color = normalTimerColor; // Reset to normal color
            }
            
            // Start countdown audio in last 10 seconds
            if (timeRemaining <= 10f && !countdownStarted)
            {
                countdownStarted = true;
                StartCoroutine(CountdownAudio());
            }
        }
    }
    
    private IEnumerator FlashTimer()
    {
        while (isTimerFlashing && !gameOver)
        {
            float timeElapsed = Time.time - startTime;
            float timeRemaining = gameTime - timeElapsed;
            
            // Flash faster when time is almost up (last 5 seconds)
            float flashSpeed = timeRemaining <= 5f ? 0.2f : 0.5f;
            
            // Flash between red and normal color
            timerText.color = warningTimerColor;
            yield return new WaitForSeconds(flashSpeed);
            timerText.color = normalTimerColor;
            yield return new WaitForSeconds(flashSpeed);
        }
        
        // Reset to normal color when flashing stops
        if (timerText != null)
        {
            timerText.color = normalTimerColor;
        }
    }
    
    private IEnumerator CountdownAudio()
    {
        while (!gameOver)
        {
            float timeElapsed = Time.time - startTime;
            float timeRemaining = gameTime - timeElapsed;
            
            // Stop if time is up or more than 10 seconds remaining
            if (timeRemaining <= 0 || timeRemaining > 10f)
            {
                break;
            }
            
            // Play countdown sound every second
            if (audioSource != null && countdownSound != null)
            {
                audioSource.PlayOneShot(countdownSound);
            }
            
            yield return new WaitForSeconds(1f); // Wait 1 second before next beep
        }
    }
    
    private IEnumerator DisableParticlesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (celebrationParticles != null)
        {
            celebrationParticles.SetActive(false);
        }
    }
    
    private void GameOver(string reason)
    {
        gameOver = true;
        isTimerFlashing = false; // Stop timer flashing
        countdownStarted = false; // Stop countdown audio
        
        Debug.Log("Game Over: " + reason);
        
        // Reset timer color to normal
        if (timerText != null)
        {
            timerText.color = normalTimerColor;
        }
        
        // Stop all cube movement
        Cube[] allCubes = FindObjectsByType<Cube>(FindObjectsSortMode.None);
        foreach (Cube cube in allCubes)
        {
            if (cube != null)
            {
                cube.enabled = false; // Disable cube movement and clicking
                
                // Also stop the Rigidbody physics
                Rigidbody cubeRb = cube.GetComponent<Rigidbody>();
                if (cubeRb != null)
                {
                    cubeRb.linearVelocity = Vector3.zero;
                    cubeRb.angularVelocity = Vector3.zero;
                    cubeRb.isKinematic = true; // Stop all physics
                }
            }
        }
        
        // Handle different game ending scenarios
        if (reason == "You Win!")
        {
            HandleWinCondition();
        }
        else if (reason == "Time's Up!")
        {
            HandleTimeUpCondition();
        }
        
        // Show game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        // You can add more game over logic here (restart button, score display, etc.)
    }
    
    private void HandleWinCondition()
    {
        // Calculate time bonus - reward for finishing early
        float timeElapsed = Time.time - startTime;
        float timeRemaining = gameTime - timeElapsed;
        int timeBonus = Mathf.RoundToInt(timeRemaining * 1); // 1 point per second remaining
        
        score += timeBonus;
        
        // Update the main score text to show final score with bonus
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
                
        // Play victory sound
        if (audioSource != null && victorySound != null)
        {
            audioSource.PlayOneShot(victorySound);
        }
        
        // Show confetti/celebration particles
        if (celebrationParticles != null)
        {
            celebrationParticles.SetActive(true);
            // Auto-disable particles after 5 seconds
            StartCoroutine(DisableParticlesAfterDelay(5f));
        }
        
        // Display "YOU WIN!" message
        if (gameOverMessageText != null)
        {
            gameOverMessageText.gameObject.SetActive(true); // Make sure it's active
            gameOverMessageText.text = "YOU WIN!!!";
            gameOverMessageText.color = Color.green;
            Debug.Log("Set win message text!");
        }
        else
        {
            Debug.LogWarning("gameOverMessageText is NULL! Please assign it in the Inspector.");
        }
        
        // Display final score with bonus
        if (finalScoreText != null)
        {
            finalScoreText.gameObject.SetActive(true); // Make sure it's active
            finalScoreText.text = "Final Score: " + score + "\nTime Bonus: +" + timeBonus + " points!";
            Debug.Log("Set final score text!");
        }
        else
        {
            Debug.LogWarning("finalScoreText is NULL! Please assign it in the Inspector.");
        }
        
        // Temporary: Show win message in console and debug overlay
        Debug.Log("YOU WIN! FINAL SCORE: " + score + " (+" + timeBonus + " bonus)");
        
        // Save high score (simple PlayerPrefs implementation)
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            Debug.Log("New High Score: " + score);
            
            if (gameOverMessageText != null)
            {
                gameOverMessageText.text = "YOU WIN! NEW HIGH SCORE!";
            }
        }
        
        // Show next level button only on win
        if (nextLevelButton != null)
        {
            nextLevelButton.SetActive(true);
        }
    }
    
    private void HandleTimeUpCondition()
    {
        int cubesClicked = Cube.nextCubeNumber - 1;
        Debug.Log("Time's up! You clicked " + cubesClicked + " out of " + cubesNumer + " cubes correctly.");
        
        // Play defeat sound
        if (audioSource != null && defeatSound != null)
        {
            audioSource.PlayOneShot(defeatSound);
        }
        
        // Show "TIME'S UP!" message
        if (gameOverMessageText != null)
        {
            gameOverMessageText.gameObject.SetActive(true); // Make sure it's active
            gameOverMessageText.text = "TIME'S UP! YOU LOSE!";
            gameOverMessageText.color = Color.red;
        }
        
        // Display progress made
        if (finalScoreText != null)
        {
            finalScoreText.gameObject.SetActive(true); // Make sure it's active
            float progressPercentage = ((float)cubesClicked / cubesNumer) * 100f;
            finalScoreText.text = "Final Score: " + score + 
                               "\nProgress: " + cubesClicked + "/" + cubesNumer + " cubes (" + 
                               Mathf.RoundToInt(progressPercentage) + "%)";
        }
        
        // Show high score comparison
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentHighScore > 0 && finalScoreText != null)
        {
            finalScoreText.text += "\nHigh Score: " + currentHighScore;
        }
    }
    
    // Public method to manually trigger hint (for testing)
    [System.Obsolete("This method is for testing only")]
    public void TriggerHintManually()
    {
        MakeNextCubeBigger();
    }
    
    // Public method to restart the game (can be called from UI button)
    public void RestartGame()
    {
        // Instead of reloading scene, just restart with current level settings
        StartNewGame();
    }
    
    // Method to go back to main menu
    public void BackToMainMenu()
    {
        // Stop the current game
        gameOver = true;
        StopAllCoroutines();
        
        // Destroy all cubes
        GameObject[] existingCubes = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in existingCubes)
        {
            if (obj.GetComponent<Cube>() != null)
            {
                Destroy(obj);
            }
        }
        
        // Hide game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        
        // Find and show main menu via LevelManager
        LevelManager levelManager = FindFirstObjectByType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.ShowMainMenu();
        }
    }
    
    // Method to advance to next level
    public void NextLevel()
    {
        Debug.Log("NextLevel() called!");
        Debug.Log("Current level before change: " + cubesNumer + " cubes, " + gameTime + " seconds");
        Debug.Log("Current LevelManager.currentLevel: " + LevelManager.currentLevel.cubeCount + " cubes");
        
        LevelManager levelManager = FindFirstObjectByType<LevelManager>();
        if (levelManager != null)
        {
            LevelManager.LevelSettings nextLevel;
            string levelName;
            
            // Determine next level based on current GameController settings (more reliable)
            if (cubesNumer == 10) // Easy -> Medium
            {
                nextLevel = LevelManager.mediumLevel;
                levelName = "Medium";
            }
            else if (cubesNumer == 20) // Medium -> Hard
            {
                nextLevel = LevelManager.hardLevel;
                levelName = "Hard";
            }
            else // Hard -> Easy (loop back)
            {
                nextLevel = LevelManager.easyLevel;
                levelName = "Easy";
            }
            
            Debug.Log("Advancing to " + levelName + " level: " + nextLevel.cubeCount + " cubes, " + nextLevel.cubeSpeed + " speed, " + nextLevel.gameTime + " seconds");
            
            // Apply the next level settings directly
            LevelManager.currentLevel = nextLevel;
            Cube.cubeSpeed = nextLevel.cubeSpeed;
            cubesNumer = nextLevel.cubeCount;
            gameTime = nextLevel.gameTime;
            
            Debug.Log("Settings applied - cubesNumer: " + cubesNumer + ", gameTime: " + gameTime + ", cubeSpeed: " + Cube.cubeSpeed);
            
            // Start the new game with next level settings
            StartNewGame();
        }
        else
        {
            Debug.LogError("LevelManager not found!");
        }
    }
    
    // Set level settings without starting the game yet
    public void SetLevelSettings(int numberOfCubes, float gameTimeLimit)
    {
        cubesNumer = numberOfCubes;
        gameTime = gameTimeLimit;
        
        Debug.Log("Level settings applied: " + numberOfCubes + " cubes, " + gameTimeLimit + " seconds");
    }
    
    // Start a new game with current settings
    public void StartNewGame()
    {
        // Reset all game state variables
        gameOver = false;
        isTimerFlashing = false;
        countdownStarted = false;
        score = 0;
        hintLevel = 0;
        
        // Reset static cube counter
        Cube.nextCubeNumber = 1;
        
        // Hide game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        
        // Reset UI elements
        if (timerText != null)
        {
            timerText.color = normalTimerColor;
        }
        
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        
        // Hide game over message texts
        if (gameOverMessageText != null)
        {
            gameOverMessageText.gameObject.SetActive(false);
        }
        
        if (finalScoreText != null)
        {
            finalScoreText.gameObject.SetActive(false);
        }
        
        // Hide next level button on restart
        if (nextLevelButton != null)
        {
            nextLevelButton.SetActive(false);
        }
        
        // Clear existing cubes
        GameObject[] existingCubes = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in existingCubes)
        {
            if (obj.GetComponent<Cube>() != null)
            {
                Destroy(obj);
            }
        }
        
        // Clear and create new cubes
        availableCubes.Clear();
        
        Debug.Log("StartNewGame: Creating " + cubesNumer + " cubes with speed " + Cube.cubeSpeed);
        
        for (int i = 1; i <= cubesNumer; i++)
        {
            float randomX = Random.Range(-boundariesX, boundariesX);
            float randomZ = Random.Range(boundariesZ1, boundariesZ2);
            float randomY = Random.Range(3f, 6f);
            startPosition = new Vector3(randomX, randomY, randomZ);
            startPosition = new Vector3(0, 3, 0);
            GameObject cube = Instantiate(cubePrefab, startPosition, Quaternion.identity);
            cube.GetComponent<Cube>().cubeID = i;
            availableCubes.Add(cube.GetComponent<Cube>());
        }
        
        // Start the game systems
        lastClickTime = Time.time;
        startTime = Time.time;
        
        // Stop existing coroutines and start new ones
        StopAllCoroutines();
        StartCoroutine(CheckForHint());
        StartCoroutine(GameTimer());
        
        Debug.Log("New game started with " + cubesNumer + " cubes!");
    }


}
