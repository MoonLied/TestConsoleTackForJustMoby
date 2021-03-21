using System.Text;

namespace SimpleJson
{
    abstract class JSONParseData
    {
        public abstract int Length { get; }
        public abstract char GetChar(int index);
        public abstract string GetString(int index, int length);
    }

    class JSONStringParseData : JSONParseData
    {
        private readonly string _text;

        public override int Length { get { return _text.Length; } }

        public JSONStringParseData(string text) { _text = text; }

        public override char GetChar(int index)
        {
            return _text[index];
        }

        public override string GetString(int index, int length)
        {
            return _text.Substring(index, length);
        }
    }

    class JSONBytesParseData : JSONParseData
    {
        private readonly byte[] _bytes;

        public override int Length { get { return _bytes.Length; } }

        public JSONBytesParseData(byte[] bytes) { _bytes = bytes; }

        public override char GetChar(int index)
        {
            return (char) _bytes[index];
        }

        public override string GetString(int index, int length)
        {
            return Encoding.UTF8.GetString(_bytes, index, length);
        }
    }
}
