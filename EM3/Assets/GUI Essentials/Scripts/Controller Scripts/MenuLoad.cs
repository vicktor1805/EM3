using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;

public class MenuLoad : MonoBehaviour {

    private ButtonController controller;
    public GameObject ButtonUnidad;
    public GameObject ButtonCapitulo;
    private GameObject ObjetoUnidades;
	// Use this for initialization
	void Start () {

        controller = new ButtonController();
        MostrarSemanas();
        MostrarTemas("PC2");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void MostrarSemanas()
    {
        List<Semana> lstSemanas = controller.ListarSemanas();

        DestruirHijos("Unidades");

        foreach(Semana semana in lstSemanas)
        {
            GameObject nuevoBotonUnidad = Instantiate(ButtonUnidad) as GameObject;
            nuevoBotonUnidad.transform.SetParent(ObjetoUnidades.transform,false);
        }
    }
    void MostrarTemas(string pName)
    {
        Semana semana = controller.ObtenerSemana(pName);
        List<Tema> lstTemas = controller.ListarTemas(pName);

        DestruirHijos("Capitulos");

        foreach (Tema tema in lstTemas)
        {
            GameObject nuevoButtonCapitulo = Instantiate(ButtonCapitulo) as GameObject;
            nuevoButtonCapitulo.transform.SetParent(ObjetoUnidades.transform, false);
        }
    }

    void MostrarActividades(string pName)
    {
        Semana semana = controller.ObtenerSemana(pName);
        Tema tema = controller.ObtenerTema(pName);
    }

    void DestruirHijos(string name)
    {
        ObjetoUnidades = GameObject.Find(name);
        var BotonesUnidad = new List<GameObject>();
        foreach (Transform child in ObjetoUnidades.transform)
            BotonesUnidad.Add(child.gameObject);
        BotonesUnidad.ForEach(child => Destroy(child));
    }
}
