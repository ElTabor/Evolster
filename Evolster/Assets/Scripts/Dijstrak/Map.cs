using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] public int width;
    [SerializeField] public int height;
    [SerializeField] public Tile[,] tiles;
    [SerializeField] public GameObject[] tilePrefabs;

    private void Start()
    {
        GenerateMap();
        GenerateMapWithDijkstra(0, 0);
        GenerateTiles();
    }

    private void GenerateMap()
    {
        if (tiles == null)
        {
            tiles = new Tile[width, height];
        }
    }
    private void GenerateTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int prefabIndex = tiles[x, y].walkable ? 0 : 1; // El �ndice 0 es para tiles caminables, el �ndice 1 es para tiles de obst�culos
                GameObject tilePrefab = tilePrefabs[prefabIndex];

                GameObject tileGO = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                tileGO.transform.SetParent(transform); // Asegura que los tiles son hijos del objeto Map
            }
        }
    }

    public void GenerateMapWithDijkstra(int startX, int startY)
    {
        // No es necesario inicializar 'tiles' nuevamente aqu�, ya se hizo en Start().

        // L�gica de generaci�n de mapa procedural o manual

        int[,] distances = Dijkstra.CalculateDistances(tiles, startX, startY);

        // Utiliza las distancias para influir en la generaci�n del mapa
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bool isWalkable = tiles[x, y].walkable && distances[x, y] < int.MaxValue;
                tiles[x, y] = new Tile(isWalkable);
            }
        }
    }
}

