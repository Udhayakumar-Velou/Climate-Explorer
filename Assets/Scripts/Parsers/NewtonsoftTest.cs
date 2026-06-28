using UnityEngine;
using Newtonsoft.Json.Linq;

public class NewtonsoftTest : MonoBehaviour
{
    void Start()
    {
        JObject obj = new JObject();
        obj["name"] = "Unity";

        Debug.Log(obj.ToString());
    }
}