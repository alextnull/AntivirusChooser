using System;
using System.Xml.Serialization;

namespace AntivirusChooser
{
    [Serializable]
    public class Answer
    {
        [XmlAttribute]
        public int ToNodeId { get; set; }
        [XmlAttribute]
        public string Text { get; set; }
    }
}
