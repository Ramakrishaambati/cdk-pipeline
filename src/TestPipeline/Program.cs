using Amazon.CDK;

namespace TestPipeline
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new TestPipelineStack(app, "TestPipelineStack", new StackProps
            {
                Env = new Amazon.CDK.Environment {
                Account = "597011385066", Region = "us-east-1" }
            });
            app.Synth();
        }
    }
}
