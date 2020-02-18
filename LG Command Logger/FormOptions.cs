using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LGCommands;
namespace LG_Command_Logger
    {
    public partial class FormOptions : Form
        {
        public FormOptions()
            {
            InitializeComponent();
            }

        private void FormOptions_Load(object sender, EventArgs e)
            {
            treeView1.Nodes.Add("Conexiones");
            TreeNode nodoPuertoCOM = new TreeNode("Puerto Serie");
            treeView1.Nodes[0].Nodes.Add(nodoPuertoCOM);
            
            ///sobre el nodo "Conexiones" cargo una
            ///descripcion del nodo.
            TextBox descriptionText = new TextBox();
            splitContainer1.Panel2.Controls.Add(descriptionText);
            descriptionText.Location = new Point(10, 5);
            descriptionText.Size = new Size(300, 50);
            descriptionText.BorderStyle = BorderStyle.None;
            descriptionText.Multiline = true;
            
            descriptionText.Text = "Menu Conexiones: Permite configurar los distintos parametros del puerto serie";

            treeView1.Nodes.Add("Session");
            TreeNode nodoLogging= new TreeNode("Logging");
            treeView1.Nodes[1].Nodes.Add(nodoLogging);
            ///muestra el treeview con sus opciones
            ///desplegadas
            treeView1.ExpandAll();


            
            }

        //private void onNode_Click(object sender, EventArgs e)
        //    {
        //    string node = ((TreeView)sender).SelectedNode.Text;
        //    }

        private void onNode_Click(object sender, TreeNodeMouseClickEventArgs e)
            {
            string node = ((TreeView)sender).SelectedNode.Text;

            switch (node)
                {
                case "Puerto Serie":

                    SerialCommunication PuertoCOM = new SerialCommunication();
                    PropertyGrid propGrid = new PropertyGrid();
                    PuertoCOM.BaudRate = 1152002;
                    propGrid.SelectedObject = PuertoCOM;
                    splitContainer1.Panel2.Controls.Add(propGrid);
                    propGrid.Size = new Size(200, 300);

                    break;
                case "Logging":
                    break;
                }

            }


       

        }
    }
