using UnityEngine;
using System.Collections;
using Assets.Scripts.Model.Interface;

public class UEColumna : MonoBehaviour , IColumna {


    public string id { get; set; }
    public string nombre { get; set; }
    public string jugadores { get; set; }
    public bool clicked { get; set; }
    
	void Start () {
	}

}
