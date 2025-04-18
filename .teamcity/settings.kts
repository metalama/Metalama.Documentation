// This file is automatically generated by `Build.ps1 generate-scripts`.

// Both Swabra and swabra need to be imported
import jetbrains.buildServer.configs.kotlin.*
import jetbrains.buildServer.configs.kotlin.buildFeatures.sshAgent
import jetbrains.buildServer.configs.kotlin.buildFeatures.Swabra
import jetbrains.buildServer.configs.kotlin.buildFeatures.swabra
import jetbrains.buildServer.configs.kotlin.buildSteps.powerShell
import jetbrains.buildServer.configs.kotlin.failureConditions.*
import jetbrains.buildServer.configs.kotlin.triggers.*

version = "2024.03"

project {

    buildType(DebugBuild)
    buildType(ReleaseBuild)
    buildType(PublicBuild)
    buildType(PublicDeployment)
    buildType(PublicDeploymentNoDependency)
    buildType(PublicUpdateSearch)
    buildType(PublicUpdateSearchNoDependency)

    buildTypesOrder = arrayListOf(DebugBuild,ReleaseBuild,PublicBuild,PublicDeployment,PublicDeploymentNoDependency,PublicUpdateSearch,PublicUpdateSearchNoDependency)

}

object DebugBuild : BuildType({

    name = "Build [Debug]"

    artifactRules = "+:artifacts/publish/public/**/*=>artifacts/publish/public\n+:artifacts/publish/private/**/*=>artifacts/publish/private\n+:artifacts/testResults/**/*=>artifacts/testResults\n+:artifacts/logs/**/*=>logs\n"

    params {
        text("BuildArguments", "", label = "Build Arguments", description = "Arguments to append to the 'Build' build step.", allowEmpty = true)
        text("DefaultBranch", "develop/2025.1", label = "Default Branch", description = "The default branch of this build configuration.")
        text("TimeOut", "300", label = "Time-Out Threshold", description = "Seconds after the duration of the last successful build.", regex = """\d+""", validationMessage = "The timeout has to be an integer number.")
    }

    vcs {
        root(AbsoluteId("Metalama_Metalama20251_MetalamaDocumentation"))
        root(AbsoluteId("Metalama_Metalama20251_MetalamaSamples"), "+:. => source-dependencies/Metalama.Samples")
        root(AbsoluteId("Metalama_Metalama20251_MetalamaCommunity"), "+:. => source-dependencies/Metalama.Community")
    }

    steps {
        powerShell {
            name = "Kill background processes before cleanup"
            id = "PreKill"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            scriptArgs = "tools kill"
        }
        powerShell {
            name = "Build"
            id = "Build"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            scriptArgs = "test --configuration Debug --buildNumber %build.number% --buildType %system.teamcity.buildType.id% %BuildArguments%"
        }
        powerShell {
            name = "Kill background processes before next build"
            id = "PostKill"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            scriptArgs = "tools kill"
        }
    }

    failureConditions {
        failOnMetricChange {
            metric = BuildFailureOnMetric.MetricType.BUILD_DURATION
            units = BuildFailureOnMetric.MetricUnit.DEFAULT_UNIT
            comparison = BuildFailureOnMetric.MetricComparison.MORE
            compareTo = build {
                buildRule = lastSuccessful()
            }
            stopBuildOnFailure = true
            param("metricThreshold", "%TimeOut%")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04cloud")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
    }

    dependencies {
        dependency(AbsoluteId("Metalama_Metalama20251_Metalama_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaCommunity_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaCompiler_ReleaseBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/packages/Release/Shipping/**/*=>dependencies/Metalama.Compiler"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaPremium_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Premium"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaSamples_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Samples"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaSamples_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }
     }

})

object ReleaseBuild : BuildType({

    name = "Build [Release]"

    artifactRules = "+:artifacts/publish/public/**/*=>artifacts/publish/public\n+:artifacts/publish/private/**/*=>artifacts/publish/private\n+:artifacts/testResults/**/*=>artifacts/testResults\n+:artifacts/logs/**/*=>logs\n"

    params {
        text("BuildArguments", "", label = "Build Arguments", description = "Arguments to append to the 'Build' build step.", allowEmpty = true)
        text("DefaultBranch", "develop/2025.1", label = "Default Branch", description = "The default branch of this build configuration.")
        text("TimeOut", "300", label = "Time-Out Threshold", description = "Seconds after the duration of the last successful build.", regex = """\d+""", validationMessage = "The timeout has to be an integer number.")
    }

    vcs {
        root(AbsoluteId("Metalama_Metalama20251_MetalamaDocumentation"))
        root(AbsoluteId("Metalama_Metalama20251_MetalamaSamples"), "+:. => source-dependencies/Metalama.Samples")
        root(AbsoluteId("Metalama_Metalama20251_MetalamaCommunity"), "+:. => source-dependencies/Metalama.Community")
    }

    steps {
        powerShell {
            name = "Kill background processes before cleanup"
            id = "PreKill"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            scriptArgs = "tools kill"
        }
        powerShell {
            name = "Build"
            id = "Build"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            scriptArgs = "test --configuration Release --buildNumber %build.number% --buildType %system.teamcity.buildType.id% %BuildArguments%"
        }
        powerShell {
            name = "Kill background processes before next build"
            id = "PostKill"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            scriptArgs = "tools kill"
        }
    }

    failureConditions {
        failOnMetricChange {
            metric = BuildFailureOnMetric.MetricType.BUILD_DURATION
            units = BuildFailureOnMetric.MetricUnit.DEFAULT_UNIT
            comparison = BuildFailureOnMetric.MetricComparison.MORE
            compareTo = build {
                buildRule = lastSuccessful()
            }
            stopBuildOnFailure = true
            param("metricThreshold", "%TimeOut%")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04cloud")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
    }

    dependencies {
        dependency(AbsoluteId("Metalama_Metalama20251_Metalama_ReleaseBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaCommunity_ReleaseBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaCompiler_ReleaseBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/packages/Release/Shipping/**/*=>dependencies/Metalama.Compiler"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaPremium_ReleaseBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Premium"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaSamples_ReleaseBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Samples"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaSamples_ReleaseBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }
     }

})

object PublicBuild : BuildType({

    name = "Build [Public]"

    artifactRules = "+:artifacts/publish/public/**/*=>artifacts/publish/public\n+:artifacts/publish/private/**/*=>artifacts/publish/private\n+:artifacts/testResults/**/*=>artifacts/testResults\n+:artifacts/logs/**/*=>logs\n"

    params {
        text("BuildArguments", "", label = "Build Arguments", description = "Arguments to append to the 'Build' build step.", allowEmpty = true)
        text("DefaultBranch", "develop/2025.1", label = "Default Branch", description = "The default branch of this build configuration.")
        text("TimeOut", "300", label = "Time-Out Threshold", description = "Seconds after the duration of the last successful build.", regex = """\d+""", validationMessage = "The timeout has to be an integer number.")
    }

    vcs {
        root(AbsoluteId("Metalama_Metalama20251_MetalamaDocumentation"))
        root(AbsoluteId("Metalama_Metalama20251_MetalamaSamples"), "+:. => source-dependencies/Metalama.Samples")
        root(AbsoluteId("Metalama_Metalama20251_MetalamaCommunity"), "+:. => source-dependencies/Metalama.Community")
    }

    steps {
        powerShell {
            name = "Kill background processes before cleanup"
            id = "PreKill"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            scriptArgs = "tools kill"
        }
        powerShell {
            name = "Build"
            id = "Build"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            scriptArgs = "test --configuration Public --buildNumber %build.number% --buildType %system.teamcity.buildType.id% %BuildArguments%"
        }
        powerShell {
            name = "Kill background processes before next build"
            id = "PostKill"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            scriptArgs = "tools kill"
        }
    }

    failureConditions {
        failOnMetricChange {
            metric = BuildFailureOnMetric.MetricType.BUILD_DURATION
            units = BuildFailureOnMetric.MetricUnit.DEFAULT_UNIT
            comparison = BuildFailureOnMetric.MetricComparison.MORE
            compareTo = build {
                buildRule = lastSuccessful()
            }
            stopBuildOnFailure = true
            param("metricThreshold", "%TimeOut%")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04cloud")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
    }

    dependencies {
        dependency(AbsoluteId("Metalama_Metalama20251_Metalama_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaCommunity_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaCompiler_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/packages/Release/Shipping/**/*=>dependencies/Metalama.Compiler"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaPremium_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Premium"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaSamples_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Samples"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaSamples_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }
     }

})

object PublicDeployment : BuildType({

    name = "Deploy [Public]"

    type = Type.DEPLOYMENT

    params {
        text("PublishArguments", "", label = "Publish Arguments", description = "Arguments to append to the 'Publish' build step.", allowEmpty = true)
        text("DefaultBranch", "release/2025.1", label = "Default Branch", description = "The default branch of this build configuration.")
        text("TimeOut", "300", label = "Time-Out Threshold", description = "Seconds after the duration of the last successful build.", regex = """\d+""", validationMessage = "The timeout has to be an integer number.")
    }

    vcs {
        root(AbsoluteId("Metalama_Metalama20251_MetalamaDocumentation"))
    }

    steps {
        powerShell {
            name = "Publish"
            id = "Publish"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            scriptArgs = "publish --configuration Public %PublishArguments%"
        }
    }

    failureConditions {
        failOnMetricChange {
            metric = BuildFailureOnMetric.MetricType.BUILD_DURATION
            units = BuildFailureOnMetric.MetricUnit.DEFAULT_UNIT
            comparison = BuildFailureOnMetric.MetricComparison.MORE
            compareTo = build {
                buildRule = lastSuccessful()
            }
            stopBuildOnFailure = true
            param("metricThreshold", "%TimeOut%")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04cloud")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
        sshAgent {
            // By convention, the SSH key name is always PostSharp.Engineering for all repositories using SSH to connect.
            teamcitySshKey = "PostSharp.Engineering"
        }
    }

    dependencies {
        dependency(AbsoluteId("Metalama_Metalama20251_Metalama_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_Metalama_PublicDeployment")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaCommunity_PublicDeployment")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaCompiler_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/packages/Release/Shipping/**/*=>dependencies/Metalama.Compiler"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaPremium_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Premium"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaPremium_PublicDeployment")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaSamples_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Samples"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaSamples_PublicDeployment")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }
        dependency(PublicBuild) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/public/**/*=>artifacts/publish/public\n+:artifacts/publish/private/**/*=>artifacts/publish/private"
            }
        }
     }

})

object PublicDeploymentNoDependency : BuildType({

    name = "Standalone Deploy [Public]"

    type = Type.DEPLOYMENT

    params {
        text("PublishArguments", "", label = "Publish Arguments", description = "Arguments to append to the 'Publish' build step.", allowEmpty = true)
        text("DefaultBranch", "develop/2025.1", label = "Default Branch", description = "The default branch of this build configuration.")
        text("TimeOut", "300", label = "Time-Out Threshold", description = "Seconds after the duration of the last successful build.", regex = """\d+""", validationMessage = "The timeout has to be an integer number.")
    }

    vcs {
        root(AbsoluteId("Metalama_Metalama20251_MetalamaDocumentation"))
    }

    steps {
        powerShell {
            name = "Publish"
            id = "Publish"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            scriptArgs = "publish --configuration Public --standalone %PublishArguments%"
        }
    }

    failureConditions {
        failOnMetricChange {
            metric = BuildFailureOnMetric.MetricType.BUILD_DURATION
            units = BuildFailureOnMetric.MetricUnit.DEFAULT_UNIT
            comparison = BuildFailureOnMetric.MetricComparison.MORE
            compareTo = build {
                buildRule = lastSuccessful()
            }
            stopBuildOnFailure = true
            param("metricThreshold", "%TimeOut%")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04cloud")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
        sshAgent {
            // By convention, the SSH key name is always PostSharp.Engineering for all repositories using SSH to connect.
            teamcitySshKey = "PostSharp.Engineering"
        }
    }

    dependencies {
        dependency(AbsoluteId("Metalama_Metalama20251_Metalama_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaCompiler_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/packages/Release/Shipping/**/*=>dependencies/Metalama.Compiler"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaPremium_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Premium"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20251_MetalamaSamples_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Samples"
            }
        }
        dependency(PublicBuild) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/public/**/*=>artifacts/publish/public\n+:artifacts/publish/private/**/*=>artifacts/publish/private"
            }
        }
     }

})

object PublicUpdateSearch : BuildType({

    name = "Update Search [Public]"

    type = Type.DEPLOYMENT

    params {
        text("UpdateSearchArguments", "", label = "Update search Arguments", description = "Arguments to append to the 'Update search' build step.", allowEmpty = true)
        text("DefaultBranch", "release/2025.1", label = "Default Branch", description = "The default branch of this build configuration.")
        text("TimeOut", "300", label = "Time-Out Threshold", description = "Seconds after the duration of the last successful build.", regex = """\d+""", validationMessage = "The timeout has to be an integer number.")
    }

    vcs {
        root(AbsoluteId("Metalama_Metalama20251_MetalamaDocumentation"))
    }

    steps {
        powerShell {
            name = "Update search"
            id = "UpdateSearch"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            scriptArgs = "tools search update https://0fpg9nu41dat6boep.a1.typesense.net metalamadoc https://doc-production.metalama.net/sitemap.xml --ignore-tls %UpdateSearchArguments%"
        }
    }

    failureConditions {
        failOnMetricChange {
            metric = BuildFailureOnMetric.MetricType.BUILD_DURATION
            units = BuildFailureOnMetric.MetricUnit.DEFAULT_UNIT
            comparison = BuildFailureOnMetric.MetricComparison.MORE
            compareTo = build {
                buildRule = lastSuccessful()
            }
            stopBuildOnFailure = true
            param("metricThreshold", "%TimeOut%")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04cloud")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
    }

    dependencies {
        dependency(PublicDeployment) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }
     }

})

object PublicUpdateSearchNoDependency : BuildType({

    name = "Standalone Update Search [Public]"

    type = Type.DEPLOYMENT

    params {
        text("UpdateSearchArguments", "", label = "Update search Arguments", description = "Arguments to append to the 'Update search' build step.", allowEmpty = true)
        text("DefaultBranch", "develop/2025.1", label = "Default Branch", description = "The default branch of this build configuration.")
        text("TimeOut", "300", label = "Time-Out Threshold", description = "Seconds after the duration of the last successful build.", regex = """\d+""", validationMessage = "The timeout has to be an integer number.")
    }

    vcs {
        root(AbsoluteId("Metalama_Metalama20251_MetalamaDocumentation"))
    }

    steps {
        powerShell {
            name = "Update search"
            id = "UpdateSearch"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            scriptArgs = "tools search update https://0fpg9nu41dat6boep.a1.typesense.net metalamadoc https://doc-production.metalama.net/sitemap.xml --ignore-tls %UpdateSearchArguments%"
        }
    }

    failureConditions {
        failOnMetricChange {
            metric = BuildFailureOnMetric.MetricType.BUILD_DURATION
            units = BuildFailureOnMetric.MetricUnit.DEFAULT_UNIT
            comparison = BuildFailureOnMetric.MetricComparison.MORE
            compareTo = build {
                buildRule = lastSuccessful()
            }
            stopBuildOnFailure = true
            param("metricThreshold", "%TimeOut%")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04cloud")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
    }

})

