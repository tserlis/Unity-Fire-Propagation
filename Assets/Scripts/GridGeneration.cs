using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGeneration : MonoBehaviour
{

    public Terrain terrain; //terrain grid is attached to
    public bool ShowGrid = false;
    public int CellSize = 10;

    private Vector3 terrainSize;
    private Vector3 origin;

    private int width;
    private int height;

    private List<GameObject> objects;


    // Start is called before the first frame update
    void Start()
    {
        terrainSize = terrain.terrainData.size;
        origin = terrain.transform.position;

        width = (int)terrainSize.z / CellSize;
        height = (int)terrainSize.x / CellSize;

        objects = new List<GameObject>();

        BuildGrid();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject obj in objects)
            obj.SetActive(ShowGrid);
    }

    void BuildGrid()
    {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Vector3 pos = GetWorldPosition(new Vector2(x,y));
                pos += new Vector3(CellSize / 2, terrain.SampleHeight(GetWorldPosition(new Vector2(x,y))), CellSize / 2);
                go.transform.position = pos;
                if (x == 0) {
                    go.GetComponent<Renderer>().material.color = Color.red;
                }
                if (y == 0) {
                    go.GetComponent<Renderer>().material.color = Color.green;
                }

                go.transform.localScale = new Vector3(CellSize / 2, CellSize / 2, CellSize / 2);
                go.transform.parent = transform;
                go.SetActive(false);


                objects.Add(go);
            }
        }
    }

    public Vector3 GetWorldPosition(Vector2 gridPosition) 
    {
        return new Vector3(origin.z + (gridPosition.x * CellSize), origin.y, origin.x + (gridPosition.y * CellSize));
    }

    public Vector2 GetGridPosition(Vector3 worldPosition)
    {
        return new Vector2(worldPosition.z / CellSize, worldPosition.x / CellSize);
    }
}
