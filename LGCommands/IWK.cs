using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGCommands
{

    public class IWK_Request:LGCommand_Request
        {
        
        }


    public class IWK_Response : LGCommand_Response
        {

       
        private byte[] _wklp = new byte[8];
        /// <summary>
        /// obtiene o establece el valor de la
        /// Working Key LG-POS
        /// </summary>
        public string WKLP
            {
            get { return Utilities.Utilities.ToString( _wklp); }
            set
                {
                for (int i = 0; i <= _dataLG.Length; i++)
                    {
                    if (i < 7)
                        {
                        _wklp[i] = _dataLG[i];
                        }
                    }
                }
            }

        private byte[] _xi= new byte[8];
        /// <summary>
        /// obtiene o establece el valor de la clave XI
        /// </summary>
        public byte[] XI
            {
            get { return _xi; }
            set { _xi = value; }
            }


        }

}
