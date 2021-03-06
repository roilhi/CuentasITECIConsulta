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
        public bool userExists { get; }
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
                string theName = tbFirstName.Text.Trim().ToUpper() + " " + tbLastName.Text.Trim().ToUpper();
                tbName.Text = theName;
                //var settings = MongoClientSettings.FromConnectionString("mongodb+srv://itecidb2:iteci2021@clusteriteci.rnxhk.mongodb.net/Prepa_ITECI_Ens?connect=replicaSet");
                //var settings = MongoClientSettings.FromConnectionString("mongodb+srv://itecidb:iteci2021@clusteriteci.rnxhk.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
                    var settings = MongoClientSettings.FromConnectionString("mongodb+srv://itecidb:iteci2021@clusteriteci.rnxhk.mongodb.net/Prepa_ITECI_Ens?connect=replicaSet");
                    var client = new MongoClient(settings);
                    var database = client.GetDatabase("Prepa_ITECI_Ens");
                    var collection = database.GetCollection<BsonDocument>("Mails_Prepa_Ens");
                    var filter = Builders<BsonDocument>.Filter.Eq("WholeName", theName);
                    var fieldValueIsNullFilter = Builders<BsonDocument>.Filter.Eq("WholeName", BsonNull.Value.ToString());
                    //Console.WriteLine(fieldValueIsNullFilter);
                    var BsonDoc = collection.Find(filter).FirstOrDefault();
                try
                {
                    string modalidad = BsonDoc["modalidad"].AsString;
                    tbModalidad.Text = modalidad;
                    if (modalidad == "escolarizado")
                    {
                        tbPassword.Text = BsonDoc["password"].AsString;
                        tbeMail.Text = BsonDoc["email"].AsString;
                    }
                    else if (modalidad == "semiescolarizado") 
                    {
                        string grupo = BsonDoc["group"].AsString;
                        tbGrupo.Text = grupo;
                        string matricula = BsonDoc["servo_id"].AsString;
                        tbServo.Text = matricula;
                        string userFenix = BsonDoc["moodleUser"].AsString;
                        tbUser.Text = userFenix;
                        string password = BsonDoc["password"].AsString;
                        tbPassword.Text = password;
                        string email = BsonDoc["email"].AsString;
                        tbeMail.Text = email;
                    }
                }
                catch
                {
                    MetroMessageBox.Show(this, "El alumno no existe en las bases de datos. Verifique el nombre o comunique al Departamento de Sistemas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void btnLimpiar_Click(object sender, EventArgs e) 
        {
            tbName.Text = " ";
            tbeMail.Text = " ";
            tbGrupo.Text = " ";
            tbModalidad.Text = " ";
            tbPassword.Text = " ";
            tbServo.Text = " ";
            tbUser.Text = " ";
            tbFirstName.Text = "";
            tbLastName.Text = "";
        }
    }
}
