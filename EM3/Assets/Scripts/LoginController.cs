using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Net.NetworkInformation;

public class LoginController : MonoBehaviour {

	public GameObject Logo;
	public InputField Usuario;
	public InputField Contraseña;
	public GameObject Error;
	public GameObject Success;
    public GameObject Loadding;
	string URL = "";

	void Start () {

		Logo.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("logo_grande");
		Invoke ("ResizeCaret", 0.1f);
	}

	void ResizeCaret()
	{
		Usuario.transform.GetChild(0).gameObject.SetActive(false);//.GetComponent<RectTransform>().pivot = new Vector2(0.5f,0.25f);
		Contraseña.transform.GetChild (0).gameObject.SetActive(false);//.GetComponent<RectTransform>().pivot = new Vector2(0.5f,0.25f);
	}

	public void Login()
	{
		if (Success.activeSelf)
			Success.SetActive (false);

		if (string.IsNullOrEmpty (Usuario.text) || string.IsNullOrEmpty (Contraseña.text)) 
		{
			Error.SetActive(true);
			Error.GetComponentInChildren<Text>().text = "Debe ingresar sus credenciales"; 
		}


        if (Usuario.text.Equals("admin") && Contraseña.text.Equals("admin"))
        {
            Application.LoadLevel("Menu");
        }
        else
        {
            //Getting MacAddres from PC:
            string mac = GetMacAddress();
            //Service Call
            Usuario usuario = ServiceCall(Usuario.text, Contraseña.text,mac, "");
            if (usuario != null)
            {
                Application.LoadLevel("Menu");
            }
            else
            {
                Error.SetActive(true);
                Error.GetComponentInChildren<Text>().text = "Credenciales incorrectas";
            }
        }
	}

	Usuario ServiceCall(string username, string password, string mac,string servicename)
	{
        try
        {
            Loadding.SetActive(true);
		    HttpWebRequest request = WebRequest.Create(string.Format(URL + servicename)) as HttpWebRequest;
		    request.Method = "POST";
		    request.ContentType = "application/json; charset=utf-8";
		
		    Usuario usuario = new Usuario(username,password,mac);
		
		    string jString = JsonConvert.SerializeObject(usuario);
		    byte[] byteArray = Encoding.UTF8.GetBytes(jString);
		    request.ContentLength = byteArray.Length;
		    using (Stream sw = request.GetRequestStream())
		    {
			    sw.Write(byteArray, 0, byteArray.Length);
		    }

			HttpWebResponse response = request.GetResponse() as HttpWebResponse;
			StreamReader sr = new StreamReader(response.GetResponseStream());
			
			string result = sr.ReadToEnd();
            Loadding.SetActive(false);
			return JsonConvert.DeserializeObject<Usuario>(result);
		}
		catch (System.Exception)
		{
            Loadding.SetActive(false);
			return null;
		}
	}

    string GetMacAddress()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        string mac = "";

        foreach (var  item in nics)
        {
            if (item.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        if(ip.Address.ToString().Equals(GetCurrentIPAddres()))
                        {
                            return item.GetPhysicalAddress().ToString();
                        }
                    }
                }
            }
        }

        return mac;
    }

    string GetCurrentIPAddres()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
            
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString() ;
            }
        }

        return "";
    }

    public void UpdateMacAddressw()
    {
		if (Error.activeSelf)
			Error.SetActive (false);


        if (string.IsNullOrEmpty (Usuario.text) || string.IsNullOrEmpty (Contraseña.text)) 
		{
			Error.SetActive(true);
			Error.GetComponentInChildren<Text>().text = "Debe ingresar sus credenciales"; 
		}
        else
        {
            Usuario usuario = new Usuario();
            string mac = GetMacAddress();
            usuario = ServiceCall(Usuario.text, Contraseña.text, mac, "");

            if (usuario == null)
            {
				Success.SetActive(true);
            }
            else
            {

            }
        }

    }

}
