using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string Name { get; set; }
    public int Score { get; set; }

    public Player(string name, int score)
    {
        Name = name;
        Score = score;
    }
}

public class NodoABB
{
    public Player Player { get; set; }
    public NodoABB Left { get; set; }
    public NodoABB Right { get; set; }

    public NodoABB(Player player)
    {
        Player = player;
        Left = null;
        Right = null;
    }
}

public class ABB : IABBTDA
{
    private NodoABB root;

    public void AgregarElem(Player player)
    {
        root = AgregarElem(root, player);
    }

    private NodoABB AgregarElem(NodoABB node, Player player)
    {
        if(node == null)
        {
            return new NodoABB(player);
        }
        if(player.Score < node.Player.Score)
        {
            node.Left = AgregarElem(node.Left, player);
        }
        else if (player.Score > node.Player.Score)
        {
            node.Right = AgregarElem(node.Right, player);
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

    private NodoABB EliminarElem(NodoABB node, int score)
    {
        if (node == null)
        {
            return node;
        }
        if (score < node.Player.Score)
        {
            node.Left = EliminarElem(node.Left, score);
        }
        else if (score > node.Player.Score)
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

            node.Player = LowestScore(node.Right);
            node.Right = EliminarElem(node.Right, node.Player.Score);
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

    public Player Raiz()
    {
        if(root == null)
        {
            throw new System.InvalidOperationException("El arbol esta vacio.");
        }
        return root.Player;
    }

    public Player HighestScore()
    {
        if(root == null)
        {
            throw new System.InvalidOperationException("El arbol esta vacio.");
        }

        return HighestScore(root);
    }

    private Player HighestScore(NodoABB node)
    {
        if(node.Right == null)
        {
            return node.Player;
        }
        return HighestScore(node.Right);
    }

    public Player LowestScore()
    {
        if (root == null)
        {
            throw new System.InvalidOperationException("El arbol esta vacio.");
        }

        return LowestScore();
    }

    private Player LowestScore(NodoABB node)
    {
        if (node.Left == null)
        {
            return node.Player;
        }
        return LowestScore(node.Left);
    }

    public List<Player> GetPlayersInOrder()
    {
        List<Player> playersInOrder = new List<Player>();
        GetPlayersInOrder(root, playersInOrder);
        return playersInOrder;
    }

    private void GetPlayersInOrder(NodoABB node, List<Player> players)
    {
        if(node != null)
        {
            GetPlayersInOrder(node.Right, players);
            players.Add(node.Player);
            GetPlayersInOrder(node.Left, players);
        }
    }
}
