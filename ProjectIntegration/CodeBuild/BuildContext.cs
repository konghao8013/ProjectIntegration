using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuild
{
    public class BuildContext
    {
        string _buildPath;
        public string BuildPath
        {
            get
            {
                return _buildPath;
            }
            set
            {
                _buildPath = Folder.CheckFile(value);
            }
        }
        string _codePath;
        public string CodePath
        {
            get
            {
                return _codePath;
            }
            set
            {
                _codePath = Folder.CheckFile(value);
            }
        }
        string _publishItemPath;
        public string PublishItemPath
        {
            set
            {
                _publishItemPath = value;
            }
            get
            {
                return _publishItemPath;

            }
        }
        string _publishDirectory;
        public string publishDirectory
        {
            set
            {
                _publishDirectory = value;
            }
            get
            {
                return _publishDirectory;
            }
        }
        public BuildTypeEnum Type { set; get; }
        public string Log { set; get; }

        public static BuildContext CreateBuildContext(BuildTypeEnum type, string buildPath, string codePath, string publishItemPath, string publishDirectory)
        {
            return new BuildContext { Type=type, BuildPath = buildPath, CodePath = codePath, PublishItemPath = publishItemPath, publishDirectory = publishDirectory };
        }
    }
}
