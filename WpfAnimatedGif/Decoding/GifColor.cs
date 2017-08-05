namespace WpfAnimatedGif.Decoding
{
    internal struct GifColor
    {
        private readonly byte _r;
        private readonly byte _g;
        private readonly byte _b;

        internal GifColor(byte r, byte g, byte b)
        {
            _r = r;
            _g = g;
            _b = b;
        }

        public int R { get { return _r; } }
        public int G { get { return _g; } }
        public int B { get { return _b; } }

        public override string ToString()
        {
            return string.Format("#{0:x2}{1:x2}{2:x2}", _r, _g, _b);
        }
    }
}
