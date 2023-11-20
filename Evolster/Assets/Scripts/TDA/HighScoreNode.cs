using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreNode 
{
    public float Score { get; set; }
    public HighScoreNode Left { get; set; }
    public HighScoreNode Right { get; set; }

    public HighScoreNode(float score)
    {
        Score = score;
        Left = null;
        Right = null;
    }
}
