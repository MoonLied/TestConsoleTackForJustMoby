namespace SimpleJson
{
    public class JSONData : JSONNode
    {
        private string m_Data;

        private bool IsSimpleType
        {
            get
            {
                switch (_dataType)
                {
                    case DataType.String:
                        return false;
                }

                return true;
            }
        }


        public override string Value
        {
            get
            {
                return GetStringValue();
            }
            set
            {
                m_Data = value;
                _dataType = DataType.String;
            }
        }

        public JSONData(string aData)
        {
            m_Data = aData;
        }

        public JSONData(float aData)
        {
            AsFloat = aData;
        }

        public JSONData(double aData)
        {
            AsDouble = aData;
        }

        public JSONData(bool aData)
        {
            AsBool = aData;
        }

        public JSONData(long aData)
        {
            AsLong = aData;
        }

        public override System.Text.StringBuilder ToStringBuilder(System.Text.StringBuilder builder)
        {
            if (IsSimpleType)
            {
                builder.Append(GetStringValue());
            }
            else
            {
                builder.Append("\"");
                EscapeToBuilder(builder, GetStringValue());
                builder.Append("\"");
            }
            return builder;
        }

        public override void Serialize(System.IO.BinaryWriter aWriter)
        {
            var tmp = new JSONData("");

            tmp.AsInt = AsInt;
            if (tmp.m_Data == this.m_Data)
            {
                aWriter.Write((byte)JSONBinaryTag.IntValue);
                aWriter.Write(AsInt);
                return;
            }
            tmp.AsFloat = AsFloat;
            if (tmp.m_Data == this.m_Data)
            {
                aWriter.Write((byte)JSONBinaryTag.FloatValue);
                aWriter.Write(AsFloat);
                return;
            }
            tmp.AsDouble = AsDouble;
            if (tmp.m_Data == this.m_Data)
            {
                aWriter.Write((byte)JSONBinaryTag.DoubleValue);
                aWriter.Write(AsDouble);
                return;
            }

            tmp.AsBool = AsBool;
            if (tmp.m_Data == this.m_Data)
            {
                aWriter.Write((byte)JSONBinaryTag.BoolValue);
                aWriter.Write(AsBool);
                return;
            }
            aWriter.Write((byte)JSONBinaryTag.Value);
            aWriter.Write(m_Data);
        }


        private enum DataType
        {
            String,
            Long,
            ULong,
            Int,
            UInt,
            Double,
            Bool
        }

        private DataType _dataType = DataType.String;
        private long _dataBuffer = 0;

        public override long AsLong
        {
            get
            {
                if (_dataType != DataType.Long)
                {
                    long v;
                    if (long.TryParse(Value, out v)) AsLong = v;
                    if (_dataType != DataType.Long) AsLong = (long) AsDouble;
                }
                return _dataBuffer;
            }
            set
            {
                _dataType = DataType.Long;
                _dataBuffer = value;
            }
        }

        public override unsafe ulong AsULong
        {
            get
            {
                if (_dataType != DataType.ULong)
                {
                    ulong v;
                    if (ulong.TryParse(Value, out v)) AsULong = v;
                    if (_dataType != DataType.ULong) AsULong = (ulong) AsDouble;
                }
                fixed (long* l = &_dataBuffer)
                    return *(ulong*)l;
            }
            set
            {
                _dataType = DataType.ULong;
                _dataBuffer = *(long*)&value;
            }
        }

        public override unsafe int AsInt
        {
            get
            {
                if (_dataType != DataType.Int)
                {
                    int v;
                    if (int.TryParse(Value, out v)) AsInt = v;
                    if (_dataType != DataType.Int) AsInt = (int) AsDouble;
                }
                fixed (long* l = &_dataBuffer)
                    return *(int*)l;
            }
            set
            {
                _dataType = DataType.Int;
                _dataBuffer = *(long*)&value;
            }
        }

        public override unsafe uint AsUInt
        {
            get
            {
                if (_dataType != DataType.UInt)
                {
                    uint v;
                    if (uint.TryParse(Value, out v)) AsUInt = v;
                    if (_dataType != DataType.UInt) AsUInt = (uint) AsDouble;
                }
                fixed (long* l = &_dataBuffer)
                    return *(uint*)l;
            }
            set
            {
                _dataType = DataType.Int;
                _dataBuffer = *(long*)&value;
            }
        }

        public override unsafe double AsDouble
        {
            get
            {
                if (_dataType != DataType.Double)
                {
                    double v;
                    if (double.TryParse(Value, out v)) AsDouble = v;
                    if (_dataType != DataType.Double) AsDouble = 0.0;
                }
                fixed (long* l = &_dataBuffer)
                    return *(double*)l;
            }
            set
            {
                _dataType = DataType.Double;
                _dataBuffer = *(long*) &value;
            }
        }

        public override float AsFloat
        {
            get
            {
                return (float) AsDouble;
            }
            set
            {
                AsDouble = value;
            }
        }

        public override bool AsBool
        {
            get
            {
                if (_dataType != DataType.Bool)
                {
                    bool v;
                    if (bool.TryParse(Value, out v))
                    {
                        _dataType = DataType.Bool;
                        _dataBuffer = v ? 1 : 0;
                    }

                    if (_dataType != DataType.Bool)
                    {
                        var val = Value;
                        _dataType = DataType.Bool;
                        _dataBuffer = val == "0" || string.IsNullOrEmpty(val) ? 0 : 1;
                    }
                }

                return _dataBuffer != 0;
            }
            set
            {
                _dataType = DataType.Bool;
                _dataBuffer = value ? 1 : 0;
            }
        }

        private string GetStringValue()
        {
            switch (_dataType)
            {
                case DataType.String:
                    return m_Data;
                case DataType.Long:
                    return AsLong.ToString();
                case DataType.ULong:
                    return AsULong.ToString();
                case DataType.Int:
                    return AsInt.ToString();
                case DataType.UInt:
                    return AsUInt.ToString();
                case DataType.Double:
                    return AsDouble.ToString();
                case DataType.Bool:
                    return AsBool ? "true" : "false";
            }

            return string.Empty;
        }

    } // End of JSONData
}