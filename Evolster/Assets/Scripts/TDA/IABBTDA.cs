using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IABBTDA
{
    ScoreRec Raiz();
    IABBTDA HijoIzq();
    IABBTDA HijoDer();
    bool ArbolVacio();
    void InicializarArbol();
    void AgregarElem(ScoreRec player);
    void EliminarElem(int x);
    ScoreRec HighestScore();
    ScoreRec LowestScore();
}
