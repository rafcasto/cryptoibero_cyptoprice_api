pipeline {
     agent { docker 
        { 
            image 'rafcasto/nodejs-build'
            args '-e GIT_TOKEN=$GIT_TOKEN -v /var/run/docker.sock:/var/run/docker.sock:rw -v /usr/bin/docker:/usr/bin/docker:rw -v /usr/local/bin/kubectl:/usr/local/bin/kubectl  -v /var/lib/jenkins/kube/kubeconfig.yml:/home/node/kube/kubeconfig.yml' 
        } 
     }
     environment {
        registry = "rafcasto/crypto-ibero-api"
        registryCredential = 'rafcasto-dockerhub-crls'
        dockerImage = ''
    }
    stages {
        stage('Build image') {
            steps {
                script {
                    dockerImage = docker.build registry
                }
            }
        }
         stage('Push image') {
            steps {
              script {
                docker.withRegistry( '', registryCredential ) {
                        dockerImage.push()
                    }
                }
                catchError(buildResult: 'SUCCESS', stageResult: 'FAILURE'){
                    sh 'docker rmi $(docker images)'
                }
            }
        }

        stage('deploy'){
            steps {
                catchError(buildResult: 'SUCCESS', stageResult: 'FAILURE'){
                   sh 'kubectl delete svc cryptoibero-svc -n cryptoibero --kubeconfig=/home/node/kube/kubeconfig.yml'
                   sh 'kubectl delete -n cryptoibero  deployment cryptoibero-dep --kubeconfig=/home/node/kube/kubeconfig.yml'
                }
                catchError(buildResult: 'SUCCESS', stageResult: 'FAILURE'){
                   sh 'kubectl create namespace cryptoibero --kubeconfig=/home/node/kube/kubeconfig.yml'
                }
                sh 'kubectl apply -f cryptoiberoapi-deployment.yaml -n cryptoibero --kubeconfig=/home/node/kube/kubeconfig.yml'
                sh 'kubectl apply -f cryptoiberoapi-service.yaml -n cryptoibero --kubeconfig=/home/node/kube/kubeconfig.yml'
            }
        }

    }
    post {
        // Clean after build
        always {
            //cleanWs()
            deleteDir()
        }
    }
    
}
