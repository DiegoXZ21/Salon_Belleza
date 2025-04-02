using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalonBellezaDesk_DEGM
{
    public partial class FormActualizarClientes : Form
    {
        private int Id_Cliente;
        private SQLiteConnection con;
        public FormActualizarClientes(int id, SQLiteConnection conexion)
        {
            InitializeComponent();
            this.Id_Cliente = id;
            this.con = conexion;
            //MessageBox.Show("ID: "+this.Id_Cliente);
            CargarDatosCLiente();
        }

        private void CargarDatosCLiente()
        {
            try
            {
                con.Open();
                String query = "SELECT Nombre, Apellido, Telefono, Email FROM Clientes WHERE Id_Cliente = @ID";
                SQLiteCommand command = new SQLiteCommand(query, con);
                command.Parameters.AddWithValue("@ID", Id_Cliente);
                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.Read()) 
                { 
                    //Asignamos el resultado de la consulta a las respectivas variables
                    String Nombre = reader["Nombre"].ToString();
                    String Apellido = reader["Apellido"].ToString();
                    String Telefono = reader["Telefono"].ToString();
                    String Email = reader["Email"].ToString();

                    //Asignamos a los txt los valores de las variables
                    textNombre.Text = Nombre;
                    textApellido.Text = Apellido;
                    textTelefono.Text = Telefono;
                    textEmail.Text = Email;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textNombre.Text.ToString() == "" || textApellido.Text.ToString() == "" || textTelefono.Text.ToString() == "") 
                {
                    con.Open();
                    String query = "UPDATE Clientes set Nombre=@Nombre, Apelldio=@Apellido, Telefono=@Telefono, Email=@Email WHERE Id_Cliente=@Id_Cliente";
                    SQLiteCommand cmd = new SQLiteCommand(query, con);
                    cmd.Parameters.AddWithValue("@Nombre", textNombre.Text.ToString());
                    cmd.Parameters.AddWithValue("@Apellido", textApellido.Text.ToString());
                    cmd.Parameters.AddWithValue("@Telefono", textTelefono.Text.ToString());
                    cmd.Parameters.AddWithValue("@Email", textEmail.Text.ToString());
                    cmd.Parameters.AddWithValue("@Id_Cliente", Id_Cliente);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Cliente actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Que pasooooo");
                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
