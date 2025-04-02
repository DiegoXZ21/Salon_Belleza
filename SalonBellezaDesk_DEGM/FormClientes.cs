using SalonBellezaDesk_DEGM.DB_Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalonBellezaDesk_DEGM
{
    public partial class FormClientes : Form
    {
        public FormClientes()
        {
            InitializeComponent();
            //IniciarConexion();
            MostrarCLientes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            String nombre = textNombre.Text.ToString();
            String apellido = textApellido.Text.ToString();
            String telefono = textTelefono.Text.ToString();
            String email = textEmail.Text.ToString();
            
            //Este if nos asegura de que los valores que se agreguen no sean nulos en nombre, apellido y telefono. Email es opcional.
            if (nombre == "" || apellido == "" || telefono == "")
            {
                MessageBox.Show("Asegurese de que el nombre, apellido y telefono no esten vacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    //Se hacen las inserciones a la base de datos luego de haber superado las validaciones previas
                    using (SQLiteConnection con = DB_Connection.Database.GetConnection())
                    {
                        con.Open();
                        String query = "INSERT INTO Clientes (Nombre, Apellido, Telefono, Email) VALUES (@Nombre, @Apellido, @Telefono, @Email)";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Apellido", apellido);
                        command.Parameters.AddWithValue("@Telefono", telefono);
                        command.Parameters.AddWithValue("@Email", email);
                        command.ExecuteNonQuery();
                       
                        MessageBox.Show("Cliente agregado con exito", "Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Refrescamos la vista de la tabla
                        MostrarCLientes();
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message);
                    throw;
                }
                MessageBox.Show("Nombre, apellido y telefono estan con valores", "Funciona", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textNombre.Text = null;
                textApellido.Text = null;
                textTelefono.Text = null;
                textEmail.Text = null;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            textNombre.Text = null;
            textApellido.Text = null;
            textTelefono.Text = null;
            textEmail.Text = null;
        }

        private void MostrarCLientes()
        {
            try
            {
                using (SQLiteConnection con = DB_Connection.Database.GetConnection())
                {
                    con.Open();
                    String query = "SELECT Id_Cliente, Nombre || ' ' || Apellido AS NombreCompleto, Telefono, Email FROM CLientes";
                    SQLiteDataAdapter da = new SQLiteDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvClientes.AutoGenerateColumns = false;
                    dgvClientes.DataSource = dt;
                }
                
                
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void dgvClientes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SQLiteConnection con = DB_Connection.Database.GetConnection();
                DataGridViewRow row = dgvClientes.Rows[e.RowIndex];
                int Id_Cliente = Convert.ToInt32(row.Cells["Id_Cliente"].Value);

                //Abrir un formulario como modal y pasamos solo el ID
                using (FormActualizarClientes formActualizarClientes = new FormActualizarClientes(Id_Cliente, con))
                {
                    if (formActualizarClientes.ShowDialog() == DialogResult.OK)
                    {
                        //Si llego a esto es porque en el modal si se actualizaron datos
                        MostrarCLientes();
                    }
                }
                
            }
        }
    }
}
