using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using HermEsb.Extended.JobObjects;
using HermEsb.Extended.MongoDb.Embedded.Configuration;

namespace HermEsb.Extended.MongoDb.Embedded
{
    public class MongoDeployer : IMongoDeployer
    {
        private readonly IEmbeddedResourceHelper _embeddedResourceHelper;
        private readonly IMongoDbEmbeddedConfig _dbEmbeddedConfig;
#if !__MonoCS__
        private IJobObject _jobObject;
#else
		private Process _process;
#endif

#if !__MonoCS__
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDeployer"/> class.
        /// </summary>
        /// <param name="embeddedResourceHelper">The EmbeddedResourceHelper.</param>
        /// <param name="jobObject">The job object.</param>
        /// <param name="dbEmbeddedConfig">The dbEmbeddedConfig.</param>
        public MongoDeployer(IEmbeddedResourceHelper embeddedResourceHelper, IJobObject jobObject, IMongoDbEmbeddedConfig dbEmbeddedConfig)
        {
            _embeddedResourceHelper = embeddedResourceHelper;
            _dbEmbeddedConfig = dbEmbeddedConfig;
            _jobObject = jobObject;
            _jobObject.KillProcessesOnJobClose = true;
        }
#else
		public MongoDeployer(IEmbeddedResourceHelper embeddedResourceHelper, IMongoDbEmbeddedConfig dbEmbeddedConfig)
		{
			_embeddedResourceHelper = embeddedResourceHelper;
			_dbEmbeddedConfig = dbEmbeddedConfig;
			_process = new Process();
		}
#endif

        #region IMongoDeployer Members

        /// <summary>
        /// Deploys the specified redeploy.
        /// </summary>
        /// <param name="redeploy">if set to <c>true</c> [redeploy].</param>
        public virtual void Deploy(bool redeploy = false)
        {
            if (redeploy) Remove();
            DeployIfNeeded();
        }

        /// <summary>
        /// Kills this instance.
        /// </summary>
        public virtual void Kill()
        {            
            while (IsRunning())        
#if !__MonoCS__				   
                _jobObject.TerminateProcesses(0);
#else
				_process.Kill();
#endif            
        }

        /// <summary>
        /// Determines whether this instance is running.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsRunning()
        {
            return Process.GetProcessesByName("mongod").Any();
            //return Process.GetProcesses().Any(p => p.ProcessName.ToLower().Contains("mongod") && _jobObject.HasProcess(p));
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public virtual void Run()
        {
            if (!IsDeployed()) return;
            var process = new Process
                {
                    StartInfo =
                        {
                            FileName = _dbEmbeddedConfig.Daemon,
                            Arguments = _dbEmbeddedConfig.Arguments,
                            WorkingDirectory = _dbEmbeddedConfig.BinFolder,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                };            
            process.Start();
			#if !__MonoCS__
            _jobObject.AddProcess(process);
			#else
			_process = process;
			#endif
        }

        /// <summary>
        /// Determines whether this instance is deployed.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is deployed; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsDeployed()
        {
            var targetDirectory = new DirectoryInfo(_dbEmbeddedConfig.BinFolder);
            return targetDirectory.Exists;
        }

        /// <summary>
        /// Removes this instance.
        /// </summary>
        public virtual void Remove()
        {
            if (IsRunning()) Kill();
            Delete();
        }

        #endregion

        /// <summary>
        /// Deploys if needed.
        /// </summary>
        private void DeployIfNeeded()
        {
            if (IsDeployed()) return;
            var targetDirectory = new DirectoryInfo(_dbEmbeddedConfig.BinFolder);
            targetDirectory.Create();
            var targetDataDirectory = new DirectoryInfo(_dbEmbeddedConfig.DataFolder);
            targetDataDirectory.Create();
            _dbEmbeddedConfig
                .BinariesPaths
                .ToList()
                .ForEach(
                    f =>
                        {
                            using (var outputStream = new FileStream(f, FileMode.Create))
                                _embeddedResourceHelper.CopyStream(Path.GetFileName(f), outputStream);
                        });
            // Create, if specified, the log directory and log file.
            if (string.IsNullOrWhiteSpace(_dbEmbeddedConfig.LogFolder)) return;
            var targetLogDirectory = new DirectoryInfo(_dbEmbeddedConfig.LogFolder);
            targetLogDirectory.Create();
            File.Create(Path.Combine(targetLogDirectory.FullName, "mongodb.log"));
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        protected void Delete()
        {
            if (!IsDeployed()) return;
            var directory = new DirectoryInfo(_dbEmbeddedConfig.BinFolder);
            if (directory.Exists)
                directory.Delete(true);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="MongoDeployer"/> is reclaimed by garbage collection.
        /// </summary>
        ~MongoDeployer() 
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
#if !__MonoCS__
            if (_jobObject == null) return;
            _jobObject.Dispose();
            _jobObject = null;
#else
			if (_process == null) return;
			if (IsRunning())
				_process.Kill();
			_process.Dispose();
#endif
        }

        /// <summary>
        /// Realiza tareas definidas por la aplicación asociadas a la liberación o al restablecimiento de recursos no administrados.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
