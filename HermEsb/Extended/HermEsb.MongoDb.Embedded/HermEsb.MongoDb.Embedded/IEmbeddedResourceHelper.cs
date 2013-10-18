using System.IO;

namespace HermEsb.Extended.MongoDb.Embedded
{
    public interface IEmbeddedResourceHelper
    {
        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns></returns>
        Stream GetStream(string resourceName);
       
        /// <summary>
        /// Copies the stream.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="outputStream">The output stream.</param>
        void CopyStream(Stream input, Stream outputStream);
        
        /// <summary>
        /// Copies the stream.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="outputStream">The output stream.</param>
        void CopyStream(string resourceName, Stream outputStream);
    }
}