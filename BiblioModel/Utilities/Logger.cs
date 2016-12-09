using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BiblioModel.Utilities
{
    public class Logger
    {
        public static void writeToLog(string content) 
        {
            try
            {
                File.AppendAllText("ideibiblio.log", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " >> " + content + Environment.NewLine);
            }
            catch (Exception)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " >> " + content + Environment.NewLine);
                throw;
            }            
        }

        public static void writeToLog(Exception e)
        {
            try
            {
                File.AppendAllText("ideibiblio.log", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " >> " + e.Message + e.StackTrace + Environment.NewLine);
            }
            catch (Exception)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " >> " + e.Message + e.StackTrace + Environment.NewLine);
                throw;
            }  
        }

    }
}
