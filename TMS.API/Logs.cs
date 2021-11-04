using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.API
{
    public class Logs
    {
        public void Logger(Exception ex)
        {
            try
            {
                string NoDetail = ex.InnerException == null ? "No Detail" : ex.InnerException.ToString();
                DateTime ExceptionDate = System.DateTime.Now;
                if (!File.Exists("Portal_Logs.txt"))
                {
                    File.Create("Portal_Logs.txt").Close();
                    using (StreamWriter sw = File.AppendText("Portal_Logs.txt"))
                    {
                        sw.WriteLine($"Exception Message: {ex.Message} \nDate: {DateTime.Now.ToString("dd/MM/yyyy hh:mm tt")} \nDetail: {NoDetail}");
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText("Portal_Logs.txt"))
                    {
                        sw.WriteLine($"Exception Message: {ex.Message} \nDate: {DateTime.Now.ToString("dd/MM/yyyy hh:mm tt")} \nDetail: {NoDetail}");
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void Logger(string message)
        {
            try
            {
                string NoDetail = "No Detail";
                DateTime ExceptionDate = System.DateTime.Now;
                if (!File.Exists("Portal_Logs.txt"))
                {
                    File.Create("Portal_Logs.txt").Close();
                    using (StreamWriter sw = File.AppendText("Portal_Logs.txt"))
                    {
                        sw.WriteLine($"Log Message: {message} \nDate: {DateTime.Now.ToString("dd/MM/yyyy hh:mm tt")} \nDetail: {NoDetail}");
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText("Portal_Logs.txt"))
                    {
                        sw.WriteLine($"Log Message: {message} \nDate: {DateTime.Now.ToString("dd/MM/yyyy hh:mm tt")} \nDetail: {NoDetail}");
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
