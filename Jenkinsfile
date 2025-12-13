pipeline {
    agent any

    tools {
        dotnet 'DotNet-8.0'
    }

    options {
        skipDefaultCheckout(true)
        timestamps()
    }

    stages {

        stage('Checkout') {
            steps {
                checkout scmGit(
                    branches: [
                        [name: '*/release/v*.*.*'], 
                        [name: '*/v*.*.*']
                    ],
                    extensions: [],
                    userRemoteConfigs: [
                        [
                            credentialsId: 'dbda54b3-5b5d-43e9-b8d6-8a78a9c7a194',
                            url: 'https://github.com/hallowslab/ICSModMenu.git'
                        ]
                    ]
                )
            }
        }

        stage('Restore') {
            steps {
                dotnetRestore project: 'ICSModMenu.csproj', sdk: '8.0'
            }
        }

        stage('Test Debug') {
            steps {
                dotnetBuild configuration: 'Debug', project: 'ICSModMenu.csproj', properties: [CI: 'true', UseStubs: 'true'], sdk: '8.0'
            }
        }

        stage('Build Debug') {
            when {
                tag "v*.*.*"
            }
            steps {
                dotnetBuild project: 'ICSModMenu.csproj', configuration: 'Debug', output: 'build/debug', sdk: '8.0'

                def bepinexDir = 'build/BepInEx'
                sh '''
                rm -rf ${bepinexDir}
                mkdir -p ${bepinexDir}/plugins
                cp build/debug/*.dll build/debug/*.pdb BepInEx/plugins/
                zip -r ICSModMenu-Debug.zip BepInEx
                '''
            }
        }

        stage('Build Release') {
            when {
                tag "v*.*.*"
            }
            steps {
                dotnetBuild project: 'ICSModMenu.csproj', configuration: 'Release', output: 'build/release', sdk: '8.0'

                def bepinexDir = 'build/BepInEx'
                sh '''
                rm -rf ${bepinexDir}
                mkdir -p ${bepinexDir}/plugins
                cp build/release/*.dll BepInEx/plugins/
                zip -r ICSModMenu-Release.zip BepInEx
                '''
            }
        }

        stage('Archive Artifacts') {
            when {
                tag "v*.*.*"
            }
            steps {
                archiveArtifacts artifacts: 'ICSModMenu-Debug.zip'
                archiveArtifacts artifacts: 'ICSModMenu-Release.zip'
            }
        }

    }
}
