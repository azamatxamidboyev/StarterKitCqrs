namespace PracticeWithCqrs.Middleware
{
    public class GlobalExeptionMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly ILogger<GlobalExeptionMiddleware> _logger;
        public GlobalExeptionMiddleware(RequestDelegate request, ILogger<GlobalExeptionMiddleware> logger)
        {
            _logger = logger;
            _request = request;
        }
        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _request(context);    

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "xatolik sodir boldi");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                var response = new
                {
                    Massage = "kutilmagan xatolik sodir boldi",
                    detail = ex.Message
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }

    }
}
