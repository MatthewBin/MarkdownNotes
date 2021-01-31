using MarkdownNodes.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MarkdownNodes.utils
{
    public class Helper
    {
        public static MarkDownDir GetMarkDownDir(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                return null;
            }

            DirectoryInfo info = new DirectoryInfo(dirPath);
            FileInfo[] files = info.GetFiles("*.md", SearchOption.AllDirectories);
            MarkDownDir dir = new MarkDownDir()
            {
                FullPath = dirPath,
                DisplayName = info.Name,
                MarkDownFiles = new List<MarkDownFile>()
            };

            for (int i = 0; i < files.Length; i++)
            {
                MarkDownFile mf = new MarkDownFile()
                {
                    Name = files[i].Name,
                    FullName = files[i].FullName
                };
                dir.MarkDownFiles.Add(mf);
            }
            return dir;
        }

        /// <summary>
        /// 将对象序列化为指定的文件名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool XmlSerializer<T>(T obj, string fileName)
        {
            try
            {
                FileStream fs = new FileStream(fileName,FileMode.OpenOrCreate);
                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(fs, obj);
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 将xml文件进行反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(string fileName)
        {
            try
            {
                if (File.Exists(fileName) == false)
                    return default(T);
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                XmlSerializer xs = new XmlSerializer(typeof(T));
                T obj = (T)xs.Deserialize(fs);
                return obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
