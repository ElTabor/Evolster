using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject mapPrefab; // Asigna tu prefab de mapa en el editor
    public int mapWidth = 10;
    public int mapHeight = 10;
    public int startX = 0;
    public int startY = 0;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        // Instancia el prefab del mapa
        GameObject mapGO = Instantiate(mapPrefab);
        Map map = mapGO.GetComponent<Map>(); // Obtén el componente Map del objeto

        // Configura el mapa
        map.width = mapWidth;
        map.height = mapHeight;

        // Genera el mapa con Dijkstra
        map.GenerateMapWithDijkstra(startX, startY);

        // Configura la posición y escala según tus necesidades
        mapGO.transform.position = Vector3.zero;
        mapGO.transform.localScale = Vector3.one;
    }
}

