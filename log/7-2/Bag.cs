namespace test
{
    internal class Bag
    {
        private string _color;
        private string[] _containedBagColors;
        private int[] _containedBagCounts;

        public Bag(string color_, string[] containedBagColors_, int[] containedBagCounts_)
        {
            _color = color_;
            _containedBagColors = containedBagColors_;
            _containedBagCounts = containedBagCounts_;
        }

        // Checks direct containment only
        public bool Contains(string keyColor)
        {
            foreach (string color in ContainedBagColors)
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

        public string[] ContainedBagColors
        {
            get { return _containedBagColors; }
        }

        public int[] ContainedBagCounts
        {
            get { return _containedBagCounts; }
        }
    }
}
