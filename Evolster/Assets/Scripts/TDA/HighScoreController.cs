using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreController : MonoBehaviour
{
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
        Quicksort(scores, 0, scores.Count - 1);
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

    private void Quicksort(List<float> scores, int low, int high)
    {
        if (low < high)
        {
            int partitionIndex = Partition(scores, low, high);

            Quicksort(scores, low, partitionIndex - 1);
            Quicksort(scores, partitionIndex + 1, high);
        }
    }

    private int Partition(List<float> scores, int low, int high)
    {
        float pivot = scores[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (scores[j] >= pivot)
            {
                i++;
                float temp = scores[i];
                scores[i] = scores[j];
                scores[j] = temp;
            }
        }

        float temp2 = scores[i + 1];
        scores[i + 1] = scores[high];
        scores[high] = temp2;

        return i + 1;
    }
    private void UpdateScoreText()
    {
        List<float> scores = new List<float>();
        InOrderTraversal(root, scores);
        Quicksort(scores, 0, scores.Count - 1);

        for (int i = 0; i < scoreTextList.Count && i < scores.Count; i++)
        {
            scoreTextList[i] = scores[i];
        }
    }
}
