using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score;
    static ScoreKeeper instance;
    public ScoreKeeper GetScoreKeeper()
    {
        return instance;
    }
    void Awake()
    {
        ManageSingleton();
    }
    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    public int GetScore()
    {
        return score;
    }
    public void SetScore(int sc)
    {
        score = sc;
    }
    public void ResetScore()
    {
        score = 0;
    }
    public void AddScore(int sc)
    {


        score += sc;

        Mathf.Clamp(score, 0, int.MaxValue);
    }
}
