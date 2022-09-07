﻿using Microsoft.Extensions.DependencyInjection;

namespace IssueTracker.FileSystem;

public static class FileSystemServiceExtensions
{
    public static IServiceCollection AddFileSystemServices(this IServiceCollection services)
    {
        services.AddSingleton<IFileProvider, FileProvider>();
        return services;
    }
}
