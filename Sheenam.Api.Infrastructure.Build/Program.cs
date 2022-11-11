// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use comfort and pease
// ---------------------------------------------------

using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

namespace Sheenam.Api.Infrastructure.Build
{
    public class Program
    {
        static void Main(string[] args)
        {
            var githubPipeline = new GithubPipeline
            {
                Name = "Sheenam Build Pipeline",

                OnEvents = new Events
                {
                    PullRequest = new PullRequestEvent
                    {
                        Branches = new string[] { "master" }
                    },

                    Push = new PushEvent
                    {
                        Branches = new string[] { "master" }
                    }
                },

                Jobs = new Jobs
                {
                    Build = new BuildJob
                    {
                        RunsOn = BuildMachines.Windows2022,
                        Steps = new List<GithubTask>
                        {
                            new CheckoutTaskV2
                            {
                                Name = "Checking Out Code"
                            },
                            new SetupDotNetTaskV1
                            {
                                Name = "Setting Up .NET",
                                TargetDotNetVersion = new TargetDotNetVersion
                                {
                                    DotNetVersion = "6.0.400"
                                }
                            },
                            new RestoreTask
                            {
                                Name = "Restoring Nuget Packages"
                            },
                            new DotNetBuildTask
                            {
                                Name = "Building Project"
                            },
                            new TestTask
                            {
                                Name = "Running task"
                            }
                        }
                    }
                }
            };

            var client = new ADotNetClient();
            client.SerializeAndWriteToFile(adoPipeline: githubPipeline, 
                path: "../../../../.github/workflows/dotnet.yaml");
        }
    }
}