using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathService : MonoBehaviour
{
    [Range(3, 360)]
    public int segment;
    public Path path;
    LineRenderer lr;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        GeneratePath();
    }

    private void GeneratePath()
    {
        Vector3[] points = new Vector3[segment + 1];
        for(int i=0;i<segment;i++)
        {
            Vector2 pos2D = path.Evaluate((float)i / (float)segment);
            points[i] = new Vector3(pos2D.x, 0f , pos2D.y);
        }

        points[segment] = points[0];
        lr.positionCount = segment + 1;
        lr.SetPositions(points);
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
            GeneratePath();
    }
}
