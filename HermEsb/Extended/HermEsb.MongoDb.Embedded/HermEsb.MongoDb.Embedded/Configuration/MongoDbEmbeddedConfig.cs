using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace HermEsb.Extended.MongoDb.Embedded.Configuration
{
    public class MongoDbEmbeddedConfig : ConfigurationSection, IMongoDbEmbeddedConfig
    {
        /// <summary>
        /// Gets the files.
        /// </summary>
        [ConfigurationProperty("binaries", IsDefaultCollection = false, IsRequired = true)]
        public BinariesConfig Binaries
        {
            get { return (BinariesConfig) base["binaries"]; }
        }

        /// <summary>
        /// Gets the binaries paths.
        /// </summary>
        public IEnumerable<string> BinariesPaths
        {
            get { return (Binaries.Cast<BinaryConfig>().Select(binary => Path.Combine(BinFolder, binary.Name))).ToList(); }
        }

        /// <summary>
        /// Gets the mongo db daemon.
        /// </summary>
        public string Daemon
        {
            get { return Path.Combine(BinFolder, "mongod.exe"); }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public string Arguments
        {
            get { return string.Format("--dbpath \"{0}\" --logpath \"{1}\" --port {2} {3} {4} {5} {6} {7} {8}", 
                                        DataFolder, 
                                        LogFolder,
                                        Port,
                                        RestInterface ? "--rest" : string.Empty,
                                        LogAppend ? "--logappend" : string.Empty,
                                        DirectoryPerDb ? "--directoryperdb" : string.Empty,
                                        Journal ? "--journal" : string.Empty,
                                        NoScripting ? "--noscripting" : string.Empty,
                                        SmallFiles ? "--smallfiles" : string.Empty);
            }
        }

        /// <summary>
        /// Gets the port.
        /// </summary>
        [ConfigurationProperty("port", DefaultValue = "27017", IsRequired = false)]
        public int Port
        {
            get { return (int) base["port"]; }
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public string ConnectionString
        {
            get { return string.Format("mongodb://localhost:{0}", Port); }
        }

        /// <summary>
        /// Gets the directory.
        /// </summary>
        [ConfigurationProperty("binFolder", DefaultValue = "MongoDb\\bin", IsRequired = false)]
        public string BinFolder
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, base["binFolder"] as string); }
        }

        /// <summary>
        /// Gets the data directory.
        /// </summary>
        [ConfigurationProperty("dataFolder", DefaultValue = "MongoDb\\data", IsRequired = false)]
        public string DataFolder
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, base["dataFolder"] as string); }
        }

        /// <summary>
        /// Gets a value indicating whether [directory per db].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [directory per db]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("directoryPerDb", DefaultValue = false, IsRequired = false)]
        public bool DirectoryPerDb
        {
            get { return (bool)base["directoryPerDb"]; }
        }

        /// <summary>
        /// Gets the log folder.
        /// </summary>
        [ConfigurationProperty("logFolder", DefaultValue = "MongoDb\\log", IsRequired = false)]
        public string LogFolder
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, base["logFolder"] as string); }
        }

        /// <summary>
        /// Gets a value indicating whether [log append].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [log append]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("logAppend", DefaultValue = false, IsRequired = false)]
        public bool LogAppend
        {
            get { return (bool) base["logAppend"]; }
        }

        /// <summary>
        /// Gets a value indicating whether [rest interface].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [rest interface]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("rest", DefaultValue = false, IsRequired = false)]
        public bool RestInterface
        {
            get { return (bool)base["rest"]; }
        }

        /// <summary>
        /// Gets the max connections.
        /// </summary>
        [ConfigurationProperty("maxConns", DefaultValue = 500, IsRequired = false)]
        public int MaxConnections
        {
            get { return (int)base["maxConns"]; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IMongoDbEmbeddedConfig"/> is journal.
        /// </summary>
        /// <value>
        ///   <c>true</c> if journal; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("journal", DefaultValue = false, IsRequired = false)]
        public bool Journal
        {
            get { return (bool)base["journal"]; }
        }

        /// <summary>
        /// Gets the journal commit interval.
        /// </summary>
        [ConfigurationProperty("journalCommitInterval", DefaultValue = 100, IsRequired = false)]
        public int JournalCommitInterval
        {
            get { return (int)base["journalCommitInterval"]; }
        }

        /// <summary>
        /// Gets a value indicating whether [no scripting].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [no scripting]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("noScripting", DefaultValue = true, IsRequired = false)]
        public bool NoScripting
        {
            get { return (bool)base["noScripting"]; }
        }

        /// <summary>
        /// Gets the quota files.
        /// </summary>
        [ConfigurationProperty("quotaFiles", DefaultValue = 5, IsRequired = false)]
        public int QuotaFiles
        {
            get { return (int)base["quotaFiles"]; }
        }

        /// <summary>
        /// Gets a value indicating whether [small files].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [small files]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("smallFiles", DefaultValue = true, IsRequired = false)]
        public bool SmallFiles
        {
            get { return (bool)base["smallFiles"]; }
        }
    }
}