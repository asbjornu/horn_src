using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Horn.Core.Dsl;
using Horn.Services.Core.Extensions;

namespace horn.services.core.Value
{
    [DataContract(Name = "Package", Namespace = "http://hornget.com/services")]
    public class Package : IResource
    {
        public bool IsRoot
        {
            get { return false; }
        }

        public string FileName
        {
            get
            {
                return string.Format("{0}-{1}", Name, Version);
            }
        }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public List<MetaData> MetaData { get; set; }

        public IResource Parent { get; private set; }

        public bool IsTrunk
        {
            get { return Version == "trunk"; }
        }

        public void SetContents(DirectoryInfo buildDirectory)
        {            
            foreach (var file in buildDirectory.GetFiles())
            {
                Contents.Add(new PackageFile(file));
            }

            Contents.Sort((x, y) => string.Compare(x.Name, y.Name));
        }

        [DataMember(Order = 4)]
        public string Url
        {
            get
            {
                var url = this.GetResourceUrl().Trim(new[] { '/' });

                return string.Format("{0}-{1}", url, Version);
            }
            set
            {
                Console.WriteLine(value);
            }
        }

        [DataMember(Order = 3)]
        public string Version { get; set; }

        [DataMember(Order = 4)]
        public PackageFile ZipFileName { get; set; }

        [DataMember(Order = 5)]
        public List<PackageFile> Contents { get; set; }

        [DataMember(Order = 6)]
        public bool IsError { get; set; }

        [DataMember(Order = 7)]
        public string ErrorMessage { get; set; }

        public Package(Category parent, IBuildMetaData buildMetaData)
        {
            Contents = new List<PackageFile>();

            Name = parent.Name;

            Version = buildMetaData.Version;

            MetaData = new List<MetaData>();

            Parent = parent;

            foreach (var projectInfo in buildMetaData.ProjectInfo)
            {
                MetaData.Add(new MetaData(projectInfo.Key, projectInfo.Value.ToString()));   
            }
        }
    }
}