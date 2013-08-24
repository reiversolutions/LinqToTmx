using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using LINQtoTMX.Serializable;

namespace LINQtoTMX.Internal
{
    internal class TmxParser : AbstractTmxParser
    {
        internal override TmxFile ParseFile(FileInfo file)
        {
            if (file.Exists && file.Extension == ".tmx")
            {
                TmxFile tmxFile = new TmxFile();

                // Allocate file information
                tmxFile.FileInfo = file;
                tmxFile.Id = Guid.NewGuid();
                
                // Assign file contents
                tmxFile.Data = Parse(file);

                // Assign language code
                tmxFile.LanguageCode = 
                    tmxFile.Data.Body.TranslationUnits.Find(tuv => tuv.TranslationUnitVariants.Count > 0) != null
                    ? 
                        tmxFile.Data
                        .Body
                        .TranslationUnits
                            .First<TranslationUnit>(tu => tu.TranslationUnitVariants.Count > 0)
                        .TranslationUnitVariants
                            .First<TranslationUnitVariant>()
                        .Language
                    : 
                        "Unkown"
                    ;

                return tmxFile;
            } else
            {
                // Return that file does not exist
                return null;
            }
        }

        #region private methods
        private Tmx Parse(FileInfo file)
        {
            if (file.Exists && file.Extension == ".tmx")
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Tmx));
                Tmx data = null;
                using (StreamReader reader = new StreamReader(file.FullName))
                {
                    data = (Tmx) serializer.Deserialize(reader);
                }

                return data;
            } else
            {
                return null;
            }
        }
        #endregion
    }
}
