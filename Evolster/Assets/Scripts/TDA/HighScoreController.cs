using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreController : MonoBehaviour
{
    #region Initialize_ABBHighScores
    private HighScoreNode root;
    public List<float> scoreTextList;
    public static HighScoreController instance;

    public HighScoreController(List<float> scoreTextList)
    {
        root = null;
        this.scoreTextList = scoreTextList;
    }
    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    #region ABB_Methods
    // Inserta un nuevo highscore en el árbol
    public void InsertScore(float score)
    {
        root = Insert(root, score);
        UpdateScoreText();
    }
    private HighScoreNode Insert(HighScoreNode node, float score)
    {
        if (node == null)
        {
            return new HighScoreNode(score);
        }

        if (score < node.Score)
        {
            node.Right = Insert(node.Right, score);
        }
        else
        {
            node.Left = Insert(node.Left, score);
        }
        return node;
    }

    // Obtiene una lista ordenada de highscores de mayor a menor utilizando quicksort
    public List<float> GetSortedScores()
    {
        List<float> scores = new List<float>();
        InOrderTraversal(root, scores);
        return scores;
    }
    
    private void InOrderTraversal(HighScoreNode node, List<float> scores)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left, scores);
            scores.Add(node.Score);
            InOrderTraversal(node.Right, scores);
        }
    }
#endregion
    private void UpdateScoreText()
    {
        List<float> scores = new List<float>();
        InOrderTraversal(root, scores);

        for (int i = 0; i < scoreTextList.Count && i < scores.Count; i++)
        {
            scoreTextList[i] = scores[i];
        }
    }
}
