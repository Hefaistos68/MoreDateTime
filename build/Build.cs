using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.CI.AppVeyor;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.EnvironmentInfo;

class Build : NukeBuild
{
    // Console application entry. Also defines the default target.
    public static int Main () => Execute<Build>(x => x.Test);

    // Auto-injection fields:
    // [GitVersion] readonly GitVersion GitVersion;
    //  - Semantic versioning. Must have 'GitVersion.CommandLine' referenced.
    // [GitRepository] readonly GitRepository GitRepository;
    //  - Parses origin, branch name and head from git config.
    // [Parameter] readonly string MyGetApiKey;
    //  - Returns command-line arguments and environment variables.

    public bool IsTagged => AppVeyor.Instance?.RepositoryTag ?? false;

    int Revision => AppVeyor.Instance?.BuildNumber ?? 1;

    public string RevisionString => IsTagged ? null : $"rev{Revision:D4}";

    public AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    public AbsolutePath OutputDirectory => RootDirectory / ".output";

    Target Clean => _ => _
            .Executes(() =>
            {
                CleanDirectory(OutputDirectory);
                CleanDirectory(ArtifactsDirectory);
            });

    Target Restore => _ => _
            .DependsOn(Clean)
            .Executes(() =>
            {
                DotNetRestore();
            });

    Target Compile => _ => _
            .DependsOn(Restore)
            .Executes(() =>
            {
                DotNetBuild(s => s
                    .SetVersionSuffix(RevisionString));
            });

    Target Pack => _ => _
        .DependsOn(Compile, Test)
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetOutputDirectory(ArtifactsDirectory)
                .SetProject(RootDirectory / "src" / "MoreDateTime")
                .SetVersionSuffix(RevisionString));
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(RootDirectory / "tests" / "MoreDateTime.Tests"));
        });

    private static void CleanDirectory(AbsolutePath path)
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }

        Directory.CreateDirectory(path);
    }
}
