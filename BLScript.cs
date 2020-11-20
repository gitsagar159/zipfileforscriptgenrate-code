using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GenerateSPScript.Models
{
    public class BLScript
    {
        
        public static DataSet SPSchemaReader(string strSPName)
        {
            try
            {
                return DLScript.SPSchemaReader(strSPName);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GetModifyedSPNames(string strSPName)
        {
            try
            {
                return DLScript.GetModifyedSPNames(strSPName);
            }
            catch
            {
                throw;
            }
        }
    }
}