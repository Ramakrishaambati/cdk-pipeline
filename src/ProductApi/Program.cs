using Amazon.CDK;

namespace ProductApi
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new ProductApiStack(app, "ProductApiStack", new StackProps
            {
            });
            app.Synth();
        }
    }
}
