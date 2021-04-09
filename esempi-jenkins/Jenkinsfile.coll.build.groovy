def getMESZipFile() {
    return "mes-api.zip"
}
def getWebZipFile() {
    return "mes-web.zip"
}
def getEBRZipFile() {
    return "ebr-api.zip"
}
def getBrokerSRVZipFile() {
    return "mes-srv-broker.zip"
}
def getSQLSRVZipFile() {
    return "mes-srv-sql.zip"
}
def getXMLSRVZipFile() {
    return "mes-srv-xml.zip"
}

pipeline {
   agent any

    stages {
        stage('Checkout Branch #coll#') {
            steps {
                git branch: 'coll',
                    credentialsId: 'GitLab-FF-User',
                    url: 'https://gitlab.com/finefoods/mes.git'
    
                bat "dir"
            }
        }
        stage('Write Build file') {
            steps {
               writeFile file: 'BuildInfo.md', text: """# Build Informations
               - Build number: ${BUILD_NUMBER}
               - Built by: Jenkins - ${JOB_NAME}
               - Built at: ${new Date().format( 'yyyy-MM-dd HH:mm:ss' )}
               - Git commit: ${env.GIT_COMMIT}
               """
            }
        }    
        stage('Nuget Restore') {
          steps {
            bat "c:\\Bin\\NuGet.exe restore projects\\backend\\MES.sln -MSBuildPath \"${tool 'MSBuild-amd64'}\" -Source http://nexus.svc.ff.prod:8081/repository/nuget-group/"
          }
        }
        stage('Copy default configs') {
          steps {
            bat "copy projects\\backend\\MESApi\\Web.Default.config projects\\backend\\MESApi\\Web.config"
            bat "copy projects\\backend\\MESBatchJobs\\App.Default.config projects\\backend\\MESBatchJobs\\App.config"
            bat "copy projects\\backend\\MESBrokerCore\\App.Sample.config projects\\backend\\MESBrokerCore\\app.config"
            bat "copy projects\\backend\\MESBrokerService\\App.Sample.config projects\\backend\\MESBrokerService\\app.config"
            bat "copy projects\\backend\\MESCore\\App.Sample.config projects\\backend\\MESCore\\app.config"
            bat "copy projects\\backend\\MESLib\\App.Sample.config projects\\backend\\MESLib\\app.config"
            bat "copy projects\\backend\\MESPalletsCore\\App.Sample.config projects\\backend\\MESPalletsCore\\app.config"
            bat "copy projects\\backend\\MESPalletsSQL\\App.Default.config projects\\backend\\MESPalletsSQL\\App.config"
            bat "copy projects\\backend\\MESPalletsXML\\App.Default.config projects\\backend\\MESPalletsXML\\App.config"
            bat "copy projects\\backend\\MESAXWSClient\\app.Sample.config projects\\backend\\MESAXWSClient\\app.config"
            bat "copy projects\\backend\\MESSCADARESTClient\\app.Sample.config projects\\backend\\MESSCADARESTClient\\app.config"
            bat "copy projects\\backend\\MESUnitTests\\app.Default.config projects\\backend\\MESUnitTests\\app.config"
            bat "copy projects\\backend\\EBRApi\\Web.Default.config projects\\backend\\EBRApi\\Web.config"
            bat "copy projects\\backend\\EBRFragments\\Web.Default.config projects\\backend\\EBRFragments\\Web.config"
            bat "copy projects\\backend\\EBRLib\\App.Sample.config projects\\backend\\EBRLib\\App.config"
          }
        }        
        stage('Build Solution') {
          steps {
            bat "\"${tool 'MSBuild-amd64'}\\MSBuild.exe\" projects\\backend\\MES.sln"
          }
        }
        stage('Package Frontends, APIs & Services') {
            parallel {
                stage('Package Angular MES') {
                    steps {
                        bat "echo \"Build Angular MES\""
                        bat "npm config set strict-ssl false"
                        dir("${env.WORKSPACE}\\projects\\frontend\\ng-mes"){
                            bat "npm install"
                            bat "npm run ng -- build --configuration=coll"
                        }
                        bat "del $webZipFile"
                        bat "copy BuildInfo.md projects\\frontend\\ng-mes\\dist\\ng-mes\\BuildInfo.md"
                        zip zipFile: "$webZipFile", archive: true, dir: 'projects\\frontend\\ng-mes\\dist\\ng-mes'
                    }
                }            
                stage('Package EBR Api') {
                    steps {
                        bat "echo \"Publish EBRAPI\""
                        bat "\"${tool 'MSBuild-amd64'}\\MSBuild.exe\" projects\\backend\\EBRApi\\EBRApi.csproj /p:DeployOnBuild=true /p:PublishProfile=FolderProfile"
                        bat "del $EBRZipFile"
                        bat "copy BuildInfo.md projects\\backend\\EBRApi\\bin\\Release\\Publish\\BuildInfo.md"
                        zip zipFile: "$EBRZipFile", archive: true, dir: 'projects\\backend\\EBRApi\\bin\\Release\\Publish'
                    }
                }
                stage('Package MES Api') {
                    steps {
                        bat "echo \"Publish MESApi\""
                        bat "\"${tool 'MSBuild-amd64'}\\MSBuild.exe\" projects\\backend\\MESApi\\MESApi.csproj /p:DeployOnBuild=true /p:PublishProfile=FolderProfile"
                        bat "del $MESZipFile"
                        bat "copy BuildInfo.md projects\\backend\\MESApi\\bin\\Release\\Publish\\BuildInfo.md"
                        zip zipFile: "$MESZipFile", archive: true, dir: 'projects\\backend\\MESApi\\bin\\Release\\Publish'
                    }
                }
                stage('Package MES Broker Service') {
                    steps {
                        bat "echo \"Publish MESBrokerService\""
                        bat "del $brokerSRVZipFile"
                        bat "copy BuildInfo.md projects\\backend\\MESBrokerService\\bin\\Debug\\BuildInfo.md"
                        zip zipFile: "$brokerSRVZipFile", archive: true, dir: 'projects\\backend\\MESBrokerService\\bin\\Debug'
                    }
                }   
                stage('Package MES Pallets SQL') {
                    steps {
                        bat "echo \"Publish MESPalletsSQL\""
                        bat "del $SQLSRVZipFile"
                        bat "copy BuildInfo.md projects\\backend\\MESPalletsSQL\\bin\\Debug\\BuildInfo.md"
                        zip zipFile: "$SQLSRVZipFile", archive: true, dir: 'projects\\backend\\MESPalletsSQL\\bin\\Debug'
                    }
                }
                stage('Package MES Pallets XML') {
                    steps {
                        bat "echo \"Publish MESPalletsXML\""
                        bat "del $XMLSRVZipFile"
                        bat "copy BuildInfo.md projects\\backend\\MESPalletsXML\\bin\\Debug\\BuildInfo.md"
                        zip zipFile: "$XMLSRVZipFile", archive: true, dir: 'projects\\backend\\MESPalletsXML\\bin\\Debug'
                    }
                }            
            }        
        }
    }
    post {
        success {
            emailext (
                subject: "Job '${env.JOB_NAME} ${env.BUILD_NUMBER}'",
                body: """<p>Check console output at <a href="${env.BUILD_URL}">${env.JOB_NAME}</a></p>
                <h2>Build Informations</h2>
                <ul>
                    <li>Build number: ${BUILD_NUMBER}</li>
                    <li>Built by: Jenkins - ${JOB_NAME}</li>
                    <li>Built at: ${new Date().format( 'yyyy-MM-dd HH:mm:ss' )}</li>
                    <li>Git commit: ${env.GIT_COMMIT}</li>
                </ul>""",
                to: "andrea@colleoni.info",
                from: "jenkins@finefoods.it"
            )
        }
        always {
            cleanWs()
        }
    }
}
