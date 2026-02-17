namespace Learning_ASP_Dot_Net.Middlewares
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync (HttpContext context)
        {
             Console.WriteLine("Custom Middleware Executed");

            await _next(context);
            
            Console.WriteLine("Custom Middleware Executed After Request");
        }
    }
}
