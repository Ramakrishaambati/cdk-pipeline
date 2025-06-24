using Amazon.CDK;
using Constructs;

namespace Ec2InstanceStgServer
{
    public class Ec2InstanceStgServerStack : Stack
    {
        internal Ec2InstanceStgServerStack(Construct scope, string id, IStackProps props = null) : base(scope, id,
            props)
        {
            var vpcStack = new VpcStack(this, "VpcStgStack");
            var ec2Stack = new Ec2Stack(this, "Ec2StgStack", vpcStack.VPC);

            Output("instance-id", ec2Stack.Ec2Instance.InstanceId);
            
            
            if (ec2Stack.KeyPairPrivateKey is not null)
            {
                var ssmParameterName = ec2Stack.KeyPairPrivateKey.ParameterName;
                var keyPairFileName = $"{ec2Stack.KeyPairName}.pem";
                
                Output("Download key command",
                    $"aws ssm get-parameter --name {ssmParameterName} --query Parameter.Value --with-decryption --output text > {keyPairFileName} && chmod 400 {keyPairFileName}");
            }

            Output("Open tunnel command", $"aws ec2-instance-connect open-tunnel --instance-id {ec2Stack.Ec2Instance.InstanceId} --local-port 1234");
        }

        private void Output(string name, string value)
        {
            new CfnOutput(this, name, new CfnOutputProps { Value = value });
        }
    }
}