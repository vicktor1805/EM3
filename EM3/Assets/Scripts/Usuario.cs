using System;

public class Usuario
{
	int IDUsuaruo { get; set; }
	string CodUsuario { get; set; }
	string Nombres {get; set;}
	string ApellidoPaterno { get; set; }
	string ApellidoMaterno { get; set; }
	string Carrera { get; set; }
	string Password { get; set; }
	string MacAddress { get; set; }
	string Email { get; set; }
	string Telefono { get; set;}
	string Celular { get; set; }
	String Rol { get; set;}

	public Usuario ()
	{
	}

	public Usuario(string CodUsuario, string Password)
	{
		this.CodUsuario = CodUsuario;
		this.Password = Password;
	}
}
