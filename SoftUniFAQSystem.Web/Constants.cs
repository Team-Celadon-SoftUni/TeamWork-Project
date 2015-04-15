namespace SoftUniFAQSystem.Web
{
    public static class Constants
    {
        public const string NoSuchQuestion = "Couldn't find question with such id. Please try again.";
        public const string NoSuchAnswer = "Couldn't find answer with such id. Please try again.";
        public const string NoSuchUser = "Couldn't find user with such id. Please try again.";
        public const string NotLoggedOn = "You are not logged in. Please log in.";
        public const string BestAnswersLimitReached = "Invalid operation. The limit(2) for best answers per question has already been reached.";
        public const string CannotClose = "This question cannot be close. It must have at least one answer marked as best!";
    }
}