using Amazon.CDK;
using Constructs;

namespace Ec2InstanceProdServer
{
    public class Ec2InstanceProdServerStack : Stack
    {
        internal Ec2InstanceProdServerStack(Construct scope, string id, IStackProps props = null) : base(scope, id,
            props)
        {
            var vpcStack = new VpcStack(this, "VpcProdStack");
            var ec2Stack = new Ec2Stack(this, "Ec2ProdStack", vpcStack.VPC);

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