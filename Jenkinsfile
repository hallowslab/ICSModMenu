pipeline {
    agent any

    tools {
        dotnetsdk '8.0'
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

        stage('Prepare GamePath.props') {
            steps {
                sh 'cp "/opt/game-deps/Internet Cafe Simulator/GamePath.props" ./GamePath.props'
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
                tag "v*"
            }
            steps {
                dotnetBuild project: 'ICSModMenu.csproj', configuration: 'Debug', properties: [CI: 'true', OutputPath: 'build/debug'], sdk: '8.0'

                script {
                    def bepinexDir = './BepInEx'
                    sh """
                    rm -rf ${bepinexDir}
                    mkdir -p ${bepinexDir}/plugins
                    cp build/debug/*.dll build/debug/*.pdb ${bepinexDir}/plugins/
                    zip -r ICSModMenu-Debug.zip BepInEx
                    """
                }
            }
        }

        stage('Build Release') {
            when {
                tag "v*"
            }
            steps {
                dotnetBuild project: 'ICSModMenu.csproj', configuration: 'Release', properties: [CI: 'true', OutputPath: 'build/release'], sdk: '8.0'

                script {
                    def bepinexDir = './BepInEx'
                    sh """
                    rm -rf ${bepinexDir}
                    mkdir -p ${bepinexDir}/plugins
                    cp build/release/*.dll ${bepinexDir}/plugins/
                    zip -r ICSModMenu-Release.zip BepInEx
                    """
                }
            }
        }

        stage('Archive Artifacts') {
            when {
                tag "v*"
            }
            steps {
                archiveArtifacts artifacts: 'ICSModMenu-Debug.zip'
                archiveArtifacts artifacts: 'ICSModMenu-Release.zip'
            }
        }

    }
}
