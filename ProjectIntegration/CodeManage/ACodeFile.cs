using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeManage
{
    public abstract class ACodeFile : ICodeFile
    {
        string _source;
        string _target;
        string _commandPath;
        public string Source
        {
            get
            {
                return _source;
            }

            set
            {
                _source = value;
            }
        }

        public string Target
        {
            get
            {
                return _target;
            }

            set
            {
                _target = Folder.CheckDirectory(value);
            }
        }
        public string CommandPath
        {
            get
            {
                return _commandPath;
            }
            set
            {
                _commandPath = Folder.CheckFile(value);
            }
        }
        public IdentityKey IdentityKey { set; get; }
        public abstract ResultContext Clone();

        public abstract ResultContext Commit();
    }
}
