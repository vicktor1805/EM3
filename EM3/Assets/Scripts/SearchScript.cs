using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Assets;
using System;

public class SearchScript : MonoBehaviour {

    private PersistenceHelper PersistanceHelper;
    private ActividadController ActividadController;
    private List<ModeloDescripcion> Diccionario;
    private List<String> PalabrasTraducidas;
    private List<String> ActividadesResultado;
    private List<ModeloPadre> Actividades;
	// Use this for initialization
	void Start () {

        PersistanceHelper = new PersistenceHelper();
        ActividadController = new ActividadController();
        Diccionario = new List<ModeloDescripcion>();
        PalabrasTraducidas = new List<String>();
        ActividadesResultado = new List<String>();
        Actividades = new List<ModeloPadre>();

        DebugObtenerResultadoBusqueda("Deltoide");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private List<ModeloDescripcion> CargarDiccionario()
    {
        List<ModeloDescripcion> DiccionarioTemp;

        DiccionarioTemp = PersistanceHelper.CargarDiccionario();//se puede cargar el xml que sirva de diccionario

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

    private void DebugObtenerResultadoBusqueda(string PalabraBusqueda)
    {
        //PalabrasTraducidas = ObtenerTraduccion(PalabraBusqueda);

        ActividadesResultado = ObtenerRessultadosBusqueda(PalabraBusqueda);

        foreach (String palabra in ActividadesResultado)
        {
            Debug.Log(palabra);
        }
    }

    private List<String> ObtenerRessultadosBusqueda(string PalabraBusqueda)
    {
        List<String> ActividadesResultadoTemp = new List<String>();

        Actividades = ActividadController.ListarModelosPadre("Actividades.xml");

        foreach(ModeloPadre actividad in Actividades)
        {
            bool EncontradoEnTags = false;
            foreach (String tag in actividad.ListTags)
            {
                if(tag.Contains(PalabraBusqueda))
                {
                    ActividadesResultadoTemp.Add(actividad.ActividadId);
                    EncontradoEnTags = true;
                    break;
                }
            }
            if (!EncontradoEnTags)
            {
                PalabrasTraducidas = ObtenerTraduccion(PalabraBusqueda);

                foreach (String palabratraducida in PalabrasTraducidas)
                {
                    //Debug.Log("Entro a palabras");
                    foreach (ModeloOrgano organo in actividad.ListOrganos)
                    {
                        //Debug.Log("Entro a organos");
                        if(organo.ModeloOrganoId.Equals(palabratraducida))
                        {
                            ActividadesResultadoTemp.Add(actividad.ActividadId);
                            break;
                        }
                    }
                }
            }
        }
        return ActividadesResultadoTemp;
    }

    //private void 
}
