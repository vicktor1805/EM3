using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text;

public class LoginController : MonoBehaviour {

	public GameObject Logo;
	public InputField Usuario;
	public InputField Contraseña;
	public GameObject Error;
	bool start = false;
	string URL = "";

	void Start () {

		Logo.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("logo_grande");
		start = true;
		Invoke ("ResizeCaret", 0.1f);
	}

	void ResizeCaret()
	{
		Usuario.transform.GetChild(0).GetComponent<RectTransform>().pivot = new Vector2(0.5f,0.25f);
		Contraseña.transform.GetChild(0).GetComponent<RectTransform>().pivot = new Vector2(0.5f,0.25f);
		start = false;
	}

	public void Login()
	{
		if (string.IsNullOrEmpty (Usuario.text) || string.IsNullOrEmpty (Contraseña.text)) 
		{
			Error.SetActive(true);
		}
		//Service Call
		Usuario usuario = Authentication (Usuario.text, Contraseña.text, "");
		if (usuario != null) 
		{
			Application.LoadLevel ("");
		} 
		else 
		{
			Error.SetActive(true);
			Error.GetComponentInChildren<Text>().text = "Credenciales incorrectas";
		}
	}

	Usuario Authentication(string username, string password,string servicename)
	{
		
		HttpWebRequest request = WebRequest.Create(string.Format(URL + servicename)) as HttpWebRequest;
		request.Method = "POST";
		request.ContentType = "application/json; charset=utf-8";
		
		Usuario usuario = new Usuario(username,password);
		
		string jString = JsonConvert.SerializeObject(usuario);
		byte[] byteArray = Encoding.UTF8.GetBytes(jString);
		request.ContentLength = byteArray.Length;
		using (Stream sw = request.GetRequestStream())
		{
			sw.Write(byteArray, 0, byteArray.Length);
		}
		
		try
		{
			HttpWebResponse response = request.GetResponse() as HttpWebResponse;
			StreamReader sr = new StreamReader(response.GetResponseStream());
			
			string result = sr.ReadToEnd();
			return JsonConvert.DeserializeObject<Usuario>(result);
		}
		catch (System.Exception)
		{
			return null;
		}
	}
}
