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
       stage ('Approve Operation') {
            steps {
                timeout(time: 1, unit: 'DAYS') {
                    input message: 'Do you want to proceed to deploy?'
                }
            }
        }
        stage ('Copy Build Artifacts') {
            steps {
                copyArtifacts(projectName: 'MES COLL - Build and Package');
            }
        }
        stage ('Unzip artifacts') {
            steps {
                bat "echo Unzip artifacts..."
                unzip zipFile: "$webZipFile", dir: 'mes-web'
                unzip zipFile: "$EBRZipFile", dir: 'ebr-api'
                unzip zipFile: "$MESZipFile", dir: 'mes-api'
            }
        }
        stage ('Deploy MES APi') {
            steps {
                bat 'echo Deploy...'
                bat 'RMDIR \\\\devserv02\\c$\\inetpub\\webapps\\mes-ws /S /Q'
                bat 'MKDIR \\\\devserv02\\c$\\inetpub\\webapps\\mes-ws'
                bat 'xcopy mes-api\\*.* \\\\devserv02\\c$\\inetpub\\webapps\\mes-ws\\ /Y /E'
                script {
                    def response = httpRequest 'http://mes.api.ff.test'
                    println("Status: "+response.status)
                    println("Content: "+response.content)
                }
            }   
        }
        stage ('Deploy EBR APi') {
            steps {
                bat 'echo Deploy...'
                bat 'RMDIR \\\\devserv02\\c$\\inetpub\\webapps\\ebr-ws /S /Q'
                bat 'MKDIR \\\\devserv02\\c$\\inetpub\\webapps\\ebr-ws'                
                bat 'xcopy ebr-api\\*.* \\\\devserv02\\c$\\inetpub\\webapps\\ebr-ws\\ /Y /E'
                script {
                    def response = httpRequest 'http://ebr.api.ff.test'
                    println("Status: "+response.status)
                    println("Content: "+response.content)
                }                
            }   
        }
        stage ('Deploy MES web') {
            steps {
                bat 'echo Deploy...'
                bat 'RMDIR \\\\devserv02\\c$\\inetpub\\webapps\\mes /S /Q'
                bat 'MKDIR \\\\devserv02\\c$\\inetpub\\webapps\\mes'                  
                bat 'xcopy mes-web\\*.* \\\\devserv02\\c$\\inetpub\\webapps\\mes\\ /Y /E'
                script {
                    def response = httpRequest 'http://mes.ff.test'
                    println("Status: "+response.status)
                    println("Content: "+response.content)
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