using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQueue : MonoBehaviour, IQueue
{
    public GameObject[] a;
    public int index;
    public void InitializeQueue()
    {
        a = new GameObject[100];
        index = 0;
    }

    public void Enqueue(GameObject x)
    {
        for (int i = index - 1; i >= 0; i--) a[i + 1] = a[i];
        a[0] = x;
        index++;
    }

    public void Dequeue()
    {
        index--;
    }

    public bool EmptyQueue()
    {
        return (index == 0);
    }

    public GameObject First()
    {
        return a[index - 1];
    }
    public void Quicksort()
    {
        Quicksort(0, index - 1);
    }

    private void Quicksort(int low, int high)
    {
        if (low < high)
        {
            int partitionIndex = Partition(low, high);

            Quicksort(low, partitionIndex - 1);
            Quicksort(partitionIndex + 1, high);
        }
    }

    private int Partition(int low, int high)
    {
        GameObject pivot = a[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (CompareObjects(a[j], pivot) >= 0)
            {
                i++;
                Swap(i, j);
            }
        }

        Swap(i + 1, high);

        return i + 1;
    }

    private int CompareObjects(GameObject obj1, GameObject obj2)
    {
        // Implementa tu propia lógica de comparación basada en los nombres de los enemigos.
        // Cambia estos valores según tus necesidades específicas.
        if (obj1.name == "heavy enemy")
        {
            return (obj2.name == "heavy enemy") ? 0 : 1;
        }
        else if (obj1.name == "range enemy")
        {
            return (obj2.name == "heavy enemy") ? -1 : (obj2.name == "range enemy") ? 0 : 1;
        }
        else if (obj1.name == "light enemy")
        {
            return (obj2.name == "light enemy") ? 0 : -1;
        }
        else
        {
            // Lógica predeterminada en caso de nombres diferentes.
            return obj1.name.CompareTo(obj2.name);
        }
    }

    private void Swap(int i, int j)
    {
        GameObject temp = a[i];
        a[i] = a[j];
        a[j] = temp;
    }
}
