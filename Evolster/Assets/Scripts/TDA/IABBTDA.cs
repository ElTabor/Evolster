using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IABBTDA
{
    Player Raiz();
    IABBTDA HijoIzq();
    IABBTDA HijoDer();
    bool ArbolVacio();
    void InicializarArbol();
    void AgregarElem(Player player);
    void EliminarElem(int x);
    Player HighestScore();
    Player LowestScore();
}
