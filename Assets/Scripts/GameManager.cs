using System.Collections;
using System.Collections.Generic;
using Facade;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private int startingUndoCount = 3;
    [SerializeField] private int undoCount;
    public bool gameLost;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip undoSound;
    
    private IUndoCanvasProvider _undoCanvasProvider;

    private int turnsElapsed;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        _undoCanvasProvider = MainCanvas.Instance;
        undoCount = startingUndoCount;
        
        _undoCanvasProvider.UndoCanvas.updateUndoLeft(undoCount);
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void OnEnable()
    {
        PlayerMovement.onPlayerMoved += TurnElapsed;
    }

    private void OnDisable()
    {
        PlayerMovement.onPlayerMoved -= TurnElapsed;
    }
    
    public void UndoLastTurn()
    {
        if (undoCount > 0 && turnsElapsed > 0)
        {
            EventQueue.Instance.UndoLatest();
            EventQueue.Instance.UndoLatest();
            undoCount--;
            turnsElapsed--;
            _undoCanvasProvider.UndoCanvas.updateUndoLeft(undoCount);
            gameLost = false;
            _audioSource.PlayOneShot(undoSound);
            Debug.Log("Undo");
        }
    }
    
    public void LoseGame()
    {
        gameLost = true;
        MainCanvas.Instance.GameOver();
        Debug.Log("Game Over");
    }

    public void WinGame()
    {
        MainCanvas.Instance.WinGame();
        Debug.Log("You Win");
    }

    private void TurnElapsed()
    {
        turnsElapsed++;
    }

    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
