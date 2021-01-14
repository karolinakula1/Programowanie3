using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace karolinakulalista3
{
    public class Class1
    {
        public string imie { get; set;}
        public string nazwisko { get; set;}
        public string pesel { get; set;}
        public string rokur { get; set;}
        public string specjalizacja { get; set;}
        public string oddzial { get; set;}

        [XmlIgnore]
        public BitmapSource zdjecie { get; set;}


        [XmlElement("zdjecie")]

        public byte[] ImageBuffer
        {
            get
            {
                byte[] ImageBuffer = null;

                if (zdjecie != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        var encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(zdjecie));
                        encoder.Save(stream);
                        ImageBuffer = stream.ToArray();
                    }
                }
                return ImageBuffer;
            }
            set
            {
                if ( value == null)
                {
                    zdjecie = null;
                }
                else
                {
                    using (var stream = new MemoryStream(value))
                    {
                        var decoder = BitmapDecoder.Create(stream,
                            BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        zdjecie = decoder.Frames[0];
                    }
                }
            }
        }

    }
}
