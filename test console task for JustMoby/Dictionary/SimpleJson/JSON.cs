namespace SimpleJson
{
    public static class JSON
    {
        public static JSONNode Parse(string aJSON)
        {
            return JSONNode.Parse(new JSONStringParseData(aJSON));
        }

        public static JSONNode Parse(byte[] bytes)
        {
            return JSONNode.Parse(new JSONBytesParseData(bytes));
        }

        public static JSONNode ParseIosReceipt(byte[] bytes)
        {
            return JSONNode.Parse(new JSONBytesParseData(bytes), true);
        }
    }
}