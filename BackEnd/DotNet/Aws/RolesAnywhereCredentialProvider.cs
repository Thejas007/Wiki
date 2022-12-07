namespace Common.Aws.Credential
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Numerics;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    using Amazon.Runtime;
    using Amazon.SecurityToken.Model;    
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;

    /// <summary>
    /// RolesAnywhereCredentialProvider.
    /// </summary>
    public class RolesAnywhereCredentialProvider : AWSCredentials
    {
        private readonly string trustAnchorArn;
        private readonly string profileArn;
        private readonly string roleArn;
        private readonly string certificateThumbprint;

        /// <summary>
        /// The memory cache.
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesAnywhereCredentialProvider"/> class.
        /// </summary>
        /// <param name="trustAnchorArn">trustAnchorArn.</param>
        /// <param name="profileArn">profileArn.</param>
        /// <param name="roleArn">roleArn.</param>
        /// <param name="certificateThumbprint">certificateThumbprint.</param>
        /// <param name="memoryCache">The memory cache.</param>
        public RolesAnywhereCredentialProvider(string trustAnchorArn, string profileArn, string roleArn, string certificateThumbprint, IMemoryCache memoryCache)
        {
            this.trustAnchorArn = trustAnchorArn;
            this.profileArn = profileArn;
            this.roleArn = roleArn;
            this.certificateThumbprint = certificateThumbprint;
            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// Return credential obtained from roles anywhere session api.
        /// </summary>
        /// <returns>Credentials.</returns>
        public override ImmutableCredentials GetCredentials()
        {
            return this.GetCredentialAsync().Result;
        }

        /// <summary>
        /// GetCredential.
        /// </summary>
        /// <returns>ImmutableCredentials.</returns>
        public async Task<ImmutableCredentials> GetCredentialAsync()
        {
            var roleSessionName = "assume_role_session";
            //  From 900 to 3600
            var durationSeconds = 900;
            return await this.memoryCache.GetOrCreateAsync(roleSessionName, async (entry) =>
            {
                var credential = await this.GetCredentialFromRolesAnywhere(roleSessionName, durationSeconds).ConfigureAwait(false);
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(durationSeconds - 60);
                return new ImmutableCredentials(credential.AccessKeyId, credential.SecretAccessKey, credential.SessionToken);
            });
        }

        private static string ToHexString(IReadOnlyCollection<byte> array)
        {
            var hex = new StringBuilder(array.Count * 2);
            foreach (var b in array)
            {
                hex.Append(b.ToString("x2"));
            }

            return hex.ToString()
                .ToLowerInvariant();
        }

        private static string Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2")); // x2 is lowercase
                }

                return sb.ToString()
                    .ToLowerInvariant();
            }
        }

        private async Task<Credentials> GetCredentialFromRolesAnywhere(string sessionName, int durationSeconds)
        {
            //  Roles Anywhere Profile ARN
            //// var profile_arn = "arn:aws:rolesanywhere:us-east-1:123456:profile/b78b1cb9-b7c7-4fb7-a02a-10fe8e06513d";
            //  IAM Role ARN
            //// var role_arn = "arn:aws:iam::123456:role/rolesanywhere-trusted-role";
            //  Roles Anywhere Trust Anchor ARN
            //// var trust_anchor_arn = "arn:aws:rolesanywhere:us-east-1:123456:trust-anchor/981a287e-a8b0-4694-a517-07e0cbad5201";
            //  AWS Region.
            var region = this.profileArn.Split(":")[3];

            //  ************* REQUEST VALUES *************
            var method = "POST";
            var service = "rolesanywhere";
            var host = $"{service}.{region}.amazonaws.com";
            var endpoint = $"https://{host}";

            //  POST requests use a content type header.
            var content_type = "application/json";

            //  Create a date for headers and the credential string
            var today = DateTime.UtcNow;
            var amz_date = today.ToString("yyyyMMddTHHmmssZ");

            //  Date w/o time, used in credential scope
            var date_stamp = today.ToString("yyyyMMdd");

            var certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            certStore.Open(OpenFlags.ReadOnly);
            var certs = certStore.Certificates.Find(X509FindType.FindByThumbprint, this.certificateThumbprint, false);
            var x509cert2 = certs.First();

            // Request parameters for CreateSession--passed in a JSON block.
            var payload = JsonConvert.SerializeObject(new
            {
                durationSeconds = durationSeconds,
                profileArn = this.profileArn,
                roleArn = this.roleArn,
                sessionName = sessionName,
                trustAnchorArn = this.trustAnchorArn,
            });

            // X509 bash64 encoded DER data
            var amz_x509 = Convert.ToBase64String(x509cert2.GetRawCertData());

            Console.WriteLine("amz_x509:");
            Console.WriteLine(amz_x509);

            // Public certificate serial number
            var ca_serial_number = BigInteger.Parse(x509cert2.SerialNumber, NumberStyles.HexNumber).ToString();

            //  ************* TASK 1: CREATE A CANONICAL REQUEST *************
            //  http://docs.aws.amazon.com/general/latest/gr/sigv4-create-canonical-request.html
            //  Step 1 is to define the verb (GET, POST, etc.)--already done.
            //  Step 2: Create canonical URI--the part of the URI from domain to query
            //  string (use "/" if no path)
            var canonical_uri = "/sessions";

            // //  Step 3: Create the canonical query string. In this example, request
            //  parameters are passed in the body of the request and the query string
            //  is blank.
            var canonical_querystring = string.Empty;

            //  Step 4: Create the canonical headers. Header names must be trimmed
            //  and lowercase, and sorted in code point order from low to high.
            //  Note that there is a trailing \n.
            var canonical_headers = "content-type:" + content_type + "\n" + "host:" + host + "\n" + "x-amz-date:" + amz_date + "\n" + "x-amz-x509:" + amz_x509 + "\n";

            //  Step 5: Create the list of signed headers. This lists the headers
            //  in the canonical_headers list, delimited with ";" and in alpha order.
            //  Note: The request can include any headers; canonical_headers and
            //  signed_headers include those that you want to be included in the
            //  hash of the request. "Host" and "x-amz-date" are always required.
            //  For Roles Anywhere, content-type and x-amz-x509 are also required.
            var signed_headers = "content-type;host;x-amz-date;x-amz-x509";

            //  Step 6: Create payload hash. In this example, the payload (body of
            // the request) contains the request parameters.
            var payload_hash = Hash(payload);

            Console.WriteLine("payload:");
            Console.WriteLine(payload);

            Console.WriteLine("payload_hash:");
            Console.WriteLine(payload_hash);

            // Step 7: Combine elements to create canonical request
            var canonical_request = method + "\n" + canonical_uri + "\n" + canonical_querystring + "\n" + canonical_headers + "\n" + signed_headers + "\n" + payload_hash;

            // ************* TASK 2: CREATE THE STRING TO SIGN*************
            // Match the algorithm to the hashing algorithm you use, SHA-256
            var algorithm = "AWS4-X509-RSA-SHA256";
            var credential_scope = date_stamp + "/" + region + "/" + service + "/" + "aws4_request";

            var string_to_sign = algorithm + "\n" + amz_date + "\n" + credential_scope + "\n" + Hash(canonical_request);

            // ************* TASK 3: CALCULATE THE SIGNATURE *************
            // Sign the string_to_sign using the private_key and hex encode
            var rsaPrivateKey = x509cert2.GetRSAPrivateKey();
            var signature = rsaPrivateKey.SignData(Encoding.UTF8.GetBytes(string_to_sign), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            var signature_hex = ToHexString(signature);

            // ************* TASK 4: ADD SIGNING INFORMATION TO THE REQUEST *************
            // Put the signature information in a header named Authorization.
            var authorization_header = algorithm + " " + "Credential=" + ca_serial_number + "/" + credential_scope + ", " + "SignedHeaders=" + signed_headers + ", " + "Signature=" + signature_hex;

            //  For Roles Anywhere, the request  MUST include "host", "x-amz-date",
            //  "x-amz-x509", "content-type", and "Authorization". Except for the authorization
            //  header, the headers must be included in the canonical_headers and signed_headers values, as
            //  noted earlier. Order here is not significant.

            //  ************* SEND THE REQUEST *************
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization_header);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoint + canonical_uri);
            requestMessage.Headers.Add("X-Amz-Date", amz_date);
            requestMessage.Headers.Add("X-Amz-X509", amz_x509);

            requestMessage.Content = new StringContent(payload, Encoding.UTF8, content_type);

            // ; charset=utf-8 is getting added by default. we will remove charset part. other signature will not match.
            requestMessage.Content.Headers.ContentType.CharSet = string.Empty;
            var r = await httpclient.SendAsync(requestMessage).ConfigureAwait(false);

            var resposneContent = await r.Content.ReadAsStringAsync();
            var credentialResponse = JsonConvert.DeserializeObject<SessionCredentialResponse>(resposneContent);
            return credentialResponse.CredentialSet.First().Credentials;
        }
    }
    
    /// <summary>
    /// SessionCredentialResponse.
    /// </summary>
    public class SessionCredentialResponse
    {
        /// <summary>
        /// CredentialSet.
        /// </summary>
        [JsonProperty("credentialSet")]
        public List<CredentialSet> CredentialSet { get; set; }
    }
    
        /// <summary>
    /// CredentialSet.
    /// </summary>
    public class CredentialSet
    {
        /// <summary>
        /// Credentials.
        /// </summary>
        public Credentials Credentials { get; set; }
    }
}
