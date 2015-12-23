using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Assets;
using System;

public class SearchScript : MonoBehaviour {

    private PersistenceHelper PersistanceHelper;
    private List<ModeloDescripcion> Diccionario;
    private List<String> PalabrasTraducidas;
    private List<String> PracticasResultado;
	// Use this for initialization
	void Start () {

        PersistanceHelper = new PersistenceHelper();
        Diccionario = new List<ModeloDescripcion>();
        PalabrasTraducidas = new List<String>();
        PracticasResultado = new List<String>();
        DebugObtenerTraduccion("Corazon");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private List<ModeloDescripcion> CargarDiccionario()
    {
        List<ModeloDescripcion> DiccionarioTemp;

        DiccionarioTemp = PersistanceHelper.CargarDescripciones();//se puede cargar el xml que sirva de diccionario

        Debug.Log("Number of descriptions on XML file: " + DiccionarioTemp.Count);

        return DiccionarioTemp;
    }

    private List<String> ObtenerTraduccion(string PalabraBusqueda)
    {
        List<String> PalabrasTraducidasTemp = new List<string>();

        Diccionario = CargarDiccionario();

        foreach(ModeloDescripcion desc in Diccionario)
        {
            if (desc.Descripcion.Contains(PalabraBusqueda))
            {
                PalabrasTraducidasTemp.Add(desc.Nombre);
            }
        }

        return PalabrasTraducidasTemp;
    }

    private void DebugObtenerTraduccion(string PalabraBusqueda)
    {
        PalabrasTraducidas = ObtenerTraduccion(PalabraBusqueda);

        foreach(String palabra in PalabrasTraducidas)
        {
            Debug.Log(palabra);
        }
    }

    //private List<String> ObtenerRessultadosBusqueda(string PalabraBusqueda)
    //{
    //    PalabrasTraducidas = ObtenerTraduccion(PalabraBusqueda);


    //}

    //private void 
}
