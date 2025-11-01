using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelSettings
    {
        public int cubeCount;
        public float cubeSpeed;
        public float gameTime;
        
        public LevelSettings(int count, float speed, float time)
        {
            cubeCount = count;
            cubeSpeed = speed;
            gameTime = time;
        }
    }
    
    // Level configurations
    public static LevelSettings easyLevel = new LevelSettings(10, 5f, 30f);
    public static LevelSettings mediumLevel = new LevelSettings(20, 7f, 45f);
    public static LevelSettings hardLevel = new LevelSettings(30, 10f, 60f);
    
    // Current level settings (default to medium)
    public static LevelSettings currentLevel = mediumLevel;
    
    // UI References
    public GameObject mainMenuPanel;
    public GameObject gameUI;
    
    // Buttons
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    
    // Reference to GameController
    public GameController gameController;

    void Start()
    {
        // Set up button listeners
        if (easyButton != null)
            easyButton.onClick.AddListener(() => SelectLevel(easyLevel, "Easy"));
            
        if (mediumButton != null)
            mediumButton.onClick.AddListener(() => SelectLevel(mediumLevel, "Medium"));
            
        if (hardButton != null)
            hardButton.onClick.AddListener(() => SelectLevel(hardLevel, "Hard"));
        
        // Show main menu at start, but don't hide game UI yet
        ShowMainMenu();
    }
    
    public void SelectLevel(LevelSettings level, string levelName)
    {
        currentLevel = level;
        Debug.Log("Selected " + levelName + " level: " + level.cubeCount + " cubes, " + level.cubeSpeed + " speed");
        
        // Apply level settings to Cube class
        Cube.cubeSpeed = level.cubeSpeed;
        
        // Start the game
        StartGame();
    }
    
    public void StartGame()
    {
        // Hide main menu
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);
            
        // Don't touch gameUI - let GameController manage it
        // Just apply the level settings and start the game
        
        // Apply level settings to Cube class
        Cube.cubeSpeed = currentLevel.cubeSpeed;
        
        // Initialize game with current level settings
        if (gameController != null)
        {
            gameController.SetLevelSettings(currentLevel.cubeCount, currentLevel.gameTime);
            gameController.StartNewGame();
        }
    }
    
    public void ShowMainMenu()
    {
        // Show main menu
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
            
        // Don't hide game UI completely, just ensure main menu is visible
        // The game UI can stay active in the background
    }
    
    public void BackToMainMenu()
    {
        ShowMainMenu();
    }
}
