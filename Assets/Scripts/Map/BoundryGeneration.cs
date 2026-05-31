using UnityEngine;

public class BoundryGeneration : MonoBehaviour
{

    public float height = 1000f;
    public float thickness = .5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform[] nodes = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            nodes[i] = transform.GetChild(i);
        
        for (int i = 0;i < nodes.Length - 1; i++)
        {
            CreateWallSegament(nodes[i].position, nodes[i + 1].position);
        }
    }

   void CreateWallSegament(Vector3 start, Vector3 end)
    {
        GameObject wall = new GameObject("Boundry");
        wall.transform.parent = transform;

        wall.transform.position = (start + end) / 2f;
        wall.transform.LookAt(end);

        float distance = Vector3.Distance(start, end);
        wall.transform.localScale = new Vector3(thickness, height, distance);


        wall.AddComponent<BoxCollider>();
    }
}
