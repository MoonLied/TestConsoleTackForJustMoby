using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleJson
{
    public partial class JSONNode
    {
        internal class Parser : IDisposable
        {
            private struct NodeState
            {
                public string TokenName;
                public JSONNode Node;
                public int Count;

                public override string ToString()
                {
                    return string.Format("{0}:{1}({2})", TokenName, Node, Count);
                }
            }

            private readonly List<NodeState> _stack = new List<NodeState>();
            private NodeState _ctx;
            private StringBuilder _token = new StringBuilder(200);
            private bool _tokenExists = false;
            private string _tokenName = string.Empty;

            public JSONNode Parse(JSONParseData aJSON, bool receipt = false)
            {
                bool quoteMode = false;
                char quoteSymbol = (char)0;

                _stack.Clear();
                _ctx = new NodeState();
                CleanToken();

                int i = 0;
                while (i < aJSON.Length)
                {
                    var curChar = aJSON.GetChar(i);
                    switch (curChar)
                    {
                        case '{':
                            if (quoteMode) goto default;

                            AddCollection(new JSONClass(false));
                            break;

                        case '[':
                            if (quoteMode) goto default;

                            AddCollection(new JSONArray(false));
                            break;

                        case '}':
                        case ']':
                            if (quoteMode) goto default;

                            if (_stack.Count <= _ctx.Count) throw new Exception("JSON Parse: Too many closing brackets");

                            if (_tokenExists) AddNode(_token.ToString(), _tokenName);
                            CleanToken();

                            var col = _ctx;
                            var array = col.Node as JSONArray;
                            var count = (int)col.Count;
                            if (array != null) array.Init(count);
                            else ((JSONClass)col.Node).Init(count);

                            for (int j = _stack.Count - count; j < _stack.Count; j++)
                            {
                                var child = _stack[j];
                                col.Node.Add(child.TokenName, child.Node);
                            }
                            _stack.RemoveRange(_stack.Count - count, count);

                            _ctx = _stack[_stack.Count - 1];
                            _stack.RemoveAt(_stack.Count - 1);
                            AddNode(col.Node, col.TokenName);
                            _tokenExists = false;
                            break;

                        case ':':
                        case '=':
                            if (!receipt && curChar == '=') goto default;

                            if (quoteMode)
                            {
                                _token.Append(curChar);
                                break;
                            }
                            _tokenName = _token.ToString();
                            _token.Remove(0, _token.Length);
                            break;

                        case '"':
                        case '\'':
                            if (!quoteMode)
                            {
                                quoteMode = true;
                                quoteSymbol = curChar;
                            }
                            else if (quoteSymbol == curChar)
                            {
                                quoteMode = false;
                                _tokenExists = true;
                            }
                            else
                            {
                                goto default;
                            }
                            break;

                        case ',':
                        case ';':
                            if (!receipt && curChar == ';') goto default;
                            if (quoteMode)
                            {
                                _token.Append(curChar);
                                break;
                            }

                            if (_tokenExists) AddNode(_token.ToString(), _tokenName);
                            CleanToken();
                            break;

                        case '\r':
                        case '\n':
                            break;

                        case ' ':
                        case '\t':
                            if (quoteMode) goto default;
                            break;

                        case '\\':
                            ++i;
                            if (quoteMode)
                            {
                                var C = aJSON.GetChar(i);
                                switch (C)
                                {
                                    case 't':
                                        _token.Append('\t');
                                        break;
                                    case 'r':
                                        _token.Append('\r');
                                        break;
                                    case 'n':
                                        _token.Append('\n');
                                        break;
                                    case 'b':
                                        _token.Append('\b');
                                        break;
                                    case 'f':
                                        _token.Append('\f');
                                        break;
                                    case 'u':
                                    {
                                        var s = aJSON.GetString(i + 1, 4);
                                        _token.Append((char)int.Parse(s, System.Globalization.NumberStyles.AllowHexSpecifier));
                                        i += 4;
                                        break;
                                    }
                                    default:
                                        _token.Append(C);
                                        break;
                                }
                            }
                            break;

                        default:
                            _token.Append(curChar);
                            _tokenExists = true;
                            break;
                    }
                    ++i;
                }
                if (quoteMode)
                {
                    throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
                }

                if (_stack.Count > 0)
                    return _stack[0].Node;
                if (_tokenExists)
                    return (JSONNode) _token.ToString();

                return null;
            }

            private void AddCollection(JSONNode node)
            {
                var next = new NodeState { Node = node, TokenName = _tokenName };

                _stack.Add(_ctx);
                _ctx = next;

                CleanToken();
            }
            private void AddNode(JSONNode node, string nodeTokenName)
            {
                _stack.Add(new NodeState { Node = node, TokenName = nodeTokenName });
                if ((_ctx.Node is JSONArray || _ctx.Node is JSONClass)) ++_ctx.Count;
            }
            
            private void CleanToken()
            {
                _tokenName = string.Empty;
                _token.Remove(0, _token.Length);
                _tokenExists = false;
            }

            public void Dispose()
            {
                _stack.Clear();
                _ctx = new NodeState();
                _tokenName = null;
                _token = null;
            }
        }
    }
}