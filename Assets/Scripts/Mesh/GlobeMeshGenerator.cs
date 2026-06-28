using System.Collections.Generic;
using UnityEngine;
using LibTessDotNet;

public class GlobeMeshGenerator : MonoBehaviour
{
    public Material countryMaterial;

    public float globeRadius = 5f;

    public void GenerateCountry(CountryData country)
    {
        if (country == null)
            return;

        if (country.Borders == null)
            return;

        if (country.Borders.Count == 0)
            return;

        UnityEngine.Mesh mesh = BuildMesh(country);

        if (mesh == null)
            return;

        GameObject obj = new GameObject(country.Name);

        obj.transform.SetParent(transform);

        MeshFilter filter = obj.AddComponent<MeshFilter>();

        MeshRenderer renderer = obj.AddComponent<MeshRenderer>();

        MeshCollider collider = obj.AddComponent<MeshCollider>();

        filter.mesh = mesh;

        collider.sharedMesh = mesh;

        if (countryMaterial != null)
        {
            Material mat = new Material(countryMaterial);
            renderer.material = mat;
        }

        CountryBehaviour behaviour =
            obj.AddComponent<CountryBehaviour>();

        behaviour.Country = country;

        country.Mesh = mesh;

        country.GameObject = obj;
    }

    private UnityEngine.Mesh BuildMesh(CountryData country)
    {
        Tess tess = new Tess();

        foreach (List<Vector2> border in country.Borders)
        {
            if (border.Count < 3)
                continue;

            ContourVertex[] contour = new ContourVertex[border.Count];

            for (int i = 0; i < border.Count; i++)
            {
                Vector3 p = LatLonToSphere(border[i]);

                contour[i].Position = new Vec3(
                    p.x,
                    p.y,
                    p.z
                );
            }

            tess.AddContour(contour);
        }

        tess.Tessellate(
            WindingRule.EvenOdd,
            ElementType.Polygons,
            3
        );

        UnityEngine.Mesh mesh = new UnityEngine.Mesh();

        Vector3[] vertices = new Vector3[tess.Vertices.Length];

        for (int i = 0; i < tess.Vertices.Length; i++)
        {
            vertices[i] = new Vector3(
                tess.Vertices[i].Position.X,
                tess.Vertices[i].Position.Y,
                tess.Vertices[i].Position.Z
            );
        }

        mesh.vertices = vertices;

        mesh.triangles = tess.Elements;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }

    private Vector3 LatLonToSphere(Vector2 point)
    {
        float latitude = point.y * Mathf.Deg2Rad;
        float longitude = point.x * Mathf.Deg2Rad;

        float x = globeRadius * Mathf.Cos(latitude) * Mathf.Cos(longitude);

        float y = globeRadius * Mathf.Sin(latitude);

        float z = globeRadius * Mathf.Cos(latitude) * Mathf.Sin(longitude);

        return new Vector3(x, y, z);
    }

}