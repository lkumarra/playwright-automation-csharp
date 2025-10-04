pipeline {
  agent any

  environment {
    IMAGE_NAME = "playwright-tests:latest"
  }

  stages {
    stage('Build Docker image') {
      steps {
        script {
          sh 'docker build -t ${IMAGE_NAME} .'
        }
      }
    }

    stage('Run tests in container') {
      steps {
        script {
          // By default UI tests are disabled. To enable them set PLAYWRIGHT_RUN_UI=1
          sh 'docker run --rm -e PLAYWRIGHT_RUN_UI=1 ${IMAGE_NAME}'
        }
      }
    }

    stage('Collect results') {
      steps {
        // Copy out the TestResults folder from a temporary container run if needed
        // This example mounts a workspace dir to extract results. Adjust as Jenkins workspace path if required.
        script {
          sh 'mkdir -p test-output || true'
          sh 'docker create --name tmp_extract ${IMAGE_NAME} || true'
          sh 'docker cp tmp_extract:/src/PlaywrightTests/TestResults ./test-output || true'
          sh 'docker rm -f tmp_extract || true'
        }
        archiveArtifacts artifacts: 'test-output/**', allowEmptyArchive: true
      }
    }
  }

  post {
    always {
      echo 'Pipeline finished.'
    }
  }
}
