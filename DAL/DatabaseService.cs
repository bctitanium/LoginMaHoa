using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace DAL
{
    public class DatabaseService
    {
        //private const string connectionString = "Data Source = BC; Initial Catalog = btBMTT; Integrated Security = True";
        protected const string connectionString = "Data Source = BC; Initial Catalog = btBMTT; User Id=sa; Password=123";
        protected SqlConnection connection;
        protected SqlCommand cmd;
        protected static string key = "d9a6yla21chuoi64khoa1dai2oi7la2dai2";

        public DatabaseService()
        {
            connection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            else
            {

            }
        }

        public void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
            else
            {

            }
        }

        public SqlDataReader ReadData(string sql)
        {
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = connection;
            OpenConnection();
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public SqlDataReader ReadDataPars(string sql, SqlParameter[] pars)
        {
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = connection;
            OpenConnection();
            cmd.Parameters.AddRange(pars);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public bool WriteData(string sql)
        {
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = connection;
            OpenConnection();

            if (cmd.ExecuteNonQuery() != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WriteDataPars(string sql, SqlParameter[] pars)
        {
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = connection;
            OpenConnection();
            cmd.Parameters.AddRange(pars);

            if (cmd.ExecuteNonQuery() != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// hàm mã hóa 1 chuổi nhập vào
        /// </summary>
        /// <param name="toEncrypt">chuổi cần mã hóa</param>
        /// <returns>chuổi mã hóa</returns>
        public static string Encrypt(string toEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            //sử dụng mã hóa md5 để tạo key từ key có sẵn để sử dụng cho mã hóa 3DES
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key)); //tạo khóa bí mật
            hashmd5.Clear(); //phải giải phóng tài nguyên sử dụng

            //sử dụng mã hóa 3DES
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray; //gán khóa bí mật cho 3DES
            tdes.Mode = CipherMode.ECB;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //biến đổi về dạng byte
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear(); //phải giải phóng tài nguyên sử dụng
            //trả về chuỗi mã hóa
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// hàm giải mã 1 chuỗi nhập vào
        /// </summary>
        /// <param name="toDecrypt">chuỗi cần giải mã</param>
        /// <returns>chuổi văn bản rõ</returns>
        public static string Decrypt(string toDecrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            //sử dụng mã hóa md5 để tạo key từ key có sẵn để sử dụng cho mã hóa 3DES
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key)); //tạo khóa bí mật
            hashmd5.Clear(); //phải giải phóng tài nguyên sử dụng

            //sử dụng mã hóa 3DES
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray; //gán khóa bí mật cho 3DES
            tdes.Mode = CipherMode.ECB;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            //biến đổi về dạng byte
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear(); //phải giải phóng tài nguyên sử dụng
            //trả về chuỗi đã giải mã
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
