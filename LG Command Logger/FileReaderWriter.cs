using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace LG_Command_Logger
    {
   public class FileReaderWriter
        {
            /// <summary>
      /// agrega una linea de texto al archivo Log
      /// definido en el parametro Path
      /// </summary>
      /// <param name="message">el mensaje a escribir en archivo</param>
      /// <param name="Path">la ruta completa, incluido el nombre de archivo del Log</param>
       public void log(string message, string Path)
          {
           StreamWriter writer = new StreamWriter(Path, true);
          try
              {
              lock (writer)
                  {
                  writer.WriteLine(System.DateTime.Now.ToString() + " " + message);
                  writer.Flush();
                  writer.Dispose();
                  }
              }
          catch (Exception)
              {
              
              
              }
          }

        }
    }
        
    
