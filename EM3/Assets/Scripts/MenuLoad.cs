﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Assets;

public class MenuLoad : MonoBehaviour {

    private ButtonController controller;
    public GameObject ButtonUnidad;
    public GameObject ButtonCapitulo;
	public GameObject ButtonActividad;
	public GameObject Cargando;
	public GameObject Unidades;
	public GameObject Capitulos;
    public GameObject MenuModelo;
    public GameObject Header;
    public GameObject Background;
    private GameObject ObjetoUnidades;



	void Start () {

        controller = new ButtonController();
        MostrarSemanas();
	}

    void MostrarSemanas()
    {
        List<Semana> lstSemanas = controller.ListarSemanas();
        DestruirHijos("Unidades");

        foreach(Semana semana in lstSemanas)
        {
            GameObject nuevoBotonUnidad = Instantiate(ButtonUnidad) as GameObject;
			nuevoBotonUnidad.name = semana.SemanaId;
			nuevoBotonUnidad.GetComponentInChildren<Text>().text = "";
            nuevoBotonUnidad.GetComponent<Image>().sprite = Resources.Load<Sprite>(semana.SemanaId);
            nuevoBotonUnidad.GetComponent<Button>().onClick.AddListener(() => { MostrarTemas(nuevoBotonUnidad.name); SetButtons(nuevoBotonUnidad.GetComponent<Button>()); });
            nuevoBotonUnidad.transform.SetParent(ObjetoUnidades.transform,false);

            if(nuevoBotonUnidad.name.Equals("S1") )
            {
                nuevoBotonUnidad.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }

        }
		MostrarTemas (lstSemanas[0].SemanaId);
        
    }

    void MostrarTemas(string pName)
    {
		Semana semana = controller.GetSemana(pName);
        List<Tema> lstTemas = semana.ListTemas;

        DestruirHijos("Capitulos");

        foreach (Tema tema in lstTemas)
        {
            GameObject nuevoButtonCapitulo = Instantiate(ButtonCapitulo) as GameObject;
			nuevoButtonCapitulo.name = tema.TemaId;
			nuevoButtonCapitulo.GetComponentInChildren<Text>().text = tema.Descripcion;
            nuevoButtonCapitulo.transform.SetParent(ObjetoUnidades.transform, false);
			var Panel = nuevoButtonCapitulo.transform.GetComponentsInChildren<Image>(true);
            float ParentWidth = nuevoButtonCapitulo.transform.parent.GetComponent<GridLayoutGroup>().cellSize.x;
            print(ParentWidth);

			foreach(Actividad actividad in tema.ListActividad)
			{
				GameObject nuevoButtonActividad = Instantiate(ButtonActividad) as GameObject;
                nuevoButtonActividad.GetComponent<LayoutElement>().preferredWidth = ParentWidth;
				nuevoButtonActividad.name = actividad.ActividadId;
				nuevoButtonActividad.GetComponentInChildren<Text>().text = actividad.ActividadId;
				nuevoButtonActividad.GetComponent<Button>().onClick.AddListener(() => { CargarModelo( nuevoButtonCapitulo.name + "_" + nuevoButtonActividad.name);});
				nuevoButtonActividad.transform.SetParent(Panel[1].transform,false);
			}
        }
    }

    void MostrarActividades(string pName)
    { 
        Semana semana = controller.ObtenerSemana(pName);
        Tema tema = controller.ObtenerTema(pName);
		DestruirHijos ("Capitulos");
		MostrarTemas (pName);
    }

    void DestruirHijos(string name)
    {
        ObjetoUnidades = GameObject.Find(name);
        var BotonesUnidad = new List<GameObject>();
        foreach (Transform child in ObjetoUnidades.transform)
            BotonesUnidad.Add(child.gameObject);
        BotonesUnidad.ForEach(child => Destroy(child));
    }

	void CargarModelo(string NombreActividad)
	{
		Cargando.SetActive (true);
		CambioEscena (false);
		StartCoroutine (CargarModeloAsync (NombreActividad));
	}

	IEnumerator CargarModeloAsync(string actividad)
	{
		csVariablesGlobales.ActividadXML = actividad;
		AsyncOperation async = Application.LoadLevelAdditiveAsync("EscenaEditor");
		yield return async;

		if (Cargando != null)
			Cargando.SetActive(false);
	}

    public void Back()
    {
        Destroy(Camera.main.GetComponent<MouseOrbitC>());
        CambioEscena(true);
        MostrarSemanas();
        csVariablesGlobales.ObjetosActividad.ForEach(x => Destroy(x));
    }

	void CambioEscena(bool estado)
	{
		Unidades.SetActive (estado);
		Capitulos.SetActive (estado);
        MenuModelo.SetActive(!estado);
        Header.SetActive(estado);
        Background.SetActive(estado);      
	}

    public void SetButtons(Button button)
    {
        var botones = Unidades.GetComponentsInChildren<Image>();
        foreach (var item in botones)
        {
            if (!item.name.Equals("Unidades"))
            {
                if (item.name.Equals(button.name))
                {
                    item.color = new Color(0.5f, 0.5f, 0.5f);
                }
                else
                {
                    item.color = new Color(255, 255, 255);
                }
            }
        }
    }
}
