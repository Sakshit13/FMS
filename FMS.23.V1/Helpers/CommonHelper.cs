
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace FMS._23.V1.Helpers
{
    public static class CommonHelper
    {
        private static object lockObject { get; set; }

        public static object LockObject
        {
            get
            {
                if (lockObject == null)
                    lockObject = new object();
                return lockObject;
            }
        }
        public static DateTime IndianStandard(DateTime currentDate)
        {
            TimeZoneInfo mountain = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime utc = currentDate;
            return TimeZoneInfo.ConvertTimeFromUtc(utc, mountain);
            //return DateTime.Now;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string generateID()
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            string number = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);

            return number;
        }
        public static string Get6DigitOTP()
        {
            Random generator = new Random();
            int r = generator.Next(1, 1000000);
            string result = r.ToString().PadLeft(6, '0');

            return result;
        }
        public static DataTable toDataTable(string json)
        {
            var result = new DataTable();
            var jArray = JArray.Parse(json);
            //Initialize the columns, If you know the row type, replace this   
            foreach (var row in jArray)
            {
                foreach (var jToken in row)
                {
                    var jproperty = jToken as JProperty;
                    if (jproperty == null) continue;
                    if (result.Columns[jproperty.Name] == null)
                        result.Columns.Add(jproperty.Name, typeof(string));
                }
            }
            // result.Columns.Add("OrderId", typeof(int));
            int Count = 1;
            foreach (var row in jArray)
            {
                var datarow = result.NewRow();
                foreach (var jToken in row)
                {
                    var jProperty = jToken as JProperty;
                    if (jProperty == null) continue;
                    datarow[jProperty.Name] = jProperty.Value.ToString();
                }
                // datarow["OrderId"] = Count.ToString();
                result.Rows.Add(datarow);
                Count++;
            }

            return result;
        }
        public static string SmsApiUrl(string ContactNo, string Msg)
        {
            string Sms = "http://bhashsms.com/api/sendmsg.php?user=Techinfy&pass=123456&sender=TCHNFY&phone=" + ContactNo + "&" + Msg + "&priority=ndnd&stype=normal";
            return Sms;
        }
        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        private static Random RNG = new Random();

        public static string Create16DigitString()
        {
            var builder = new System.Text.StringBuilder();
            while (builder.Length < 16)
            {
                builder.Append(RNG.Next(10).ToString());
            }
            return builder.ToString();
        }
        public static void WriteToJsonFile(string FName, string text, string path)
        {
            lock (LockObject)
            {
                string FileName = FName + DateTime.Now.ToString("dd-MM-yyyy") + ".json";
                path = path + FileName;
                //string path = HttpContext.Current.Server.MapPath("~/Files/" + FileName);
                // string path = HostingEnvironment.MapPath("/Files/" + FileName);
                if (!File.Exists(path))
                    File.Create(path).Dispose();
                using (TextWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(text);
                    writer.Close();
                }
            }
        }
        #region Constants
        public const string CCOde = "ORG/00001";
        #endregion
    }



}
