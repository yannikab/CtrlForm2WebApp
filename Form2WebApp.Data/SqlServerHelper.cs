using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Text;

namespace Schematrix.Data
{
    public static class SqlServerHelper
    {
        private const string nullParameterMessage = "Null value cannot be assigned to a non-nullable type.";

        private static string connectionString;
        private static SqlConnection connection;
        private static SqlTransaction transaction;

        public static string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public static SqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public static SqlTransaction Transaction
        {
            get { return transaction; }
            set { transaction = value; }
        }

        #region Nullable Type Conversions

        public static Boolean? ToNullableBoolean(SqlParameter sqlParameter)
        {
            if (sqlParameter.IsNullable && (sqlParameter.Value == null))
                return null;
            else if (sqlParameter.Value.Equals(DBNull.Value))
                return null;
            else if (sqlParameter.Value is SqlBoolean)
            {
                SqlBoolean sqlBoolean = (SqlBoolean)sqlParameter.Value;
                if (sqlBoolean.IsNull)
                    return null;
                else
                    return sqlBoolean.IsTrue;
            }
            else
            {
                return Convert.ToBoolean(sqlParameter.Value);
            }
        }

        public static Boolean? ToNullableBoolean(object o)
        {
            if ((o == null) || (o is DBNull))
                return null;
            else
                return Convert.ToBoolean(o);
        }

        public static Byte? ToNullableByte(SqlParameter sqlParameter)
        {
            if (sqlParameter.IsNullable && (sqlParameter.Value == null))
                return null;
            else if (sqlParameter.Value.Equals(DBNull.Value))
                return null;
            else if (sqlParameter.Value is SqlByte)
            {
                SqlByte sqlByte = (SqlByte)sqlParameter.Value;
                if (sqlByte.IsNull)
                    return null;
                else
                    return sqlByte.Value;
            }
            else
                return Convert.ToByte(sqlParameter.Value);
        }

        public static Byte? ToNullableByte(object o)
        {
            if ((o == null) || (o is DBNull))
                return null;
            else
                return Convert.ToByte(o);
        }

        public static Int16? ToNullableInt16(SqlParameter sqlParameter)
        {
            if (sqlParameter.IsNullable && (sqlParameter.Value == null))
                return null;
            else if (sqlParameter.Value.Equals(DBNull.Value))
                return null;
            else if (sqlParameter.Value is SqlInt16)
            {
                SqlInt16 sqlInt16 = (SqlInt16)sqlParameter.Value;
                if (sqlInt16.IsNull)
                    return null;
                else
                    return sqlInt16.Value;
            }
            else
                return Convert.ToInt16(sqlParameter.Value);
        }

        public static Int16? ToNullableInt16(object o)
        {
            if ((o == null) || (o is DBNull))
                return null;
            else
                return Convert.ToInt16(o);
        }

        public static Int32? ToNullableInt32(SqlParameter sqlParameter)
        {
            if (sqlParameter.IsNullable && (sqlParameter.Value == null))
                return null;
            else if (sqlParameter.Value.Equals(DBNull.Value))
                return null;
            else if (sqlParameter.Value is SqlInt32)
            {
                SqlInt32 sqlInt32 = (SqlInt32)sqlParameter.Value;
                if (sqlInt32.IsNull)
                    return null;
                else
                    return sqlInt32.Value;
            }
            else
                return Convert.ToInt32(sqlParameter.Value);
        }

        public static Int32? ToNullableInt32(object o)
        {
            if ((o == null) || (o is DBNull))
                return null;
            else
                return Convert.ToInt32(o);
        }

        public static Int64? ToNullableInt64(SqlParameter sqlParameter)
        {
            if (sqlParameter.IsNullable && (sqlParameter.Value == null))
                return null;
            else if (sqlParameter.Value.Equals(DBNull.Value))
                return null;
            else if (sqlParameter.Value is SqlInt64)
            {
                SqlInt64 sqlInt64 = (SqlInt64)sqlParameter.Value;
                if (sqlInt64.IsNull)
                    return null;
                else
                    return sqlInt64.Value;
            }
            else
                return Convert.ToInt64(sqlParameter.Value);
        }

        public static Int64? ToNullableInt64(object o)
        {
            if ((o == null) || (o is DBNull))
                return null;
            else
                return Convert.ToInt64(o);
        }

        public static Decimal? ToNullableDecimal(SqlParameter sqlParameter)
        {
            if (sqlParameter.IsNullable && (sqlParameter.Value == null))
                return null;
            else if (sqlParameter.Value.Equals(DBNull.Value))
                return null;
            else if (sqlParameter.Value is SqlDecimal)
            {
                SqlDecimal sqlDecimal = (SqlDecimal)sqlParameter.Value;
                if (sqlDecimal.IsNull)
                    return null;
                else
                    return sqlDecimal.Value;
            }
            else
                return Convert.ToDecimal(sqlParameter.Value);
        }

        public static Decimal? ToNullableDecimal(object o)
        {
            if ((o == null) || (o is DBNull))
                return null;
            else
                return Convert.ToDecimal(o);
        }

        public static Single? ToNullableSingle(SqlParameter sqlParameter)
        {
            if (sqlParameter.IsNullable && (sqlParameter.Value == null))
                return null;
            else if (sqlParameter.Value.Equals(DBNull.Value))
                return null;
            else if (sqlParameter.Value is SqlSingle)
            {
                SqlSingle sqlSingle = (SqlSingle)sqlParameter.Value;
                if (sqlSingle.IsNull)
                    return null;
                else
                    return sqlSingle.Value;
            }
            else
                return Convert.ToSingle(sqlParameter.Value);
        }

