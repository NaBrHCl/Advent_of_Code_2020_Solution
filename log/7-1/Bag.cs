namespace test
{
    internal class Bag
    {
        private string _color;
        private string[] _containedColors;

        public Bag(string color_, string[] containedColors_)
        {
            _color = color_;
            _containedColors = containedColors_;
        }

        // Checks direct containment only
        public bool Contains(string keyColor)
        {
            foreach (string color in ContainedColors)
            {
                if (color == keyColor)
                    return true;
            }

            return false;
        }

        public string Color
        {
            get { return _color; }
        }

        public string[] ContainedColors
        {
            get { return _containedColors; }
        }
    }
}
