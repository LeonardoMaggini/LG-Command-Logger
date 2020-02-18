using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGCommands
    {
  public   class ErrorCodeList
      {
      #region Properties

      private  List<Error_Code> _errorCodeList = new List<Error_Code>() ;

      public  List<Error_Code> ErrorCodes
          {
          get { return _errorCodeList  ; }
          set { _errorCodeList = value; }
          }
      

      #endregion


      //#region Methods


      ///// <summary>
      ///// obtiene la descripcion de un error a partir 
      ///// del codigo de error
      ///// </summary>
      ///// <param name="ErrorCode">codigo de error</param>
      ///// <returns></returns>
      public string getErrorDescription(int ErrorCode)
          {
          foreach (Error_Code err in _errorCodeList)
              {
              if (err.ErrorCode == ErrorCode)
                  {
                  return err.Descripcion;
                  }
              }
          return "Codigo de error no definido";
          }


      ///// <summary>
      ///// obtiene el la categoria asociada a un codigo de error.
      ///// </summary>
      ///// <param name="ErrorCode"></param>
      ///// <returns></returns>
      public LGCommands.ErrorCategory getErrorCategory(int ErrorCode)
          {
          try
              {

              foreach (Error_Code err in _errorCodeList)
                  {
                  if (err.ErrorCode == ErrorCode)
                      {
                      return (ErrorCategory) err.Category;
                      }
                  }
              return ErrorCategory.No_definido;

              }
          catch (Exception)
              {

              throw;
              }

          }


      public LGCommands.ErrorCategory getErrorCategory(string ErrorCode)
          {
          try
              {

              int code = Convert.ToInt32(ErrorCode, 16);
              
              return getErrorCategory(code);

              }
          catch (Exception)
              {

              throw;
              }

          }
      //#endregion


      }
    }
