using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudManager : MonoBehaviour
{
    public Button restartButton;
    public RectTransform title;
    public Text score;
    public int scoreInt;
    public Text bestScoreText;
    public int bestscore;
    public CameraManager cm;
    public void OnClickRestart() {
       
        SceneManager.LoadScene(0);
    }
    public void OnClickstart()
    {

        title.gameObject.SetActive(false);
    }
    public void OnClickExit()
    {

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    private void Start()
    {
        bestscore=PlayerPrefs.GetInt("MaxScore");
        bestScoreText.text = "Best Score : " + bestscore;
    }


    public void SetRestartUI() {
        restartButton.gameObject.SetActive(true);
    }
    


    // Update is called once per frame
    void Update()
    {
        scoreInt = (int)cm.transform.position.y;
        score.text = "Score : " + scoreInt;
    }
}
