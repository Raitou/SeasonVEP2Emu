using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using libcomservice.REQUEST;
using System.IO;

namespace libcomservice
{
    public static class Ultilize
    {        
        public static Database DATA;

        public static void ConfigServer()
        {
            LoadConfigurations();
        }

        private static void LoadConfigurations()
        {
            var Ini_DBConfigurator = new TIniFile(string.Format(@"{0}\odbc.dsn", Directory.GetCurrentDirectory()));
            DATA = new Database
                (
                    Ini_DBConfigurator.Read("ODBC", "SERVER"),
                    Ini_DBConfigurator.Read("ODBC", "DATABASE"),
                    Ini_DBConfigurator.Read("ODBC", "UID"),
                    Ini_DBConfigurator.Read("ODBC", "PWD")
                );
            DataSet Query = new DataSet();
        }
    }
}
