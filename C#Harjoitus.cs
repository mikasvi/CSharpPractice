using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*
Tee ohjelma, jolla voit laskea kuinka paljon saat työmarkkinatukea kuukaudessa 
jäädessäsi työttömäksi, kun et ole ollut niin pitkään töissä että saisit 
ansiosidonnaista työttömyyspäivärahaa. Työmarkkinatuen ehdot ja määrät ovat 
yksinkertaistettu malli oikeista työmarkkinatuen ehdoista ja ovat seuraavat: 
 
·        Työmarkkinatuen määrä on 32,68 euroa/päivä ja sitä maksetaan viideltä päivältä viikossa.
·        Lapset korottavat työmarkkinatukea seuraavasti: yksi lapsi 5,27 euroa/pv, kaksi lasta 7,74 euroa/ pv ja kolmesta tai useammasta yhteensä 9,98 e/pv
·        Työllistymistä edistävä palvelu korottaa tukea 4,78 e/pv
·        Jos tulot ylittävät 300 euroa, niin jokainen sen määrän ylittävä palkkana maksettu euro vähentää tukea 50 senttiä
·        Jos asut vanhempiesi taloudessa tukea vähennetään 50% 
 
Alla on suuntaa antava esimerkki ohjelman toiminnasta. Ohjelman käyttöliittymän ei 
tarvitse olla kuvan mukainen, mutta tarvittava toiminnallisuus on siinä esillä. 
Ohjelmalla voi toistaa työmarkkinatuen laskemista niin monta kertaa kuin haluaa. 
Tuki lasketaan kuukaudelle, jossa on 4 viikkoa.
 
Tämä ohjelma laskee työmarkkinatuen määrän.
 
Kuinka monta lasta sinulla on: 5
Kuinka monena päivänä olet osallistunut työllistymistä edistävään palveluun: 2
Kuinka paljon olet saanut palkkaa: 320
Asutko vanhempiesi luona (k/e): k
Saat työmarkkinatukea 426.38 euroa kuukaudessa
 
Haluatko laske työmarkkinatuen uusilla tiedoilla (k/e): e
*/
namespace Tentti
{
    class Program
    {
        // jos haluaisi hifistellä, niin tekisi ohjelman sellaiseksi ettei kaatuisi vaikka
        // syöttäisi mitä, mutta en monimutkaista tätä nyt enempää
        static void KysyTiedot(out int lapset, out int tep, out double palkka, out char asuuKotona)
        {
            Console.Write("Montako lasta sinulla on : ");
            lapset = int.Parse(Console.ReadLine());
            Console.Write("Kuinka monena päivänä olet osallistunut työllistymistä edistävään palveluun :");
            tep = int.Parse(Console.ReadLine());
            Console.Write("Kuinka paljon olet saanut palkkaa :");
            palkka = double.Parse(Console.ReadLine());
            Console.Write("Asutko vanhempiesi luona (k/e) : ");
            asuuKotona = char.Parse(Console.ReadLine());
        }

        static void LaskeTuki(int lapset, int tep, double palkka, char asuuKotona, out double tuki)
        {
            tuki = 32.68; // alustetaan aina joka laskennan aluksi

            // lasten vaikutus tukeen
            if (lapset == 1)
                tuki += 5.27;
            else if (lapset == 2)
                tuki += 7.74;
            else if (lapset >= 3)
                tuki += 9.98;

            // kuukauden perustuki
            tuki = tuki * 20;

            // työllistämistä edistävä palvelu lisää tukea
            if (tep > 0)
                tuki = tuki + tep * 4.78;

            // palkan vaikutus tukeen
            if (palkka > 300)
                tuki = tuki - ((int)palkka - 300) * 0.5; // jokainen euro vähentää 50 senttiä

            // jos asuu kotona
            if (char.ToLower(asuuKotona) == 'k')
                tuki = tuki / 2;
        }

        static void Main()
        {
            char uusiLaskenta; // Lasketaanko tuki uudestaan
            double tuki; // tähän lasketaan tuki
            int lapset; // lasten lukumäärä
            int tep; // työllistämistä edistävä palvelu, montako päivää
            double palkka; // paljonko saanut palkkaa
            char asuuKotona; // asuuko vanhempien luona

            do
            {
                KysyTiedot(out lapset, out tep, out palkka, out asuuKotona);
                LaskeTuki(lapset, tep, palkka, asuuKotona, out tuki);

                Console.WriteLine("Saat työmarkkinatukea {0:f2} euroa kuukaudessa", tuki);

                Console.Write("Haluatko laske työmarkkinatuen uusilla tiedoilla (k/e) : ");
                uusiLaskenta = char.Parse(Console.ReadLine());
            } while (char.ToLower(uusiLaskenta) == 'k'); // muutetaan pieneksi ja vertaillaan
        }
    }
}
