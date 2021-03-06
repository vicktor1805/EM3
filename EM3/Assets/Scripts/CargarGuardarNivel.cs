﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets;
using System.Collections.Generic;
using System.Linq;
using System;

public class CargarGuardarNivel : MonoBehaviour {

    GameObject Parent;
    public InputField fileName;

	void Start () {

        //csVariablesGlobales.ActividadXML = "PC2_A5a";//this line is for edition time
        Parent = GameObject.Find("Humano");
        List<ModeloOrgano> listaActivos = new List<ModeloOrgano>();

        var ph = new PersistenceHelper();
        listaActivos = ph.CargarNivelXML(csVariablesGlobales.ActividadXML + ".xml");

        var allChildrens = Parent.GetComponentsInChildren(typeof(Transform), true);

        var camara = ph.getCamaraScene(csVariablesGlobales.ActividadXML + ".xml");
        Vector3 pos = camara.pos;
        Camera.main.transform.position = pos;
        Camera.main.transform.rotation = camara.rot;
        csVariablesGlobales.ZoomCamera = camara.ZoomCamara;
        Camera.main.orthographicSize = camara.ZoomCamara; // this line is for edition time
        foreach (ModeloOrgano organo in listaActivos)
        {
            for (int j = 0; j < allChildrens.Length; ++j)
            {
                if (organo.ModeloOrganoId.Equals(allChildrens[j].name))
                {
                    allChildrens[j].gameObject.SetActive(true);
                    allChildrens[j].gameObject.transform.position = organo.pos;
                    allChildrens[j].gameObject.transform.rotation = organo.rot;
                }
            }
        }

	}

	void Update () {

        if (Input.GetKeyDown(KeyCode.F9))
        {
            var ph = new PersistenceHelper();
            String fName = fileName.textComponent.text;
            //String fName = "PC1_A1";
            if (!fName.Equals(""))
            {
                ph.GuardarNivelXML(fName);
            }
        }
	}
}
