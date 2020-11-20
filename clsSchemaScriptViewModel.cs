using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GenerateSPScript.Models
{
    public class clsSchemaScriptViewModel
    {
        public DataTable tblData { get; set; }
        public DataSet dsData { get; set; }

        public string strSPText { get; set; }
    }
}