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

    public partial class tblEmploymentStatus
    {
        private static ItblEmploymentStatusPersister _DefaultPersister;
        private ItblEmploymentStatusPersister _Persister;
        private long _id;
        private string _descr;

        static tblEmploymentStatus()
        {
            // Assign default persister
            _DefaultPersister = new SqlServertblEmploymentStatusPersister();
        }

        public tblEmploymentStatus()
        {
            // Assign default persister to instance persister
            _Persister = _DefaultPersister;
        }

        public tblEmploymentStatus(long _id)
        {
            // Assign default persister to instance persister
            _Persister = _DefaultPersister;

            // Assign method parameter to private fields
            this._id = _id;

            // Call associated retrieve method
            Retrieve();
        }

        public tblEmploymentStatus(DataRow row)
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

                    case "DESCR":
                        this.descr = (string)row[i, DataRowVersion.Current];
                        break;

                }
            }
        }

        public static ItblEmploymentStatusPersister DefaultPersister
        {
            get { return _DefaultPersister; }
            set { _DefaultPersister = value; }
        }

        public ItblEmploymentStatusPersister Persister
        {
            get { return _Persister; }
            set { _Persister = value; }
        }

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string descr
        {
            get { return _descr; }
            set { _descr = value; }
        }

        public virtual void Clone(tblEmploymentStatus sourceObject)
        {
            if (sourceObject == null)
            {
                throw new ArgumentNullException("sourceObject");
            }

            // Clone attributes from source object
            this._id = sourceObject.id;
            this._descr = sourceObject.descr;
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

        public static IReader<tblEmploymentStatus> ListAll()
        {
            return _DefaultPersister.ListAll();
        }

    }

    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]

    public partial interface ItblEmploymentStatusPersister : IPersister
    {
        int Retrieve(tblEmploymentStatus tblEmploymentStatus);
        int Update(tblEmploymentStatus tblEmploymentStatus);
        int Delete(tblEmploymentStatus tblEmploymentStatus);
        int Insert(tblEmploymentStatus tblEmploymentStatus);
        IReader<tblEmploymentStatus> ListAll();
    }

    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]

    public partial class SqlServertblEmploymentStatusPersister : SqlPersisterBase, ItblEmploymentStatusPersister
    {
        public SqlServertblEmploymentStatusPersister()
        {
        }

        public SqlServertblEmploymentStatusPersister(string connectionString) : base(connectionString)
        {
        }

        public SqlServertblEmploymentStatusPersister(SqlConnection connection) : base(connection)
        {
        }

        public SqlServertblEmploymentStatusPersister(SqlTransaction transaction) : base(transaction)
        {
        }

        public int Retrieve(tblEmploymentStatus tblEmploymentStatus)
        {
            int __rowsAffected = 1;

            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEmploymentStatusGet"))
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
                    SqlParameter vdescr = new SqlParameter("@descr", SqlDbType.NVarChar, 255);
                    vdescr.Direction = ParameterDirection.Output;
                    sqlCommand.Parameters.Add(vdescr);

                    // Set input parameter values
                    SqlServerHelper.SetParameterValue(vid, tblEmploymentStatus.id);

                    // Execute command
                    sqlCommand.ExecuteNonQuery();

                    try
                    {
                        // Get output parameter values
                        tblEmploymentStatus.id = SqlServerHelper.ToInt64(vid);
                        tblEmploymentStatus.descr = SqlServerHelper.ToString(vdescr);

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

        public int Update(tblEmploymentStatus tblEmploymentStatus)
        {
            int __rowsAffected = 0;

            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEmploymentStatusUpdate"))
            {
                // Set command type
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Add command parameters
                SqlParameter vid = new SqlParameter("@id", SqlDbType.BigInt);
                vid.Direction = ParameterDirection.Input;
                sqlCommand.Parameters.Add(vid);
                SqlParameter vdescr = new SqlParameter("@descr", SqlDbType.NVarChar, 255);
                vdescr.Direction = ParameterDirection.Input;
                sqlCommand.Parameters.Add(vdescr);

                // Set input parameter values
                SqlServerHelper.SetParameterValue(vid, tblEmploymentStatus.id);
                SqlServerHelper.SetParameterValue(vdescr, tblEmploymentStatus.descr);

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

        public int Delete(tblEmploymentStatus tblEmploymentStatus)
        {
            int __rowsAffected = 0;

            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEmploymentStatusDelete"))
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
                    SqlServerHelper.SetParameterValue(vid, tblEmploymentStatus.id);

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

        public int Insert(tblEmploymentStatus tblEmploymentStatus)
        {
            int __rowsAffected = 0;

            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEmploymentStatusInsert"))
            {
                // Set command type
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Add command parameters
                SqlParameter vid = new SqlParameter("@id", SqlDbType.BigInt);
                vid.Direction = ParameterDirection.InputOutput;
                sqlCommand.Parameters.Add(vid);
                SqlParameter vdescr = new SqlParameter("@descr", SqlDbType.NVarChar, 255);
                vdescr.Direction = ParameterDirection.Input;
                sqlCommand.Parameters.Add(vdescr);

                // Set input parameter values
                SqlServerHelper.SetParameterValue(
                    vid,
                    tblEmploymentStatus.id,
                    0);
                SqlServerHelper.SetParameterValue(vdescr, tblEmploymentStatus.descr);

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
                    tblEmploymentStatus.id = SqlServerHelper.ToInt64(vid);

                }
                finally
                {
                    // Detach command
                    DetachCommand(sqlCommand);
                }

            }

            return __rowsAffected;
        }

        public IReader<tblEmploymentStatus> ListAll()
        {
            // Create command
            using (SqlCommand sqlCommand = new SqlCommand("tblEmploymentStatusListAll"))
            {
                // Set command type
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Execute command
                SqlDataReader reader = sqlCommand.ExecuteReader(AttachReaderCommand(sqlCommand));

                // Return reader
                return new SqlServertblEmploymentStatusReader(reader);
            }
        }

    }

    [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]

    public partial class SqlServertblEmploymentStatusReader : IReader<tblEmploymentStatus>
    {
        private SqlDataReader sqlDataReader;

        private tblEmploymentStatus _tblEmploymentStatus;

        private int _idOrdinal = -1;
        private int _descrOrdinal = -1;

        public SqlServertblEmploymentStatusReader(SqlDataReader sqlDataReader)
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

                    case "DESCR":
                        _descrOrdinal = i;
                        break;

                }
            }
        }

        #region IReader<tblEmploymentStatus> Implementation

        public bool Read()
        {
            _tblEmploymentStatus = null;
            return this.sqlDataReader.Read();
        }

        public tblEmploymentStatus Current
        {
            get
            {
                if (_tblEmploymentStatus == null)
                {
                    _tblEmploymentStatus = new tblEmploymentStatus();
                    if (_idOrdinal != -1)
                    {
                        _tblEmploymentStatus.id = SqlServerHelper.ToInt64(sqlDataReader, _idOrdinal);
                    }
                    _tblEmploymentStatus.descr = SqlServerHelper.ToString(sqlDataReader, _descrOrdinal);
                }


                return _tblEmploymentStatus;
            }
        }

        public void Close()
        {
            sqlDataReader.Close();
        }

        public List<tblEmploymentStatus> ToList()
        {
            List<tblEmploymentStatus> list = new List<tblEmploymentStatus>();
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

        #region IEnumerable<tblEmploymentStatus> Implementation

        public IEnumerator<tblEmploymentStatus> GetEnumerator()
        {
            return new tblEmploymentStatusEnumerator(this);
        }

        #endregion

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new tblEmploymentStatusEnumerator(this);
        }

        #endregion

        [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]

        private partial class tblEmploymentStatusEnumerator : IEnumerator<tblEmploymentStatus>
        {
            private IReader<tblEmploymentStatus> tblEmploymentStatusReader;

            public tblEmploymentStatusEnumerator(IReader<tblEmploymentStatus> tblEmploymentStatusReader)
            {
                this.tblEmploymentStatusReader = tblEmploymentStatusReader;
            }

            #region IEnumerator<tblEmploymentStatus> Members

            public tblEmploymentStatus Current
            {
                get { return this.tblEmploymentStatusReader.Current; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                this.tblEmploymentStatusReader.Dispose();
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return this.tblEmploymentStatusReader.Current; }
            }

            public bool MoveNext()
            {
                return this.tblEmploymentStatusReader.Read();
            }

            public void Reset()
            {
                throw new Exception("Reset of tblemploymentstatus reader is not supported.");
            }

            #endregion

        }
    }
}
