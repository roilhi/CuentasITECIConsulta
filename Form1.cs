using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cuentas_ITECI_Consulta
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Ingrese los apellidos y posteriormente los nombres del estudiante, después presione Enter", "Inicio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //LoginForm MyLoginWin = new LoginForm();
            //MyLoginWin.Show();
            //tbName.Select();
            //tbName.Focus();
            tbLastName.Select();
            tbLastName.Focus();
        }
        public bool alumnoExiste(string nombreCompleto)
        {
            bool result = false;
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://itecidb2:iteci2021@clusteriteci.rnxhk.mongodb.net/Prepa_ITECI_Ens?connect=replicaSet");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("estudiantes_ITECI");
            var collection = database.GetCollection<BsonDocument>("infoMails");
            var filter = Builders<BsonDocument>.Filter.Eq("nombre_completo", nombreCompleto);
            var BsonDoc = collection.Find(filter).FirstOrDefault();
            try
            {
                if (BsonDoc != null)
                {
                    result = true;
                }
            }
            catch
            {
                MessageBox.Show("Error, el alumno no existe, favor de contactar al departamento de sistemas");
            }
            return result;
        }

        private void tbLastName_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.KeyCode == Keys.Enter) 
            {
                tbLastName.Text = tbLastName.Text.Trim().ToUpper();
                tbFirstName.Select();
                tbFirstName.Focus();
            }
        }
        private void tbFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //tbName.Text = tbName.Text.Trim().ToUpper();
                tbFirstName.Text = tbFirstName.Text.Trim().ToUpper();
                //string theName = tbName.Text.Trim().ToUpper();
                string theName = tbLastName.Text.Trim().ToUpper() + " " + tbFirstName.Text.Trim().ToUpper();
                tbName.Text = theName;

                if (alumnoExiste(theName))
                {
                    var settings = MongoClientSettings.FromConnectionString("mongodb+srv://itecidb2:iteci2021@clusteriteci.rnxhk.mongodb.net/Prepa_ITECI_Ens?connect=replicaSet");
                    var client = new MongoClient(settings);
                    var database = client.GetDatabase("estudiantes_ITECI");
                    var collection = database.GetCollection<BsonDocument>("infoMails");
                    var filter = Builders<BsonDocument>.Filter.Eq("nombre_completo", theName);
                    var BsonDoc = collection.Find(filter).FirstOrDefault();
                    tbPassword.Text = BsonDoc["password"].AsString;
                    tbeMail.Text = BsonDoc["email"].AsString;
                    string grupo = BsonDoc["grupo"].AsString;
                    tbGrupo.Text = grupo;
                    // string matricula = BsonDoc["user"].AsString;
                    // tbServo.Text = matricula;
                    string userFenix = BsonDoc["usuario_moodle"].AsString;
                    tbUser.Text = userFenix;
                    // tbModalidad.Text = "semiescolarizado";
                }
                else
                {
                  MessageBox.Show(this, "El alumno no existe en las bases de datos. Verifique el nombre o comunique al Departamento de Sistemas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);  
                }

                //var settings = MongoClientSettings.FromConnectionString("mongodb+srv://itecidb2:iteci2021@clusteriteci.rnxhk.mongodb.net/Prepa_ITECI_Ens?connect=replicaSet");
                //var settings = MongoClientSettings.FromConnectionString("mongodb+srv://itecidb:iteci2021@clusteriteci.rnxhk.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");

                //var filter = Builders<BsonDocument>.Filter.Eq("serie", serie);
                // var fieldValueIsNullFilter = Builders<BsonDocument>.Filter.Eq("nombre_completo", BsonNull.Value.ToString());
                //Console.WriteLine(fieldValueIsNullFilter);

                // try
                // {
                // string modalidad = BsonDoc["modalidad"].AsString;
                //    tbModalidad.Text = modalidad;
                //if (modalidad == "escolarizado")
                //{


                //}
                //else if (modalidad == "semiescolarizado") 
                //{

                //string password = BsonDoc["password"].AsString;
                //tbPassword.Text = password;
                //string email = BsonDoc["email"].AsString;
                //tbeMail.Text = email;
                //}
                //}


            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e) 
        {
            tbName.Text = " ";
            tbeMail.Text = " ";
            tbGrupo.Text = " ";
            // tbModalidad.Text = " ";
            tbPassword.Text = " ";
            // tbServo.Text = " ";
            tbUser.Text = " ";
            tbFirstName.Text = "";
            tbLastName.Text = "";
        }
    }
}
