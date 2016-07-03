using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeManage
{
    public interface ICodeFile
    {
        /// <summary>
        /// 签出文件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ResultContext Clone();
        /// <summary>
        /// 提交文件
        /// </summary>
        /// <param name="key">身份信息</param>
        /// <returns></returns>
        ResultContext Commit();
    }
}
