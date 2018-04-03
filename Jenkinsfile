pipeline {
  agent {
    label 'windows && vs-15'
  }
  stages {
    stage('Build') {
      steps {
        bat 'nuget restore'
        bat 'msbuild AuraSDK.sln /t:Build /p:Configuration=Release'
      }
    }
    stage('Test') {
      steps {
        vsTest(testFiles: 'AuraSDKTests\\bin\\Release\\AuraSDKTests.dll', enablecodecoverage: true)
      }
    }
    stage('Archive') {
      steps {
        archiveArtifacts 'AuraSDK\\bin\\Release\\AuraSDK.dll'
      }
    }
    stage('Clean') {
      steps {
        cleanWs(cleanWhenAborted: true, cleanWhenFailure: true, cleanWhenNotBuilt: true, cleanWhenSuccess: true, cleanWhenUnstable: true, deleteDirs: true)
      }
    }
  }
}