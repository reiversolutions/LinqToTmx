#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LINQtoTMX.Serializable
{
    [Serializable]
    public class Header
    {
        [XmlAttribute("creationtoolversion")]
        public string CreationToolVersion { get; set; }
        
        [XmlAttribute("datatype")]
        public string DataType { get; set; }
        
        [XmlAttribute("segtype")]
        public string SegmentType { get; set; }
        
        [XmlAttribute("adminlang")]
        public string AdminLanguage { get; set; }
        
        [XmlAttribute("srclang")]
        public string SourceLanguage { get; set; }
        
        [XmlAttribute("o-tmf")]
        public string OriginalTranslationMemoryFormat { get; set; }
        
        [XmlAttribute("creationtool")]
        public string CreationTool { get; set; }
    }
}
#pragma warning restore 1591