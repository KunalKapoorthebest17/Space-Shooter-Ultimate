using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;
    [SerializeField]
    private Text _ammoText;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score : " + 0;
        _ammoText.text = "Ammo : " + 15;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {

            Debug.Log("Game Manager is null");
        }

    }

    // Update is called once per frame
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score : " + playerScore.ToString();
    }
    public void UpdateLives(int currentLives)
    {
        Debug.Log("Current Lives: " + currentLives);
        if (currentLives > 0 && currentLives <= 3)
        {
            _livesImage.sprite = _liveSprites[currentLives];
        }
        
        else if (currentLives == 0 || currentLives < 0)
        {
            _gameOverText.gameObject.SetActive(true);
            GameOver();
        }
        else if (currentLives > 3)
        {
            return;
        }

    }
    public void UpdateAmmo(float ammoLeft)
    {
        _ammoText.text = "Ammo : " + ammoLeft.ToString();
    }

    public void GameOver()
    {
        _gameManager.GameOver();
        _restartText.gameObject.SetActive(true);

        StartCoroutine(GameOverFlickerRoutine());



    }
    

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = ("GAME OVER");
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }

    }





}
