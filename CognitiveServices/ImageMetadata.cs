using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveServices
{
    public class ImageMetadata
    {

        public string Caption { get; set; }

        public ICollection<string> Tags { get; set; }

        public string Format { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

    }
}
