using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Assets;
using System;
using System.Text;
using System.Globalization;

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

        //DebugObtenerResultadoBusqueda("Deltoide");

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

    public List<String> ObtenerRessultadosBusqueda(string PalabraBusqueda)
    {
        //PalabraBusqueda = PalabraBusqueda.ToLower();///VERSION PARA ESTANDARIZACION EN XML
        //PalabraBusqueda = PalabraBusqueda.ToUpper();///VERSION PARA ESTANDARIZACION EN XML

        List<String> ActividadesResultadoTemp = new List<String>();

        Actividades = ActividadController.ListarModelosPadre("Actividades.xml");

        foreach(ModeloPadre actividad in Actividades)
        {
            bool EncontradoEnTags = false;
            foreach (String tag in actividad.ListTags)
            {
                string PalabraBusquedatemp = PalabraBusqueda;

                if (tag.Contains(PalabraBusqueda))
                {
                    PalabraBusqueda = PalabraBusquedatemp;
                    ActividadesResultadoTemp.Add(actividad.ActividadId);
                    EncontradoEnTags = true;
                    break;
                }///VERSION PARA ESTANDARIZACION EN XML

                PalabraBusqueda = PalabraBusqueda.ToLower();

                if (tag.Contains(PalabraBusqueda))
                {
                    PalabraBusqueda = PalabraBusquedatemp;
                    ActividadesResultadoTemp.Add(actividad.ActividadId);
                    EncontradoEnTags = true;
                    break;
                }
                else if (tag.Contains(RemoveDiacritics(PalabraBusqueda)))
                {
                    PalabraBusqueda = PalabraBusquedatemp;
                    ActividadesResultadoTemp.Add(actividad.ActividadId);
                    EncontradoEnTags = true;
                    break;
                }

                PalabraBusqueda = PalabraBusqueda.ToUpper();

                if (tag.Contains(PalabraBusqueda))
                {
                    PalabraBusqueda = PalabraBusquedatemp;
                    ActividadesResultadoTemp.Add(actividad.ActividadId);
                    EncontradoEnTags = true;
                    break;
                }
                else if (tag.Contains(RemoveDiacritics(PalabraBusqueda)))
                {
                    PalabraBusqueda = PalabraBusquedatemp;
                    ActividadesResultadoTemp.Add(actividad.ActividadId);
                    EncontradoEnTags = true;
                    break;
                }

                PalabraBusqueda = PalabraBusquedatemp;
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

    private string RemoveDiacritics(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    //private void 
}
