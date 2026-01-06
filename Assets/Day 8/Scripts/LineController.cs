using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;

    [SerializeField] private Transform[] cubeLocations;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = cubeLocations.Length;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cubeLocations.Length; i++)
        {
            lr.SetPosition(i,cubeLocations[i].position);
        }
    }
}
