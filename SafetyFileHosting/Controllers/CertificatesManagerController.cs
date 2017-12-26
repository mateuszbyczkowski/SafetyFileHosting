using SafetyFileHosting.Models;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;

namespace SafetyFileHosting.Controllers
{
    public class CertificatesManagerController : Controller
    {
        // GET: CertificatesManager
        public ActionResult Index()
        {
            /* WYGENEROWANIE NOWEGO CERTYFIKATU */
            CertificatesManager.Client = Request.UserHostAddress;
            X509Certificate2 x509 = CertificatesManager.X509Certificate2;

            /*
              DLA CHROME:
              1) WPISZ NA PASKU ADRESU: chrome://flags/#allow-insecure-localhost
              2) WŁĄCZ OPCJĘ 'Allow invalid certificates for resources loaded from localhost'
              3) Ustawienia -> Zaawansowane -> Zarządzaj certyfikatami:
              3a) Importuj -> (Wybierz wygenerowany certyfikat)
              3b) Zaawansowane -> Uwierzytelnienie klienta
             */

            /* POBRANIE INFORMACJI O CERTYFIKACIE ZALACZANYM DO REQUESTA W POSTACI JEDNEGO Z NAGLOWKOW
               NIEZBEDNE W CELU SPRAWDZENIA CZY KLIENT POSLAL ŻĄDANIE ZALACZAJAC CERTYFIKAT CZY NIE
             */
            var certificate = Request.RequestContext.HttpContext.Request.ClientCertificate;

            return View();
        }
    }
}