using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Assets;

public class SearchScript : MonoBehaviour {

    private PersistenceHelper ph;
    private List<ModeloDescripcion> diccionario;
	// Use this for initialization
	void Start () {

        ph = new PersistenceHelper();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private List<ModeloDescripcion> CargarDiccionario()
    {
        diccionario = ph.CargarDescripciones();//se puede cargar el xml que sirva de diccionario

        Debug.Log("Number of descriptions on XML file: " + diccionario.Count);

        return diccionario;
    }

    //private void 
}
