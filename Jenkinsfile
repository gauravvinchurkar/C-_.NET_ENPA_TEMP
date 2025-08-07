pipeline {
    agent any

    environment {
        DOTNET_ROOT = tool name: 'dotnet-sdk'
        REMOTE_USER = 'ec2-user'  // Default AWS Linux user
        REMOTE_HOST = 'your.aws.ec2.ip'  // Update with your EC2 public IP
        API_REMOTE_PATH = '/var/www/sampleapi'
        WEB_REMOTE_PATH = '/var/www/sampleweb'
        SSH_KEY_CREDENTIALS_ID = 'aws-ssh-key'  // Jenkins credentials ID for SSH key
        API_SERVICE = 'sampleapi.service'
        WEB_SERVICE = 'sampleweb.service'
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
                script {
                    // Save current commit SHA for rollback
                    currentCommit = sh(returnStdout: true, script: "git rev-parse HEAD").trim()
                    echo "Building commit: ${currentCommit}"
                }
            }
        }

        stage('Build & Test') {
            parallel {
                stage('Build API') {
                    steps {
                        dir('API') {
                            sh "${env.DOTNET_ROOT}/dotnet restore"
                            sh "${env.DOTNET_ROOT}/dotnet build -c Release --no-restore"
                            sh "${env.DOTNET_ROOT}/dotnet test --no-build --verbosity normal"
                        }
                    }
                }
                stage('Build Web') {
                    steps {
                        dir('Web') {
                            sh "${env.DOTNET_ROOT}/dotnet restore"
                            sh "${env.DOTNET_ROOT}/dotnet build -c Release --no-restore"
                            sh "${env.DOTNET_ROOT}/dotnet test --no-build --verbosity normal"
                        }
                    }
                }
            }
        }

        stage('Publish') {
            steps {
                dir('API') {
                    sh "${env.DOTNET_ROOT}/dotnet publish -c Release -o ./publish --no-build"
                    archiveArtifacts artifacts: 'publish/**', fingerprint: true
                }
                dir('Web') {
                    sh "${env.DOTNET_ROOT}/dotnet publish -c Release -o ./publish --no-build"
                    archiveArtifacts artifacts: 'publish/**', fingerprint: true
                }
            }
        }

        stage('Deploy') {
            steps {
                script {
                    // Deploy API
                    sshagent(credentials: [env.SSH_KEY_CREDENTIALS_ID]) {
                        // Stop services
                        sh "ssh -o StrictHostKeyChecking=no ${env.REMOTE_USER}@${env.REMOTE_HOST} 'sudo systemctl stop ${env.API_SERVICE} || true'"
                        
                        // Copy files
                        sh "scp -o StrictHostKeyChecking=no -r API/publish/* ${env.REMOTE_USER}@${env.REMOTE_HOST}:${env.API_REMOTE_PATH}/"
                        
                        // Set permissions
                        sh "ssh -o StrictHostKeyChecking=no ${env.REMOTE_USER}@${env.REMOTE_HOST} 'chmod +x ${env.API_REMOTE_PATH}/SampleAPI'"
                        
                        // Start service
                        sh "ssh -o StrictHostKeyChecking=no ${env.REMOTE_USER}@${env.REMOTE_HOST} 'sudo systemctl start ${env.API_SERVICE}'"
                        
                        // Verify service status
                        sh "ssh -o StrictHostKeyChecking=no ${env.REMOTE_USER}@${env.REMOTE_HOST} 'sudo systemctl status ${env.API_SERVICE}'"
                    }
                }
            }
        }
    }

    post {
        failure {
            echo 'Pipeline failed! Check the logs above for details.'
            // Notification can be added here (Slack, Email, etc.)
        }
        success {
            echo 'Pipeline completed successfully!'
            // Add success notifications if needed
        }
    }
}