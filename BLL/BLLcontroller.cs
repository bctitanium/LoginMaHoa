using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class BLLcontroller
    {
        DALcontroller dalc = new DALcontroller();

        public bool CheckAccount(string username, string password)
        {
            bool kq;

            if (dalc.CheckAccount(username, password))
            {
                kq = true;
            }
            else
            {
                kq = false;
            }

            return kq;
        }

        public bool CheckUsername(string username)
        {
            bool kq;

            if (dalc.CheckUsername(username))
            {
                kq = true;
            }
            else
            {
                kq = false;
            }

            return kq;
        }

        public bool isAdmin(string username, string password)
        {
            bool kq;

            if (dalc.isAdmin(username, password))
            {
                kq = true;
            }
            else
            {
                kq = false;
            }

            return kq;
        }

        public bool CreateAccount(string username, string password)
        {
            bool kq;

            if (dalc.CreateAccount(username, password))
            {
                kq = true;
            }
            else
            {
                kq = false;
            }

            return kq;
        }

        public bool AddHuman(string ten, string tuoi)
        {
            bool kq;

            if (dalc.AddHuman(ten, tuoi))
            {
                kq = true;
            }
            else
            {
                kq = false;
            }

            return kq;
        }

        public bool EditHuman(int id, string ten, string tuoi)
        {
            bool kq;

            if (dalc.EditHuman(id, ten, tuoi))
            {
                kq = true;
            }
            else
            {
                kq = false;
            }

            return kq;
        }

        public string getUsername(string username)
        {
            return dalc.getUsername(username);
        }

        public string getPassword(string username)
        {
            return dalc.getPassword(username);
        }

        public string passwordEncryption(string password)
        {
            return dalc.passwordEncryption(password);
        }

        public string passwordDecryption(string password)
        {
            return dalc.passwordDecryption(password);
        }

        public bool DoiMatKhau(string username, string password)
        {
            bool kq;

            if (dalc.DoiMatKhau(username, password))
            {
                kq = true;
            }
            else
            {
                kq = false;
            }

            return kq;
        }

        public bool GhiLog(string username)
        {
            bool kq;

            if (dalc.GhiLog(username))
            {
                kq = true;
            }
            else
            {
                kq = false;
            }

            return kq;
        }

        public bool GhiTheoDoi(int id)
        {
            bool kq;

            if (dalc.GhiTheoDoi(id))
            {
                kq = true;
            }
            else
            {
                kq = false;
            }

            return kq;
        }

        public bool BiTheoDoi(int id)
        {
            bool kq;

            if (dalc.BiTheoDoi(id))
            {
                kq = true;
            }
            else
            {
                kq = false;
            }

            return kq;
        }

        public bool XoaTheoDoi()
        {
            bool kq;

            if (dalc.XoaTheoDoi())
            {
                kq = true;
            }
            else
            {
                kq = false;
            }

            return kq;
        }

        public bool GhiPasswordChangeLog(string username)
        {
            bool kq;

            if (dalc.GhiPasswordChangeLog(username))
            {
                kq = true;
            }
            else
            {
                kq = false;
            }

            return kq;
        }

        public bool GhiHumanChange(string username, int num, int Id, string tencu, string tenmoi, string tuoicu, string tuoimoi)
        {
            bool kq;

            if (dalc.GhiHumanChange(username, num, Id, tencu, tenmoi, tuoicu, tuoimoi))
            {
                kq = true;
            }
            else
            {
                kq = false;
            }
            
            return kq;
        }

        public bool RemoveHuman(int id)
        {
            bool kq;

            if (dalc.RemoveHuman(id))
            {
                kq = true;
            }
            else
            {
                kq = false;
            }
            
            return kq;
        }

        public DataTable LoadDGLog()
        {
            return dalc.LoadDataGridLogCheck();
        }

        public DataTable LoadDGTheoDoi()
        {
            return dalc.LoadDataGridTheoDoi();
        }

        public DataTable LoadDGPasswordChangeLog()
        {
            return dalc.LoadDataGridPasswordChangeLog();
        }

        public DataTable LoadDGHuman()
        {
            return dalc.LoadDataGridHuman();
        }

        public DataTable LoadDGHumanChange()
        {
            return dalc.LoadDataGridHumanChange();
        }

        public DataTable LoadDGAcc()
        {
            return dalc.LoadDataGridAccount();
        }

        public bool setAuditingOn()
        {
            bool kq;

            if (dalc.setAuditingOn())
            {
                kq = true;
            }
            else
            {
                kq = false;
            }

            return kq;
        }

        public bool setAuditingOff()
        {
            bool kq;

            if (dalc.setAuditingOff())
            {
                kq = true;
            }
            else
            {
                kq = false;
            }

            return kq;
        }

        public int GetIdFromUsername(string username)
        {
            return dalc.GetIdFromUsername(username);
        }

        public bool GhiTheoDoiTatCa()
        {
            return dalc.GhiTheoDoiTatCa();
        }

        //public bool isAuditing()
        //{
        //    bool kq;

        //    if (dalc.isAuditing())
        //    {
        //        kq = true;
        //    }
        //    else
        //    {
        //        kq = false;
        //    }

        //    return kq;
        //}
    }
}
