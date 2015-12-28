using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Assets.Scripts.Model;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;


public class Util : MonoBehaviour {

    private static Util _instance;
    public readonly string LOGO_BIT = "Logo_BIT";
    public readonly string LOGO_EXPOTIC = "Logo_Expotic";
    public readonly string IMAGES_PARENT = "Image";
    public readonly string SCENE_0 = "Scene0";
    public readonly string SCENE_1 = "Scene1";
    public readonly string SCENE_2 = "Scene2";
    public readonly string SCENE_3 = "Scene3";
    private readonly string METHOD_GET = "GET";
    public readonly string CONTAINER = "Container";
    public readonly string CONTAINER_GRID_ROMMS = "grvRooms";
    public readonly string CONTAINER_HEADER = "Header";
    public readonly string FONT_SOLANOGOTHIC_MVB_MDCAP = "Fonts/SolanoGothicMVB-MdCap";
    public readonly string FONT_MONTSERRAT_BOLD = "Fonts/Montserrat-Bold";
    public readonly string ROOM_NAME = "RoomName";
    public readonly string ROOM_NAME_CONTAINER = "RoomNameContainer";
    public readonly string ERROR_UI = "ErrorBackground";
    public readonly string BUTTON_CREAR = "btnCrear";
    public readonly string BUTTON_UNIRSE = "btnUnirse";
    public readonly string MESSAGE_NO_NAME_ROOM = "Ingrese un nombre para el Room";
    public readonly string PLAYER_LIST_PANEL = "PanelList";
    public readonly string PLAYER_NAME = "txtNombreJugador";
    public readonly string PLAYER_NAME_ERROR = "ErrorName";
    public int OPERATION;
    public int currentID = -1;
    public bool isLoggedIn = false;	
    public bool isConnected = false;
    //public List<Player> listPlayers;
	public List<GameObject> listCards;
	public Vector3[] StartPosition;
	public Vector3[] StartRotaion;

	void Start()
    {
		StartPosition = new [] {new Vector3(-0.1f,2.5f,-9.8f),new Vector3(4.6f,2.5f,-5.8f),new Vector3(-0.1f,2.5f,-1.86f),new Vector3(-5f,2.5f,-5.8f)};
		StartRotaion = new [] { new Vector3(36f,0,0),new Vector3(36f,270f,0),new Vector3(36f,180f,0),new Vector3(36f,90f,0)};
    }
    public static Util instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Util>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public void LoadScene(string name)
    {
        Application.LoadLevel(name);
    }

    public T GetJson<T>(string url)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = METHOD_GET;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receive = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receive, System.Text.Encoding.UTF8);
            string res = readStream.ReadToEnd();

            T objeto = JsonConvert.DeserializeObject<T>(res);
            return objeto;
        }
        catch (HttpListenerException ex)
        {
            OPERATION = 0;
            print("[ERROR]: Hubo un error recolectando la data: " + ex.Message);
            return default(T);
        }
    }

    public bool TestInternetConnection(GameObject go)
    {
        //HttpWebRequest request = null; 
        //try
        //{
        //    request = (HttpWebRequest)WebRequest.Create("http://peru21.pe/");
        //    // request.Timeout = 5;
        //    return true;
        //}
        //catch (System.Exception ex)
        //{
        //    ShowPanelHandleUIExeption("There is no internet connection: " + ex.Message, go);
        //    print("There is no internet connection: " + ex.Message);
        //    return false;
        //}
        return true;
    }

    public void ShowPanelHandleUIExeption(string message,GameObject go)
    {
        go.SetActive(true);
        go.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = message;
    }

    public void HidePanelHandleUIExeption(GameObject go)
    {
        if (OPERATION == 0)
        {
            Application.Quit();
        }
        if (OPERATION == 1)
        {
            go.SetActive(false);
            OPERATION = 0;
        }
    }

    //public Grid ParseRoomArrayToList(RoomInfo[] rooms)
    //{
    //    if (rooms.Length > 0)
    //    {
    //        Grid temp = new Grid();
    //        List<Row> filas = new List<Row>();
    //        int cont = 1;

    //        Row rowheader = new Row();
    //        Columna header = new Columna();
    //        header.id = "ID";
    //        header.nombre = "Sala";
    //        header.jugadores = "N° de jugadores";
    //        rowheader.columna = header;
    //        filas.Add(rowheader);

    //        foreach (RoomInfo room in rooms)
    //        {
    //            Row rowTemp = new Row();
    //            Columna colTemp = new Columna();
    //            colTemp.id = cont.ToString();
    //            colTemp.nombre = room.name;
    //            colTemp.jugadores = room.playerCount.ToString();
    //            rowTemp.columna = colTemp;
    //            filas.Add(rowTemp);
    //            cont++;
    //        }

    //        temp.filas = filas;
    //        return temp;
    //    }

    //    return null;
    //}

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{

    //    //if (stream.isWriting)
    //    //{
    //    //    stream.SendNext(listPlayers);
    //    //}
    //    //else
    //    //{
    //    //    listPlayers = (List<Player>)stream.ReceiveNext();
    //    //}
    //}

    //[PunRPC]
    //public void UpdateListOfPlayers(string data)
    //{
    //    this.listPlayers = DeserializeList<Player>(data);
    //}

    public string  SerializeList<T>(List<T> listplayers)
    {
        MemoryStream holdData = new MemoryStream();
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(holdData, listplayers);
        return Convert.ToBase64String(holdData.GetBuffer());
    }

    public List<T> DeserializeList<T>(string data)
    {
        List<T> list = new List<T>();

        MemoryStream holdData = new  MemoryStream(Convert.FromBase64String(data));
        BinaryFormatter bf = new BinaryFormatter();
        list = bf.Deserialize(holdData) as List<T>; 
        return list;
    }

	public List<T> UnOrderList<T>(List<T> input)
	{
		List<T> arr = input;
		List<T> arrDes = new List<T>();
		int cont = 0;

		System.Random randNum = new System.Random();
		while (arr.Count > 0)
		{
			int val = randNum.Next(0, arr.Count - 1);
			arrDes.Add(arr[val]);
			arr.RemoveAt(val);
			cont++;
		}
		return arrDes;
	}
}
