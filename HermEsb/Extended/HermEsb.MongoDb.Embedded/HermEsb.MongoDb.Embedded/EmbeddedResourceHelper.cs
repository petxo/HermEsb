using System;
using System.IO;
using System.Linq;

namespace HermEsb.Extended.MongoDb.Embedded
{
    public class EmbeddedResourceHelper : IEmbeddedResourceHelper
    {
        private const int BufferSize = 8 * 1024;

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <param name="resourceName">Name of the EmbeddedResourceHelper.</param>
        /// <returns></returns>
        public Stream GetStream(string resourceName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var actualResourceName = string.Empty;
                foreach (var embeddedResource in assembly.GetManifestResourceNames().Where(embeddedResource => embeddedResource.ToLower().EndsWith(resourceName.ToLower())))
                    actualResourceName = embeddedResource;
                if (actualResourceName != string.Empty)
                    return assembly.GetManifestResourceStream(actualResourceName);
            }
            return null;
        }

        /// <summary>
        /// Copies the stream.
        /// </summary>
        /// <param name="resourceName">Name of the EmbeddedResourceHelper.</param>
        /// <param name="outputStream">The output stream.</param>
        public void CopyStream(string resourceName, Stream outputStream)
        {
            using (var inputStream = GetStream(resourceName))
                CopyStream(inputStream, outputStream);
        }

        /// <summary>
        /// Copies the stream.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="outputStream">The output stream.</param>
        public void CopyStream(Stream input, Stream outputStream)
        {
            var buffer = new byte[BufferSize];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                outputStream.Write(buffer, 0, len);
        }
    }
}