        public static Single? ToNullableSingle(object o)
        {
            if ((o == null) || (o is DBNull))
                return null;
            else
                return Convert.ToSingle(o);
        }

        public static Double? ToNullableDouble(SqlParameter sqlParameter)
        {
            if (sqlParameter.IsNullable && (sqlParameter.Value == null))
                return null;
            else if (sqlParameter.Value.Equals(DBNull.Value))
                return null;
            else if (sqlParameter.Value is SqlDouble)
            {
                SqlDouble sqlDouble = (SqlDouble)sqlParameter.Value;
                if (sqlDouble.IsNull)
                    return null;
                else
                    return sqlDouble.Value;
            }
            else
                return Convert.ToDouble(sqlParameter.Value);
        }

        public static Double? ToNullableDouble(object o)
        {
            if ((o == null) || (o is DBNull))
                return null;
            else
                return Convert.ToDouble(o);
        }

        public static DateTime? ToNullableDateTime(SqlParameter sqlParameter)
        {
            if (sqlParameter.IsNullable && (sqlParameter.Value == null))
                return null;
            else if (sqlParameter.Value.Equals(DBNull.Value))
                return null;
            else if (sqlParameter.Value is SqlDateTime)
            {
                SqlDateTime sqlDateTime = (SqlDateTime)sqlParameter.Value;
                if (sqlDateTime.IsNull)
                    return null;
                else
                    return sqlDateTime.Value;
            }
            else
                return Convert.ToDateTime(sqlParameter.Value);
        }

        public static DateTime? ToNullableDateTime(object o)
        {
            if ((o == null) || (o is DBNull))
                return null;
            else
                return Convert.ToDateTime(o);
        }

        public static Guid? ToNullableGuid(SqlParameter sqlParameter)
        {
            if (sqlParameter.IsNullable && (sqlParameter.Value == null))
                return null;
            else if (sqlParameter.Value.Equals(DBNull.Value))
                return null;
            else if (sqlParameter.Value is SqlGuid)
            {
                SqlGuid sqlGuid = (SqlGuid)sqlParameter.Value;
                if (sqlGuid.IsNull)
                    return null;
                else
                    return sqlGuid.Value;
            }
            else
                return (Guid)sqlParameter.Value;
        }

        public static Guid? ToNullableGuid(object o)
        {
            if ((o == null) || (o is DBNull))
                return null;
            else if (o is byte[])
                return new Guid((byte[])o);
            else
                return new Guid(Convert.ToString(o));
        }

        #endregion

        #region Stream and Reference Type Conversions

        public static Byte[] ToByteArray(SqlParameter sqlParameter)
        {
            if (sqlParameter.IsNullable && sqlParameter.Value == null)
                return null;
            else if (sqlParameter.Value.Equals(DBNull.Value))
                return null;
            else if (sqlParameter.Value is SqlBinary)
            {
                SqlBinary sqlBinary = (SqlBinary)sqlParameter.Value;
                if (sqlBinary.IsNull)
                    return null;
                else
                    return sqlBinary.Value;
            }
            else if (sqlParameter.Value is SqlBytes)
            {
                SqlBytes sqlBytes = (SqlBytes)sqlParameter.Value;
                if (sqlBytes.IsNull)
                    return null;
                else
                    return sqlBytes.Value;
            }
            else
            {
                return (byte[])sqlParameter.Value;
            }
        }

        public static String ToString(SqlParameter sqlParameter)
        {
            if (sqlParameter.IsNullable && (sqlParameter.Value == null))
                return null;
            else if (sqlParameter.Value.Equals(DBNull.Value))
                return null;
            else if (sqlParameter.Value is SqlString)
            {
                SqlString sqlString = (SqlString)sqlParameter.Value;
                if (sqlString.IsNull)
                    return null;
                else
                    return sqlString.Value;
            }
            else
                return Convert.ToString(sqlParameter.Value);
        }

        public static Int64 ToStream(SqlParameter sqlParameter, Stream stream)
        {
            long bytesStreamed = 0;
            using (stream)
            {
                try
                {
                    if (sqlParameter.IsNullable && sqlParameter.Value == null)
                        return bytesStreamed;
                    else if (sqlParameter.Value.Equals(DBNull.Value))
                        return bytesStreamed;
                    else if (sqlParameter.Value is SqlBinary)
                    {
                        SqlBinary sqlBinary = (SqlBinary)sqlParameter.Value;
                        if (!sqlBinary.IsNull)
                        {
                            stream.Write(sqlBinary.Value, 0, (int)sqlBinary.Length);
                            bytesStreamed = sqlBinary.Length;
                        }
                    }
                    else if (sqlParameter.Value is SqlBytes)
                    {
                        SqlBytes sqlBytes = (SqlBytes)sqlParameter.Value;
                        if (!sqlBytes.IsNull)
                        {
                            if (sqlBytes.Storage == StorageState.Buffer)
                            {
                                stream.Write(sqlBytes.Value, 0, (int)sqlBytes.Length);
                                bytesStreamed = sqlBytes.Length;
                            }
                            else
                            {
                                byte[] buffer = new byte[8192];
                                int bytesRead = 0;
                                long offset = 0;
                                while ((bytesRead = (int)sqlBytes.Read(offset, buffer, 0, 8192)) > 0)
                                {
                                    stream.Write(buffer, 0, bytesRead);
                                    offset += bytesRead;
                                    bytesStreamed += bytesRead;
                                }
                                sqlBytes.Stream.Close();
                            }
                        }
                    }
                    else
                    {
                        byte[] buffer = (byte[])sqlParameter.Value;
                        stream.Write(buffer, 0, buffer.Length);
                        bytesStreamed = buffer.Length;
                    }
                }
                finally
                {
                    stream.Close();
                }
                return bytesStreamed;
            }
        }

