pipeline {
    agent any
    
    stages {
        stage ('GIT checkout') {
            steps {
                bat echo "Check out"
                git branch: 'main', 
                    url: 'https://github.com/andrea-colleoni/thales-appsec.git', 
                    credentialsId: 'github-andrea'
            }
        }
    }
}