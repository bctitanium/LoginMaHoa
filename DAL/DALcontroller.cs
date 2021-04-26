using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALcontroller : DatabaseService
    {
        /// <summary>
        /// kiểm tra sự tồn tại của account
        /// </summary>
        /// <param name="username">tên tài khoản</param>
        /// <param name="password">mật khẩu</param>
        /// <returns>true thì có tài khoản này, ngược lại thì chưa có</returns>
        public bool CheckAccount(string username, string password)
        {
            bool kq;

            try
            {
                string sql = "Select UserName, PassWord from AccountTable where UserName=@Username and PassWord=@Password";
                SqlParameter parUser = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };
                SqlParameter parPass = new SqlParameter("@Password", SqlDbType.NVarChar) { Value = password };

                SqlDataReader reader = ReadDataPars(sql, new[] { parUser, parPass });
                
                if (reader.Read())
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool CheckUsername(string username)
        {
            bool kq;

            try
            {
                string sql = "Select UserName from AccountTable where UserName=@Username";
                SqlParameter parUser = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };

                SqlDataReader reader = ReadDataPars(sql, new[] { parUser });

                if (reader.Read())
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool isAdmin(string username, string password)
        {
            bool kq;

            try
            {
                string sql = "Select accType from AccountTable where UserName=@Username and PassWord=@Password";
                SqlParameter parUser = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };
                SqlParameter parPass = new SqlParameter("@Password", SqlDbType.NVarChar) { Value = password };

                SqlDataReader reader = ReadDataPars(sql, new[] { parUser, parPass });

                if (reader.Read() && reader.GetInt32(0) == 0)
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool CreateAccount(string username, string password)
        {
            bool kq;

            try
            {
                string sql = "Insert into AccountTable(UserName, PassWord, AccType) " +
                    "values(@Username, @Password, @Type)";
                SqlParameter parUser = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };
                SqlParameter parPass = new SqlParameter("@Password", SqlDbType.NVarChar) { Value = password };
                SqlParameter parType = new SqlParameter("@Type", SqlDbType.Int) { Value = 1 };

                if (WriteDataPars(sql, new[] { parUser, parPass, parType}))
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool AddHuman(string ten, string tuoi)
        {
            bool kq;

            try
            {
                string sql = "Insert into Human(Ten, Tuoi) " +
                    "values(@ten, @tuoi)";
                SqlParameter parUser = new SqlParameter("@ten", SqlDbType.NVarChar) { Value = ten };
                SqlParameter parPass = new SqlParameter("@tuoi", SqlDbType.Int) { Value = tuoi };

                if (WriteDataPars(sql, new[] { parUser, parPass }))
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool EditHuman(int id, string ten, string tuoi)
        {
            bool kq;

            try
            {
                string sql = "Update Human set Ten=@ten, Tuoi=@tuoi where Id=@id";
                SqlParameter parUser = new SqlParameter("@ten", SqlDbType.NVarChar) { Value = ten };
                SqlParameter parPass = new SqlParameter("@tuoi", SqlDbType.Int) { Value = tuoi };
                SqlParameter parId = new SqlParameter("@id", SqlDbType.Int) { Value = id };

                if (WriteDataPars(sql, new[] { parUser, parPass, parId }))
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool RemoveHuman(int id)
        {
            bool kq;

            try
            {
                string sql = "Delete from Human where Id=@id";
                SqlParameter parId = new SqlParameter("@id", SqlDbType.Int) { Value = id };

                if (WriteDataPars(sql, new[] { parId }))
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public string getUsername(string username)
        {
            string kq = "";

            try
            {
                string sql = "Select UserName from AccountTable where UserName=@Username";
                SqlParameter parUser = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };

                SqlDataReader reader = ReadDataPars(sql, new[] { parUser });

                if (reader.Read())
                {
                    kq = reader.GetString(0);
                }
                else
                {
                    kq = "";
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public string getPassword(string username)
        {
            string kq = "";

            try
            {
                string sql = "Select PassWord from AccountTable where UserName=@Username";
                SqlParameter parUser = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };

                SqlDataReader reader = ReadDataPars(sql, new[] { parUser });

                if (reader.Read())
                {
                    kq = reader.GetString(0);
                }
                else
                {
                    kq = "";
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public string passwordEncryption(string password)
        {
            return Encrypt(password);
        }

        public string passwordDecryption(string password)
        {
            return Decrypt(password);
        }

        public bool DoiMatKhau(string username, string password)
        {
            bool kq;

            try
            {
                string sql = "Update AccountTable set PassWord=@Password where UserName=@Username";
                SqlParameter parUser = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };
                SqlParameter parPass = new SqlParameter("@Password", SqlDbType.NVarChar) { Value = password };

                if (WriteDataPars(sql, new[] { parUser, parPass }))
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool GhiLog(string username)
        {
            bool kq;

            try
            {
                string sql = "Insert into LogCheck(UserName, DateTimeLog) values(@Username, @Dt)";
                SqlParameter parUser = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };
                SqlParameter parDt = new SqlParameter("@Dt", SqlDbType.DateTime) { Value = DateTime.Now };

                if (WriteDataPars(sql, new[] { parUser, parDt }))
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool GhiTheoDoi(int id)
        {
            bool kq;

            try
            {
                string sql = "Insert into TheoDoi(IdTheoDoi) values(@id)";
                SqlParameter parIdtd = new SqlParameter("@id", SqlDbType.Int) { Value = id };

                if (WriteDataPars(sql, new[] { parIdtd }))
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool GhiTheoDoiTatCa()
        {
            bool kq;

            try
            {
                string sql = "Insert into TheoDoi(IdTheoDoi) Select Id from AccountTable where Id != 1";

                if (WriteData(sql))
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool BiTheoDoi(int id)
        {
            bool kq;

            try
            {
                string sql = "Select IdTheoDoi from TheoDoi where IdTheoDoi=@id";
                SqlParameter parIdtd = new SqlParameter("@id", SqlDbType.Int) { Value = id };

                if (ReadDataPars(sql, new[] { parIdtd }).Read())
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool XoaTheoDoi()
        {
            bool kq;

            try
            {
                string sql = "Delete from TheoDoi";

                if (WriteData(sql))
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public int GetIdFromUsername(string username)
        {
            int kq = 0;

            try
            {
                string sql = "Select Id from AccountTable where UserName=@username";
                SqlParameter parUser = new SqlParameter("@username", SqlDbType.NVarChar) { Value = username };

                SqlDataReader reader = ReadDataPars(sql, new[] { parUser });

                if (reader.Read())
                {
                    kq = reader.GetInt32(0);
                }
                else
                {
                    kq = 0;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool GhiPasswordChangeLog(string username)
        {
            bool kq;

            try
            {
                string sql = "Insert into PasswordChangeLog(UserName, DateTimeLog) values(@Username, @Dt)";
                SqlParameter parUser = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };
                SqlParameter parDt = new SqlParameter("@Dt", SqlDbType.DateTime) { Value = DateTime.Now };

                if (WriteDataPars(sql, new[] { parUser, parDt }))
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool GhiHumanChange(string username, int num, int Id, string tencu, string tenmoi, string tuoicu, string tuoimoi)
        {
            bool kq;
            SqlParameter parDes = null;

            try
            {
                string sql = "Insert into HumanChange(UserName, Description, DateTimeLog) values(@Username, @des, @Dt)";
                SqlParameter parUser = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };

                if (num == 1)
                {
                    parDes = new SqlParameter("@des", SqlDbType.NVarChar) { Value = "'" + username + "'" + " đã thêm " + "'" + tenmoi + "'"};
                }
                else
                {
                    if (num == 2)
                    {
                        parDes = new SqlParameter("@des", SqlDbType.NVarChar) { Value = "'" + username + "'" + " đã sửa người có Id = " + Id.ToString() + ", tên cũ: " + "'" + tencu + "'" + " thành " + "'" + tenmoi + "'" + ", tuổi cũ = " + tuoicu + " thành " + tuoimoi };
                    }
                    else
                    {
                        if (num == 3)
                        {
                            parDes = new SqlParameter("@des", SqlDbType.NVarChar) { Value = "'" + username + "'" + " đã xóa người có Id = " + Id.ToString() + ", tên là: " + tencu + ", tuổi: " + tuoicu };
                        }
                        else
                        {

                        }
                    }
                }

                SqlParameter parDt = new SqlParameter("@Dt", SqlDbType.DateTime) { Value = DateTime.Now };

                if (WriteDataPars(sql, new[] { parUser, parDes, parDt }))
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public DataTable LoadDataGridLogCheck()
        {
            DataTable dt = new DataTable(); //tạo mới bảng dữ liệu

            string sql = "Select Id, UserName, DateTimeLog from LogCheck where UserName != 'admin'";
            SqlDataAdapter da = new SqlDataAdapter(sql, connection); //truyền câu truy vấn và kết nối vào cổng kết nối
            da.Fill(dt); //đổ dữ liệu lấy từ câu truy vấn vào bảng

            return dt; //trả về dữ liệu của bảng
        }

        public DataTable LoadDataGridTheoDoi()
        {
            DataTable dt = new DataTable(); //tạo mới bảng dữ liệu

            string sql = "Select Id, IdTheoDoi from TheoDoi";
            SqlDataAdapter da = new SqlDataAdapter(sql, connection); //truyền câu truy vấn và kết nối vào cổng kết nối
            da.Fill(dt); //đổ dữ liệu lấy từ câu truy vấn vào bảng

            return dt; //trả về dữ liệu của bảng
        }

        public DataTable LoadDataGridPasswordChangeLog()
        {
            DataTable dt = new DataTable(); //tạo mới bảng dữ liệu

            string sql = "Select Id, UserName, DateTimeLog from PasswordChangeLog where UserName != 'admin'";
            SqlDataAdapter da = new SqlDataAdapter(sql, connection); //truyền câu truy vấn và kết nối vào cổng kết nối
            da.Fill(dt); //đổ dữ liệu lấy từ câu truy vấn vào bảng

            return dt; //trả về dữ liệu của bảng
        }

        public DataTable LoadDataGridHuman()
        {
            DataTable dt = new DataTable(); //tạo mới bảng dữ liệu

            string sql = "Select Id, Ten, Tuoi from Human";
            SqlDataAdapter da = new SqlDataAdapter(sql, connection); //truyền câu truy vấn và kết nối vào cổng kết nối
            da.Fill(dt); //đổ dữ liệu lấy từ câu truy vấn vào bảng

            return dt; //trả về dữ liệu của bảng
        }

        public DataTable LoadDataGridHumanChange()
        {
            DataTable dt = new DataTable(); //tạo mới bảng dữ liệu

            string sql = "Select Id, UserName, Description, DateTimeLog from HumanChange where UserName != 'admin'";
            SqlDataAdapter da = new SqlDataAdapter(sql, connection); //truyền câu truy vấn và kết nối vào cổng kết nối
            da.Fill(dt); //đổ dữ liệu lấy từ câu truy vấn vào bảng

            return dt; //trả về dữ liệu của bảng
        }

        public DataTable LoadDataGridAccount()
        {
            DataTable dt = new DataTable(); //tạo mới bảng dữ liệu

            string sql = "Select ID, UserName, PassWord from AccountTable where accType = 1";
            SqlDataAdapter da = new SqlDataAdapter(sql, connection); //truyền câu truy vấn và kết nối vào cổng kết nối
            da.Fill(dt); //đổ dữ liệu lấy từ câu truy vấn vào bảng

            return dt; //trả về dữ liệu của bảng
        }

        public bool setAuditingOn()
        {
            bool kq;

            try
            {
                string sql = "Update ConstNum set isAuditing = @num where constName = @name";
                SqlParameter parNum = new SqlParameter("@num", SqlDbType.Int) { Value = 1 };
                SqlParameter parName = new SqlParameter("@name", SqlDbType.VarChar) { Value = "Auditing" };

                if (WriteDataPars(sql, new[] { parNum, parName }))
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool setAuditingOff()
        {
            bool kq;

            try
            {
                string sql = "Update ConstNum set isAuditing = @num where constName = @name";
                SqlParameter parNum = new SqlParameter("@num", SqlDbType.Int) { Value = 0 };
                SqlParameter parName = new SqlParameter("@name", SqlDbType.VarChar) { Value = "Auditing" };

                if (WriteDataPars(sql, new[] { parNum, parName }))
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }

        public bool isAuditing()
        {
            bool kq;

            try
            {
                string sql = "Select isAuditing from ConstNum where constName = @name";
                SqlParameter parName = new SqlParameter("@name", SqlDbType.VarChar) { Value = "Auditing" };

                SqlDataReader reader = ReadDataPars(sql, new[] { parName });

                if (reader.Read() && reader.GetInt32(0) == 1)
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            finally
            {
                CloseConnection();
            }

            return kq;
        }
    }
}
