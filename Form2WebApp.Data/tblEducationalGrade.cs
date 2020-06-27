// This code was generated by an EVALUATION copy of Schematrix SchemaCoder.
// Redistribution of this source code, or an application developed from it, is forbidden.
// Modification of this source code to remove this comment is also forbidden.
// Please visit http://www.schematrix.com/ to obtain a license to use this software.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;

using Schematrix.Data;

namespace Form2WebApp.Data
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]

    public partial class tblEducationalGrade
    {
        private static ItblEducationalGradePersister _DefaultPersister;
        private ItblEducationalGradePersister _Persister;
        private long _id;
        private string _name;
        private long _stageId;

        static tblEducationalGrade()
        {
            // Assign default persister
            _DefaultPersister = new SqlServertblEducationalGradePersister();
        }

        public tblEducationalGrade()
        {
            // Assign default persister to instance persister
            _Persister = _DefaultPersister;
        }

        public tblEducationalGrade(long _id)
        {
            // Assign default persister to instance persister
            _Persister = _DefaultPersister;

            // Assign method parameter to private fields
            this._id = _id;

            // Call associated retrieve method
            Retrieve();
        }

        public tblEducationalGrade(DataRow row)
        {
            // Assign default persister to instance persister
            _Persister = _DefaultPersister;

            // Assign column values to private members
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                switch (row.Table.Columns[i].ColumnName.ToUpper())
                {
                    case "ID":
                        this.id = Convert.ToInt64(row[i, DataRowVersion.Current]);
                        break;

                    case "NAME":
                        this.name = (string)row[i, DataRowVersion.Current];
                        break;

                    case "STAGEID":
                        this.stageId = Convert.ToInt64(row[i, DataRowVersion.Current]);
                        break;

                }
            }
        }

        public static ItblEducationalGradePersister DefaultPersister
        {
            get { return _DefaultPersister; }
            set { _DefaultPersister = value; }
        }

        public ItblEducationalGradePersister Persister
        {
            get { return _Persister; }
            set { _Persister = value; }
        }

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public long stageId
        {
            get { return _stageId; }
            set { _stageId = value; }
        }

        public virtual void Clone(tblEducationalGrade sourceObject)
        {
            if (sourceObject == null)
            {
                throw new ArgumentNullException("sourceObject");
            }

            // Clone attributes from source object
            this._id = sourceObject.id;
            this._name = sourceObject.name;
            this._stageId = sourceObject.stageId;
        }

        public virtual int Retrieve()
        {
            return _Persister.Retrieve(this);
        }

        public virtual int Update()
        {
            return _Persister.Update(this);
        }

        public virtual int Delete()
        {
            return _Persister.Delete(this);
        }

        public virtual int Insert()
        {
            return _Persister.Insert(this);
        }

        public static IReader<tblEducationalGrade> ListAll()
        {
            return _DefaultPersister.ListAll();
        }

        public static IReader<tblEducationalGrade> ListForstageId(long stageId)
        {
            return _DefaultPersister.ListForstageId(stageId);
        }

    }

    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]

    public partial interface ItblEducationalGradePersister : IPersister
    {
        int Retrieve(tblEducationalGrade tblEducationalGrade);
        int Update(tblEducationalGrade tblEducationalGrade);
        int Delete(tblEducationalGrade tblEducationalGrade);
        int Insert(tblEducationalGrade tblEducationalGrade);
        IReader<tblEducationalGrade> ListAll();
        IReader<tblEducationalGrade> ListForstageId(long stageId);
    }

    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]

    public partial class SqlServertblEducationalGradePersister : SqlPersisterBase, ItblEducationalGradePersister
    {
        public SqlServertblEducationalGradePersister()
        {
        }

        public SqlServertblEducationalGradePersister(string connectionString) : base(connectionString)
        {
        }

        public SqlServertblEducationalGradePersister(SqlConnection connection) : base(connection)
        {
        }

        public SqlServertblEducationalGradePersister(SqlTransaction transaction) : base(transaction)
        {
        }

        public int Retrieve(tblEducationalGrade tblEducationalGrade)
        {
            int __rowsAffected = 1;

            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEducationalGradeGet"))
            {
                // Set command type
                sqlCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    // Attach command
                    AttachCommand(sqlCommand);

                    // Add command parameters
                    SqlParameter vid = new SqlParameter("@id", SqlDbType.BigInt);
                    vid.Direction = ParameterDirection.InputOutput;
                    sqlCommand.Parameters.Add(vid);
                    SqlParameter vname = new SqlParameter("@name", SqlDbType.NVarChar, 255);
                    vname.Direction = ParameterDirection.Output;
                    sqlCommand.Parameters.Add(vname);
                    SqlParameter vstageId = new SqlParameter("@stageId", SqlDbType.BigInt);
                    vstageId.Direction = ParameterDirection.Output;
                    sqlCommand.Parameters.Add(vstageId);

                    // Set input parameter values
                    SqlServerHelper.SetParameterValue(vid, tblEducationalGrade.id);

                    // Execute command
                    sqlCommand.ExecuteNonQuery();

                    try
                    {
                        // Get output parameter values
                        tblEducationalGrade.id = SqlServerHelper.ToInt64(vid);
                        tblEducationalGrade.name = SqlServerHelper.ToString(vname);
                        tblEducationalGrade.stageId = SqlServerHelper.ToInt64(vstageId);

                    }
                    catch (Exception ex)
                    {
                        if (ex is System.NullReferenceException)
                        {
                            __rowsAffected = 0;
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                }
                finally
                {
                    // Detach command
                    DetachCommand(sqlCommand);
                }

            }

            return __rowsAffected;
        }

        public int Update(tblEducationalGrade tblEducationalGrade)
        {
            int __rowsAffected = 0;

            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEducationalGradeUpdate"))
            {
                // Set command type
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Add command parameters
                SqlParameter vid = new SqlParameter("@id", SqlDbType.BigInt);
                vid.Direction = ParameterDirection.Input;
                sqlCommand.Parameters.Add(vid);
                SqlParameter vname = new SqlParameter("@name", SqlDbType.NVarChar, 255);
                vname.Direction = ParameterDirection.Input;
                sqlCommand.Parameters.Add(vname);
                SqlParameter vstageId = new SqlParameter("@stageId", SqlDbType.BigInt);
                vstageId.Direction = ParameterDirection.Input;
                sqlCommand.Parameters.Add(vstageId);

                // Set input parameter values
                SqlServerHelper.SetParameterValue(vid, tblEducationalGrade.id);
                SqlServerHelper.SetParameterValue(vname, tblEducationalGrade.name);
                SqlServerHelper.SetParameterValue(vstageId, tblEducationalGrade.stageId);

                try
                {
                    // Attach command
                    AttachCommand(sqlCommand);

                    // Execute command
                    __rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (__rowsAffected == 0)
                    {
                        return __rowsAffected;
                    }


                }
                finally
                {
                    // Detach command
                    DetachCommand(sqlCommand);
                }

            }

            return __rowsAffected;
        }

        public int Delete(tblEducationalGrade tblEducationalGrade)
        {
            int __rowsAffected = 0;

            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEducationalGradeDelete"))
            {
                // Set command type
                sqlCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    // Attach command
                    AttachCommand(sqlCommand);

                    // Add command parameters
                    SqlParameter vid = new SqlParameter("@id", SqlDbType.BigInt);
                    vid.Direction = ParameterDirection.Input;
                    sqlCommand.Parameters.Add(vid);

                    // Set input parameter values
                    SqlServerHelper.SetParameterValue(vid, tblEducationalGrade.id);

                    // Execute command
                    __rowsAffected = sqlCommand.ExecuteNonQuery();

                }
                finally
                {
                    // Detach command
                    DetachCommand(sqlCommand);
                }

            }

            return __rowsAffected;
        }

        public int Insert(tblEducationalGrade tblEducationalGrade)
        {
            int __rowsAffected = 0;

            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEducationalGradeInsert"))
            {
                // Set command type
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Add command parameters
                SqlParameter vid = new SqlParameter("@id", SqlDbType.BigInt);
                vid.Direction = ParameterDirection.InputOutput;
                sqlCommand.Parameters.Add(vid);
                SqlParameter vname = new SqlParameter("@name", SqlDbType.NVarChar, 255);
                vname.Direction = ParameterDirection.Input;
                sqlCommand.Parameters.Add(vname);
                SqlParameter vstageId = new SqlParameter("@stageId", SqlDbType.BigInt);
                vstageId.Direction = ParameterDirection.Input;
                sqlCommand.Parameters.Add(vstageId);

                // Set input parameter values
                SqlServerHelper.SetParameterValue(
                    vid,
                    tblEducationalGrade.id,
                    0);
                SqlServerHelper.SetParameterValue(vname, tblEducationalGrade.name);
                SqlServerHelper.SetParameterValue(vstageId, tblEducationalGrade.stageId);

                try
                {
                    // Attach command
                    AttachCommand(sqlCommand);

                    // Execute command
                    __rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (__rowsAffected == 0)
                    {
                        return __rowsAffected;
                    }


                    // Get output parameter values
                    tblEducationalGrade.id = SqlServerHelper.ToInt64(vid);

                }
                finally
                {
                    // Detach command
                    DetachCommand(sqlCommand);
                }

            }

            return __rowsAffected;
        }

        public IReader<tblEducationalGrade> ListAll()
        {
            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEducationalGradeListAll"))
            {
                // Set command type
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Execute command
                SqlDataReader reader = sqlCommand.ExecuteReader(AttachReaderCommand(sqlCommand));

                // Return reader
                return new SqlServertblEducationalGradeReader(reader);
            }
        }

        public IReader<tblEducationalGrade> ListForstageId(long stageId)
        {
            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEducationalGradeListForstageId"))
            {
                // Set command type
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Add command parameters
                SqlParameter vstageId = new SqlParameter("@stageId", SqlDbType.BigInt);
                vstageId.Direction = ParameterDirection.Input;
                sqlCommand.Parameters.Add(vstageId);

                // Set input parameter values
                SqlServerHelper.SetParameterValue(vstageId, stageId);

                // Execute command
                SqlDataReader reader = sqlCommand.ExecuteReader(AttachReaderCommand(sqlCommand));

                // Return reader
                return new SqlServertblEducationalGradeReader(reader);
            }
        }

    }

    [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]

    public partial class SqlServertblEducationalGradeReader : IReader<tblEducationalGrade>
    {
        private SqlDataReader sqlDataReader;

        private tblEducationalGrade _tblEducationalGrade;

        private int _idOrdinal = -1;
        private int _nameOrdinal = -1;
        private int _stageIdOrdinal = -1;

        public SqlServertblEducationalGradeReader(SqlDataReader sqlDataReader)
        {
            this.sqlDataReader = sqlDataReader;
            for (int i = 0; i < sqlDataReader.FieldCount; i++)
            {
                string columnName = sqlDataReader.GetName(i);
                columnName = columnName.ToUpper();
                switch (columnName)
                {
                    case "ID":
                        _idOrdinal = i;
                        break;

                    case "NAME":
                        _nameOrdinal = i;
                        break;

                    case "STAGEID":
                        _stageIdOrdinal = i;
                        break;

                }
            }
        }

        #region IReader<tblEducationalGrade> Implementation

        public bool Read()
        {
            _tblEducationalGrade = null;
            return this.sqlDataReader.Read();
        }

        public tblEducationalGrade Current
        {
            get
            {
                if (_tblEducationalGrade == null)
                {
                    _tblEducationalGrade = new tblEducationalGrade();
                    if (_idOrdinal != -1)
                    {
                        _tblEducationalGrade.id = SqlServerHelper.ToInt64(sqlDataReader, _idOrdinal);
                    }
                    _tblEducationalGrade.name = SqlServerHelper.ToString(sqlDataReader, _nameOrdinal);
                    if (_stageIdOrdinal != -1)
                    {
                        _tblEducationalGrade.stageId = SqlServerHelper.ToInt64(sqlDataReader, _stageIdOrdinal);
                    }
                }


                return _tblEducationalGrade;
            }
        }

        public void Close()
        {
            sqlDataReader.Close();
        }

        public List<tblEducationalGrade> ToList()
        {
            List<tblEducationalGrade> list = new List<tblEducationalGrade>();
            while (this.Read())
            {
                list.Add(this.Current);
            }
            this.Close();
            return list;
        }

        public DataTable ToDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);
            return dataTable;
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            sqlDataReader.Dispose();
        }
        #endregion

        #region IEnumerable<tblEducationalGrade> Implementation

        public IEnumerator<tblEducationalGrade> GetEnumerator()
        {
            return new tblEducationalGradeEnumerator(this);
        }

        #endregion

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new tblEducationalGradeEnumerator(this);
        }

        #endregion

        [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]

        private partial class tblEducationalGradeEnumerator : IEnumerator<tblEducationalGrade>
        {
            private IReader<tblEducationalGrade> tblEducationalGradeReader;

            public tblEducationalGradeEnumerator(IReader<tblEducationalGrade> tblEducationalGradeReader)
            {
                this.tblEducationalGradeReader = tblEducationalGradeReader;
            }

            #region IEnumerator<tblEducationalGrade> Members

            public tblEducationalGrade Current
            {
                get { return this.tblEducationalGradeReader.Current; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                this.tblEducationalGradeReader.Dispose();
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return this.tblEducationalGradeReader.Current; }
            }

            public bool MoveNext()
            {
                return this.tblEducationalGradeReader.Read();
            }

            public void Reset()
            {
                throw new Exception("Reset of tbleducationalgrade reader is not supported.");
            }

            #endregion

        }
    }
}