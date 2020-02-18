using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGCommands
    {

    public enum ErrorCodes
        {
        LG_OK = 0x9000,
        LG_Comando_No_Reconocido = 0x9001,
        LG_Comando_No_Habilitado = 0x9002,
        LG_Identificacion_Requerida = 0x9003,
        LG_Inicializacion_Requerida = 0x9004,
        LG_Parametro_Incorrecto = 0x9005,
        LG_WK_Bloqueada = 0x9006,
        LG_Verificacion_Fallida = 0x9007,
        LG_Bloqueado = 0x9008,
        LG_Actualizando_FW = 0x9009,
        LG_Actualizacion_Requerida = 0x900A,
        LG_Apertura_Turno_Requerida = 0x900B,
        LG_Cierre_Turno_Requerido = 0x900C,
        LG_Apertura_Turno_Ya_Efectuada = 0x900D,
        LG_Limite_Horas_Sin_Inicializar = 0x900E,
        LG_Limite_Turnos_Sin_Inicializar = 0x900F,
        LG_Limite_Recargas_Por_Turno = 0x9010,
        LG_Pin_Requerido = 0x9011,
        LG_MSG_NORESP_ERR = 0x90F1,
        LG_MSG_LENGHT_ERR = 0x90F2,
        LG_MSG_PACKET_ERR = 0x90F3,
        LG_TM_Hw_No_Responde = 0x9300,
        LG_TM_Ninguna_O_Mas_De_Una_Tarjeta = 0x9301,
        LG_TM_Modelo_De_Tarjeta_No_Soportado = 0x9302,
        LG_TM_Tarjeta_No_Compatible_SUBE = 0x9303,
        LG_TM_Tarjeta_NO_Corresponde_Con_Seleccionada = 0x9304,
        LG_TM_Error_Autenticacion = 0x9305,
        LG_TM_Error_Lectura = 0x9306,
        LG_TM_Error_Escritura = 0x9307,
        LG_TM_Error_Validacion_SGC = 0x9308,
        LG_TM_Error_Validacion_Card_Info = 0x9309,
        LG_TM_Tarjeta_Bloqueada = 0x930A,
        LG_TM_Tarjeta_Suspendida = 0x930B,
        LG_TM_Tarjeta_Vencida = 0x930C,
        LG_TM_Error_Validacion_Purse = 0x930D,
        LG_TM_Error_Validacion_History = 0x930E,
        LG_TM_Error_Validacion_Transit = 0x930F,
        LG_TM_Error_Validacion_Generico = 0x9310,
        LG_TM_Emisor_Inexistente = 0x9311,
        LG_TM_Error_Validacion_Aplicacion = 0x9312,
        LG_TM_Tarjeta_Activada = 0x9313,
        LG_TM_Existe_Mas_De_Una_Tarjeta = 0x9314,
        LG_TM_Carga_Dudosa = 0x9321,
        LG_TM_Error_Validacion_M1 = 0x9316,
        LG_SC_Slot_SmartCard_Incorrecto = 0x9FF0,
        LG_SC_TimeOut_Recepcion_SmartCard = 0x9FF1,
        LG_SC_APDU_Invalido = 0x9FF2,
        LG_SC_Largo_Invalido = 0x9FF3,
        LG_SC_StatusByte_invalido = 0x9FF4,
        LG_SC_ProcedureByte_Invalido = 0x9FF5,
        LG_SC_TimeOut_Recepcion_ATR_En_PowerUp = 0x9FF6,
        LG_SC_ATR_Invalido = 0x9FF7,
        LG_SC_SmartCard_No_Energizada = 0x9FF8,
        LG_SC_Protocolo_No_Soportado = 0x9FF9,
        LG_CMD_POSID_enviado_es_incorrecto = 0xF001,
        LG_CMD_Identificadores_enviados_por_el_switch_son_incorrectos = 0xF002,
        LG_CMD_Se_Requiere_Actualizar_Tablas = 0xF003,
        LG_CMD_El_largo_del_bloque_es_incorrecto = 0xF004,
        LG_CMD_El_bloque_de_la_tabla_es_incorrecto = 0xF005,
        LG_CMD_La_secuencia_enviada_es_incorrecta = 0xF006,
        LG_CMD_CRC_de_la_tabla_es_incorrecto = 0xF007,
        LG_HW_SAM_Incorrecto = 0xFE81,
        LG_SAM_PIN_ERROR_5 = 0x63C0,
        LG_SAM_PIN_ERROR_4 = 0x63C1,
        LG_SAM_PIN_ERROR_3 = 0x63C2,
        LG_SAM_PIN_ERROR_2 = 0x63C3,
        LG_SAM_PIN_ERROR_1 = 0x63C4,
        LG_CSTU_Bitmap_Invalido = 0x110
        //LG_WKLP_Not_Initialized = 0x80131430
        }


    public enum ErrorCategory
        {
        Technical = 1, ///codigos de error asociados a los estados del LG
        CardOperation = 2,///codigos de error asociados a operaciones sobre la tarjeta
        Information = 3,///codigos de error OK
        Critical = 4,///codigos de error criticos
        No_definido = 5          ///
        }
   public  class Error_Code
       {

      


       #region Properties

       private  int _errorCoded;

       public  int ErrorCode
           {
           get { return _errorCoded; }
           set { _errorCoded = value; }
           }


       private  string _descripcion;

       public  string Descripcion
           {
           get { return _descripcion; }
           set { _descripcion = value; }
           }


       private  int _category;

       public int Category
           {
           get { return _category; }
           set { _category = value; }
           }
       
       #endregion

       
       }
    }
