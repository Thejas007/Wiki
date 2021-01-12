using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(typeof(Program).FullName);
            Console.WriteLine(typeof(Program).Name);
            Console.WriteLine(nameof(Program));
            var patchProperties = new List<string>();
            patchProperties.Add("ExpirationDate");
            patchProperties.Add("Quantity");
            var request = new PatchV1Model();
            request.Quantity = null;
            request.SetSerializableProperties(patchProperties);

            var body = JsonConvert.SerializeObject(request);
            Console.WriteLine(body);
        }
    }

    public class PatchV1Model
    {
        /// <summary>
        /// JSON merge serialization properties.
        /// </summary>
        private IList<string> serializableProperties;

        /// <summary>
        ///  Gets or sets the license quantity.
        /// <remarks>Positive integer value, null for unlimited quantity.</remarks>
        /// </summary>
        [JsonProperty("quantity")]
        public int? Quantity { get; set; }

        /// <summary>
        ///  Gets or sets the license end date.
        /// <remarks>Null value represents perpetual license.</remarks>
        /// </summary>
        [JsonProperty("end_date")]
        public string ExpirationDate { get; set; }

        /// <summary>
        ///  Gets or sets the license status ACTIVE or DISABLED.
        /// <remarks>Default value is ACTIVE.</remarks>
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Flag to indicate ExpirationDate to serialize.
        /// </summary>
        /// <returns>Flag.</returns>
        public bool ShouldSerializeExpirationDate()
        {
            return this.serializableProperties.Contains(nameof(this.ExpirationDate), StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Flag to indicate Quantity to serialize.
        /// </summary>
        /// <returns>Flag.</returns>
        public bool ShouldSerializeQuantity()
        {
            return this.serializableProperties.Contains(nameof(this.Quantity), StringComparer.OrdinalIgnoreCase);
        }

        public bool ShouldSerializeStatus()
        {
            return this.serializableProperties.Contains(nameof(this.Status), StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Sets JSON merge properties to external service for conditional serialization.
        /// </summary>
        /// <param name="patchProperties">The inbound patch properties.</param>
        public void SetSerializableProperties(IList<string> patchProperties)
        {
            this.serializableProperties = patchProperties;
        }
    }
}
