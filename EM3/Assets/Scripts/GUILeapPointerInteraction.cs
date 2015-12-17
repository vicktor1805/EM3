using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GUILeapPointerInteraction : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject cameraEditor;
    public GameObject Pivot;
    public GameObject prefabGUI;
    public float velocidad = 80;
    public float movementSensitivity = 0.02f;
    public float rearmVelocity = 0.09f;

    private float xDeg;
    private float yDeg;
    public int speed;
    private GameObject modelo;
    private Vector3 starPosition;
    private Quaternion startRotation;
    private Vector2 startScale;
    private bool resize;
    private bool loaded;
    private Vector3[] subModels;
    private Quaternion[] quad;

    IEnumerator CaragarModelo(string NombreSistema)
    {
        AsyncOperation async = Application.LoadLevelAdditiveAsync(NombreSistema);
        yield return async;
        loaded = true;
    }

    void Awake()
    {
        csVariablesGlobales.HabilitarDesarme = true;
        csVariablesGlobales.HabilitarDesplazamiento = true;
        csVariablesGlobales.HabilitarGiro = true;
        csVariablesGlobales.HabilitarZoom = true;
        modelo = GameObject.FindGameObjectWithTag("Modelo");
        startRotation = modelo.transform.rotation;
        startScale = modelo.transform.localScale;
    }

    void Start()
    {
        loaded = true;
        resize = false;

		csVariablesGlobales.ObjetosActividad = new List<GameObject> ();
        csVariablesGlobales.ObjetosActividad.Add(modelo);
        csVariablesGlobales.ObjetosActividad.Add(prefabGUI);
        csVariablesGlobales.ObjetosActividad.Add(Canvas);
        csVariablesGlobales.ObjetosActividad.Add(cameraEditor);
        Debug.Log("Objects activity to destroy: " + csVariablesGlobales.ObjetosActividad.Count);
        Camera.main.gameObject.AddComponent<MouseOrbitC>();
    }

    void setListSubModels()
    {
        var listChild = modelo.GetComponentsInChildren<Transform>();
        subModels = new Vector3[listChild.Length];
        quad = new Quaternion[listChild.Length];
        for (int i = 0; i < listChild.Length; ++i)
        {
            subModels[i] = listChild[i].gameObject.transform.position;
            quad[i] = listChild[i].rotation;
        }
    }

    void FixedUpdate()
    {
        if (resize)
        {
            float step = velocidad * Time.deltaTime;
            var listChild = modelo.GetComponentsInChildren<Transform>();

            for (int i = 0; i < listChild.Length; ++i)
            {
                var gameObject = listChild[i].gameObject;
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, subModels[i], step * rearmVelocity);
                gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, quad[i], step * rearmVelocity);

            }
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, starPosition, step * rearmVelocity);
            modelo.transform.rotation = Quaternion.RotateTowards(modelo.transform.rotation, startRotation, step * rearmVelocity);
        }
    }

    void LateUpdate()
    {
        if (loaded)
        {
            starPosition = Camera.main.transform.position;
            setListSubModels();
            loaded = false;
        }

        float step = velocidad * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R))
        {
            resize = true;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            resize = false;
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0) 
        {
            Camera.main.orthographicSize -= 0.35f;

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize += 0.35f;

        }

    }
}
