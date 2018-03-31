pipeline {
  agent {label 'windows||vs-15'}
  stages {
    stage('Build') {
      steps {
        bat 'msbuild AuraSDK\\AuraSDK.csproj /t:Build /p:Configuration=Release'
      }
    }
    stage('Archive') {
      steps {
        archiveArtifacts 'AuraSDK\\bin\\Release\\AuraSDK.dll'
      }
    }
    stage('Clean') {
      steps {
        cleanWs(cleanWhenAborted: true, cleanWhenFailure: true, cleanWhenNotBuilt: true, cleanWhenSuccess: true, cleanWhenUnstable: true, cleanupMatrixParent: true, deleteDirs: true)
      }
    }
  }
}
