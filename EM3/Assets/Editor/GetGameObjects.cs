using UnityEngine;
using UnityEditor;
using System.IO;

public class GetGameObjects : Editor
{

    const string menuName = "GameObject/Generate Text File";

    [MenuItem(menuName)]
    static void CreatePrefabMenu()
    {
        var go = Selection.activeGameObject;
        var obj = go.GetComponentsInChildren(typeof(Transform), true);

        string path = "Nombres.txt";
        TextWriter f = new StreamWriter(path);

        foreach (var item in obj)
        {
            GameObject g = item.gameObject;
            if (item.transform.childCount == 0)
            {
                f.WriteLine(g.name);
            }


        }
        f.Close();
    }

}

