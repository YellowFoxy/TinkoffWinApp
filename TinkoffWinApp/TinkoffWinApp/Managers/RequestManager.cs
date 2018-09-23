using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TinkoffWinApp.Models.ResponceModels;
using Windows.Web.Http;

namespace TinkoffWinApp.Managers
{
    public interface IRequestManager
    {
        Task<ProductsResponceModel> LoadProducts();
    }

    public class RequestManager : IRequestManager
    {
        #region 

        private const string test = "{ \"result\":{ \"type\":\"json\", \"value\":[{\"id\":\"platformCardsCreditPlatinum\"," +
        "\"programId\":\"Bravo\",\"product\":\"CC\",\"order\":\"0\",\"type\":\"Credit\",\"title\":\"Tinkoff Platinum\",\"slogan\"" +
        ":\"Выгодная карта\nна каждый день\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"text\":\"300K\"},\"text\":" +
        "\"Кредитный лимит до 300 000 ₽\"},{\"icon\":{\"name\":\"Letter\"},\"text\":\"Доставка по всей России\"},{\"icon\":{\"text\"" +
        ":\"0%\"},\"text\":\"Без процентов до 55 дней\"}],\"hrefTariff\":\"/cards/credit-cards/tinkoff-platinum/tariffs/\",\"bgColor\":" +
        "\"#8c8c8f\"},{\"id\":\"platformCardsCreditAllAirlines\",\"programId\":\"AllAirlines\",\"product\":\"AA\",\"order\":\"1\",\"type\":" +
        "\"Credit\",\"title\":\"ALL Airlines\",\"slogan\":\"Премиальная карта\nдля путешествий\",\"multiCurrencies\":\"false\",\"benefits\":" +
        "[{\"icon\":{\"name\":\"Avia\"},\"text\":\"Бесплатные полеты любыми авиакомпаниями\"},{\"icon\":{\"text\":\"30%\"},\"text\":\"Мили " +
        "за любые покупки\"},{\"icon\":{\"text\":\"700K\"},\"text\":\"Кредитный лимит до 700 000 ₽\"}],\"hrefTariff\":\"/cards/credit-cards/all" +
        "-airlines/tariffs/\",\"bgColor\":\"#1172ae\"},{\"id\":\"platformCardsCobrandsCreditKanobu\",\"programId\":\"Kanobu\",\"product\":\"KB\"," +
        "\"order\":\"2\",\"type\":\"Credit\",\"title\":\"All Games\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"text\":\"5%\"}," +
        "\"text\":\"До 5% за покупки в магазинах Steam, Origin, Xbox Games и PlayStation Store\"},{\"icon\":{\"name\":\"Games\"},\"text\":\"Об" +
        "менивайте на покупки в магазинах электроники и игр\"}],\"hrefTariff\":\"/cards/credit-cards/all-games/tariffs/\",\"bgColor\":\"#4b28" +
        "3a\"},{\"id\":\"platformCardsCobrandsDebitKanobu\",\"programId\":\"KanobuDebit\",\"product\":\"DAG\",\"order\":\"2\",\"type\":\"Curre" +
        "nt\",\"title\":\"All Games\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"text\":\"5%\"},\"text\":\"До 5% за покупки в маг" +
        "азинах Steam, Origin, Xbox Games и PlayStation Store\"},{\"icon\":{\"name\":\"Games\"},\"text\":\"Обменивайте на покупки в магазинах эл" +
        "ектроники и игр\"}],\"hrefTariff\":\"/cards/debit-cards/all-games/tariffs/\",\"bgColor\":\"#4b283a\"},{\"id\":\"platformCardsCobrands" +
        "CreditAliexpress\",\"programId\":\"AliExpress\",\"product\":\"AE\",\"order\":\"3\",\"type\":\"Credit\",\"title\":\"AliExpress\",\"mul" +
        "tiCurrencies\":\"false\",\"slogan\":\"Совершайте покупки по карте, и получайте бонусные баллы, которые можно обменять на любые това" +
        "ры в AliExpress!\",\"benefits\":[{\"icon\":{\"text\":\"5%\"},\"text\":\"Возвращаем до 5% баллами AliExpress\"},{\"icon\":{\"name\":\"Ast" +
        "roprogramId\"},\"text\":\"Меняйте бонусы на покупки в AliExpress\"}],\"hrefTariff\":\"/cards/credit-cards/aliexpress/tariffs/\",\"bgC" +
        "olor\":\"#cc3014\"},{\"id\":\"platformCardsCreditEBay\",\"programId\":\"eBay\",\"product\":\"EB\",\"order\":\"4\",\"type\":\"Cre" +
        "dit\",\"title\":\"Ebay\",\"multiCurrencies\":\"false\",\"slogan\":\"3% от стоимости каждой покупки в интернете возвращаются на кар" +
        "ту!\",\"benefits\":[{\"icon\":{\"text\":\"3%\"},\"text\":\"Возвращаем 3% от любых покупок в интернете\"},{\"icon\":{\"text\":\"10" +
        "00\"},\"text\":\"Каждому клиенту — 1000 баллов eBay в подарок\"},{\"icon\":{\"name\":\"Ebay\"},\"text\":\"Меняйте бонусы на поку" +
        "пки на eBay\"}],\"hrefTariff\":\"/cards/credit-cards/ebay/tariffs/\",\"bgColor\":\"#5297e0\"},{\"id\":\"platformCardsCobrandsCre" +
        "ditOneTwoTrip\",\"programId\":\"OneTwoTrip\",\"order\":\"5\",\"type\":\"Credit\",\"title\":\"OneTwoTrip\",\"slogan\":\"С кредит" +
        "ной картой лояльности OneTwoTrip вы получите до 9% от суммы покупки в виде бонусов. Потратить бонусы можно на бронирование оте" +
        "лей и покупку авиабилетов на сайте или в приложении OneTwoTrip 1 бонус = 1 рубль\",\"multiCurrencies\":\"false\",\"bene" +
        "fits\":[{\"icon\":{\"name\":\"SuitcaseOTT\"},\"text\":\"Бонусы OneTwoTrip за все покупки\"},{\"icon\":{\"text\":\"9%\"},\"te" +
        "xt\":\"До 9% баллов с покупок через OneTwoTrip\"},{\"icon\":{\"name\":\"World\"},\"text\":\"Оплачивайте полеты в любых авиа" +
        "компаниях мира\"}],\"hrefTariff\":\"/cards/credit-cards/onetwotrip/tariffs/\",\"bgColor\":\"#f59e00\"},{\"id\":\"platformCa" +
        "rdsCreditRendezvous\",\"programId\":\"RendezVous\",\"order\":\"6\",\"type\":\"Credit\",\"title\":\"Рандеву\",\"slogan\":\"За каж" +
        "дую покупку вы получите баллы, которые можно потратить на обувь, сумки и аксессуары в магазинах Rendez-Vous\",\"multiCu" +
        "rrencies\":\"false\",\"benefits\":[{\"icon\":{\"text\":\"10%\"},\"text\":\"Возвращаем на карту 10% за покупки в Rendez‑" +
        "Vous\"},{\"icon\":{\"text\":\"2%\"},\"text\":\"За покупки в магазинах одежды, обуви и аксессуаров — 2%\"},{\"icon\":{\"te" +
        "xt\":\"1%\"},\"text\":\"За все остальные покупки — 1%\"}],\"hrefTariff\":\"/cards/credit-cards/rendezvous/tariffs/\",\"bgC" +
        "olor\":\"#262e43\"},{\"id\":\"platformCardsCobrandsCreditLamoda\",\"programId\":\"Lamoda\",\"order\":\"7\",\"type\":\"Cred" +
        "it\",\"title\":\"Lamoda\",\"slogan\":\"Оформи карту Lamoda. Соверши любую покупку по карте и получи 1000 бонусов. Использ" +
        "уй бонусы на любые покупки на Lamoda.ru 1 бонус = 1 рубль.\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"tex" +
        "t\":\"5%\"},\"text\":\"До 5% начисляются в виде баллов Lamoda\"},{\"icon\":{\"name\":\"Clothes\"},\"text\":\"Меняйте бону" +
        "сы на покупки в Lamoda\"}],\"hrefTariff\":\"/cards/credit-cards/lamoda/tariffs/\",\"bgColor\":\"#175dd2\"},{\"id\":\"plat" +
        "formCardsBlack\",\"programId\":\"Cashback\",\"product\":\"DC\",\"order\":\"10\",\"type\":\"Current\",\"title\":\"Tinkoff Bl" +
        "ack\",\"slogan\":\"Ваши деньги\nработают всегда\",\"multiCurrencies\":\"true\",\"benefits\":[{\"icon\":{\"text\":\"0%\"},\"te" +
        "xt\":\"Наличные от 3000 ₽ без комиссии\"},{\"icon\":{\"text\":\"30%\"},\"text\":\"Кэшбэк в рублях до 30%\"},{\"icon\":{\"tex" +
        "t\":\"6%\"},\"text\":\"До 6% годовых на остаток\"}],\"hrefTariff\":\"/cards/debit-cards/tinkoff-black/tariffs/\",\"bgCo" +
        "lor\":\"#262e43\"},{\"id\":\"platformCardsCobrandsOneTwoTrip\",\"programId\":\"OTTDebit\",\"order\":\"11\",\"type\":\"Curre" +
        "nt\",\"title\":\"OneTwoTrip\",\"slogan\":\"За все операции по карте начисляются бонусы, которые можно потратить на оплату пут" +
        "ешествий на сайте или в приложении OneTwoTrip.\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"text\":\"6%\"},\"te" +
        "xt\":\"Начисляем 6% годовых на остаток\"},{\"icon\":{\"text\":\"2%\"},\"text\":\"За OneTwoTrip на карту возв" +
        "ращается 2% баллами\"},{\"icon\":{\"name\":\"SuitcaseOTT\"},\"text\":\"Тратьте баллы OneTwoTrip на беспла" +
        "тные полеты\"}],\"hrefTariff\":\"/cards/debit-cards/onetwotrip/tariffs/\",\"bgColor\":\"#f5" +
        "9e00\"},{\"id\":\"platformCardsCobrandsAliExpress\",\"programId\":\"AliExpressDebit\",\"pr" +
        "oduct\":\"DAE\",\"order\":\"12\",\"type\":\"Current\",\"title\":\"AliEx" +
        "press\",\"slogan\":\"Совершайте покупки по карте и получайте бонусы\",\"mult" +
        "iCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"text\":\"6%\"},\"text\":\"Нач" +
        "исляем 6% годовых баллами AliExpress\"},{\"icon\":{\"text\":\"2%\"},\"text\":\"За поку" +
        "пки на AliExpress возвращается 2%\"},{\"icon\":{\"text\":\"1=1\"},\"text\":\"Покуп" +
        "ки на AliExpress за бонусы (1 бонус = 1 руб.)\"}],\"hrefTariff\":\"/cards/debit-cards/al" +
        "iexpress/tariffs/\",\"bgColor\":\"#cc3014\"},{\"id\":\"platformCardsCobrandsDebitLam" +
        "oda\",\"programId\":\"LamodaDebit\",\"order\":\"13\",\"type\":\"Current\",\"title\":\"Lam" +
        "oda\",\"slogan\":\"Получайте бонусы за все покупки и обменивайте на новые товары в La" +
        "moda по курсу 1 бонус = 1 рубль.\",\"multiCurrencies\":\"false\",\"benefits\":[{\"ic" +
        "on\":{\"text\":\"6%\"},\"text\":\"Начисляем 6% годовых баллами Lamoda\"},{\"icon\":{\"t" +
        "ext\":\"2%\"},\"text\":\"С каждой покупки на Lamoda возвращается 2%\"},{\"icon\":{\"na" +
        "me\":\"Clothes\"},\"text\":\"Меняйте бонусы на покупки в Lamoda\"}],\"hrefTariff\":\"/ca" +
        "rds/debit-cards/lamoda/tariffs/\",\"bgColor\":\"#175dd2\"},{\"id\":\"platformCard" +
        "sEbay\",\"programId\":\"eBayDebit\",\"product\":\"DEB\",\"order\":\"14\",\"type\":\"Cu" +
        "rrent\",\"title\":\"Ebay\",\"slogan\":\"Обменивайте баллы на любые товары на eBay по к" +
        "урсу 1 бонус = 1 рубль.\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"te" +
        "xt\":\"6%\"},\"text\":\"Начисляем 6% годовых баллами eBay\"},{\"icon\":{\"text\":\"2" +
        "%\"},\"text\":\"За любые покупки в интернете или на eBay.com возвращается 2%\"},{\"ic" +
        "on\":{\"name\":\"Ebay\"},\"text\":\"Меняйте бонусы на покупки на eBay\"}],\"hrefTariff\":\"/ca" +
        "rds/debit-cards/ebay/tariffs/\",\"bgColor\":\"#5297e0\"},{\"id\":\"platformCardsSClub\",\"prog" +
        "ramId\":\"SvyaznoyClub\",\"order\":\"15\",\"type\":\"Current\",\"title\":\"Св" +
        "язной‑клуб\",\"slogan\":\"Карта привязана к бонусной программе Связной-Клуб. Чем ча" +
        "ще вы платите ей, тем больше плюсов получаете\",\"multiCurrencies\":\"false\",\"ben" +
        "efits\":[{\"icon\":{\"text\":\"10%\"},\"text\":\"Начисляем 10% годовых на остаток\"},{\"ic" +
        "on\":{\"text\":\"40+\"},\"text\":\"Тратьте бонусы у 40+ партнеров Связного‑клуба\"},{\"ic" +
        "on\":{\"text\":\"14%\"},\"text\":\"Кэшбэк за покупки у партнеров Связного‑клуба — до 14%\"}],\"hr" +
        "efTariff\":\"/cards/debit-cards/svyaznoy/tariffs/\",\"bgColor\":\"#262e43\"},{\"id\":\"platformC" +
        "ardsCreditGooglePlay\",\"programId\":\"GooglePlay\",\"order\":\"17\",\"type\":\"Credit\",\"title\":\"Goo" +
        "gle Play\",\"slogan\":\"Игры, фильмы, музыка за любые покупки\",\"multiCurrencies\":\"false\",\"benef" +
        "its\":[{\"icon\":{\"name\":\"GooglePlay\"},\"text\":\"Возвращаем до 10% от покупок в Google Play\"},{\"ic" +
        "on\":{\"name\":\"Fun\"},\"text\":\"За траты в ресторанах и на развлечения — 2%\"},{\"icon\":{\"text\":\"1%\"},\"te" +
        "xt\":\"За все остальные покупки — 1%  \"}],\"hrefTariff\":\"/cards/credit-cards/google-play/\",\"bgColor\":\"#4f8" +
        "4aa\"},{\"id\":\"platformCardsCobrandsGooglePlay\",\"programId\":\"GooglePlayDebit\",\"order\":\"18\",\"type\":\"Cur" +
        "rent\",\"title\":\"Google Play\",\"slogan\":\"Игры, фильмы, музыка за любые покупки\",\"multiCurrencies\":\"false\",\"be" +
        "nefits\":[{\"icon\":{\"name\":\"GooglePlay\"},\"text\":\"Возвращаем до 5% от покупок в Google Play\"},{\"icon\":{\"nam" +
        "e\":\"Fun\"},\"text\":\"За траты в ресторанах и на развлечения — 1,5%\"},{\"icon\":{\"text\":\"1%\"},\"text\":\"За все ост" +
        "альные покупки — 1%\"}],\"hrefTariff\":\"/cards/debit-cards/google-play/\",\"bgColor\":\"#4f84aa\"},{\"id\":\"platformCard" +
        "sCobrandsUlmartDebit\",\"programId\":\"UlmartDebit\",\"order\":\"19\",\"type\":\"Current\",\"title\":\"Юлмарт\",\"slogan\":\"Пок" +
        "упки на Ulmart.ru еще выгоднее!\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"text\":\"2%\"},\"text\":\"За покупк" +
        "и на Ulmart.ru возвращаем до 2%\"},{\"icon\":{\"text\":\"1,5%\"},\"text\":\"За покупки в категориях «Рестораны», «АЗС» и «Апте" +
        "ки»\"},{\"icon\":{\"text\":\"1%\"},\"text\":\"За все остальные покупки — 1%\"}],\"hrefTariff\":\"/cards/debit-ca" +
        "rds/ulmart/\",\"bgColor\":\"#E70034\"},{\"id\":\"platformCardsCobrandsUlmartCredit\",\"programId\":\"Ulmart\",\"o" +
        "rder\":\"19\",\"type\":\"Credit\",\"title\":\"Юлмарт\",\"slogan\":\"Покупки на Ulmart.ru еще выгоднее!\",\"multi" +
        "Currencies\":\"false\",\"benefits\":[{\"icon\":{\"text\":\"3%\"},\"text\":\"За покупки на Ulmart.ru возвращаем д" +
        "о 3%\"},{\"icon\":{\"text\":\"2%\"},\"text\":\"За покупки в категориях «Рестораны», «АЗС» и «Аптеки»\"},{\"ic" +
        "on\":{\"text\":\"1%\"},\"text\":\"За все остальные покупки — 1%\"}],\"hrefTariff\":\"/cards/credit-cards/ulm" +
        "art/\",\"bgColor\":\"#E70034\"},{\"id\":\"platformCardsDebitAllAirlines\",\"programId\":\"AllAirLin" +
        "esDebit\",\"product\":\"DAA\",\"order\":\"21\",\"type\":\"Current\",\"title\":\"ALL Airlines\",\"sl" +
        "ogan\":\"Премиальная карта\nдля путешествий\",\"multiCurrencies\":\"false\",\"benefits\":[{\"i" +
        "con\":{\"text\":\"6%\"},\"text\":\"до 6% годовых на остаток милями\"},{\"icon\":{\"text\":\"3" +
        "0%\"},\"text\":\"Мили за любые покупки\"},{\"icon\":{\"name\":\"Avia\"},\"text\":\"Бесплатные по" +
        "леты любыми авиакомпаниями\"}],\"hrefTariff\":\"/cards/debit-cards/all-airlines/\",\"bgColor\":\"#0" +
        "A2148\"},{\"id\":\"platformCardsCreditWWF\",\"programId\":\"WWF\",\"order\":\"21\",\"type\":\"Cr" +
        "edit\",\"title\":\"WWF\",\"slogan\":\"Помогайте редким животным с картой WWF\",\"multiCurrenc" +
        "ies\":\"false\",\"benefits\":[{\"icon\":{\"text\":\"0,75%\"},\"text\":\"От суммы ваших покупок пере" +
        "числим в WWF\"},{\"icon\":{\"name\":\"eco\"},\"text\":\"Эко-карта из возобновляемых матери" +
        "алов\"},{\"icon\":{\"text\":\"900₽\"},\"text\":\"Вернем на карту при взносе в WWF от 900₽\"}],\"hrefT" +
        "ariff\":\"/cards/credit-cards/wwf/\",\"bgColor\":\"#085053\"},{\"id\":\"platformCardsDebit" +
        "WWF\",\"programId\":\"WWFDebit\",\"order\":\"21\",\"type\":\"Current\",\"title\":\"WWF\",\"slogan\":\"По" +
        "могайте редким животным с картой WWF\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"te" +
        "xt\":\"0,75%\"},\"text\":\"От суммы ваших покупок перечислим в WWF\"},{\"icon\":{\"name\":\"eco\"},\"te" +
        "xt\":\"Эко-карта из возобновляемых материалов\"},{\"icon\":{\"text\":\"200₽\"},\"text\":\"Вернем на карту пр" +
        "и взносе в WWF от 900₽\"}],\"hrefTariff\":\"/cards/debit-cards/wwf/\",\"bgColor\":\"#085053\"},{\"id\":\"plat" +
        "formCardsDebitBlackS7\",\"programId\":\"S7BlackDebit\",\"product\":\"DS7B\",\"order\":\"22\",\"type\":\"Curre" +
        "nt\",\"title\":\"S7 Airlines\",\"slogan\":\"Двойные мили за покупки на s7.ru, закрытые распродажи и другие при" +
        "вилегии в S7 Priority\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"name\":\"Avia\"},\"text\":\"До" +
        " 4 миль за 60 рублей\"},{\"icon\":{\"name\":\"Percent\"},\"text\":\"Закрытые распродажи авиабилетов\"},{\"ic" +
        "on\":{\"name\":\"Star\"},\"text\":\"Доступ в бизнес-залы по всему миру\"}],\"hrefTariff\":\"/cards/debit-car" +
        "ds/s7-airlines/\",\"bgColor\":\"#010101\"},{\"id\":\"platformCardsDebitS7\",\"programId\":\"S7Debit\",\"pro" +
        "duct\":\"DS7W\",\"order\":\"22\",\"type\":\"Current\",\"title\":\"S7 Airlines\",\"slogan\":\"Двойные мили з" +
        "а покупки на s7.ru, закрытые распродажи и другие привилегии в S7 Priority\",\"multiCurrencies\":\"false\",\"b" +
        "enefits\":[{\"icon\":{\"name\":\"Avia\"},\"text\":\"До 3 миль за каждые 60 рублей\"},{\"icon\":{\"name\":\"Pe" +
        "rcent\"},\"text\":\"Закрытые распродажи авиабилетов\"},{\"icon\":{\"name\":\"Convert\"},\"text\":\"Выгодный к" +
        "урс конвертации валют\"}],\"hrefTariff\":\"/cards/debit-cards/s7-airlines/\",\"bgColor\":\"#a4b429\"},{\"i" +
        "d\":\"platformCardsCrebitBlackS7\",\"programId\":\"S7Black\",\"product\":\"S7B\",\"order\":\"23\",\"type\":\"Cr" +
        "edit\",\"title\":\"S7 Airlines\",\"slogan\":\"Двойные мили за покупки на s7.ru, закрытые распродажи и другие пр" +
        "ивилегии в S7 Priority\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"name\":\"Avia\"},\"text\":\"До 4 ми" +
        "ль за 60 рублей\"},{\"icon\":{\"name\":\"Percent\"},\"text\":\"Закрытые распродажи авиабилетов 2 раза в год\"},{\"ic" +
        "on\":{\"name\":\"Credit\"},\"text\":\"Кредитный лимит до 1500000 рублей\"}],\"hrefTariff\":\"/cards/credit-cards/s7-air" +
        "lines/\",\"bgColor\":\"#010101\"},{\"id\":\"platformCardsCrebitS7\",\"programId\":\"S7\",\"product\":\"S7W\",\"order\":\"2" +
        "4\",\"type\":\"Credit\",\"title\":\"S7 Airlines\",\"slogan\":\"Двойные мили за покупки на s7.ru, закрытые распродажи и друг" +
        "ие привилегии в S7 Priority\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"name\":\"Avia\"},\"tex" +
        "t\":\"До 3 миль за 60 рублей\"},{\"icon\":{\"name\":\"Percent\"},\"text\":\"Закрытые распродажи авиаби" +
        "летов 2 раза в год\"},{\"icon\":{\"name\":\"Credit\"},\"text\":\"Кредитный лимит до 700 000 руб" +
        "лей\"}],\"hrefTariff\":\"/cards/credit-cards/s7-airlines/\",\"bgColor\":\"#a4b429\"},{\"id\":\"platfor" +
        "mCardsCreditLeto\",\"programId\":\"LetoCard\",\"product\":\"LET\",\"order\":\"32\",\"type\":\"Credit\",\"tit" +
        "le\":\"Лето\",\"slogan\":\"TODO\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"text\":\"300K\"},\"te" +
        "xt\":\"Кредитный лимит до 300 000₽\"},{\"icon\":{\"text\":\"5%\"},\"text\":\"Возвращаем до 5% бонусами\"},{\"i" +
        "con\":{\"name\":\"AstroprogramId\"},\"text\":\"Меняйте бонусы на покупки в ТЦ \\\"ЛЕТО\\\"\"}],\"hrefTariff\":\"/car" +
        "ds/credit-cards/leto/\",\"bgColor\":\"#117331\"},{\"id\":\"platformCardsCreditPlaneta\",\"programId\":\"Planeta" +
        "Card\",\"product\":\"PLT\",\"order\":\"33\",\"type\":\"Credit\",\"title\":\"Планета\",\"slogan\":\"TODO\",\"mult" +
        "iCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"text\":\"300K\"},\"text\":\"Кредитный лимит до 300 000₽\"},{\"i" +
        "con\":{\"text\":\"5%\"},\"text\":\"Возвращаем до 5% бонусами\"},{\"icon\":{\"name\":\"AstroprogramId\"},\"text\":\"Ме" +
        "няйте бонусы на покупки в ТЦ \\\"ПЛАНЕТА\\\"\"}],\"hrefTariff\":\"/cards/credit-cards/planeta/\",\"bgCo" +
        "lor\":\"#f57c52\"},{\"id\":\"platformCardsCreditAura\",\"programId\":\"AuraCard\",\"product\":\"AUR\",\"ord" +
        "er\":\"30\",\"type\":\"Credit\",\"title\":\"Аура\",\"slogan\":\"TODO\",\"multiCurrencies\":\"false\",\"bene" +
        "fits\":[{\"icon\":{\"text\":\"300K\"},\"text\":\"Кредитный лимит до 300 000₽\"},{\"icon\":{\"text\":\"5%\"},\"tex" +
        "t\":\"Возвращаем до 5% бонусами\"},{\"icon\":{\"name\":\"AstroprogramId\"},\"text\":\"Меняйте бонусы на пок" +
        "упки в ТЦ \\\"АУРА\\\"\"}],\"hrefTariff\":\"/cards/credit-cards/aura/\",\"bgColor\":\"#e2231a\"},{\"id\":\"platfo" +
        "rmCardsCreditPerekrestok\",\"programId\":\"Perekrestok\",\"product\":\"PRT\",\"order\":\"34\",\"type\":\"Cre" +
        "dit\",\"title\":\"Перекресток\",\"slogan\":\"TODO\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"n" +
        "ame\":\"8_bonuses\"},\"text\":\"До 8 баллов за каждые 10₽ в Перекрёстке\"},{\"icon\":{\"name\":\"Delivery\"},\"te" +
        "xt\":\"Бесплатная доставка первые 3 месяца на Perekrestok.ru\"},{\"icon\":{\"name\":\"AstroprogramId\"},\"text\":\"Кре" +
        "дитный лимит до 700 000₽\"}],\"hrefTariff\":\"/cards/credit-cards/perekrestok/\",\"bgColor\":\"#004819\"},{\"id\":\"platformCard" +
        "sDebitPerekrestok\",\"programId\":\"PerekrestokDebit\",\"product\":\"PRT\",\"order\":\"28\",\"type\":\"Current\",\"title\":\"Пер" +
        "екресток\",\"slogan\":\"TODO\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"name\":\"6_bonuses\"},\"text\":\"до 6 бал" +
        "лов за каждые 10 ₽ в Перекрёстке\"},{\"icon\":{\"name\":\"Delivery\"},\"text\":\"Бесплатная доставка первые 3 месяца на Perekrest" +
        "ok.ru\"},{\"icon\":{\"name\":\"AstroprogramId\"},\"text\":\"4000 баллов в подарок за первую покупку по карте\"}],\"hrefTariff\":\"/car" +
        "ds/debit-cards/perekrestok/\",\"bgColor\":\"#004819\"},{\"id\":\"platformCardsCreditAzbukaVkusa\",\"programId\":\"AzbukaWorld\",\"pr" +
        "oduct\":\"AVW\",\"order\":\"30\",\"type\":\"Credit\",\"title\":\"Азбука Вкуса\",\"slogan\":\"TODO\",\"multiCurre" +
        "ncies\":\"false\",\"benefits\":[{\"icon\":{\"name\":\"6_bonuses\"},\"text\":\"До 6 бонусов за каждый рубль в Аз" +
        "буке Вкуса\"},{\"icon\":{\"name\":\"2_bonuses\"},\"text\":\"До 2 бонусов за каждый рубль на други" +
        "е покупки\"},{\"icon\":{\"name\":\"AstroprogramId\"},\"text\":\"Кредитный лимит до 70" +
        "0 0000₽\"}],\"hrefTariff\":\"/cards/credit-cards/azbukavkusa/\",\"bgColor\":\"#7979" +
        "79\"},{\"id\":\"platformCardsDebitAzbukaVkusa\",\"programId\":\"AzbukaWorldDebit\",\"pr" +
        "oduct\":\"AVW\",\"order\":\"30\",\"type\":\"Current\",\"title\":\"Азбука Вкуса\",\"slog" +
        "an\":\"TODO\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"name\":\"6_bonus" +
        "es\"},\"text\":\"до 6 бонусов за каждый рубль в Азбуке Вкуса\"},{\"icon\":{\"name\":\"1_ru" +
        "b\"},\"text\":\"1 рубль за каждый рубль на другие покупки\"},{\"icon\":{\"name\":\"Astropro" +
        "gramId\"},\"text\":\"До 20 000 бонусов в подарок  после активации во «Вкусомании»\"}],\"hrefTa" +
        "riff\":\"/cards/debit-cards/azbukavlusa/\",\"bgColor\":\"#797979\"},{\"id\":\"platformCardsCreditAzbukaV" +
        "kusaBE\",\"programId\":\"AzbukaBlack\",\"product\":\"AVB\",\"order\":\"29\",\"type\":\"Credit\",\"tit" +
        "le\":\"Азбука Вкуса\",\"slogan\":\"TODO\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"na" +
        "me\":\"6_bonuses\"},\"text\":\"До 6 бонусов за каждый рубль в Азбуке Вкуса\"},{\"icon\":{\"name\":\"2_bon" +
        "uses\"},\"text\":\"До 2 бонусов за каждый рубль на другие покупки\"},{\"icon\":{\"name\":\"Astroprog" +
        "ramId\"},\"text\":\"Кредитный лимит до 1 500 0000₽\"}],\"hrefTariff\":\"/cards/credit-cards/azbukavkus" +
        "abe/\",\"bgColor\":\"#212121\"},{\"id\":\"platformCardsDebitAzbukaVkusaBE\",\"programId\":\"AzbukaBla" +
        "ckDebit\",\"product\":\"AVB\",\"order\":\"29\",\"type\":\"Current\",\"title\":\"Азбука Вкуса\",\"slo" +
        "gan\":\"TODO\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"name\":\"6_bonuses\"},\"te" +
        "xt\":\"до 6 бонусов за каждый рубль в Азбуке Вкуса\"},{\"icon\":{\"name\":\"1_rub\"},\"text\":\"1 руб" +
        "ль за каждый рубль на другие покупки\"},{\"icon\":{\"name\":\"AstroprogramId\"},\"text\":\"Привиле" +
        "гии Club Platinum на весь период действия карты\"}],\"hrefTariff\":\"/cards/debit-cards/azbukavlu" +
        "sabe/\",\"bgColor\":\"#212121\"},{\"id\":\"platformCardsCreditDrive\",\"programId\":\"Drive\",\"prod" +
        "uct\":\"DRV\",\"order\":\"31\",\"type\":\"Credit\",\"title\":\"Tinkoff Drive\",\"slogan\":\"TODO\",\"mu" +
        "ltiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"name\":\"Gas_station\"},\"text\":\"10% за покупки н" +
        "а АЗС\"},{\"icon\":{\"name\":\"Car\"},\"text\":\"5% за любые автоуслуги, даже за оплату штрафов\"},{\"icon\":{\"na" +
        "me\":\"AstroprogramId\"},\"text\":\"Обменивайте баллы на покупки на АЗС и автоуслуги\"}],\"hrefTariff\":\"/cards/cre" +
        "dit-cards/drive/\",\"bgColor\":\"#1d1d1b\"},{\"id\":\"platformCardsDebitDrive\",\"programId\":\"DriveDebit\",\"prod" +
        "uct\":\"DRV\",\"order\":\"31\",\"type\":\"Current\",\"title\":\"Tinkoff Drive\",\"slogan\":\"TODO\",\"multiCurrenc" +
        "ies\":\"false\",\"benefits\":[{\"icon\":{\"name\":\"Gas_station\"},\"text\":\"10% за покупки на АЗС\"},{\"icon\":{\"n" +
        "ame\":\"Car\"},\"text\":\"5% за любые автоуслуги, даже оплату штрафов\"},{\"icon\":{\"name\":\"AstroprogramId\"},\"te" +
        "xt\":\"Обменивайте баллы на покупки на АЗС и автоуслуги\"}],\"hrefTariff\":\"/cards/debit-cards/drive/\",\"bgColor\":\"#1d1" +
        "d1b\"},{\"id\":\"platformCardsBlackBlackEdition\",\"programId\":\"48\",\"product\":\"DC\",\"order\":\"32\",\"type\":\"Curr" +
        "ent\",\"title\":\"Tinkoff Black\",\"slogan\":\"Ваши деньги\nработают всегда\",\"multiCurrencies\":\"true\",\"benefits\":[{\"i" +
        "con\":{\"text\":\"0%\"},\"text\":\"Наличные от 3000 ₽ без комиссии\"},{\"icon\":{\"text\":\"30%\"},\"text\":\"Кэшбэк в руб" +
        "лях до 30%\"},{\"icon\":{\"text\":\"6%\"},\"text\":\"До 6% годовых на остаток\"}],\"hrefT" +
        "ariff\":\"/cards/debit-cards/tinkoff-black/?card=debitBlackBlack\",\"bgColor\":\"#333333\"},{\"id\":\"platfor" +
        "mCardsDebitAllAirlinesBlackEdition\",\"programId\":\"50\",\"product\":\"DAA\",\"order\":\"33\",\"type\":\"Curr" +
        "ent\",\"title\":\"ALL Airlines\",\"slogan\":\"Премиальная карта\nдля путешествий\",\"multiCurrencies\":\"false\",\"bene" +
        "fits\":[{\"icon\":{\"text\":\"6%\"},\"text\":\"до 6% годовых на остаток милями\"},{\"icon\":{\"text\":\"30%\"},\"text\":\"Мили " +
        "за любые покупки\"},{\"icon\":{\"name\":\"Avia\"},\"text\":\"Бесплатные полеты любыми авиакомпаниями\"}],\"hrefTariff\":\"/cards/de" +
        "bit-cards/all-airlines/tariffs/?card=debitAllAirlinesBlack\",\"bgColor\":\"#333333\"},{\"id\":\"platformCardsCreditAllAirlinesBlack" +
        "Edition\",\"programId\":\"49\",\"product\":\"AA\",\"order\":\"34\",\"type\":\"Credit\",\"title\":\"ALL Airlines\",\"slogan\":\"Прем" +
        "иальная карта\nдля путешествий\",\"multiCurrencies\":\"false\",\"benefits\":[{\"icon\":{\"name\":\"Avia\"},\"text\":\"Бесплатные по" +
        "леты любыми авиакомпаниями\"},{\"icon\":{\"text\":\"30%\"},\"text\":\"Мили за любые покупки\"},{\"icon\":{\"text\":\"700K\"},\"te" +
        "xt\":\"Кредитный лимит до 700 000 ₽\"}],\"hrefTariff\":\"/cards/credit-cards/all-airlines/tariffs/?card=creditAllAirlinesBlack\",\"bg" +
        "Color\":\"#333333\"}]}}";

        #endregion 

        public async Task<ProductsResponceModel> LoadProducts()
        {
            return await SendRequest<ProductsResponceModel>("https://config.tinkoff.ru/resources?name=products_info");
        }

        private async Task<string> StartRequest(string address)
        {
            string httpResponseBody = "";
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var httpResponse = await httpClient.GetAsync(new Uri(address));
                    httpResponse.EnsureSuccessStatusCode();
                    httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                }
                catch
                {
                    httpResponseBody = "";
                }
            }
            return httpResponseBody;
        }

        private async Task<T> SendRequest<T>(string address)
            where T : IResponce, new()
        {
            var responce = await StartRequest(address);
            if (responce == null)
            {
                return new T()
                {
                    Success = false
                };
            }
            try
            {
                var responceDeserialize = await Task.Run(() => Serializer.Deserialize<T>(responce));
                return responceDeserialize;
            }
            catch
            {
                return new T()
                {
                    Success = false
                };
            }
        }
    }

    internal static class Serializer
    {
        internal static T Deserialize<T>(string json)
        {
            var res = JsonConvert.DeserializeObject<T>(json);
            return res;
        }
    }    
}
