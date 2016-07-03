using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CodeManage
{
    public class SVNCode : ACodeFile
    {

        private SVNCode() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="path">代码管理器路劲</param>
        public SVNCode(string source,string target,IdentityKey key) {
            Source = source;
            Target = target;
            IdentityKey = key;
           // CommandPath = path;
        }

        public override ResultContext Clone()
        {
            var result = new ResultContext();
            var cmd = new Command();
            if (Folder.GetDrive(Target) != Folder.GetTheDrive)
            {

                var drive = Folder.GetDrive(Target) + ":";
                cmd.Call(drive);
            }
            cmd.Call("cd " + Target);


            var key = IdentityKey;
            cmd.Call("svn checkout " + Source);
            cmd.Call("111111");
            cmd.Call(key.UserName);
            cmd.Call(key.Password);
            cmd.Exit();
            result.Log = cmd.CommadnLog;
            return result;
        }

        public override ResultContext Commit()
        {
            throw new NotImplementedException();
        }

        #region 私有函数
    

        string Colon {
            get {
                return ":";
            }
        }
        string SVNDirectory {
            get {
                return Target + "\\.svn";
            }
        }
        #endregion


        #region 参考命令
        /*

         如果你不知道命令怎么用svn命令,可通过如下方式查询：
        svn help
        知道了子命令，但是不知道子命令的用法，还可以查询：
        svn help ci
        开发人员常用命令
         导入项目
        svn import http://svn.chinasvn.com:82/pthread --message "Start project"
        导出项目
        svn checkout http://svn.chinasvn.com:82/pthread
        采用 export 的方式来导出一份“干净”的项目
        svn export http://svn.chinasvn.com:82/pthread pthread
        为失败的事务清场
        svn cleanup
        在本地进行代码修改，检查修改状态
        svn status -v
        svn diff
        更新(update)服务器数据到本地
        svn update directory
        svn update file
        增加(add)本地数据到服务器
        svn add file.c
        svn add dir
        对文件进行改名和删除
        svn mv b.c bb.c
        svn rm d.c
        提交(commit)本地文档到服务器
        svn commit
        svn ci
        svn ci -m "commit"
        查看日志
        svn log directory
        svn log file
        相关的一些东西：
        1、在本地文件中，每个目录下都有一个.svn文件夹(属性为隐藏)，保存了相关的信息。
        2、注册环境变量SVN_EDITOR为"E:\Program Files\Vim\vim71\gvim.exe"，结果在svn ci的时候，出现错误:

        'E:\Program' 不是内部或外部命令，也不是可运行的程序
        或批处理文件。
        svn: 提交失败(细节如下):
        svn: system('E:\Program Files\Vim\vim71\gvim.exe svn-commit.tmp') 返回 1
*/
        #endregion
    }
}
