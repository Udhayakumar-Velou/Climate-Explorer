using UnityEngine;
using System.Collections.Generic;

public static class TriangulationTest
{
    public static int[] Triangulate(List<Vector2> points)
    {
        List<int> triangles = new List<int>();

        if (points.Count < 3)
            return triangles.ToArray();

        List<int> verts = new List<int>();

        for (int i = 0; i < points.Count; i++)
            verts.Add(i);

        int guard = 0;

        while (verts.Count > 2 && guard < 5000)
        {
            guard++;

            bool earFound = false;

            for (int i = 0; i < verts.Count; i++)
            {
                int prev = verts[(i - 1 + verts.Count) % verts.Count];
                int curr = verts[i];
                int next = verts[(i + 1) % verts.Count];

                Vector2 a = points[prev];
                Vector2 b = points[curr];
                Vector2 c = points[next];

                if (Cross(a, b, c) <= 0)
                    continue;

                bool containsPoint = false;

                for (int j = 0; j < verts.Count; j++)
                {
                    int p = verts[j];

                    if (p == prev || p == curr || p == next)
                        continue;

                    if (PointInTriangle(points[p], a, b, c))
                    {
                        containsPoint = true;
                        break;
                    }
                }

                if (containsPoint)
                    continue;

                triangles.Add(prev);
                triangles.Add(curr);
                triangles.Add(next);

                verts.RemoveAt(i);

                earFound = true;
                break;
            }

            if (!earFound)
                break;
        }

        return triangles.ToArray();
    }

    static float Cross(Vector2 a, Vector2 b, Vector2 c)
    {
        return
            (b.x - a.x) * (c.y - a.y)
            -
            (b.y - a.y) * (c.x - a.x);
    }

    static bool PointInTriangle(
        Vector2 p,
        Vector2 a,
        Vector2 b,
        Vector2 c)
    {
        float d1 = Sign(p, a, b);
        float d2 = Sign(p, b, c);
        float d3 = Sign(p, c, a);

        bool hasNeg =
            (d1 < 0) ||
            (d2 < 0) ||
            (d3 < 0);

        bool hasPos =
            (d1 > 0) ||
            (d2 > 0) ||
            (d3 > 0);

        return !(hasNeg && hasPos);
    }

    static float Sign(
        Vector2 p1,
        Vector2 p2,
        Vector2 p3)
    {
        return
            (p1.x - p3.x) *
            (p2.y - p3.y)
            -
            (p2.x - p3.x) *
            (p1.y - p3.y);
    }
}