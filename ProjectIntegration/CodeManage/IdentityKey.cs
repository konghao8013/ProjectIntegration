using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeManage
{
    public class IdentityKey
    {
       
       
        /// <summary>
        /// 身份信息
        /// </summary>
        static IdentityKey key;
        public static IdentityKey CreateIdentityKey(string userName,string pwd, RTypeEnum type) {
           
            key = new IdentityKey {
                UserName=userName,
                Password=pwd,
                Type= type
            };
           
            return key;
        }

        public static IdentityKey GetIdentity {
            get {
                if (key == null) {
                    throw new Exception("请调用CreateIdentityKey创建用户信息");
                }
                return key;
            }
        }
        
        

        #region 成员定义
        internal string UserName { set; get; }
        internal string Password { set; get; }
        internal RTypeEnum Type { set; get; }

        
        #endregion


    }
}
