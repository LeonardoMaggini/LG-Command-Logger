using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGCommands
    {
    public enum TypeCommand
        {
        VLG = 0x81,
        IWK = 0x84,
        OID = 0x83,
        PIN = 0x10,
        IDE = 0x20,
        VID = 0x21,
        INI = 0x22,
        CINI = 0x23,
        VTD = 0x24,
        ATD = 0x25,
        ATO = 0x26,
        CATO = 0x27,
        SCR = 0x28,
        RCR = 0x29,
        CTO = 0x2A,
        RCTO = 0x2B,
        CL = 0x2C,
        EDT = 0x2D,
        CEMI = 0x2F,
        ETO = 0x30,
        CSTU = 0x33,
        ST = 0x39,
        CTU = 0x34,
        ACTU = 0x35,
        ET = 0x38,
        VTU = 0x45,
        ACD = 0x42,
        ACD2 = 0x43,
        CAPP = 0x40,
        OTO = 0x44,
        MAPL = 0x80,
        RST = 0x82,
        DLG = 0x85,
        EBLG = 0x86,
        CSLG = 0x32,
        DTU = 0x36,
        ADTU = 0x37,
        VPA = 0x3D,
        AVPA = 0x3E,
        LDA = 0x3C,
        OLA = 0x3B
        }

    //public enum Error
    //    {
    //    LG_OK = 0x9000,
    //    LG_Comando_No_Reconocido = 0x9001,
    //    LG_Comando_No_Habilitado = 0x9002,
    //    LG_Identificacion_Requerida = 0x9003,
    //    LG_Inicializacion_Requerida = 0x9004,
    //    LG_Parametro_Incorrecto = 0x9005,
    //    LG_WK_Bloqueada = 0x9006,
    //    LG_Verificacion_Fallida = 0x9007,
    //    LG_Bloqueado = 0x9008,
    //    LG_Actualizando_FW = 0x9009,
    //    LG_Actualizacion_Requerida = 0x900A,
    //    LG_Apertura_Turno_Requerida = 0x900B,
    //    LG_Cierre_Turno_Requerido = 0x900C,
    //    LG_Apertura_Turno_Ya_Efectuada = 0x900D,
    //    LG_Limite_Horas_Sin_Inicializar = 0x900E,
    //    LG_Limite_Turnos_Sin_Inicializar = 0x900F,
    //    LG_Limite_Recargas_Por_Turno = 0x9010,
    //    LG_Pin_Requerido = 0x9011,
    //    LG_MSG_NORESP_ERR = 0x90F1,
    //    LG_MSG_LENGHT_ERR = 0x90F2,
    //    LG_MSG_PACKET_ERR = 0x90F3,
    //    LG_TM_Hw_No_Responde = 0x9300,
    //    LG_TM_Ninguna_O_Mas_De_Una_Tarjeta = 0x9301,
    //    LG_TM_Modelo_De_Tarjeta_No_Soportado = 0x9302,
    //    LG_TM_Tarjeta_No_Compatible_SUBE = 0x9303,
    //    LG_TM_Tarjeta_NO_Corresponde_Con_Seleccionada = 0x9304,
    //    LG_TM_Error_Autenticacion = 0x9305,
    //    LG_TM_Error_Lectura = 0x9306,
    //    LG_TM_Error_Escritura = 0x9307,
    //    LG_TM_Error_Validacion_SGC = 0x9308,
    //    LG_TM_Error_Validacion_Card_Info = 0x9309,
    //    LG_TM_Tarjeta_Bloqueada = 0x930A,
    //    LG_TM_Tarjeta_Suspendida = 0x930B,
    //    LG_TM_Tarjeta_Vencida = 0x930C,
    //    LG_TM_Error_Validacion_Purse = 0x930D,
    //    LG_TM_Error_Validacion_History = 0x930E,
    //    LG_TM_Error_Validacion_Transit = 0x930F,
    //    LG_TM_Error_Validacion_Generico = 0x9310,
    //    LG_TM_Emisor_Inexistente = 0x9311,
    //    LG_TM_Error_Validacion_Aplicacion = 0x9312,
    //    LG_TM_Tarjeta_Activada = 0x9313,
    //    LG_TM_Existe_Mas_De_Una_Tarjeta = 0x9314,
    //    LG_TM_Carga_Dudosa = 0x9321,
    //    LG_TM_Error_Validacion_M1 = 0x9316,
    //    LG_SC_Slot_SmartCard_Incorrecto = 0x9FF0,
    //    LG_SC_TimeOut_Recepcion_SmartCard = 0x9FF1,
    //    LG_SC_APDU_Invalido = 0x9FF2,
    //    LG_SC_Largo_Invalido = 0x9FF3,
    //    LG_SC_StatusByte_invalido = 0x9FF4,
    //    LG_SC_ProcedureByte_Invalido = 0x9FF5,
    //    LG_SC_TimeOut_Recepcion_ATR_En_PowerUp = 0x9FF6,
    //    LG_SC_ATR_Invalido = 0x9FF7,
    //    LG_SC_SmartCard_No_Energizada = 0x9FF8,
    //    LG_SC_Protocolo_No_Soportado = 0x9FF9,
    //    LG_CMD_POSID_enviado_es_incorrecto = 0xF001,
    //    LG_CMD_Identificadores_enviados_por_el_switch_son_incorrectos = 0xF002,
    //    LG_CMD_Se_Requiere_Actualizar_Tablas = 0xF003,
    //    LG_CMD_El_largo_del_bloque_es_incorrecto = 0xF004,
    //    LG_CMD_El_bloque_de_la_tabla_es_incorrecto = 0xF005,
    //    LG_CMD_La_secuencia_enviada_es_incorrecta = 0xF006,
    //    LG_CMD_CRC_de_la_tabla_es_incorrecto = 0xF007,
    //    LG_HW_SAM_Incorrecto = 0xFE81,
    //    LG_SAM_PIN_ERROR_5 = 0x63C0,
    //    LG_SAM_PIN_ERROR_4 = 0x63C1,
    //    LG_SAM_PIN_ERROR_3 = 0x63C2,
    //    LG_SAM_PIN_ERROR_2 = 0x63C3,
    //    LG_SAM_PIN_ERROR_1 = 0x63C4,
    //    LG_CSTU_Bitmap_Invalido = 0x110
    //    //LG_WKLP_Not_Initialized = 0x80131430
    //    }

    /// <summary>
    /// esta clase representa la clase base de un 
    /// objeto de tipo LGCommand_Request.
    /// (Contiene la estructura del requerimiento de un comando LG)
    /// </summary>
    public class LGCommand_Request
        {
        protected CRC16_CCITT CRC_1021 = new CRC16_CCITT(InitialCrcValue.NonZero1);

        #region Constants

        private const byte _ETX = 0x03; /// fin del comando 
        private const byte _STX = 0x02; /// encabezado del comando
        private const byte _ACK = 0x06; /// ACK
        private const byte _NACK = 0x15;///NACK

        #endregion

        #region Properties

      
        ///Obtiene el encabezado del comando
        public byte STX
            {
            get { return _STX; }
            
            }

        protected byte _cmd;
        /// <summary>
        /// Obtiene o establece el codigo de comando
        /// </summary>
        public byte CMD
            {
            get { return _cmd; }
            set { _cmd = value; }
            }


        protected byte[] _len;

        /// <summary>
        /// Obtiene o establece la longitud del mensaje
        /// </summary>
        public UInt16 LEN
            {
            get { return Convert.ToUInt16(_len); }
            set { _len = BitConverter.GetBytes(value); }
            }


        protected byte[] _dataLG;
        /// <summary>
        /// obtiene o establece el valor del campo 
        /// DATALG
        /// </summary>
        public string DATALG
            {
            get { return Utilities.Utilities.ToString(_dataLG); }
            set { _dataLG =Utilities.Utilities.GetBytesBigEndian(value); }
            }


        private byte[] _crc;
        /// <summary>
        /// CRC del mensaje.
        /// (se calcula sobre los campos:
        /// CMD + LEN + DATA)
        /// </summary>
        public UInt16 CRC
            {
            get { return crc1021(); }
            set { _crc = BitConverter.GetBytes(value); }
            }


        /// <summary>
        /// Obtiene el fin del comando
        /// </summary>
        public byte ETX
            {
            get
                {
                return _ETX;
                }
            }

        #endregion


        #region Methods

        ///obtiene el nombre del comando LG
        ///a partir de su codigo en Hexadecimal
        public static string getCommandType(string value)
            {
            string CMD = "";
            TypeCommand tp;
            try
                {

                switch  (tp =(TypeCommand)Convert.ToByte(value, 16))
                    {
                    case TypeCommand.IWK:
                        CMD = "[IWK]";
                        break;
                    case TypeCommand.OID:
                        CMD = "[OID]";
                        break;
                    case TypeCommand.PIN:
                        CMD = "[PIN]";
                        break;
                    case TypeCommand.IDE:
                        CMD = "[IDE]";
                        break;
                    case TypeCommand.VID:
                        CMD = "[VID]";
                        break;
                    case TypeCommand.INI:
                        CMD = "[INI]";
                        break;
                    case TypeCommand.CINI:
                        CMD = "[CINI]";
                        break;
                    case TypeCommand.VTD:
                        CMD = "[VTD]";
                        break;
                    case TypeCommand.ATD:
                        CMD = "[ATD]";
                        break;
                    case TypeCommand.CL:
                        CMD = "[CL]";
                        break;
                    case TypeCommand.ATO:
                        CMD = "[ATO]";
                        break;
                    case TypeCommand.CATO:
                        CMD = "[CATO]";
                        break;
                    case TypeCommand.CTO:
                        CMD = "[CTO]";
                        break;
                    case TypeCommand.RCTO:
                        CMD = "[RCTO]";
                        break;
                    case TypeCommand.CSTU:
                        CMD = "[CSTU]";
                        break;
                    case TypeCommand.ACD:
                        CMD = "[ACD]";
                        break;
                    case TypeCommand.ACD2:
                        CMD = "[ACD2]";
                        break;
                    case TypeCommand.ST:
                        CMD = "[ST]";
                        break;
                    case TypeCommand.ET:
                        CMD = "[ET]";
                        break;
                    case TypeCommand.ETO:
                        CMD = "[ETO]";
                        break;
                    case TypeCommand.OTO:
                        CMD = "[OTO]";
                        break;
                    case TypeCommand.SCR:
                        CMD = "[SCR]";
                        break;
                    case TypeCommand.RCR:
                        CMD = "[RCR]";
                        break;
                    case TypeCommand.EDT:
                        CMD = "[EDT]";
                        break;
                    case TypeCommand.CTU:
                        CMD = "[CTU]";
                        break;
                    case TypeCommand.ACTU:
                        CMD = "[ACTU]";
                        break;
                    case TypeCommand.VTU:
                        CMD = "[VTU]";
                        break;
                    case TypeCommand.VPA:
                        CMD = "[VPA]";
                        break;
                    case TypeCommand.AVPA:
                        CMD = "[AVPA]";
                        break;
                    case TypeCommand.DTU:
                        CMD = "[DTU]";
                        break;
                    case TypeCommand.ADTU:
                        CMD = "[ADTU]";
                        break;
                    case TypeCommand.LDA:
                        CMD = "[LDA]";
                        break;
                    case TypeCommand.OLA:
                        CMD = "[OLA]";
                        break;
                    case TypeCommand.CAPP:
                        CMD = "[CAPP]";
                        break;
                    case TypeCommand.VLG:
                        CMD = "[VLG]";
                        break;
                    case TypeCommand.MAPL:
                        CMD = "[MAPL]";
                        break;
                    case TypeCommand.RST:
                        CMD = "[RST]";
                        break;
                    case TypeCommand.CSLG:
                        CMD = "[CSLG]";
                        break;
                    case TypeCommand.DLG:
                        CMD = "[DLG]";
                        break;
                    case TypeCommand.EBLG:
                        CMD = "[EBLG]";
                        break;
                    
                    }
                return CMD;
                }
            catch (Exception)
                {
                
                throw;
                }
            }
        
                /// <summary>
        /// obtiene un array de bytes que representa 
        /// la estructura del comando a enviar al LG
        /// </summary>
        /// <returns></returns>
        public virtual byte[] get()
            {
            List<Byte> cmd = new List<byte>();
            try
                {
                cmd.Add(_STX);
                cmd.Add(_cmd);
                cmd.AddRange(_len);
                if (_dataLG != null && _dataLG.Length > 0)
                    {
                    cmd.AddRange(_dataLG);
                    }
                cmd.AddRange(_crc);
                cmd.Add(_ETX);

                return cmd.ToArray();
                }
            catch (Exception)
                {

                throw;
                }
            }

        /// <summary>
        /// establece el valor de cada uno de los
        /// campos del comando a partir de un 
        /// array de bytes.
        /// </summary>
        /// <param name="value"></param>
        public virtual void set(byte[] value)
            {
            try
                {

                _cmd = value[0];

                _len[0] = value[1];
                _len[1] = value[2];

                for (int i = 0; i <= this.LEN; i++)
                    {
                    _dataLG[i] = value[i + 4];
                    }

                _crc[0] = value[value.Length - 3];
                _crc[1] = value[value.Length - 2];

                }
            catch (Exception)
                {

                throw;
                }

            }

        public virtual void set(string values)
            {
            set(Utilities.Utilities.GetBytesBigEndian(values));
            }


        private UInt16 crc1021()
            {
            byte[] array = null;

            array[0] = _cmd;
            array[1] = _len[0];
            array[2]= _len[1];

            for(int i= 0; i< _dataLG.Length-1;i++)
                {
                 array[i] = _dataLG[i];
                }

            return  CRC_1021.CRC_CCITT_UInt16(array);
            }

        #endregion

        
        }

    /// <summary>
    /// Esta clase representa la clase base de un 
    /// objeto de tipo LGCommand_Response.
    /// (Contiene la estructura de la respuesta de un comando LG)
    /// </summary>
    public class LGCommand_Response: LGCommand_Request
        {
        private byte[] _car;
        /// <summary>
        /// obtiene o establece el valor del
        /// codigo de respuesta
        /// </summary>
        public UInt16 CAR
            {
            get { return BitConverter.ToUInt16(_car,0); }
            set { _car = BitConverter.GetBytes(value); }
            }

        private byte[] _crc;
        /// <summary>
        /// CRC del mensaje.
        /// (se calcula sobre los campos:
        /// CMD + LEN + DATA)
        /// </summary>
        public UInt16 CRC
            {
            get { return crc1021(); }
            set { _crc = BitConverter.GetBytes(value); }
            }

        #region Methods


        /// <summary>
        /// obtiene un array de bytes que representa 
        /// la estructura del comando a enviar al LG
        /// </summary>
        /// <returns></returns>
        public override byte[] get()
            {
            List<Byte> cmd = new List<byte>();
            try
                {
                cmd.Add(this.STX);
                cmd.Add(this.CMD);
                cmd.AddRange(this._len);
                cmd.AddRange(this._dataLG);
                cmd.AddRange(this._crc);
                cmd.Add(this.ETX);

                return cmd.ToArray();
                }
            catch (Exception)
                {

                throw;
                }
            }

        /// <summary>
        /// establece el valor de cada uno de los
        /// campos del comando a partir de un 
        /// array de bytes.
        /// </summary>
        /// <param name="value"></param>
        public override void set(byte[] value)
            {
            try
                {

                this.CMD = value[0];
                this.CAR = value[1];
                this.CAR += value[2];

                this.LEN =(ushort) (value[3]  + value[4]);

                for (int i = 0; i <= this.LEN; i++)
                    {
                    this._dataLG[i] = value[i + 6];
                    }

               this.CRC = value[value.Length - 3];
               this.CRC += value[value.Length - 2] ;

                }
            catch (Exception)
                {

                throw;
                }

            }

        /// <summary>
        /// establece el valor de cada uno de los
        /// campos del comando a partir de una 
        /// cadena de caracteres hexadecimales.
        /// </summary>
        /// <param name="value"></param>
        public override void set(string value)
            {

            set(Utilities.Utilities.GetBytesBigEndian(value));
            }

        private UInt16 crc1021()
            {
            byte[] array= null;

            array[0] = _cmd;
            array[1] = _car[0];
            array[2] = _car[1];
            array[3] = _len[0];
            array[4] = _len[1];

            for (int i = 0; i < _dataLG.Length - 1; i++)
                {
                array[i] = _dataLG[i];
                }

            return CRC_1021.CRC_CCITT_UInt16(array);
            }

        #endregion

        }
    }
