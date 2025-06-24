using Amazon.CDK;
using Constructs;
using Amazon.CDK.Pipelines;

namespace TestPipeline
{
    public class TestPipelineStack : Stack
    {
        internal TestPipelineStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var pipeline = new CodePipeline(this, "pipeline", new CodePipelineProps
            {
                PipelineName = "MyPipeline",
                Synth = new ShellStep("Synth", new ShellStepProps
                {
                    Input = CodePipelineSource.GitHub("ramakrishna/cdk-pipeline", "main"),
                    Commands = new string[] { "npm install -g aws-cdk", "cdk synth" }
                })
            });
        }
    }
}