        #endregion

        #region Non-Nullable Value Type Conversions

        public static Boolean ToBoolean(SqlParameter sqlParameter, Boolean defaultValue, Boolean throwExceptionOnNull)
        {
            if ((sqlParameter.IsNullable && (sqlParameter.Value == null)) ||
                (sqlParameter.Value.Equals(DBNull.Value)) ||
               ((sqlParameter.Value is SqlBoolean) && ((SqlBoolean)sqlParameter.Value).IsNull))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else if (sqlParameter.Value is SqlBoolean)
            {
                return ((SqlBoolean)sqlParameter.Value).IsTrue;
            }
            else
            {
                return Convert.ToBoolean(sqlParameter.Value);
            }
        }

        public static Boolean ToBoolean(SqlParameter sqlParameter)
        {
            return ToBoolean(sqlParameter, false, true);
        }

        public static Boolean ToBoolean(SqlParameter sqlParameter, Boolean defaultValue)
        {
            return ToBoolean(sqlParameter, defaultValue, false);
        }

        public static Boolean ToBoolean(object o)
        {
            return Convert.ToBoolean(o);
        }

        public static Byte ToByte(SqlParameter sqlParameter, Byte defaultValue, Boolean throwExceptionOnNull)
        {
            if ((sqlParameter.IsNullable && (sqlParameter.Value == null)) ||
                (sqlParameter.Value.Equals(DBNull.Value)) ||
               ((sqlParameter.Value is SqlByte) && ((SqlByte)sqlParameter.Value).IsNull))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else if (sqlParameter.Value is SqlByte)
            {
                return ((SqlByte)sqlParameter.Value).Value;
            }
            else
            {
                return Convert.ToByte(sqlParameter.Value);
            }
        }

        public static Byte ToByte(SqlParameter sqlParameter)
        {
            return ToByte(sqlParameter, Byte.MinValue, true);
        }

        public static Byte ToByte(SqlParameter sqlParameter, Byte defaultValue)
        {
            return ToByte(sqlParameter, defaultValue, false);
        }

        public static Byte ToByte(object o)
        {
            return Convert.ToByte(o);
        }

        public static Int16 ToInt16(SqlParameter sqlParameter, Int16 defaultValue, Boolean throwExceptionOnNull)
        {
            if ((sqlParameter.IsNullable && (sqlParameter.Value == null)) ||
                (sqlParameter.Value.Equals(DBNull.Value)) ||
               ((sqlParameter.Value is SqlInt16) && ((SqlInt16)sqlParameter.Value).IsNull))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else if (sqlParameter.Value is SqlInt16)
            {
                return ((SqlInt16)sqlParameter.Value).Value;
            }
            else
            {
                return Convert.ToInt16(sqlParameter.Value);
            }
        }

        public static Int16 ToInt16(SqlParameter sqlParameter)
        {
            return ToInt16(sqlParameter, Int16.MinValue, true);
        }

        public static Int16 ToInt16(SqlParameter sqlParameter, Int16 defaultValue)
        {
            return ToInt16(sqlParameter, defaultValue, false);
        }

        public static Int16 ToInt16(object o)
        {
            return Convert.ToInt16(o);
        }

        public static Int32 ToInt32(SqlParameter sqlParameter, Int32 defaultValue, Boolean throwExceptionOnNull)
        {
            if ((sqlParameter.IsNullable && (sqlParameter.Value == null)) ||
                (sqlParameter.Value.Equals(DBNull.Value)) ||
               ((sqlParameter.Value is SqlInt32) && ((SqlInt32)sqlParameter.Value).IsNull))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else if (sqlParameter.Value is SqlInt32)
            {
                return ((SqlInt32)sqlParameter.Value).Value;
            }
            else
            {
                return Convert.ToInt32(sqlParameter.Value);
            }
        }

        public static Int32 ToInt32(SqlParameter sqlParameter)
        {
            return ToInt32(sqlParameter, Int32.MinValue, true);
        }

        public static Int32 ToInt32(SqlParameter sqlParameter, Int32 defaultValue)
        {
            return ToInt32(sqlParameter, defaultValue, false);
        }

        public static Int32 ToInt32(object o)
        {
            return Convert.ToInt32(o);
        }

        public static Int64 ToInt64(SqlParameter sqlParameter, Int64 defaultValue, Boolean throwExceptionOnNull)
        {
            if ((sqlParameter.IsNullable && (sqlParameter.Value == null)) ||
                (sqlParameter.Value.Equals(DBNull.Value)) ||
               ((sqlParameter.Value is SqlInt64) && ((SqlInt64)sqlParameter.Value).IsNull))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else if (sqlParameter.Value is SqlInt64)
            {
                return ((SqlInt64)sqlParameter.Value).Value;
            }
            else
            {
                return Convert.ToInt64(sqlParameter.Value);
            }
        }

        public static Int64 ToInt64(SqlParameter sqlParameter)
        {
            return ToInt64(sqlParameter, Int64.MinValue, true);
        }

