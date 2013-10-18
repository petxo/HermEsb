using System.Collections.Generic;
using System.Configuration;

namespace HermEsb.Extended.MongoDb.Embedded.Configuration
{
    public interface IMongoDbEmbeddedConfig
    {
        /// <summary>
        /// Gets the files.
        /// </summary>
        [ConfigurationProperty("binaries", IsDefaultCollection = false, IsRequired = true)]
        [ConfigurationCollection(typeof(BinaryConfig))]
        BinariesConfig Binaries { get; }

        /// <summary>
        /// Gets the binaries paths.
        /// </summary>
        IEnumerable<string> BinariesPaths { get; }

        /// <summary>
        /// Gets the mongo db daemon.
        /// </summary>
        string Daemon { get; }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        string Arguments { get; }

        /// <summary>
        /// Gets the port.
        /// </summary>
        [ConfigurationProperty("port", DefaultValue = 27017, IsRequired = false)]
        int Port { get; }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Gets the directory.
        /// </summary>
        [ConfigurationProperty("binFolder", DefaultValue = "MongoDb\\bin", IsRequired = false)]
        string BinFolder { get; }

        /// <summary>
        /// Gets the data directory.
        /// </summary>
        [ConfigurationProperty("dataFolder", DefaultValue = "MongoDb\\data", IsRequired = true)]
        string DataFolder { get; }

        /// <summary>
        /// Gets a value indicating whether [directory per db].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [directory per db]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("directoryPerDb", DefaultValue = false, IsRequired = false)]
        bool DirectoryPerDb { get; }

        /// <summary>
        /// Gets the log folder.
        /// </summary>
        [ConfigurationProperty("logFolder", DefaultValue = "MongoDb\\log", IsRequired = false)]
        string LogFolder { get; }

        /// <summary>
        /// Gets a value indicating whether [log append].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [log append]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("logAppend", DefaultValue = false, IsRequired = false)]
        bool LogAppend { get; }

        /// <summary>
        /// Gets a value indicating whether [rest interface].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [rest interface]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("rest", DefaultValue = false, IsRequired = false)]
        bool RestInterface { get; }

        /// <summary>
        /// Gets the max connections.
        /// </summary>
        [ConfigurationProperty("maxConns", DefaultValue = 500, IsRequired = false)]        
        int MaxConnections { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IMongoDbEmbeddedConfig"/> is journal.
        /// </summary>
        /// <value>
        ///   <c>true</c> if journal; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("journal", DefaultValue = false, IsRequired = false)]
        bool Journal { get; }

        /// <summary>
        /// Gets the journal commit interval.
        /// </summary>
        [ConfigurationProperty("journalCommitInterval", DefaultValue = 100, IsRequired = false)]        
        int JournalCommitInterval { get; }

        /// <summary>
        /// Gets a value indicating whether [no scripting].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [no scripting]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("noScripting", DefaultValue = true, IsRequired = false)]
        bool NoScripting { get; }

        /// <summary>
        /// Gets the quota files.
        /// </summary>
        [ConfigurationProperty("quotaFiles", DefaultValue = 5, IsRequired = false)]
        int QuotaFiles { get; }

        /// <summary>
        /// Gets a value indicating whether [small files].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [small files]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("smallFiles", DefaultValue = true, IsRequired = false)]
        bool SmallFiles { get; }         
    }
}