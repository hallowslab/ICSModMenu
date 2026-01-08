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
                checkout scm
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
                dotnetPublish project: 'ICSModMenu.csproj', configuration: 'Debug', properties: [CI: 'true', OutputPath: 'build/debug'], sdk: '8.0'

                script {
                    def bepinexDir = './BepInEx'
                    sh """
                    rm -rf ${bepinexDir}
                    mkdir -p ${bepinexDir}/plugins/ICSModMenu
                    cp build/debug/*.dll build/debug/*.pdb ${bepinexDir}/plugins/ICSModMenu
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
                dotnetPublish project: 'ICSModMenu.csproj', configuration: 'Release', properties: [CI: 'true', OutputPath: 'build/release'], sdk: '8.0'

                script {
                    def bepinexDir = './BepInEx'
                    sh """
                    rm -rf ${bepinexDir}
                    mkdir -p ${bepinexDir}/plugins/ICSModMenu
                    cp build/release/*.dll ${bepinexDir}/plugins/ICSModMenu
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
