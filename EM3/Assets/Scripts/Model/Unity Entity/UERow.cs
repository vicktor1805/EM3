using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Model.Interface;

public class UERow : MonoBehaviour , IRow {

    public Columna columna { get; set; }

	void Start () {

        gameObject.AddComponent<HorizontalLayoutGroup>();
        gameObject.GetComponent<HorizontalLayoutGroup>().childForceExpandHeight = false;
        fillRows();
	}

    void fillRows()
    {
        if(columna!=null)
        {
            foreach (var prop in columna.GetType().GetProperties())
            {
                GameObject go = new GameObject("go_" + prop.Name);
                go.transform.parent = this.transform;
                go.AddComponent<Text>();
                go.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                go.GetComponent<Text>().color = Color.black;
                go.GetComponent<Text>().font = Resources.Load<Font>(Util.instance.FONT_SOLANOGOTHIC_MVB_MDCAP);
                go.GetComponent<Text>().fontStyle = FontStyle.Bold;
                go.GetComponent<Text>().text = prop.GetValue(columna, null).ToString();
                go.GetComponent<Text>().fontSize = 25;
                go.GetComponent<RectTransform>().localScale = Vector3.one;
                go.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }
        }
    }
}
