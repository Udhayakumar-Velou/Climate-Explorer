using System;
using System.Collections.Generic;

[Serializable]
public class GeoJsonRoot
{
    public string type;
    public List<GeoJsonFeature> features;
}

[Serializable]
public class GeoJsonFeature
{
    public string type;

    // Country ISO code (AFG, FRA, IND...)
    public string id;

    public GeoJsonProperties properties;

    public GeoJsonGeometry geometry;
}

[Serializable]
public class GeoJsonProperties
{
    // Country name
    public string name;
}

[Serializable]
public class GeoJsonGeometry
{
    public string type;
}