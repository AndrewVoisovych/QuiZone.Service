namespace QuiZone.BusinessLogic.Utils.Email
{ 
    public static class EmailBody
    {
 
        public static class Registration
        {
           

            public static string GetBodyMessage(string userName, string login, string confirmEmailLink)
            {
                return
                    $"Привіт, {userName}. <br> " +
                    $"Тобі пише сайт -  QuiZone. <br>" +
                    $" Твій логін: <b>{login}</b> <br> " +
                    $"Підтверди свою пошту: " +
                    $"<a href=\"{confirmEmailLink}\">Підтвердити<a> <br>" +
                    $"Гарного дня :)";
            }
        }
    }
}
