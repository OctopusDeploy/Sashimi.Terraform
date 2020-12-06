using System;
using System.Linq.Expressions;
using Nuke.Common;

public static class TeamCityExtensions
{
    public static Expression<Func<bool>> IsRunningOnTeamCity => () =>
        NukeBuild.Host == HostType.TeamCity;
    public static Expression<Func<bool>> IsLocalBuild => () =>
        NukeBuild.Host == HostType.Console;
}
