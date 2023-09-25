
using System.Xml.Serialization;

namespace XTC.FMP.MOD.FenceMenu.LIB.Unity
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class MyConfig : MyConfigBase
    {
        public class Background
        {
            public class Image
            {
                [XmlAttribute("active")]
                public bool active { get; set; } = false;
                [XmlAttribute("source")]
                public string source { get; set; } = "";
            }

            public class Video
            {
                [XmlAttribute("active")]
                public bool active { get; set; } = false;
                [XmlAttribute("source")]
                public string source { get; set; } = "";
            }

            [XmlAttribute("visible")]
            public bool visible { get; set; } = true;
            [XmlElement("Image")]
            public Image image { get; set; } = new Image();
            [XmlElement("Video")]
            public Video video { get; set; } = new Video();
        }

        public class Content
        {
            [XmlAttribute("bg")]
            public string bg { get; set; } = "";
            [XmlElement("SlotAnchor")]
            public Anchor slotAnchor { get; set; } = new Anchor();
            [XmlElement("CloseButton")]
            public UiElement btnClose { get; set; } = new UiElement();
        }

        public class Cell
        {
            [XmlAttribute("image")]
            public string image { get; set; } = "";
            [XmlAttribute("size")]
            public int size { get; set; } = 0;
            [XmlElement("Entry")]
            public UiElement entry { get; set; }
            [XmlArray("SubjectS"), XmlArrayItem("Subject")]
            public Subject[] subjectS { get; set; } = new Subject[0];
        }

        public class Style
        {
            [XmlAttribute("name")]
            public string name { get; set; } = "";

            [XmlElement("Background")]
            public Background background { get; set; } = new Background();
            [XmlElement("Content")]
            public Content content { get; set; } = new Content();

            [XmlArray("DecalS"), XmlArrayItem("Decal")]
            public UiElement[] decalS { get; set; } = new UiElement[0];
            [XmlArray("CellS"), XmlArrayItem("Cell")]
            public Cell[] cellS { get; set; } = new Cell[0];
        }


        [XmlArray("Styles"), XmlArrayItem("Style")]
        public Style[] styles { get; set; } = new Style[0];
    }
}

