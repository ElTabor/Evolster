using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRec
{
    public string Name { get; set; }
    public float Score { get; set; }

    public ScoreRec(float score)
    {
        Score = score;
    }
}

public class NodoABB
{
    public ScoreRec Score { get; set; }
    public NodoABB Left { get; set; }
    public NodoABB Right { get; set; }

    public NodoABB(ScoreRec score)
    {
        Score = score;
        Left = null;
        Right = null;
    }
}

public class ABB : IABBTDA
{
    private NodoABB root;

    public void AgregarElem(ScoreRec score)
    {
        root = AgregarElem(root, score);
    }

    private NodoABB AgregarElem(NodoABB node, ScoreRec score)
    {
        if(node == null)
        {
            return new NodoABB(score);
        }
        if(score.Score < node.Score.Score)
        {
            node.Left = AgregarElem(node.Left, score);
        }
        else if (score.Score > node.Score.Score)
        {
            node.Right = AgregarElem(node.Right, score);
        }
        return node;
    }

    public bool ArbolVacio()
    {
        return root == null;
    }

    public void EliminarElem(int score)
    {
        root = EliminarElem(root, score);
    }

    private NodoABB EliminarElem(NodoABB node, float score)
    {
        if (node == null)
        {
            return node;
        }
        if (score < node.Score.Score)
        {
            node.Left = EliminarElem(node.Left, score);
        }
        else if (score > node.Score.Score)
        {
            node.Right = EliminarElem(node.Right, score);
        }
        else
        {
            if(node.Left == null)
            {
                return node.Right;
            }    
            else if(node.Right == null)
            {
                return node.Left;
            }

            node.Score = LowestScore(node.Right);
            node.Right = EliminarElem(node.Right, node.Score.Score);
        }
        return node;
    }

    public IABBTDA HijoDer()
    {
        if(root == null)
        {
            throw new System.InvalidOperationException("El arbol esta vacio.");
        }
        ABB hijoDer = new ABB();
        hijoDer.root = root.Right;
        return hijoDer;
    }

    public IABBTDA HijoIzq()
    {
        if (root == null)
        {
            throw new System.InvalidOperationException("El arbol esta vacio.");
        }
        ABB hijoIzq = new ABB();
        hijoIzq.root = root.Left;
        return hijoIzq;
    }

    public void InicializarArbol()
    {
        root = null;
    }

    public ScoreRec Raiz()
    {
        if(root == null)
        {
            throw new System.InvalidOperationException("El arbol esta vacio.");
        }
        return root.Score;
    }

    public ScoreRec HighestScore()
    {
        if(root == null)
        {
            throw new System.InvalidOperationException("El arbol esta vacio.");
        }

        return HighestScore(root);
    }

    private ScoreRec HighestScore(NodoABB node)
    {
        if(node.Right == null)
        {
            return node.Score;
        }
        return HighestScore(node.Right);
    }

    public ScoreRec LowestScore()
    {
        if (root == null)
        {
            throw new System.InvalidOperationException("El arbol esta vacio.");
        }

        return LowestScore();
    }

    private ScoreRec LowestScore(NodoABB node)
    {
        if (node.Left == null)
        {
            return node.Score;
        }
        return LowestScore(node.Left);
    }

    public List<ScoreRec> GetPlayersInOrder()
    {
        List<ScoreRec> scoresInOrder = new List<ScoreRec>();
        GetPlayersInOrder(root, scoresInOrder);
        return scoresInOrder;
    }

    private void GetPlayersInOrder(NodoABB node, List<ScoreRec> players)
    {
        if(node != null)
        {
            GetPlayersInOrder(node.Right, players);
            players.Add(node.Score);
            GetPlayersInOrder(node.Left, players);
        }
    }
}
