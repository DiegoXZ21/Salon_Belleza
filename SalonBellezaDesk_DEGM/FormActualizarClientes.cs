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
            CargarDatosCLiente();
        }

        private void CargarDatosCLiente()
        {
            try
            {
                using (SQLiteConnection con = DB_Connection.Database.GetConnection())
                {
                    con.Open();
                    string query = "SELECT Nombre, Apellido, Telefono, Email FROM Clientes WHERE Id_Cliente = @ID";

                    using (SQLiteCommand command = new SQLiteCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@ID", Id_Cliente);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Asignamos el resultado de la consulta a los campos de texto
                                textNombre.Text = reader["Nombre"].ToString();
                                textApellido.Text = reader["Apellido"].ToString();
                                textTelefono.Text = reader["Telefono"].ToString();
                                textEmail.Text = reader["Email"].ToString();
                            }
                        }
                    }
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
                if (!string.IsNullOrWhiteSpace(textNombre.Text) &&
                    !string.IsNullOrWhiteSpace(textApellido.Text) &&
                    !string.IsNullOrWhiteSpace(textTelefono.Text))
                {
                    using (con)
                    {
                        con.Open();
                        string query = "UPDATE Clientes SET Nombre=@Nombre, Apellido=@Apellido, Telefono=@Telefono, Email=@Email WHERE Id_Cliente=@Id_Cliente";

                        using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@Nombre", textNombre.Text);
                            cmd.Parameters.AddWithValue("@Apellido", textApellido.Text);
                            cmd.Parameters.AddWithValue("@Telefono", textTelefono.Text);
                            cmd.Parameters.AddWithValue("@Email", textEmail.Text);
                            cmd.Parameters.AddWithValue("@Id_Cliente", Id_Cliente);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Cliente actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el cliente a actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Todos los campos deben estar llenos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
