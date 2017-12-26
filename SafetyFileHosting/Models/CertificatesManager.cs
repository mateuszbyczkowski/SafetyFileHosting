using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Web.Hosting;

namespace SafetyFileHosting.Models
{
    public class CertificatesManager
    {
        #region Properties
        public static string Client { get; set; } = "Undefined";

        private static DateTime Start => DateTime.Now;
        public static DateTime End { get; set; } = Start.AddYears(1);
        #endregion

        public static X509Certificate2 X509Certificate2
        {
            get
            {
                var g = new RsaKeyPairGenerator();
                g.Init(new KeyGenerationParameters(new SecureRandom(), 1024));
                AsymmetricCipherKeyPair keys = g.GenerateKeyPair();

                SecureRandom random = new SecureRandom();
                ISignatureFactory signatureFactory = new Asn1SignatureFactory("SHA256WITHRSA", keys.Private, random);

                var gen = new Org.BouncyCastle.X509.X509V3CertificateGenerator();

                var CN = new X509Name($"CN={Client}");
                var SN = Org.BouncyCastle.Math.BigInteger.ProbablePrime(120, new Random());

                gen.SetSerialNumber(SN);
                gen.SetSubjectDN(CN);
                gen.SetIssuerDN(CN);
                gen.SetNotBefore(Start);
                gen.SetNotAfter(End);
                gen.SetPublicKey(keys.Public);

                gen.AddExtension(
                X509Extensions.AuthorityKeyIdentifier.Id,
                false,
                new AuthorityKeyIdentifier(
                    Org.BouncyCastle.X509.SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(keys.Public),
                    new GeneralNames(new GeneralName(CN)),
                    SN
                ));

                gen.AddExtension(X509Extensions.ExtendedKeyUsage.Id,
                false,
                new ExtendedKeyUsage(new KeyPurposeID[] { KeyPurposeID.IdKPClientAuth }));

                Org.BouncyCastle.X509.X509Certificate bouncyCert = gen.Generate(signatureFactory);

                byte[] ba = bouncyCert.GetEncoded();
                var msCert = new X509Certificate2(ba);

                Save(msCert);
                return msCert;
            }
        }

        private static void Save(X509Certificate cert)
        {
            string path = HostingEnvironment.MapPath($@"~/Certificates/{cert.GetSerialNumberString()}.cer");
            File.WriteAllBytes(path, cert.Export(X509ContentType.Cert));
        }
    }
}