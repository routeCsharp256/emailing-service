using EmailingService.Models;

namespace EmailingService.Extensions
{
    public static class TemplatePool
    {
        public static readonly EmailMessageTemplate Hiring = new(
            $"{EmailMessageTemplate.NamePlaceholder}, Добро пожаловать в команду!",
            $"<h1>Ура-ура!</h1> <p>{EmailMessageTemplate.NamePlaceholder}, поздравляем с устройством в Ozon!</p>");

        public static readonly EmailMessageTemplate Dismissal = new(
            "Грустная весть",
            $"<h1>Увольнение</h1> <p>{EmailMessageTemplate.NamePlaceholder}, обратите внимание на следующую инфрмацию. Вы :uvolen:. Успехов в будущих проектах!</p>");

        public static readonly EmailMessageTemplate ProbationPeriodEnding = new(
            "Счастливый день!",
            $"<p>{EmailMessageTemplate.NamePlaceholder}, мы рады поздравить Вас с успешным прохождением испытательного срока! Теперь Вам придётся работать в два раза больше, ура!</p>");

        public static readonly EmailMessageTemplate ConferenceAttendance = new(
            "Участие в концеренции",
            $"<p>{EmailMessageTemplate.NamePlaceholder}, Вы направлены на участие в конференции, будьте готовы к тому, чтобы внимательно прослушать все доклады.</p>");

        public static class MerchDelivery
        {
            public static readonly EmailMessageTemplate WelcomePack = new(
                "Для вас подготовлен мерч",
                $"<p>{EmailMessageTemplate.NamePlaceholder}, Вам необходимо подойти на 30 этаж и забрать подготовленный набор мерча. Сегодня вы получите welcome pack из носочков и футболочки!</p>");

            public static readonly EmailMessageTemplate ProbationPeriodEndingPack = new(
                "Для вас подготовлен мерч",
                $"<p>{EmailMessageTemplate.NamePlaceholder}, ещё раз поздравляем с успешным прохождением испытательного срока! Вам необходимо подойти на 30 этаж и забрать подготовленный набор мерча. Сегодня вы получите фирменную толстовочку и рюкзачок!</p>");

            public static readonly EmailMessageTemplate ConferenceListenerPack = new(
                "Для вас подготовлен мерч",
                $"<p>{EmailMessageTemplate.NamePlaceholder}, вам необходимо подойти на 30 этаж и забрать подготовленный набор мерча для участия в конференции в качестве слушателя. Чтобы выглядеть на все сто, мы дадим Вам супер классную толстовочку!</p>");
            
            public static readonly EmailMessageTemplate ConferenceSpeakerPack = new(
                "Для вас подготовлен мерч",
                $"<p>{EmailMessageTemplate.NamePlaceholder}, вам необходимо подойти на 30 этаж и забрать подготовленный набор мерча для участия в конференции в качестве спикера. Чтобы выглядеть на все сто, мы дадим Вам супер классную толстовочку и кепку!</p>");
            
            public static readonly EmailMessageTemplate VeteranPack = new(
                "Для вас подготовлен мерч",
                $"<p>{EmailMessageTemplate.NamePlaceholder}, вам необходимо подойти на 30 этаж и забрать подготовленный набор мерча за выслугу лет. Спасибо, с Вами очень приятно работать! Мы хотим отблагодарить Вас фирменным костюмом!</p>");
        }
    }
}