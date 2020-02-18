using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using LGCommands;

namespace LG_Command_Logger
    {
    public partial class FormMain : Form
        {
        IWK_Response iwk = new IWK_Response();
        /// <summary>
        /// Mantiene una lista de los codigos de error del LG
        /// con su descripcion y categorizacion
        /// </summary>
        ErrorCodeList ErrorCodes = new ErrorCodeList();

        private delegate void onNewEventHandler(string message);

        private event onNewEventHandler onNewEvent;

        LGCommands.IWK_Request iwk_Request = new IWK_Request();
        LGCommands.IWK_Response iwk_Response = new IWK_Response();

        string result = "";
        int len = 0;
        string cmd = "";
        string CAR = "";

                /// <summary>
        /// mantiene el estado del Puerto COM
        /// Cerrado = false
        /// Abierto = true
        /// </summary>
        private bool COMPortStatus = false;

        ///flag que establece cuando mostrar (o no)
        ///el time stamp de los mensajes recibidos
        private bool ShowTimeStamp = true;
       
        /// <summary>
        /// instancio el puerto COM
        /// </summary>
        SerialCommunication COMPort = new SerialCommunication();

        Logger.Log logger = new Logger.Log(Application.StartupPath +"\\" + "log.txt",Logger.Log.LogLevel.INFORMATION, Logger.Text.log);


        System.Threading.Thread threadLogging;
       

        public FormMain()
            {
            InitializeComponent();

            

            
            }

        /// <summary>
        /// inicia la escucha del puerto COM
        /// seleccionado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iniciarLoggerToolStripMenuItem_Click(object sender, EventArgs e)
            {
            COMPort.PortName = Properties.Settings.Default.COMPort;
            COMPort.NewDataReceived += new SerialCommunication.DataReceivedEventHandler(onSerialPortDataReceived);

            COMPort.Open();

            }
        /// <summary>
        /// este metodo se ejecuta cuando se
        /// reciben datos en el puerto COM
        /// </summary>
        /// <param name="data"></param>
        private void onSerialPortDataReceived(string data)
            {
            //LGCommands.TypeCommand cmd = (LGCommands.TypeCommand) Convert.ToByte(data.Substring(2, 2));
            //richTextBox_SentData.AppendText(System.DateTime.Now.ToString() + "CMD : " + cmd.ToString() + "\n");
            //richTextBox_SentData.AppendText(System.DateTime.Now.ToString() + " - " + data + "\n");
            }

        /// <summary>
        /// detiene la escucha del puerto COM
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cerrarLoggerToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

        /// <summary>
        /// despliega la lista de puertos COM
        /// disponibles
        /// </summary>
        /// <param name="sender"></param>
        ///// <param name="e"></param>
        private void toolStripComboBox1_Click(object sender, EventArgs e)
            {
            toolStripDropDownButton2.DropDownItems.Clear();

            foreach (string p in System.IO.Ports.SerialPort.GetPortNames())
                {
                toolStripDropDownButton2.DropDownItems.Add(p);
                }
            }

        /// <summary>
        /// despliega la ventana de configuracion 
        /// para el Log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lOGToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

        /// <summary>
        /// termina la ejecucion de la aplicacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
            {
            this.Close();
            }

        //private void onComboBoxSelectedIndexChanged(object sender, EventArgs e)
        //    {
        //    ///actualizo el valor del puerto seleccionado y lo almaceno 
        //    ///en el archivo de configuracion de la aplicacion
        //    Properties.Settings.Default.COMPort = toolStripComboBox1.SelectedItem.ToString();
        //    }

       
        /// <summary>
        /// abre el formulario FormOptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
            {
            FormOptions frmOptions = new FormOptions();
            
            frmOptions.Show();
            }


        private void Form1_Load(object sender, EventArgs e)
            {
            Definition errorCodesConfig = new Definition(Application.StartupPath + "\\" + "ErrorCodeList.xml");

            ///obtengo la lista de errores
            errorCodesConfig.get<ErrorCodeList>(ref ErrorCodes);

            //this.richTextBox_SentData.AppendText(ErrorCodes.getErrorDescription(ErrorCodes.ErrorCodes[2].Error_Code ));
            this.onNewEvent += new onNewEventHandler(onTextUpdated);
            stopToolStripMenuItem.Enabled = false;
            splitContainer1.Panel2Collapsed = true;
           
            richTextBox_SentData.ScrollToCaret();

            toolStripDropDownButton2.DropDownItems.Clear();

            foreach (string p in System.IO.Ports.SerialPort.GetPortNames())
                {
                toolStripDropDownButton2.DropDownItems.Add(p);
                }

           ///armo la lista de velocidades del puerto COM
            string[] baudRate = new string[5];

            baudRate[0] = "9600";
            baudRate[1] = "19200";
            baudRate[2] = "57600";
            baudRate[3] = "115200";
            baudRate[4] = "128000";
            ///cargo la lista en el dropDown...
            for (int i = 0; i < baudRate.Length; i++)
                {
                toolStripDropDownButton1.DropDownItems.Add(baudRate[i]);
                }

            updateConnectionStatus();
            }


        /// <summary>
        /// actualiza el estado de la barra de Estado del formulario
        /// </summary>
        private void updateConnectionStatus()
            {
            toolStripDropDownButton2.Text =  "Puerto: " + COMPort.PortName;
            toolStripDropDownButton1.Text = "Baud Rate : " + COMPort.BaudRate;
            toolStripStatusLabel3.Text = "Estado Conexion : " + ((COMPortStatus)? "Conectado" : "No conectado");
            }


        /// <summary>
        /// metodo que se ejecuta cada vez que se recibe un nuevo mensaje 
        /// en el puerto COM.
        /// Es el encargado de escribir en pantalla los mensajes que se
        /// van recibiendo
        /// </summary>
        /// <param name="message"></param>
        private void onTextUpdated(string message)
            {
            string timestamp = "";
           
            try
                {
                lock (message)
                    {
                    ///si esta habilitado, muestro el timestamp
                    if (ShowTimeStamp)
                        {
                        timestamp = System.DateTime.Now.ToString();
                        }
                    if (message == "")
                        {
                        return;
                        }
                    ///muestro el nombre del comando
                    if (message.Length < 3 && (message.Substring(0, 2) == "06"))
                        {
                        richTextBox_SentData.AppendText(timestamp + " - " + message.Substring(0, 2) + "\n");
                        return;
                        }
                    if (message.Length < 3 && (message.Substring(0, 2) == "15"))
                        {
                        richTextBox_SentData.AppendText(timestamp + " - " + message.Substring(0, 2) + "\n");
                        return;
                        }
                    if (message.Substring(0, 2) == "02" && message.Length > 2)
                        {

                        richTextBox_SentData.AppendText(timestamp + " - " + LGCommands.LGCommand_Request.getCommandType(message.Substring(2, 2)) + "\n");

                        }

                    if (message.Substring(0, 2) == "06" && message.Substring(2, 2) == "02")
                        {
                        richTextBox_SentData.AppendText(timestamp + " - " + LGCommands.LGCommand_Request.getCommandType(message.Substring(4, 2)) + "\n");
                        }

                    if (message.Substring(0, 2) == "15" && message.Substring(2, 2) == "02")
                        {
                        richTextBox_SentData.AppendText(timestamp + " - " + LGCommands.LGCommand_Request.getCommandType(message.Substring(4, 2)) + "\n");
                        }                    

                    ///muestro el mensaje
                    richTextBox_SentData.AppendText(timestamp + " - " + getCommand(message) + "\n");
                    
                   ///En funcion del codigo de resultado, muestro 
                   ///el mismo en color verde (OK), amarillo/naranja, o rojo (error operacional)
                   ///Para ello, utilizo el archivo de errores, para obtener el 
                   ///codigo de color a mostrar.
                    if (!string.IsNullOrEmpty(CAR))
                        {
                        if (ErrorCodes.getErrorCategory(CAR) == ErrorCategory.Information)
                            {
                            richTextBox_SentData.AppendText(timestamp + " - ");
                            richTextBox_SentData.SelectionColor = Color.Green;
                            richTextBox_SentData.AppendText("[" + CAR + "]" + "\n\n");
                            }
                        else if (ErrorCodes.getErrorCategory(CAR) == ErrorCategory.Technical)
                            {
                            richTextBox_SentData.AppendText(timestamp + " - ");
                            richTextBox_SentData.SelectionColor = Color.Orange;
                            richTextBox_SentData.AppendText("[" + CAR + "]" + "\n\n");
                            }
                        else if (ErrorCodes.getErrorCategory(CAR) == ErrorCategory.CardOperation)
                            {
                            richTextBox_SentData.AppendText(timestamp + " - ");
                            richTextBox_SentData.SelectionColor = Color.Red;
                            richTextBox_SentData.AppendText("[" + CAR + "]" + "\n\n");
                            }
                        else if (ErrorCodes.getErrorCategory(CAR) == ErrorCategory.Critical)
                            {
                            richTextBox_SentData.AppendText(timestamp + " - ");
                            richTextBox_SentData.SelectionColor = Color.Blue;
                            richTextBox_SentData.AppendText("[" + CAR + "]" + "\n\n");
                            }
                        else if (ErrorCodes.getErrorCategory(CAR) == ErrorCategory.No_definido)
                            {
                            richTextBox_SentData.AppendText(timestamp + " - ");
                            richTextBox_SentData.SelectionColor = Color.White;
                            richTextBox_SentData.AppendText("[" + CAR + "]" + "\n\n");
                            }
                        }
                                          
                    ///esta linea es a efectos de debug. Normalmente debe estar comentada.
                   //richTextBox_SentData.AppendText(timestamp + " -DEBUG - mensaje completo: " + message + "\n");
                    }
                    threadLogging = new System.Threading.Thread(logMessage);
                    threadLogging.Start(richTextBox_SentData.Text);

                    richTextBox_SentData.Focus();
                    richTextBox_SentData.ScrollToCaret();
                }
            catch (Exception ex)
                {
                richTextBox_SentData.AppendText(timestamp + " - "+ ": ¿? " + message + "\n");
                richTextBox_SentData.AppendText(ex.Message + " " + ex.StackTrace.ToString() + "\n");
                threadLogging = new System.Threading.Thread(logMessage);
                threadLogging.Start(richTextBox_SentData.Text);
                }

            }

        /// <summary>
        /// metodo encargado de realizar el 
        /// parsing del mensaje para adecuarlo
        /// a la estructura de un comando LG
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
       
        private string getCommand(string message)
            {
            result = "";
            UInt16 len_littleEndian = 0; ///contiene el valor de Len con el orden de bytes invertido
            CAR = "";
            Error_Code ec = new Error_Code();
            //int len = 0;
            //string cmd = "";
            //string CAR = "";
            try
                {
                switch (message.Substring(0, 2))
                    {
                    case "02":
                            {
                            ///obtengo el comando
                            cmd = message.Substring(2, 2);
                            ///obtengo el campo LEN
                            len = 2 * (Convert.ToUInt16(this.swap((message.Substring(4, 4))), 16));
                            len_littleEndian = swap_Uint((UInt16)(len/2));
                           
                            ///verifico que el valor de la variable len no se corresponda
                            ///con un codigo de respuesta valido, lo que implicaria que
                            ///en realidad el mensaje es uno de respuesta, y el campo analizado 
                            ///como Len en realidad es CAR
                           if( !ErrorCodes.ErrorCodes.Exists(x =>x.ErrorCode == len_littleEndian))
                                {
                                ///significa que LEN efectivamente es LEN
                                result = message.Substring(0, (len + 14));

                                ///prueba de parsin del comando LG
                               // parseLGCommand(result);
                                }
                            else
                                {
                                ///LEN en realidad es CAR...

                                len = 2 * (Convert.ToUInt16(this.swap((message.Substring(8, 4))), 16));
                                CAR = message.Substring(4, 4);
                                result =  message.Substring(0, (len + 18));
                                // result += "\n" + "[CAR: " + CAR +"]";
                                }
                            ///verifico si al finalizar el mensaje, no existen mas bytes que correspondan
                            ///al mensaje siguiente...
                            if (message.Length > result.Length)
                                {
                                ///parseo el mensaje que esta encadenado al anterior...
                                message = message.Substring(result.Length);
                                ///si el mensaje queda reducido solo al ACK...
                                if (message.Substring(0, 2) == "06" && message.Length < 3)
                                    {
                                    result += "\n" + message + "\n";
                                    break;
                                    }
                                    ///si el mensaje encadenado empieza con 0x06 0x02...
                                else if (message.Substring(0, 2) == "06" && message.Substring(2, 2) == "02")
                                    {
                                    len = 2 * (Convert.ToUInt16(this.swap((message.Substring(6, 4))), 16));
                                    len_littleEndian = swap_Uint((UInt16)(len / 2));

                                    if (!ErrorCodes.ErrorCodes.Exists(x => x.ErrorCode == len_littleEndian))
                                        {
                                        ///significa que LEN efectivamente es LEN
                                        result += "\n" + "06" + "\n";
                                        
                                        result += message.Substring(2, (len +14));
                                        ///si ademas, al final del mensaje se encuentra un nuevo ACK,
                                        ///lo separo en un renglon aparte.
                                        if (message.Substring(message.Length - 2, 2) == "06")
                                            {
                                                result += "\n" + "06";
                                            }
                                        break;
                                        }
                                    else
                                        {
                                        ///LEN en realidad es CAR...
                                        len = 2 * (Convert.ToUInt16(this.swap((message.Substring(10, 4))), 16));
                                        //result = "";
                                        CAR = message.Substring(6, 4);
                                        result += "\n" + "06" +"\n";
                                        result += message.Substring(2, (len + 18));
                                        // result += "\n" + "[CAR: " + CAR + "]";
                                        break;
                                        }
                                    }
                                }
                            }
                        break;
                    case "06":
                        ///si el mensaje queda reducido solo al ACK...
                        if (message.Substring(0, 2) == "06" && message.Length < 3)
                            {
                            CAR = "";
                            result += "\n" + message + "\n";
                            break;
                            }

                        ///si el primer byte es ACK y el segundo (de existir) es STX...
                        if (message.Substring(0, 2) == "06" && message.Substring(2, 2) == "02")
                            {
                            len = 2 * (Convert.ToUInt16(this.swap((message.Substring(6, 4))), 16));
                            len_littleEndian = swap_Uint((UInt16)(len / 2));

                            if (!ErrorCodes.ErrorCodes.Exists(x => x.ErrorCode == len_littleEndian))
                                {
                                ///significa que LEN efectivamente es LEN
                                result += "\n" + "06" + "\n";

                                result += message.Substring(2, (len + 14));
                                ///si ademas, al final del mensaje se encuentra un nuevo ACK,
                                ///lo separo en un renglon aparte.
                                if (message.Substring(message.Length - 2, 2) == "06")
                                    {
                                    result += "\n" + "06";
                                    }
                                break;
                                }
                            else
                                {
                                ///LEN en realidad es CAR...
                                len = 2 * (Convert.ToUInt16(this.swap((message.Substring(10, 4))), 16));
                                //result = "";
                                CAR = message.Substring(6, 4);
                                result += "\n" + "06" + "\n";
                                result += message.Substring(2, (len + 18));
                                
                                break;
                                }
                            ///verifico si al finalizar el mensaje, no existen mas bites que correspondan
                            ///al mensaje siguiente...

                            if (message.Length > result.Length)
                                {
                                ///parseo el mensaje que esta encadenado al anterior...
                                message = message.Substring(result.Length);
                                ///si el mensaje queda reducido solo al ACK...
                                if (message == "06")
                                    {
                                    result += "\n" + message + "\n";
                                    break;
                                    }
                                len = 2 * (Convert.ToUInt16(this.swap((message.Substring(6, 4))), 16));
                                len_littleEndian = swap_Uint((UInt16)(len / 2));

                                if (!ErrorCodes.ErrorCodes.Exists(x => x.ErrorCode == len_littleEndian))
                                    {
                                    ///significa que LEN efectivamente es LEN
                                    result += "\n" + "06" + "\n";

                                    result += message.Substring(2, (len + 14));
                                    ///si ademas, al final del mensaje se encuentra un nuevo ACK,
                                    ///lo separo en un renglon aparte.
                                    if (message.Substring(message.Length - 2, 2) == "06")
                                        {
                                        result += "\n" + "06";
                                        }
                                    break;
                                    }
                                else
                                    {
                                    ///LEN en realidad es CAR...
                                    len = 2 * (Convert.ToUInt16(this.swap((message.Substring(10, 4))), 16));
                                    //result = "";
                                    CAR = message.Substring(6, 4);
                                    result += "\n" + "06" + "\n";
                                    result += message.Substring(2, (len + 18));

                                    break;
                                    }

                                }
                            }

                        break;
                    case "15":
                        ///si el mensaje queda reducido solo al NACK...
                        if (message.Substring(0, 2) == "15" && message.Length < 3)
                            {
                            CAR = "";
                            result += "\n" + message + "\n";
                            break;
                            }

                        ///si el primer byte es ACK y el segundo (de existir) es STX...
                        if (message.Substring(0, 2) == "15" && message.Substring(2, 2) == "02")
                            {
                            len = 2 * (Convert.ToUInt16(this.swap((message.Substring(6, 4))), 16));
                            len_littleEndian = swap_Uint((UInt16)(len / 2));

                            if (!ErrorCodes.ErrorCodes.Exists(x => x.ErrorCode == len_littleEndian))
                                {
                                ///significa que LEN efectivamente es LEN
                                result += "\n" + "15" + "\n";

                                result += message.Substring(2, (len + 14));
                                ///si ademas, al final del mensaje se encuentra un nuevo ACK,
                                ///lo separo en un renglon aparte.
                                if (message.Substring(message.Length - 2, 2) == "06")
                                    {
                                    result += "\n" + "06";
                                    }
                                break;
                                }
                            else
                                {
                                ///LEN en realidad es CAR...
                                len = 2 * (Convert.ToUInt16(this.swap((message.Substring(10, 4))), 16));
                                //result = "";
                                CAR = message.Substring(6, 4);
                                result += "\n" + "15" + "\n";
                                result += message.Substring(2, (len + 18));

                                break;
                                }
                            ///verifico si al finalizar el mensaje, no existen mas bites que correspondan
                            ///al mensaje siguiente...

                            if (message.Length > result.Length)
                                {
                                ///parseo el mensaje que esta encadenado al anterior...
                                message = message.Substring(result.Length);
                                ///si el mensaje queda reducido solo al ACK...
                                if (message == "06")
                                    {
                                    result += "\n" + message + "\n";
                                    break;
                                    }
                                ///si el mensaje queda reducido solo al NACK...
                                if (message == "15")
                                    {
                                    result += "\n" + message + "\n";
                                    break;
                                    }
                                len = 2 * (Convert.ToUInt16(this.swap((message.Substring(6, 4))), 16));
                                len_littleEndian = swap_Uint((UInt16)(len / 2));

                                if (!ErrorCodes.ErrorCodes.Exists(x => x.ErrorCode == len_littleEndian))
                                    {
                                    ///significa que LEN efectivamente es LEN
                                    result += "\n" + "06" + "\n";

                                    result += message.Substring(2, (len + 14));
                                    ///si ademas, al final del mensaje se encuentra un nuevo ACK,
                                    ///lo separo en un renglon aparte.
                                    if (message.Substring(message.Length - 2, 2) == "06")
                                        {
                                        result += "\n" + "06";
                                        }
                                    break;
                                    }
                                else
                                    {
                                    ///LEN en realidad es CAR...
                                    len = 2 * (Convert.ToUInt16(this.swap((message.Substring(10, 4))), 16));
                                    //result = "";
                                    CAR = message.Substring(6, 4);
                                    result += "\n" + "06" + "\n";
                                    result += message.Substring(2, (len + 18));

                                    break;
                                    }

                                }
                            }

                        break;
                    default:
                        result = message;
                        break;
                    }
                return result;
                }
            catch (Exception ex)
                {
                return  "error : " + ex.Message + " -- " + ex.StackTrace.ToString() + "\n mensaje: " + message + " -- " ;
                threadLogging = new System.Threading.Thread(logMessage);
                threadLogging.Start("error : " + ex.Message + " -- " + ex.StackTrace.ToString() + "\n mensaje: " + message + " -- ");
                }

            }


        /// <summary>
        /// hilo que registra los mensajes en archivo
        /// </summary>
        /// <param name="message"></param>
        private void logMessage(object  message)
            {
          //  iwk.set((string)message);

            logger.info((string)message);
            }

        
        /// <summary>
        /// inicia la escucha del puerto serie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startToolStripMenuItem_Click_1(object sender, EventArgs e)
            {
            try
                {
                COMPort.NewDataReceived += new SerialCommunication.DataReceivedEventHandler(onNewDataReceived);
                COMPort.ReadTimeout = -1;
                COMPort.BaudRate = 115200;
                COMPort.DataBits = 8;
                COMPort.Parity = System.IO.Ports.Parity.None;
                COMPort.StopBits = System.IO.Ports.StopBits.One;
                COMPort.open();
                COMPortStatus = true;
                updateConnectionStatus();
                stopToolStripMenuItem.Enabled = true;
                startToolStripMenuItem.Enabled = false;


                }
            catch (Exception ex)
                {

                MessageBox.Show(ex.Message);
                }
            }

        /// <summary>
        /// este metodo se ejecuta cuando se recibe
        /// un nuevo mensaje en el puerto serie
        /// </summary>
        /// <param name="data"></param>
        private void onNewDataReceived(string data)
            {
            try
                {
                         
                this.richTextBox_SentData.Invoke(onNewEvent, data);

                }
            catch (Exception ex)
                {
                threadLogging = new System.Threading.Thread(logMessage);
                threadLogging.Start(ex.Message + " - " + ex.StackTrace.ToString());
                }
            
            }

        /// <summary>
        /// cierra el puerto COM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
            {
            try
                {
                COMPort.close();
                COMPort.NewDataReceived -= new SerialCommunication.DataReceivedEventHandler(onNewDataReceived);
                COMPortStatus = false;
                updateConnectionStatus();
                stopToolStripMenuItem.Enabled = false;
                startToolStripMenuItem.Enabled = true;
                }
            catch (Exception ex)
                {
                threadLogging = new System.Threading.Thread(logMessage);
                threadLogging.Start(ex.Message + " - " + ex.StackTrace.ToString());
                }
            }


        
        /// <summary>
        /// este metodo se ejecuta cuando se selecciona 
        /// la opcion Clear Window() del menu contextual del 
        /// control RichtextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearWindowToolStripMenuItem_Click(object sender, EventArgs e)
            {
            this.richTextBox_SentData.Clear();
            }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
            {

            }

        /// <summary>
        /// este metodo se ejecuta cuando se selecciona
        /// la opcion Exit del menu.
        /// Cierra la aplicacion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
            {
            try
                {

            ///si el thread de log esta activo, lo aborto.
            if (threadLogging!=null && threadLogging.ThreadState == System.Threading.ThreadState.Running)
                {
                threadLogging.Abort();
                }
            ///si el puerto COM aun esta abierto, lo cierro
            if (COMPort.IsOpen)
                {
                COMPort.close();
                }
            ///cierro el formulario
            this.Close();

                }
            catch (Exception)
                {

                throw;
                }
            }

        /// <summary>
        /// opcion del menu contextual que define si se muestra o no
        /// el time stamp de cada mensaje recibido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showTimeStampToolStripMenuItem_Click(object sender, EventArgs e)
            {
            if (showTimeStampToolStripMenuItem.Checked)
                {
                showTimeStampToolStripMenuItem.Checked = false;
                ShowTimeStamp = false;
                return;
                }
            if (!showTimeStampToolStripMenuItem.Checked)
                {
                showTimeStampToolStripMenuItem.Checked = true;
                ShowTimeStamp = true;
                }
            }

        private void customizeToolStripMenuItem1_Click(object sender, EventArgs e)
            {

            }

        /// <summary>
        /// Este metodo se ejecuta cuando se seleciona
        /// la opcion de mostrar el parser de comandos LG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commandParsingToolStripMenuItem_Click(object sender, EventArgs e)
            {
            if (commandParsingToolStripMenuItem.Checked)
                {
                splitContainer1.Panel2Collapsed = true;
                commandParsingToolStripMenuItem.Checked = false;
                return;
                }
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.SplitterDistance= (int)(this.Height * 50 /100);
                commandParsingToolStripMenuItem.Checked = true;
               
            
            }

        /// <summary>
        /// Este metodo se ejecuta cuando se selecciona
        /// la opcion de mostrar el panel 2 (abajo a la derecha).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showPanel2ToolStripMenuItem_Click(object sender, EventArgs e)
            {
            if (showPanel2ToolStripMenuItem.Checked)
                {
                splitContainer2.Panel2Collapsed = true;
                showPanel2ToolStripMenuItem.Checked = false;
                
                return;
                }
            splitContainer2.Panel2Collapsed = false;
            
            showPanel2ToolStripMenuItem.Checked = true;
            Form1 frmTest = new Form1();
            frmTest.MdiParent = this;
            splitContainer2.Panel2.Controls.Add(frmTest);
            splitContainer2.SplitterDistance = this.Width -frmTest.Width -15;
            
            frmTest.Show();
            }


        #region Helper Functions

        /// <summary>
        /// invierte una cadena de caracteres
        /// contemplando que la misma representa 
        /// un numero hexadecimal.
        /// Por lo tanto, 2 caracteres representan un 
        /// digito en Hexadecimal.
        /// </summary>
        /// <param name="HexValue"></param>
        /// <returns></returns>
        private string swap(string HexValue)
            {
            string aux = "";
            int count = HexValue.Count();
            for (int i = 0; i < count;i+= 2)
                {
                aux += HexValue.Substring(count-2- i, 2);
                }
            return aux;
            }


        private UInt16 swap_Uint(UInt16 value)
            {
            return (UInt16)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
            }

        #endregion

       
        /// <summary>
        /// este metodo se ejecuta cuando se selecciona
        /// un puerto COM de la lista desplegable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onDropDownPortCOMSelected(object sender, ToolStripItemClickedEventArgs e)
            {
            try
                {
                COMPort.PortName = e.ClickedItem.ToString();
                updateConnectionStatus();
                }
            catch (Exception ex)
                {

                MessageBox.Show(ex.Message);
                }
           
            }

        /// <summary>
        /// este metodo se ejecuta cuando se selecciona
        /// un valor de BaudRate de la lista desplegable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onDropDownBaudRateSelected(object sender, ToolStripItemClickedEventArgs e)
            {
            COMPort.BaudRate = (Convert.ToInt32 (e.ClickedItem.ToString()));
            updateConnectionStatus();
            }



        /// <summary>
        /// permite hacer zoom sobre el texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onZoomIn_Out(object sender, EventArgs e)
            {
            this.richTextBox_SentData.ZoomFactor = 1;
            }

        /// <summary>
        /// muestra ventana Acerca de...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
            {
            AboutBox1 acercaDe = new AboutBox1();
            //acercaDe.MdiParent = this;
            
            acercaDe.Show();
            acercaDe.BringToFront();

            }

        /// <summary>
        /// Guarda todo lo logueado a un archivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
            {
            FileReaderWriter file = new FileReaderWriter();

            
            
            }

        /// <summary>
        /// corta el texto seleccionado y lo guarda en
        /// el clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
            {
            try
                {
                if (!string.IsNullOrEmpty(richTextBox_SentData.Text))
                    {
                    Clipboard.SetText(richTextBox_SentData.SelectedText);
                    richTextBox_SentData.Cut();
                    }
                }
            catch (Exception)
                {
                
               
                }
            }

        /// <summary>
        /// copia el texto seleccionado al clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
            {
            if (! string.IsNullOrEmpty(richTextBox_SentData.SelectedText) || !string.IsNullOrWhiteSpace(richTextBox_SentData.SelectedText))
                {
                Clipboard.SetText(richTextBox_SentData.SelectedText);
                }
            }

        /// <summary>
        /// pega el contenido de texto que hay en el clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
            {
            if (Clipboard.ContainsText())
                {
                richTextBox_SentData.Text += Clipboard.GetText() + "\n";
                }
            }

        /// <summary>
        /// selecciona todo el texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
            {
            if (!string.IsNullOrEmpty(richTextBox_SentData.Text))
                {
                richTextBox_SentData.SelectAll();
                }
            }


        private void parseLGCommand(string message)
            {
             try 
	            {
                message = message.Substring(2, message.Length - 2);
                iwk_Request.set(message);
                 }
	            catch (Exception)
	            {
		
		            throw;
	            }
            }


        private void onResize(object sender, EventArgs e)
            {

            }

        }
    }
