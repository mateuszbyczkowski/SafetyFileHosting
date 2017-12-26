using SafetyFileHosting.Models;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;

namespace SafetyFileHosting.Controllers
{
    public class CertificatesManagerController : Controller
    {
        public ActionResult Index()
        {
            /* WYGENEROWANIE CERTYFIKATU DLA NOWEGO KLIENTA I ZAIMPORTOWANIE GO PO STRONIE SERWERA */
            CertificatesManager.Client = Request.UserHostAddress;
            X509Certificate2 wygenerowanyCertyfikat = CertificatesManager.X509Certificate2;

            X509Store x509Store = new X509Store(StoreName.TrustedPeople, StoreLocation.CurrentUser);
            x509Store.Open(OpenFlags.MaxAllowed);
            x509Store.Add(wygenerowanyCertyfikat);
            x509Store.Close();

            /*
            INSTRUKCJA JAK KLIENT POWINIEN DODAĆ CERTYFIKAT DO REQUESTA W CHROME:

            1) Wpisz na pasku adresu: chrome://flags/#allow-insecure-localhost
            2) Włącz opcję 'Allow invalid certificates for resources loaded from localhost'
            3) Ustawienia -> Zaawansowane -> Zarządzaj certyfikatami:
            3a) Importuj -> (Wybierz wygenerowany certyfikat)
            3b) Zaawansowane -> Uwierzytelnienie klienta
            */

            /* OBSŁUGA CERTYFIKATU ZAŁĄCZONEGO PRZEZ KLIENTA DO REQUESTA */
            var odebranyCertyfikat = Request.RequestContext.HttpContext.Request.ClientCertificate;
            if(odebranyCertyfikat.HasKeys()) {

                X509Certificate2 cert = new X509Certificate2(odebranyCertyfikat.Certificate);
                if (cert.Verify()) {
                    // TODO ..
                }
                else {
                    // TODO ..
                }
            }

            return View();
        }
    }
}