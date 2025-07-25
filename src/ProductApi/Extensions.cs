using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.IAM;

namespace ProductApi
{
  public static class Extensions
  {
    public static void GrantDescribeTable(this ITable table, IGrantable grantee) =>
      table.Grant(grantee, "dynamodb:DescribeTable");
  }
}