        public static Int64 ToInt64(SqlParameter sqlParameter, Int64 defaultValue)
        {
            return ToInt64(sqlParameter, defaultValue, false);
        }

        public static Int64 ToInt64(object o)
        {
            return Convert.ToInt64(o);
        }

        public static Decimal ToDecimal(SqlParameter sqlParameter, Decimal defaultValue, Boolean throwExceptionOnNull)
        {
            if ((sqlParameter.IsNullable && (sqlParameter.Value == null)) ||
                (sqlParameter.Value.Equals(DBNull.Value)) ||
               ((sqlParameter.Value is SqlDecimal) && ((SqlDecimal)sqlParameter.Value).IsNull))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else if (sqlParameter.Value is SqlDecimal)
            {
                return ((SqlDecimal)sqlParameter.Value).Value;
            }
            else
            {
                return Convert.ToDecimal(sqlParameter.Value);
            }
        }

        public static Decimal ToDecimal(SqlParameter sqlParameter)
        {
            return ToDecimal(sqlParameter, Decimal.MinValue, true);
        }

        public static Decimal ToDecimal(SqlParameter sqlParameter, Decimal defaultValue)
        {
            return ToDecimal(sqlParameter, defaultValue, false);
        }


        public static Decimal ToDecimal(object o)
        {
            return Convert.ToDecimal(o);
        }

        public static Single ToSingle(SqlParameter sqlParameter, Single defaultValue, Boolean throwExceptionOnNull)
        {
            if ((sqlParameter.IsNullable && (sqlParameter.Value == null)) ||
                (sqlParameter.Value.Equals(DBNull.Value)) ||
               ((sqlParameter.Value is SqlSingle) && ((SqlSingle)sqlParameter.Value).IsNull))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else if (sqlParameter.Value is SqlSingle)
            {
                return ((SqlSingle)sqlParameter.Value).Value;
            }
            else
            {
                return Convert.ToSingle(sqlParameter.Value);
            }
        }

        public static Single ToSingle(SqlParameter sqlParameter)
        {
            return ToSingle(sqlParameter, Single.MinValue, true);
        }

        public static Single ToSingle(SqlParameter sqlParameter, Single defaultValue)
        {
            return ToSingle(sqlParameter, defaultValue, false);
        }

        public static Single ToSingle(object o)
        {
            return Convert.ToSingle(o);
        }

        public static Double ToDouble(SqlParameter sqlParameter, Double defaultValue, Boolean throwExceptionOnNull)
        {
            if ((sqlParameter.IsNullable && (sqlParameter.Value == null)) ||
                (sqlParameter.Value.Equals(DBNull.Value)) ||
               ((sqlParameter.Value is SqlDouble) && ((SqlDouble)sqlParameter.Value).IsNull))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else if (sqlParameter.Value is SqlDouble)
            {
                return ((SqlDouble)sqlParameter.Value).Value;
            }
            else
            {
                return Convert.ToDouble(sqlParameter.Value);
            }
        }

        public static Double ToDouble(SqlParameter sqlParameter)
        {
            return ToDouble(sqlParameter, Double.MinValue, true);
        }

        public static Double ToDouble(SqlParameter sqlParameter, Double defaultValue)
        {
            return ToDouble(sqlParameter, defaultValue, false);
        }

        public static Double ToDouble(object o)
        {
            return Convert.ToDouble(o);
        }

        public static DateTime ToDateTime(SqlParameter sqlParameter, DateTime defaultValue, Boolean throwExceptionOnNull)
        {
            if ((sqlParameter.IsNullable && (sqlParameter.Value == null)) ||
                (sqlParameter.Value.Equals(DBNull.Value)) ||
               ((sqlParameter.Value is SqlDateTime) && ((SqlDateTime)sqlParameter.Value).IsNull))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else if (sqlParameter.Value is SqlDateTime)
            {
                return ((SqlDateTime)sqlParameter.Value).Value;
            }
            else
            {
                return Convert.ToDateTime(sqlParameter.Value);
            }
        }

        public static DateTime ToDateTime(SqlParameter sqlParameter)
        {
            return ToDateTime(sqlParameter, DateTime.MinValue, true);
        }

        public static DateTime ToDateTime(SqlParameter sqlParameter, DateTime defaultValue)
        {
            return ToDateTime(sqlParameter, defaultValue, false);
        }

        public static DateTime ToDateTime(object o)
        {
            return Convert.ToDateTime(o);
        }

        public static Guid ToGuid(SqlParameter sqlParameter, Guid defaultValue, Boolean throwExceptionOnNull)
        {
            if ((sqlParameter.IsNullable && (sqlParameter.Value == null)) ||
                (sqlParameter.Value.Equals(DBNull.Value)) ||
               ((sqlParameter.Value is SqlGuid) && ((SqlGuid)sqlParameter.Value).IsNull))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else if (sqlParameter.Value is SqlGuid)
            {
                return ((SqlGuid)sqlParameter.Value).Value;
            }
            else
            {
                return (Guid)sqlParameter.Value;
            }
        }

        public static Guid ToGuid(SqlParameter sqlParameter)
        {
            return ToGuid(sqlParameter, Guid.Empty, true);
        }

        public static Guid ToGuid(SqlParameter sqlParameter, Guid defaultValue)
        {
            return ToGuid(sqlParameter, defaultValue, false);
        }

        public static Guid ToGuid(object o)
        {
            if (o is byte[])
            {
                return new Guid((byte[])o);
            }
            else
            {
                return new Guid(Convert.ToString(o));
            }
        }

