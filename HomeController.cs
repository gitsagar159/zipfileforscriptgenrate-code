using GenerateSPScript.Common;
using GenerateSPScript.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace GenerateSPScript.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetSPSchema()
        {
            clsSchemaScriptViewModel objModel = new clsSchemaScriptViewModel();

            try
            {
                
                string strSPName = "Dashboard_Transport_Challan";

                DataTable SPNameData = BLScript.GetModifyedSPNames(string.Empty);
                List<string> lstSPName = new List<string>();

                if(SPNameData.Rows.Count > 0)
                {
                    foreach(DataRow rowItem in SPNameData.Rows)
                    {
                        lstSPName.Add(rowItem["name"].ToString());
                    }
                }

                StringBuilder sbSPText = new StringBuilder();

                foreach (string spItem in lstSPName)
                {
                    sbSPText.Append("DROP PROCEDURE [" + spItem + "]");
                    sbSPText.Append("\n");
                    sbSPText.Append("GO");
                    sbSPText.Append("\n");
                }

                
               foreach (string spItem in lstSPName)
               {

                   var data = BLScript.SPSchemaReader(spItem);
                   DataSet ResDataSet = data;

                   if (ResDataSet.Tables[0].Rows.Count > 0)
                   {
                       foreach (DataRow rowItem in ResDataSet.Tables[0].Rows)
                       {
                           string strCurrentText = !string.IsNullOrEmpty(Convert.ToString(rowItem["Text"])) ? Convert.ToString(rowItem["Text"]) : "";
                           if(!string.IsNullOrEmpty(strCurrentText))
                           {
                               sbSPText.Append(strCurrentText);
                           }
                           
                       }
                   }

                    sbSPText.Append("\n");
                    sbSPText.Append("GO");
                    sbSPText.Append("\n");
                }

                objModel.strSPText = sbSPText.ToString();

                MemoryStream memoryStream = new MemoryStream();
                TextWriter tw = new StreamWriter(memoryStream);

                tw.WriteLine(sbSPText.ToString());
                tw.Flush();
                tw.Close();

                return File(memoryStream.GetBuffer(), "text/plain", "file.txt");

                //return View(objModel);

            }
            catch (Exception ex)
            {
                AppConstant.WriteLogFile(ex.ToString());
                return View(objModel);
            }
        }
    }
}