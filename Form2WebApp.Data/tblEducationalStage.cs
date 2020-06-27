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

    public partial class tblEducationalStage
    {
        private static ItblEducationalStagePersister _DefaultPersister;
        private ItblEducationalStagePersister _Persister;
        private long _id;
        private string _name;

        static tblEducationalStage()
        {
            // Assign default persister
            _DefaultPersister = new SqlServertblEducationalStagePersister();
        }

        public tblEducationalStage()
        {
            // Assign default persister to instance persister
            _Persister = _DefaultPersister;
        }

        public tblEducationalStage(long _id)
        {
            // Assign default persister to instance persister
            _Persister = _DefaultPersister;

            // Assign method parameter to private fields
            this._id = _id;

            // Call associated retrieve method
            Retrieve();
        }

        public tblEducationalStage(DataRow row)
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

                }
            }
        }

        public static ItblEducationalStagePersister DefaultPersister
        {
            get { return _DefaultPersister; }
            set { _DefaultPersister = value; }
        }

        public ItblEducationalStagePersister Persister
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

        public virtual void Clone(tblEducationalStage sourceObject)
        {
            if (sourceObject == null)
            {
                throw new ArgumentNullException("sourceObject");
            }

            // Clone attributes from source object
            this._id = sourceObject.id;
            this._name = sourceObject.name;
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

        public static IReader<tblEducationalStage> ListAll()
        {
            return _DefaultPersister.ListAll();
        }

    }

    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]

    public partial interface ItblEducationalStagePersister : IPersister
    {
        int Retrieve(tblEducationalStage tblEducationalStage);
        int Update(tblEducationalStage tblEducationalStage);
        int Delete(tblEducationalStage tblEducationalStage);
        int Insert(tblEducationalStage tblEducationalStage);
        IReader<tblEducationalStage> ListAll();
    }

    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]

    public partial class SqlServertblEducationalStagePersister : SqlPersisterBase, ItblEducationalStagePersister
    {
        public SqlServertblEducationalStagePersister()
        {
        }

        public SqlServertblEducationalStagePersister(string connectionString) : base(connectionString)
        {
        }

        public SqlServertblEducationalStagePersister(SqlConnection connection) : base(connection)
        {
        }

        public SqlServertblEducationalStagePersister(SqlTransaction transaction) : base(transaction)
        {
        }

        public int Retrieve(tblEducationalStage tblEducationalStage)
        {
            int __rowsAffected = 1;

            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEducationalStageGet"))
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

                    // Set input parameter values
                    SqlServerHelper.SetParameterValue(vid, tblEducationalStage.id);

                    // Execute command
                    sqlCommand.ExecuteNonQuery();

                    try
                    {
                        // Get output parameter values
                        tblEducationalStage.id = SqlServerHelper.ToInt64(vid);
                        tblEducationalStage.name = SqlServerHelper.ToString(vname);

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

        public int Update(tblEducationalStage tblEducationalStage)
        {
            int __rowsAffected = 0;

            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEducationalStageUpdate"))
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

                // Set input parameter values
                SqlServerHelper.SetParameterValue(vid, tblEducationalStage.id);
                SqlServerHelper.SetParameterValue(vname, tblEducationalStage.name);

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

        public int Delete(tblEducationalStage tblEducationalStage)
        {
            int __rowsAffected = 0;

            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEducationalStageDelete"))
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
                    SqlServerHelper.SetParameterValue(vid, tblEducationalStage.id);

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

        public int Insert(tblEducationalStage tblEducationalStage)
        {
            int __rowsAffected = 0;

            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEducationalStageInsert"))
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

                // Set input parameter values
                SqlServerHelper.SetParameterValue(
                    vid,
                    tblEducationalStage.id,
                    0);
                SqlServerHelper.SetParameterValue(vname, tblEducationalStage.name);

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
                    tblEducationalStage.id = SqlServerHelper.ToInt64(vid);

                }
                finally
                {
                    // Detach command
                    DetachCommand(sqlCommand);
                }

            }

            return __rowsAffected;
        }

        public IReader<tblEducationalStage> ListAll()
        {
            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEducationalStageListAll"))
            {
                // Set command type
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Execute command
                SqlDataReader reader = sqlCommand.ExecuteReader(AttachReaderCommand(sqlCommand));

                // Return reader
                return new SqlServertblEducationalStageReader(reader);
            }
        }

    }

    [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]

    public partial class SqlServertblEducationalStageReader : IReader<tblEducationalStage>
    {
        private SqlDataReader sqlDataReader;

        private tblEducationalStage _tblEducationalStage;

        private int _idOrdinal = -1;
        private int _nameOrdinal = -1;

        public SqlServertblEducationalStageReader(SqlDataReader sqlDataReader)
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

                }
            }
        }

        #region IReader<tblEducationalStage> Implementation

        public bool Read()
        {
            _tblEducationalStage = null;
            return this.sqlDataReader.Read();
        }

        public tblEducationalStage Current
        {
            get
            {
                if (_tblEducationalStage == null)
                {
                    _tblEducationalStage = new tblEducationalStage();
                    if (_idOrdinal != -1)
                    {
                        _tblEducationalStage.id = SqlServerHelper.ToInt64(sqlDataReader, _idOrdinal);
                    }
                    _tblEducationalStage.name = SqlServerHelper.ToString(sqlDataReader, _nameOrdinal);
                }


                return _tblEducationalStage;
            }
        }

        public void Close()
        {
            sqlDataReader.Close();
        }

        public List<tblEducationalStage> ToList()
        {
            List<tblEducationalStage> list = new List<tblEducationalStage>();
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

        #region IEnumerable<tblEducationalStage> Implementation

        public IEnumerator<tblEducationalStage> GetEnumerator()
        {
            return new tblEducationalStageEnumerator(this);
        }

        #endregion

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new tblEducationalStageEnumerator(this);
        }

        #endregion

        [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]

        private partial class tblEducationalStageEnumerator : IEnumerator<tblEducationalStage>
        {
            private IReader<tblEducationalStage> tblEducationalStageReader;

            public tblEducationalStageEnumerator(IReader<tblEducationalStage> tblEducationalStageReader)
            {
                this.tblEducationalStageReader = tblEducationalStageReader;
            }

            #region IEnumerator<tblEducationalStage> Members

            public tblEducationalStage Current
            {
                get { return this.tblEducationalStageReader.Current; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                this.tblEducationalStageReader.Dispose();
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return this.tblEducationalStageReader.Current; }
            }

            public bool MoveNext()
            {
                return this.tblEducationalStageReader.Read();
            }

            public void Reset()
            {
                throw new Exception("Reset of tbleducationalstage reader is not supported.");
            }

            #endregion

        }
    }
}