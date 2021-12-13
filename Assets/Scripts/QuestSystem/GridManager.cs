using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float[,] Grid;
    float Vertical, Horizontal, Columns, Rows;
    Vector3 Begin;
    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        Columns = 3;
        Rows = 18;
        StartCoroutine(SetGridWidth());
        Grid = new float[3, 18];
        
    }
    private void SpawnTile(int x, int y, float value)
    {
        GameObject g = new GameObject("X: " + x + "Y: " + y);
        g.transform.position = new Vector3(Begin.x + (x * Horizontal), Begin.y + (y * Horizontal));
        var s = g.AddComponent<SpriteRenderer>();
        s.sprite = sprite;
        s.color = new Color(value, value, value);
        s.size = new Vector2(Horizontal, Vertical);
    }
    public IEnumerator SetGridWidth()
    {
        yield return new WaitForEndOfFrame();
        Vertical = transform.GetComponent<SpriteRenderer>().bounds.size.x / Columns;
        Horizontal = transform.GetComponent<SpriteRenderer>().bounds.size.y / Rows;
        Begin = GetComponent<SpriteRenderer>().transform.position;
        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                Grid[i, j] = Random.Range(0.0f, 1.0f);
                SpawnTile(i, j, Grid[i, j]);
            }
        }
    }
}
