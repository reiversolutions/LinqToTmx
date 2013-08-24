using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using LINQtoTMX.Serializable;

namespace LINQtoTMX.Internal
{
    internal class LanguageRepository : AbstractLanguageRepository
    {
        #region private variables
        private AbstractTmxParser Parser = new TmxParser();
        #endregion

        internal override List<TmxFile> Get(DirectoryInfo languageRepositoryRoot, string languageCode)
        {
            string directory = Path.Combine(languageRepositoryRoot.FullName, languageCode);

            if (Directory.Exists(directory))
            {
                return Get(new DirectoryInfo(directory));
            }

            directory = Path.Combine(
                languageRepositoryRoot.FullName, 
                languageCode.Split('-').First<string>()
            );

            if (Directory.Exists(directory))
            {
                return Get(new DirectoryInfo(directory));
            } else
            {
                // Return that there is no files by a specific language code
                return new List<TmxFile>();
            }
        }

        internal override List<TmxFile> Get(DirectoryInfo languageRepositoryRoot)
        {
            return ParseDirectory(languageRepositoryRoot);
        }

        internal override bool Add(TmxFile file)
        {
            try
            {
                if (file.FileInfo.Directory.Exists && file.FileInfo.Extension == ".tmx" && file.Data.Body != null)
                {
                    Serialize(file);
                    return true;
                } else
                {
                    return false;
                }
            } catch
            {
                return false;
            }
        }

        internal override bool Update(TmxFile file)
        {
            try
            {
                if (file.FileInfo.Exists && file.Data.Body != null)
                {
                    Serialize(file);
                    return true;
                } else
                {
                    return false;
                }
            } catch
            {
                return false;
            }
        }

        internal override bool Delete(TmxFile file)
        {
            if (file.FileInfo.Exists)
            {
                File.Delete(file.FileInfo.FullName);
                return true;
            } else
            {
                return false;
            }
        }

        #region private methods
        private List<TmxFile> ParseDirectory(DirectoryInfo directory)
        {
            List<TmxFile> files = new List<TmxFile>();

            if (!Directory.Exists(directory.FullName))
            {
                Directory.CreateDirectory(directory.FullName);
            } else
            {
                foreach (FileInfo file in directory.GetFiles())
                {
                    files.Add(ParseFile(file));
                }

                // Parse each subdirectory using recursion.
                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    files.AddRange(ParseDirectory(subDirectory));
                }
            }

            return files;
        }

        private TmxFile ParseFile(FileInfo file)
        {
            return Parser.ParseFile(file);
        }

        private void Serialize(TmxFile file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Tmx));
            using (StreamWriter writer = new StreamWriter(file.FileInfo.FullName))
            {
                using (XmlWriter xwriter = XmlWriter.Create(writer, new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 }))
                {
                    xwriter.WriteDocType("tmx", "SYSTEM", "tmx14.dtd", null);
                    serializer.Serialize(xwriter, file.Data);
                }
            }
        }
        #endregion
    }
}