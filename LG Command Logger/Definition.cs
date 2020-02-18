using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace LG_Command_Logger
    {
    /// <summary>
    /// Obtiene o establece la configuracion de 
    /// la aplicacion
    /// </summary>
  public  class Definition
        {

        #region Properties
        String _path;
        /// <summary>
        /// Obtiene o establece el path 
        /// donde se encuentra el archivo xml
        /// de definiciones
        /// </summary>
        public String Path
            {
            get
                {
                return _path;
                }
            set
                {
                _path = value;
                }
            }
        #endregion

        #region Methods
      /// <summary>
      /// Constructor de clase
      /// </summary>
      /// <param name="Path">Obtiene el path donde se encuentra el 
      /// archivo de definiciones</param>
       public  Definition(String Path)
            {
            _path = Path;
            }


      

        /// <summary>
       /// Obtiene la configuracion de 
       /// la aplicacion
        /// </summary>
        /// <returns></returns>
       public void get<T>(ref T Config)
           {
           StreamReader objStreamReader = new StreamReader(_path);
         
           XmlSerializer x = new XmlSerializer(Config.GetType());
           try
               {

               Config =  (T)x.Deserialize(objStreamReader);
               objStreamReader.Close();
               }
           catch(Exception)
               {
               throw;
               }
           
           }


       /// <summary>
       /// Establece la configuracion de 
       /// la aplicacion
       /// </summary>
        public void set<T> (ref T Definitions)
            {
            StreamWriter objStreamWriter = new StreamWriter(_path);
            XmlSerializer x = new XmlSerializer(Definitions.GetType());
            try
                {
                x.Serialize(objStreamWriter, Definitions);
                objStreamWriter.Close();
                }
            catch (Exception)
                {
                throw;
                }

            }

      

        #endregion

        #region Auxiliary Methods
        #endregion

        }
    }