        #endregion

        #region Nullable Parameter Value Setting Routines

        public static void SetParameterValue(SqlParameter sqlParameter, Boolean? value)
        {
            if (value.HasValue)
                sqlParameter.Value = new SqlBoolean(value.Value);
            else
                sqlParameter.Value = SqlBoolean.Null;
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Byte? value)
        {
            if (value.HasValue)
                sqlParameter.Value = new SqlByte(value.Value);
            else
                sqlParameter.Value = SqlByte.Null;
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Int16? value)
        {
            if (value.HasValue)
                sqlParameter.Value = new SqlInt16(value.Value);
            else
                sqlParameter.Value = SqlInt16.Null;
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Int32? value)
        {
            if (value.HasValue)
                sqlParameter.Value = new SqlInt32(value.Value);
            else
                sqlParameter.Value = SqlInt32.Null;
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Int64? value)
        {
            if (value.HasValue)
                sqlParameter.Value = new SqlInt64(value.Value);
            else
                sqlParameter.Value = SqlInt64.Null;
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Decimal? value)
        {
            if (value.HasValue)
                sqlParameter.Value = value.Value;
            else
                sqlParameter.Value = DBNull.Value;
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Single? value)
        {
            if (value.HasValue)
                sqlParameter.Value = new SqlSingle(value.Value);
            else
                sqlParameter.Value = SqlSingle.Null;
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Double? value)
        {
            if (value.HasValue)
                sqlParameter.Value = new SqlDouble(value.Value);
            else
                sqlParameter.Value = SqlDouble.Null;
        }

        public static void SetParameterValue(SqlParameter sqlParameter, DateTime? value)
        {
            if (value.HasValue)
                sqlParameter.Value = new SqlDateTime(value.Value);
            else
                sqlParameter.Value = SqlDateTime.Null;
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Guid? value)
        {
            if (value.HasValue)
                sqlParameter.Value = new SqlGuid(value.Value);
            else
                sqlParameter.Value = SqlGuid.Null;
        }

