namespace HermEsb.Core.Clustering
{
    public static class CluterControllerExtensions
    {
        /// <summary>
        /// Determines whether the specified cluster controller is null.
        /// </summary>
        /// <param name="clusterController">The cluster controller.</param>
        /// <returns>
        ///   <c>true</c> if the specified cluster controller is null; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNull(this IClusterController clusterController)
        {
            return clusterController == CluterControllerFactory.NullController;
        }
    }
}