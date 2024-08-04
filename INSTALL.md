## Table of Contents

1. [Step-01: Prerequisites](#step-01-prerequisites)
2. [Step-02: Deploy CloudFormation Stack for Code](#step-02-deploy-cloudformation-stack-for-code)
3. [Step-03: Create AWS IAM User for Pipeline](#step-03-create-aws-iam-user-for-pipeline)
4. [Step-04: Add Secrets to GitHub Secrets](#step-04-add-secrets-to-github-secrets)
5. [Step-05: Delete CloudFormation Stack](#step-05-delete-cloudformation-stack)
6. [Step-06: Delete AWS IAM User for Pipeline](#step-06-delete-aws-iam-user-for-pipeline)


## Step-01: Prerequisites

Before proceeding, ensure that you have the AWS CLI and jq installed on your local machine.

#### Install AWS CLI

Follow the instructions provided by AWS to install the AWS CLI on your operating
system: [Install the AWS CLI](https://docs.aws.amazon.com/cli/latest/userguide/getting-started-install.html).

After installing the AWS CLI, configure it using:

```bash
aws configure
```

#### Install jq

Follow the instructions to install jq on your operating system from the official jq
website: [Install jq](https://github.com/jqlang/jq).

## Step-02: Deploy CloudFormation Stack for Code

```bash
export S3_BUCKET_PREFIX=my-name-is-pepe
export STACK_NAME=aws-dotnet-serverless-stack

aws cloudformation deploy \
    --stack-name $STACK_NAME \
    --template-file etc/cloudformation-template.yaml \
    --parameter-overrides S3BucketPrefix=$S3_BUCKET_PREFIX \
    --capabilities CAPABILITY_NAMED_IAM \
    --region us-east-1
```

## Step-03: Create AWS IAM User for Pipeline

```bash  
# Set user name
export USER_NAME="dotnetpipeline"

# Create IAM user
aws iam create-user --user-name $USER_NAME

# Create access keys for the user
ACCESS_KEYS=$(aws iam create-access-key --user-name $USER_NAME)

# Extract AccessKeyId and SecretAccessKey
ACCESS_KEY_ID=$(echo $ACCESS_KEYS | jq -r '.AccessKey.AccessKeyId')
SECRET_ACCESS_KEY=$(echo $ACCESS_KEYS | jq -r '.AccessKey.SecretAccessKey')

# Attach necessary policies
# ⚠️ THIS IS NOT PRODUCTION READY - USE A LEAST PRIVILEGE ROLE INSTEAD
aws iam attach-user-policy --user-name $USER_NAME --policy-arn arn:aws:iam::aws:policy/AmazonS3FullAccess
aws iam attach-user-policy --user-name $USER_NAME --policy-arn arn:aws:iam::aws:policy/AWSLambda_FullAccess

# ⚠️ Output the Access Key details
echo "Access Key ID: $ACCESS_KEY_ID"
echo "Secret Access Key: $SECRET_ACCESS_KEY"
```

### Step-04: Add Secrets to GitHub Secrets

After creating the IAM user and generating the access keys, follow these steps to add these credentials to your GitHub
repository secrets for use in GitHub Actions:

1. **Navigate to Your GitHub Repository**:
    - Go to the main page of your repository on GitHub.

2. **Access Repository Settings**:
    - Click on the **Settings** tab at the top of the repository page.

3. **Navigate to Secrets**:
    - In the left sidebar, click on **Secrets and variables** > **Actions**.

4. **Add New Repository Secret**:
    - Click the **New repository secret** button.

5. **Add Secrets**:
    - **AWS_ACCESS_KEY_ID**:
        - **Name**: `AWS_ACCESS_KEY_ID`
        - **Value**: Enter the `AccessKeyId` value you obtained.
        - Click **Add secret**.
    - **AWS_SECRET_ACCESS_KEY**:
        - **Name**: `AWS_SECRET_ACCESS_KEY`
        - **Value**: Enter the `SecretAccessKey` value you obtained.
        - Click **Add secret**.
    - **S3_BUCKET_PREFIX**:
        - **Name**: `S3_BUCKET_PREFIX`
        - **Value**: Enter your S3 bucket prefix (e.g., `my-name-is-pepe`).
        - Click **Add secret**.

By following these steps, you'll securely add the necessary credentials to your GitHub repository for use in your GitHub
Actions workflow.

## Step-05: Delete CloudFormation Stack

To delete the CloudFormation stack, use the following command:

```bash
# Set the stack name
export STACK_NAME="aws-dotnet-serverless-stack"

# Delete the CloudFormation stack
aws cloudformation delete-stack --stack-name $STACK_NAME

# Wait for the stack to be deleted
aws cloudformation wait stack-delete-complete --stack-name $STACK_NAME
```

## Step-06: Delete AWS IAM User for Pipeline

To delete the IAM user created for the pipeline, use the following commands:

```bash
# Delete the IAM user (this will also delete the associated access keys and policies)
aws iam delete-user --user-name $USER_NAME
```