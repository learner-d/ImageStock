namespace ImageStock.Data.Models
{
    public class ErrorInfo
    {
        public int StatusCode { get; private set; }
        public string ImgUrl { get; private set; }
        public string Message { get; private set; }
        public string Description { get; private set; }

        public static ErrorInfo FromStatusCode(int statusCode)
        {
            ErrorInfo errorInfo = new ErrorInfo {StatusCode = statusCode};
            switch (statusCode)
            {
                case 404:
                    errorInfo.ImgUrl = "/img/ufo2_404.png";
                    errorInfo.Message = "Сторінку не знайдено!";
                    errorInfo.Description = "Схоже ви звернулися, не за адресою";
                    break;
                case 500:
                    errorInfo.ImgUrl = "/img/fire_500.png";
                    errorInfo.Message = "Збій серверу!";
                    errorInfo.Description = "Сьогодні адмін точно засмутиться";
                    break;
                default:
                    errorInfo.ImgUrl = "/img/fire_500.png";
                    errorInfo.Message = "Невідома помика";
                    errorInfo.Description = "Якби ми знали що це таке, а так ми не знаємо, що це таке...";
                    break;
            }

            return errorInfo;
        }
    }
}