        public static void SetParameterValue(SqlParameter sqlParameter, String value)
        {
            if (value == null)
                sqlParameter.Value = SqlString.Null;
            else
                sqlParameter.Value = new SqlString(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Stream value)
        {
            if (value == null)
                sqlParameter.Value = SqlBytes.Null;
            else
                sqlParameter.Value = new SqlBytes(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Byte[] value)
        {
            if (value == null)
                sqlParameter.Value = SqlBinary.Null;
            else
                sqlParameter.Value = new SqlBinary(value);
        }

        #endregion

        #region Non Nullable Parameter Value Setting Routines

        public static void SetParameterValue(SqlParameter sqlParameter, Boolean value)
        {
            sqlParameter.Value = new SqlBoolean(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Boolean value, Boolean nullEquivalent)
        {
            if (value == nullEquivalent)
                sqlParameter.Value = SqlBoolean.Null;
            else
                sqlParameter.Value = new SqlBoolean(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Byte value)
        {
            sqlParameter.Value = new SqlByte(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Byte value, Byte nullEquivalent)
        {
            if (value == nullEquivalent)
                sqlParameter.Value = SqlByte.Null;
            else
                sqlParameter.Value = new SqlByte(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Int16 value)
        {
            sqlParameter.Value = new SqlInt16(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Int16 value, Int16 nullEquivalent)
        {
            if (value == nullEquivalent)
                sqlParameter.Value = SqlInt16.Null;
            else
                sqlParameter.Value = new SqlInt16(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Int32 value)
        {
            sqlParameter.Value = new SqlInt32(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Int32 value, Int32 nullEquivalent)
        {
            if (value == nullEquivalent)
                sqlParameter.Value = SqlInt32.Null;
            else
                sqlParameter.Value = new SqlInt32(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Int64 value)
        {
            sqlParameter.Value = new SqlInt64(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Int64 value, Int64 nullEquivalent)
        {
            if (value == nullEquivalent)
                sqlParameter.Value = SqlInt64.Null;
            else
                sqlParameter.Value = new SqlInt64(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Decimal value)
        {
            sqlParameter.Value = value;
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Decimal value, Decimal nullEquivalent)
        {
            if (value == nullEquivalent)
                sqlParameter.Value = DBNull.Value;
            else
                sqlParameter.Value = value;
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Single value)
        {
            sqlParameter.Value = new SqlSingle(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Single value, Single nullEquivalent)
        {
            if (value == nullEquivalent)
                sqlParameter.Value = SqlSingle.Null;
            else
                sqlParameter.Value = new SqlSingle(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Double value)
        {
            sqlParameter.Value = new SqlDouble(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Double value, Double nullEquivalent)
        {
            if (value == nullEquivalent)
                sqlParameter.Value = SqlDouble.Null;
            else
                sqlParameter.Value = new SqlDouble(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, DateTime value)
        {
            sqlParameter.Value = new SqlDateTime(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, DateTime value, DateTime nullEquivalent)
        {
            if (value == nullEquivalent)
                sqlParameter.Value = SqlDateTime.Null;
            else
                sqlParameter.Value = new SqlDateTime(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Guid value)
        {
            sqlParameter.Value = new SqlGuid(value);
        }

        public static void SetParameterValue(SqlParameter sqlParameter, Guid value, Guid nullEquivalent)
        {
            if (value == nullEquivalent)
                sqlParameter.Value = SqlGuid.Null;
            else
                sqlParameter.Value = new SqlGuid(value);
        }

        #endregion

        #region Nullable Data Reader Conversions

        public static Boolean? ToNullableBoolean(SqlDataReader sqlDataReader, int ordinal)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
                return null;
            else
                return sqlDataReader.GetBoolean(ordinal);
        }
       
        public static Boolean? ToNullableBoolean(SqlDataReader sqlDataReader, string columnName)
        {
            return ToNullableBoolean(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Byte? ToNullableByte(SqlDataReader sqlDataReader, int ordinal)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
                return null;
            else
            {
                return (byte) sqlDataReader.GetByte(ordinal);
            }
        }

        public static Byte? ToNullableByte(SqlDataReader sqlDataReader, string columnName)
        {
            return ToNullableByte(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Int16? ToNullableInt16(SqlDataReader sqlDataReader, int ordinal)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
                return null;
            else
                return sqlDataReader.GetInt16(ordinal);
        }

        public static Int16? ToNullableInt16(SqlDataReader sqlDataReader, string columnName)
        {
            return ToNullableInt16(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Int32? ToNullableInt32(SqlDataReader sqlDataReader, int ordinal)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
                return null;
            else
                return sqlDataReader.GetInt32(ordinal);
        }

        public static Int32? ToNullableInt32(SqlDataReader sqlDataReader, string columnName)
        {
            return ToNullableInt32(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Int64? ToNullableInt64(SqlDataReader sqlDataReader, int ordinal)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
                return null;
            else
                return sqlDataReader.GetInt64(ordinal);
        }

        public static Int64? ToNullableInt64(SqlDataReader sqlDataReader, string columnName)
        {
            return ToNullableInt64(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Decimal? ToNullableDecimal(SqlDataReader sqlDataReader, int ordinal)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
                return null;
            else
                return sqlDataReader.GetDecimal(ordinal);
        }

        public static Decimal? ToNullableDecimal(SqlDataReader sqlDataReader, string columnName)
        {
            return ToNullableDecimal(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Single? ToNullableSingle(SqlDataReader sqlDataReader, int ordinal)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
                return null;
            else
                return sqlDataReader.GetFloat(ordinal);
        }

        public static Single? ToNullableSingle(SqlDataReader sqlDataReader, string columnName)
        {
            return ToNullableSingle(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Double? ToNullableDouble(SqlDataReader sqlDataReader, int ordinal)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
                return null;
            else
                return sqlDataReader.GetDouble(ordinal);
        }

        public static Double? ToNullableDouble(SqlDataReader sqlDataReader, string columnName)
        {
            return ToNullableDouble(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static DateTime? ToNullableDateTime(SqlDataReader sqlDataReader, int ordinal)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
                return null;
            else
                return sqlDataReader.GetDateTime(ordinal);
        }

        public static DateTime? ToNullableDateTime(SqlDataReader sqlDataReader, string columnName)
        {
            return ToNullableDateTime(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Guid? ToNullableGuid(SqlDataReader sqlDataReader, int ordinal)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
                return null;
            else
                return sqlDataReader.GetGuid(ordinal);
        }

        public static Guid? ToNullableGuid(SqlDataReader sqlDataReader, string columnName)
        {
            return ToNullableGuid(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static String ToString(SqlDataReader sqlDataReader, int ordinal)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
                return null;
            else
                return sqlDataReader.GetString(ordinal);
        }

        public static String ToString(SqlDataReader sqlDataReader, string columnName)
        {
            return ToString(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static byte[] ToByteArray(SqlDataReader sqlDataReader, int ordinal)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
                return null;
            else
            {
                return sqlDataReader.GetSqlBinary(ordinal).Value;
            }
        }

        public static byte[] ToByteArray(SqlDataReader sqlDataReader, string columnName)
        {
            return ToByteArray(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        #endregion

        #region Non-Nullable Data Reader Conversions

        public static Boolean ToBoolean(SqlDataReader sqlDataReader, int ordinal, Boolean defaultValue, Boolean throwExceptionOnNull)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else
            {
                return sqlDataReader.GetBoolean(ordinal);
            }
        }

        public static Boolean ToBoolean(SqlDataReader sqlDataReader, int ordinal, Boolean defaultValue)
        {
            return ToBoolean(sqlDataReader, ordinal, defaultValue, false);
        }

        public static Boolean ToBoolean(SqlDataReader sqlDataReader, string columnName, Boolean defaultValue)
        {
            return ToBoolean(sqlDataReader, sqlDataReader.GetOrdinal(columnName), defaultValue, false);
        }

        public static Boolean ToBoolean(SqlDataReader sqlDataReader, int ordinal)
        {
            return ToBoolean(sqlDataReader, ordinal, false, true);
        }

        public static Boolean ToBoolean(SqlDataReader sqlDataReader, string columnName)
        {
            return ToBoolean(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Byte ToByte(SqlDataReader sqlDataReader, int ordinal, Byte defaultValue, bool throwExceptionOnNull)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else
            {
                return sqlDataReader.GetByte(ordinal);
            }
        }

        public static Byte ToByte(SqlDataReader sqlDataReader, int ordinal, Byte defaultValue)
        {
            return ToByte(sqlDataReader, ordinal, defaultValue, false);
        }

        public static Byte ToByte(SqlDataReader sqlDataReader, string columnName, Byte defaultValue)
        {
            return ToByte(sqlDataReader, sqlDataReader.GetOrdinal(columnName), defaultValue, false);
        }

        public static Byte ToByte(SqlDataReader sqlDataReader, int ordinal)
        {
            return ToByte(sqlDataReader, ordinal, Byte.MinValue, true);
        }

        public static Byte ToByte(SqlDataReader sqlDataReader, string columnName)
        {
            return ToByte(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Int16 ToInt16(SqlDataReader sqlDataReader, int ordinal, Int16 defaultValue, bool throwExceptionOnNull)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else
            {
                return sqlDataReader.GetInt16(ordinal);
            }
        }

        public static Int16 ToInt16(SqlDataReader sqlDataReader, int ordinal, Int16 defaultValue)
        {
            return ToInt16(sqlDataReader, ordinal, defaultValue, false);
        }

        public static Int16 ToInt16(SqlDataReader sqlDataReader, string columnName, Int16 defaultValue)
        {
            return ToInt16(sqlDataReader, sqlDataReader.GetOrdinal(columnName), defaultValue, false);
        }

        public static Int16 ToInt16(SqlDataReader sqlDataReader, int ordinal)
        {
            return ToInt16(sqlDataReader, ordinal, Int16.MinValue, true);
        }

        public static Int16 ToInt16(SqlDataReader sqlDataReader, string columnName)
        {
            return ToInt16(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Int32 ToInt32(SqlDataReader sqlDataReader, int ordinal, Int32 defaultValue, bool throwExceptionOnNull)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else
            {
                return sqlDataReader.GetInt32(ordinal);
            }
        }

        public static Int32 ToInt32(SqlDataReader sqlDataReader, int ordinal, Int32 defaultValue)
        {
            return ToInt32(sqlDataReader, ordinal, defaultValue, false);
        }

        public static Int32 ToInt32(SqlDataReader sqlDataReader, string columnName, Int32 defaultValue)
        {
            return ToInt32(sqlDataReader, sqlDataReader.GetOrdinal(columnName), defaultValue, false);
        }

        public static Int32 ToInt32(SqlDataReader sqlDataReader, int ordinal)
        {
            return ToInt32(sqlDataReader, ordinal, Int32.MinValue, true);
        }

        public static Int32 ToInt32(SqlDataReader sqlDataReader, string columnName)
        {
            return ToInt32(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Int64 ToInt64(SqlDataReader sqlDataReader, int ordinal, Int64 defaultValue, bool throwExceptionOnNull)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else
            {
                return sqlDataReader.GetInt64(ordinal);
            }
        }

        public static Int64 ToInt64(SqlDataReader sqlDataReader, int ordinal, Int64 defaultValue)
        {
            return ToInt64(sqlDataReader, ordinal, defaultValue, false);
        }

        public static Int64 ToInt64(SqlDataReader sqlDataReader, string columnName, Int64 defaultValue)
        {
            return ToInt64(sqlDataReader, sqlDataReader.GetOrdinal(columnName), defaultValue, false);
        }

        public static Int64 ToInt64(SqlDataReader sqlDataReader, int ordinal)
        {
            return ToInt64(sqlDataReader, ordinal, Int64.MinValue, true);
        }

        public static Int64 ToInt64(SqlDataReader sqlDataReader, string columnName)
        {
            return ToInt64(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Decimal ToDecimal(SqlDataReader sqlDataReader, int ordinal, Decimal defaultValue, bool throwExceptionOnNull)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else
            {
                return sqlDataReader.GetDecimal(ordinal);
            }
        }

        public static Decimal ToDecimal(SqlDataReader sqlDataReader, int ordinal, Decimal defaultValue)
        {
            return ToDecimal(sqlDataReader, ordinal, defaultValue, false);
        }

        public static Decimal ToDecimal(SqlDataReader sqlDataReader, string columnName, Decimal defaultValue)
        {
            return ToDecimal(sqlDataReader, sqlDataReader.GetOrdinal(columnName), defaultValue, false);
        }

        public static Decimal ToDecimal(SqlDataReader sqlDataReader, int ordinal)
        {
            return ToDecimal(sqlDataReader, ordinal, Decimal.MinValue, true);
        }

        public static Decimal ToDecimal(SqlDataReader sqlDataReader, string columnName)
        {
            return ToDecimal(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Single ToSingle(SqlDataReader sqlDataReader, int ordinal, Single defaultValue, bool throwExceptionOnNull)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else
            {
                return sqlDataReader.GetFloat(ordinal);
            }
        }

        public static Single ToSingle(SqlDataReader sqlDataReader, int ordinal, Single defaultValue)
        {
            return ToSingle(sqlDataReader, ordinal, defaultValue, false);
        }

        public static Single ToSingle(SqlDataReader sqlDataReader, string columnName, Single defaultValue)
        {
            return ToSingle(sqlDataReader, sqlDataReader.GetOrdinal(columnName), defaultValue, false);
        }

        public static Single ToSingle(SqlDataReader sqlDataReader, int ordinal)
        {
            return ToSingle(sqlDataReader, ordinal, Single.MinValue, true);
        }

        public static Single ToSingle(SqlDataReader sqlDataReader, string columnName)
        {
            return ToSingle(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Double ToDouble(SqlDataReader sqlDataReader, int ordinal, Double defaultValue, bool throwExceptionOnNull)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else
            {
                return sqlDataReader.GetDouble(ordinal);
            }
        }

        public static Double ToDouble(SqlDataReader sqlDataReader, int ordinal, Double defaultValue)
        {
            return ToDouble(sqlDataReader, ordinal, defaultValue, false);
        }

        public static Double ToDouble(SqlDataReader sqlDataReader, string columnName, Double defaultValue)
        {
            return ToDouble(sqlDataReader, sqlDataReader.GetOrdinal(columnName), defaultValue, false);
        }

        public static Double ToDouble(SqlDataReader sqlDataReader, int ordinal)
        {
            return ToDouble(sqlDataReader, ordinal, Double.MinValue, true);
        }

        public static Double ToDouble(SqlDataReader sqlDataReader, string columnName)
        {
            return ToDouble(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static DateTime ToDateTime(SqlDataReader sqlDataReader, int ordinal, DateTime defaultValue, bool throwExceptionOnNull)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else
            {
                return sqlDataReader.GetDateTime(ordinal);
            }
        }

        public static DateTime ToDateTime(SqlDataReader sqlDataReader, int ordinal, DateTime defaultValue)
        {
            return ToDateTime(sqlDataReader, ordinal, defaultValue, false);
        }

        public static DateTime ToDateTime(SqlDataReader sqlDataReader, string columnName, DateTime defaultValue)
        {
            return ToDateTime(sqlDataReader, sqlDataReader.GetOrdinal(columnName), defaultValue, false);
        }

        public static DateTime ToDateTime(SqlDataReader sqlDataReader, int ordinal)
        {
            return ToDateTime(sqlDataReader, ordinal, DateTime.MinValue, true);
        }

        public static DateTime ToDateTime(SqlDataReader sqlDataReader, string columnName)
        {
            return ToDateTime(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        public static Guid ToGuid(SqlDataReader sqlDataReader, int ordinal, Guid defaultValue, bool throwExceptionOnNull)
        {
            if ((ordinal < 0) || sqlDataReader.IsDBNull(ordinal))
            {
                if (throwExceptionOnNull)
                    throw new NullReferenceException(nullParameterMessage);
                else
                    return defaultValue;
            }
            else
            {
                return sqlDataReader.GetGuid(ordinal);
            }
        }

        public static Guid ToGuid(SqlDataReader sqlDataReader, int ordinal, Guid defaultValue)
        {
            return ToGuid(sqlDataReader, ordinal, defaultValue, false);
        }

        public static Guid ToGuid(SqlDataReader sqlDataReader, string columnName, Guid defaultValue)
        {
            return ToGuid(sqlDataReader, sqlDataReader.GetOrdinal(columnName), defaultValue, false);
        }

        public static Guid ToGuid(SqlDataReader sqlDataReader, int ordinal)
        {
            return ToGuid(sqlDataReader, ordinal, Guid.Empty, true);
        }

        public static Guid ToGuid(SqlDataReader sqlDataReader, string columnName)
        {
            return ToGuid(sqlDataReader, sqlDataReader.GetOrdinal(columnName));
        }

        #endregion
    }

    public abstract class SqlPersisterBase
    {
        private string connectionString;
        private SqlConnection connection;
        private SqlTransaction transaction;
        private List<SqlCommand> autoCloseCommands = new List<SqlCommand>();

        public SqlPersisterBase() { }

        public SqlPersisterBase(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SqlPersisterBase(SqlConnection connection)
        {
            this.connection = connection;
        }

        public SqlPersisterBase(SqlTransaction transaction)
        {
            this.connection = transaction.Connection;
            this.transaction = transaction;
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public SqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public SqlTransaction Transaction
        {
            get { return transaction; }
            set { transaction = value; }
        }

        protected void AttachCommand(SqlCommand command)
        {
            // Ensure connection has been provided
            EnsureConnection();

            if (this.connection != null)
            {
                command.Connection = this.connection;
                if (this.transaction != null)
                {
                    command.Transaction = this.transaction;
                }
            }
            else
            {
                command.Connection = new SqlConnection(this.connectionString);
                command.Connection.Open();
                lock (autoCloseCommands)
                {
                    autoCloseCommands.Add(command);
                }
            }
        }

        protected CommandBehavior AttachReaderCommand(SqlCommand command)
        {
            // Ensure connection has been provided
            EnsureConnection();

            if (this.connection != null)
            {
                command.Connection = this.connection;
                if (this.transaction != null)
                {
                    command.Transaction = this.transaction;
                }
                return CommandBehavior.Default;
            }
            else
            {
                command.Connection = new SqlConnection(this.connectionString);
                command.Connection.Open();
                return CommandBehavior.CloseConnection;
            }
        }

        protected void DetachCommand(SqlCommand command)
        {
            lock (autoCloseCommands)
            {
                if (autoCloseCommands.Contains(command))
                {
                    command.Connection.Close();
                    command.Connection.Dispose();
                    command.Connection = null;
                    autoCloseCommands.Remove(command);
                }
                else
                {
                    command.Transaction = null;
                    command.Connection = null;
                }
            }
        }

        private void EnsureConnection()
        {
            if ((this.connectionString == null) &&
                (this.connection == null))
            {
                if (SqlServerHelper.Connection != null)
                {
                    this.connection = SqlServerHelper.Connection;
                    if (SqlServerHelper.Transaction != null)
                    {
                        this.transaction = SqlServerHelper.Transaction;
                    }
                }
                else if (SqlServerHelper.ConnectionString != null)
                {
                    this.connectionString = SqlServerHelper.ConnectionString;
                }
                else
                {
                    // Throw exception, nothing to connect
                    throw new DataException("No connection string or connection has been provided.");
                }
            }
        }
    }
}
