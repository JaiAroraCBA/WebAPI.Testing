using System.Net.Http;

namespace Nancy.Testing
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Wrapper for the HTTP response body that is used by the <see cref="BrowserResponse"/> class.
    /// </summary>
    public class BrowserResponseBodyWrapper : IEnumerable<byte>
    {
        private readonly IEnumerable<byte> responseBytes;
        private readonly DocumentWrapper responseDocument;

        public BrowserResponseBodyWrapper(HttpResponseMessage response)
        {
            var contentStream =
                GetContentStream(response);

            this.responseBytes = contentStream.ToArray();
            this.responseDocument = new DocumentWrapper(this.responseBytes);
        }

        private static MemoryStream GetContentStream(HttpResponseMessage response)
        {
            var contentsStream = new MemoryStream(response.Content.ReadAsByteArrayAsync().Result);  
            contentsStream.Position = 0;
            return contentsStream;
        }

        /// <summary>
        /// Gets a <see cref="QueryWrapper"/> for the provided <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">The CSS3 that shuold be applied.</param>
        /// <returns>A <see cref="QueryWrapper"/> instance.</returns>
        public QueryWrapper this[string selector]
        {
            get { return this.responseDocument[selector]; }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="IEnumerator{T}"/> that can be used to iterate through the collection.</returns>
        public IEnumerator<byte> GetEnumerator()
        {
            return this.responseBytes.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}