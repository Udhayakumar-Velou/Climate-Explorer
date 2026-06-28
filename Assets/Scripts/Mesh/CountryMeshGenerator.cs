using System.Collections.Generic;
using UnityEngine;
using LibTessDotNet;


public class CountryMeshGenerator : MonoBehaviour
{
    public Material countryMaterial;

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
        {
            return;
        }

        GameObject obj = new GameObject(country.Name);

        obj.transform.SetParent(transform);

        MeshFilter filter =
            obj.AddComponent<MeshFilter>();

        MeshRenderer renderer =
            obj.AddComponent<MeshRenderer>();

        MeshCollider collider =
            obj.AddComponent<MeshCollider>();

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
   
    UnityEngine.Mesh BuildMesh(CountryData country)
    {

        Tess tess = new Tess();
        

        foreach (List<Vector2> border in country.Borders)

        {


            for (int i = 0; i < Mathf.Min(border.Count, 5); i++)

            {

                Vector3 p = ConvertLatLon(border[i]);

            }

        }

        foreach (List<Vector2> border in country.Borders)
        {
            if (border.Count < 3)
                continue;

            ContourVertex[] contour = new ContourVertex[border.Count];

            for (int i = 0; i < border.Count; i++)
            {
                Vector3 p = ConvertLatLon(border[i]);

                contour[i].Position = new Vec3(p.x, p.y, p.z);
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

        int[] triangles = tess.Elements;
       
        mesh.vertices = vertices;
        Vector3[] verts = mesh.vertices;

        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] = new Vector3(
                verts[i].x,
                0f,
                verts[i].y
            );
        }

        mesh.vertices = verts;

        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        Color[] colors = new Color[vertices.Length];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.green;
        }

        mesh.colors = colors;
        return mesh;

    }
    Vector3 ConvertLatLon(Vector2 point)
    {
        float scale = 0.1f;

        float x = point.x * scale;
        float z = point.y * scale;

        return new Vector3(x, z, 0f);
    }
}