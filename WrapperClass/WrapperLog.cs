using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace WrapperUnion
{
    public class WrapperLog
    {
        public  WrapperLog(string filepath)
        {
            FILE_PATH = filepath;
        }

        private string FILE_PATH { get;set; }

        public List<string> data = new List<string>();

        public void Load(string strFilePath)
        {
            FILE_PATH = strFilePath;
            Load();
        }
        public void Load()
        {
            if( File.Exists( FILE_PATH) == true)
            {
                data.Clear();
                
                StreamReader reader = new StreamReader(FILE_PATH);
                try
                {
                    do
                    {
                        data.Add(reader.ReadLine());
                    }
                    while (reader.Peek() != -1);
                }
                catch
                {
                    Console.WriteLine("Error Reading INI...");
                }

                finally
                {
                    reader.Close();
                }
            }
        }

        public void Save()
        {
            if( FILE_PATH != string.Empty)
            {
                StreamWriter writer = new StreamWriter(FILE_PATH);

                foreach( string s in data )
                {
                    writer.WriteLine(s);
                }
                writer.Close();
            }
        }
    }
}
