using System;

public class Usuario
{
	public int IDUsuaruo { get; set; }
    public string CodUsuario { get; set; }
    public string Nombres { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    public string Carrera { get; set; }
    public string Password { get; set; }
    public string MacAddress { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public string Celular { get; set; }
    public string Rol { get; set; }

	public Usuario ()
	{
	}

	public Usuario(string CodUsuario, string Password, string MacAddress)
	{
		this.CodUsuario = CodUsuario;
		this.Password = Password;
        this.MacAddress = MacAddress;
	}
}
