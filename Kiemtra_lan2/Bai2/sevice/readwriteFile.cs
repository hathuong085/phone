using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Bai2.sevice
{
    static class readwriteFile<T>
    {
        public static void ReadData(string fullpath,ref T data)
        {
            using (StreamReader sr = File.OpenText(fullpath))
            {
                var obj = sr.ReadToEnd();
                data = JsonConvert.DeserializeObject<T>(obj);
            }
        }

        public static void WriteData(string fullpath, T data)
        {
            using (StreamWriter sw = File.CreateText(fullpath))
            {
                var resData = JsonConvert.SerializeObject(data);
                sw.WriteLine(resData);
            }
        }
    }
}
