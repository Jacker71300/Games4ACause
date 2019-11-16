using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] path;
    public float speed;

    private int currentPathPoint;
    // Start is called before the first frame update
    void Start()
    {
        currentPathPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        DrawPath();
        if(MoveTowards(path[currentPathPoint], speed))
        {
            currentPathPoint += 1;
            if (currentPathPoint >= path.Length)
            {
                currentPathPoint = 0;
            }
        }
    }

    private bool MoveTowards(Vector3 point, float speed)
    {
        if((point - transform.position).sqrMagnitude <= speed * speed * Time.deltaTime * Time.deltaTime)
        {
            transform.position = point;
            return true;
        } else
        {
            Vector3 direction = (point - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;
            return false;
        }
    }

    private void DrawPath()
    {
        Vector3 prevPoint = path[path.Length - 1];
        foreach(Vector3 pathPoint in path)
        {
            Debug.DrawLine(pathPoint, prevPoint, Color.white);
            Color drawColor;
            if(pathPoint == path[currentPathPoint])
            {
                drawColor = Color.red;
            } else
            {
                drawColor = Color.green;
            }
            Debug.DrawLine(pathPoint + new Vector3(-0.1f, 0.1f), pathPoint + new Vector3(0.1f, -0.1f), drawColor);
            Debug.DrawLine(pathPoint + new Vector3(-0.1f, -0.1f), pathPoint + new Vector3(0.1f, 0.1f), drawColor);
            prevPoint = pathPoint;
        }
    }
}
