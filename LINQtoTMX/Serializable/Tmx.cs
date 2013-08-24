#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LINQtoTMX.Serializable
{
    [Serializable]
    [XmlRoot("tmx")]
    public class Tmx
    {
        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlElement("header")]
        public Header Header { get; set; }

        [XmlElement("body")]
        public Body Body { get; set; }
    }
}
#pragma warning restore 1591